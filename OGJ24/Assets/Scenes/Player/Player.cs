using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speedMove = 10.0f;
    [SerializeField] private float turnSpeed = 360f;
    [SerializeField] private Rigidbody body;
    [SerializeField] private Animator anim;
    [SerializeField] private Animator horseAnim;
    [SerializeField] private GameObject horseModel;
    [SerializeField] private GameObject playerModel;
    [SerializeField] private GameObject horseUI;
    [SerializeField] private GameObject horsePrefab;
    private PlayerInputs inputs;
    private InputAction moveAction;
    private InputAction dashAction;
    private InputAction attackAction;
    private InputAction horseAction;
    private Vector3 inp;
    private bool dashInput;
    private bool attackInput;
    private bool horseInput;
    private float cooldownTime = 2f;
    private float distanceDash = 15f;
    private float lastUsedTime;
    private bool isRiding = false;
    public GameObject horseAvailable = null;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
        dashAction.Enable();
        attackAction.Enable();
        horseAction.Enable();
        /*lookAction = inputs.Player.Look;
        lookAction.Enable();*/
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        dashAction.Disable();
        attackAction.Disable();
        horseAction.Disable();
        //lookAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Look();
        if (dashInput && Time.time > lastUsedTime + cooldownTime && !isRiding)
        {
            body.AddForce(transform.forward*distanceDash, ForceMode.Impulse);
            lastUsedTime = Time.time;
            anim.SetTrigger("Dash");
        }

        horseUI.SetActive(horseAvailable != null);
        
        if (horseInput)
        {
            if (!isRiding && horseAvailable == null)
            {
                return;
            }
            isRiding = !isRiding;
            speedMove = isRiding ? 20f : 10f;
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
        
        if (attackInput)
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
        }
        
        /*Vector2 moveDir = moveAction.ReadValue<Vector2>();
        float horizontalInput = moveDir.x;

        float verticalInput = moveDir.y;
        transform.Translate(new Vector3(horizontalInput, 0, verticalInput) * speedMove * Time.deltaTime);*/
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

    }

    private void Look()
    {
        if (inp != Vector3.zero)
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
}
