using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speedMove = 20.0f;
    [SerializeField] private float turnSpeed = 720f;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator horseAnim;
    [SerializeField] private GameObject horseModel;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject horseUI;
    [SerializeField] private GameObject horsePrefab;
    [SerializeField] private GameObject spin;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject cake;
    [SerializeField] private TextMeshProUGUI hpText;
    private PlayerInputs inputs;
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction attackAction;
    private InputAction horseAction;
    private InputAction cookAction;
    private Vector3 inp;
    private bool dashInput;
    private bool attackInput;
    private bool horseInput;
    private bool cookInput;
    private float cooldownTime = 2f;
    private float cooldownAttackTime = 1f;
    private float hitTime;
    private float distanceDash = 15f;
    private float lastUsedTime;
    private float lastUsedAttackTime;
    private bool isRiding = false;
    private bool isInvincible = false;
    private bool isFrozen = false;
    public bool cooked = false;
    public bool canCook = false;
    public bool isAttacking = false;
    public GameObject horseAvailable = null;
    private int hp = 10;
    private GameObject gm;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        hpText.text = "HP: " + hp;
        
        gm = GameObject.Find("GameManager");
    }
    
    void Awake()
    {
        inputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        moveAction = inputs.Player.Move;
        moveAction.Enable();
        dashAction = inputs.Player.Dash;
        attackAction = inputs.Player.Attack;
        horseAction = inputs.Player.Horse;
        cookAction = inputs.Player.Cooking;
        dashAction.Enable();
        attackAction.Enable();
        horseAction.Enable();
        cookAction.Enable();
        /*lookAction = inputs.Player.Look;
        lookAction.Enable();*/
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        dashAction.Disable();
        attackAction.Disable();
        horseAction.Disable();
        cookAction.Disable();
        //lookAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeScale == 0)
        {
            return;
        }
        
        GetInput();
        Look();
        if (dashInput && Time.time > lastUsedTime + cooldownTime && !isRiding)
        {
            body.AddForce(transform.forward*distanceDash, ForceMode.Impulse);
            lastUsedTime = Time.time;
            anim.SetTrigger("Dash");
            StartCoroutine(Invincible());
        }

        horseUI.SetActive(horseAvailable != null);
        
        if (horseInput)
        {
            if (!isRiding && horseAvailable == null)
            {
                return;
            }
            isRiding = !isRiding;
            speedMove = isRiding ? 40f : 20f;
            horseModel.SetActive(isRiding);
            playerModel.SetActive(!isRiding);
            if (!isRiding)
            {
                Instantiate(horsePrefab, transform.position + new Vector3(2, -0.5f ,0), Quaternion.identity);
            }
            else
            {
                Destroy(horseAvailable);
            }
            
        }
        
        if (attackInput && Time.time > lastUsedAttackTime + cooldownAttackTime)
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
            StartCoroutine(Attack());
            if (isRiding)
            {
                StartCoroutine(Spin());
            }
            lastUsedAttackTime = Time.time;
        }
        
        if (cookInput && canCook && !cooked && gm.GetComponent<GameManager>().GetFruitCount() >= 4)
        {
            StartCoroutine(Cook());
            cooked = true;
        }
        
        /*Vector2 moveDir = moveAction.ReadValue<Vector2>();
        float horizontalInput = moveDir.x;

        float verticalInput = moveDir.y;
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * speedMove * Time.deltaTime);*/
    }

    IEnumerator Cook()
    {
       cake.SetActive(true);
           float time = 0;
                   while (time < 1)
                   {
                       time += Time.deltaTime;
                       cake.transform.position += new Vector3(0, 3f * Time.deltaTime, 0);
                       // grow fruit
                       cake.transform.localScale += new Vector3(0.4f * Time.deltaTime, 0.4f * Time.deltaTime, 0.4f * Time.deltaTime);
                       yield return null;
                   }
                   gm.GetComponent<GameManager>().StartEnding();
    }
    
    IEnumerator Invincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(1);
        isInvincible = false;
    }
    
    IEnumerator Spin()
    {
        spin.SetActive(true);
        sword.SetActive(false);
        float time = 0;
        while (time < 0.25f)
        {
            time += Time.deltaTime;
            spin.transform.Rotate(0, -1440 * Time.deltaTime, 0);
            yield return null;
        }
        spin.SetActive(false);
        sword.SetActive(true);
    }
    
    IEnumerator Attack()
    {
        isAttacking = true;
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GetInput()
    {
        Vector2 moveDir = moveAction.ReadValue<Vector2>();
        float horizontalInput = moveDir.x;

        float verticalInput = moveDir.y;
        inp = new Vector3(horizontalInput, 0, verticalInput);

        dashInput = dashAction.WasPressedThisFrame();
        attackInput = attackAction.WasPressedThisFrame();
        horseInput = horseAction.WasPressedThisFrame();
        cookInput = cookAction.WasPressedThisFrame();

    }

    private void Look()
    {
        if (inp != Vector3.zero && !isFrozen)
        {
            var relative = (transform.position + inp.ToIso()) - transform.position;
            var rot = Quaternion.LookRotation(relative, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnSpeed * Time.deltaTime);
        }
    }

    private void Move()
    {
        anim.SetFloat("Speed", inp.magnitude);
        horseAnim.SetFloat("Speed", inp.magnitude);
        body.MovePosition(transform.position + (transform.forward * inp.magnitude) * speedMove * Time.deltaTime);
    }
    
    public void TakeDamage(int value)
    {
        if (Time.time < hitTime + 1 || isInvincible)
        {
            return;
        }
        hp -= value;
        hitTime = Time.time;
        hpText.text = "HP: " + hp;
        if (hp <= 0)
        {
            // TODO: restart level
            // reload scene
            UnityEngine.SceneManagement.SceneManager.LoadScene("terrain");
        }
    }
    
    public void OpenChest()
    {
        anim.SetTrigger("Chest");
        StartCoroutine(Freeze());
    }   
    
    IEnumerator Freeze()
    {
        speedMove = 0;
        isFrozen = true; 
        yield return new WaitForSeconds(2);
        isFrozen = false;
        speedMove = 10;
    }
}
