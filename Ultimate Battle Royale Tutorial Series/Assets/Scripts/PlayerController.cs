using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Vars
    public static PlayerController instance;
    public float speed;
    public Animator anim;
    CharacterController cc;
    Vector2 moveInput;
    Vector3 rootmotion;
    public GameObject playerObj;
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
}