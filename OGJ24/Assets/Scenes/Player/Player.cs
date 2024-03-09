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
    private PlayerInputs inputs;
    private InputAction moveAction;
    private InputAction dashAction;
    private Vector3 inp;
    private bool dashInput;
    private float cooldownTime = 2f;
    private float distanceDash = 15f;
    private float lastUsedTime;
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
        dashAction.Enable();
        /*lookAction = inputs.Player.Look;
        lookAction.Enable();*/
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        dashAction.Disable();
        //lookAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Look();
        if (dashInput && Time.time > lastUsedTime + cooldownTime)
        {
            body.AddForce(transform.forward*distanceDash, ForceMode.Impulse);
            lastUsedTime = Time.time;
            anim.SetTrigger("Dash");
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
        body.MovePosition(transform.position + (transform.forward * inp.magnitude) * speedMove * Time.deltaTime);
    }
}
