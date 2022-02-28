using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vars
    public static PlayerController instance;
    [Header("Movement Speed")]
    public float speed;

    [Header("Jump Settings")]
    public float jump = 3f;   
    private float groundDistance = 0.2f;
    private float gravityValue = -9.81f;
    public LayerMask groundMask;
    public float airFriction = 0.5f;

    [Header("Components")]
    public Animator anim;
    public GameObject playerObj;
    public Transform groundCheck;

    private Vector3 velocity;
    private bool isGrounded;
    CharacterController cc;
    Vector2 moveInput;
    Vector3 rootmotion;
    //public bool isScoping;
    //public Camera scopeCam;
    //public KeyCode button;

    public void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        HandleJump();
    }

    private void OnAnimatorMove()
    {
        rootmotion += anim.deltaPosition;
    }

    public void Movement()
    {
        moveInput.x = Input.GetAxis("Horizontal") * speed;
        moveInput.y = Input.GetAxis("Vertical") * speed;

        anim.SetFloat("InputX", moveInput.x);
        anim.SetFloat("InputY", moveInput.y);

        cc.Move(rootmotion);
        rootmotion = Vector3.zero;
    }

    public void HandleJump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            anim.SetTrigger("Jump");
            velocity.y = Mathf.Sqrt(jump * -1.0f * gravityValue);
        }

        velocity.y += gravityValue * Time.deltaTime;
        cc.Move(velocity * Time.deltaTime);
    }
}