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
    private PlayerInputs inputs;
    private InputAction moveAction;
    private Vector3 inp;
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
        /*lookAction = inputs.Player.Look;
        lookAction.Enable();*/
    }
    
    private void OnDisable()
    {
        moveAction.Disable();
        //lookAction.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Look();
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
        body.MovePosition(transform.position + (transform.forward * inp.magnitude) * speedMove * Time.deltaTime);
    }
}
