using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Animator))]
public class EthanCharacter : MonoBehaviour
{
    private Animator animator;
    public Rigidbody rb;
    public float modifier = 1;
    public float modifierDefault = 4;
    public float jumpForce = 1;
    [Range(-2, 2)] public float speed = 0;
    private bool jump = false;
    private bool sprintStart = false;
    private bool sprintStop = false;


    void Awake()
    {
        animator = GetComponent<Animator>();
    
    }

    void Update()
    {
        
        
        float horizontal = Input.GetAxis("Horizontal");
        jump = (Input.GetKeyDown(KeyCode.Space)) ? true : false;
        sprintStart = (Input.GetKeyDown(KeyCode.LeftShift)) ? true : false;
        sprintStop = (Input.GetKeyUp(KeyCode.LeftShift)) ? true : false;




        //horizontal = speed;

        //Set character rotation
        float y = (horizontal < 0) ? -90 : 90;
        Quaternion newRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, y, transform.rotation.eulerAngles.z);
        transform.rotation = newRotation;

        //Set character animation
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        //move character
        transform.Translate(-1*transform.right * horizontal * modifier * Time.deltaTime);

        
    }

    void FixedUpdate()
    {
        if (jump) rb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);

        if (sprintStart) modifier = modifierDefault * 2;
        if (sprintStop) modifier = modifierDefault;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            print("You won!");
        }

        if (collision.gameObject.tag == "Lava")
        {
            print("You failed!");
            GameObject.Destroy(this);
        }
    }
}
