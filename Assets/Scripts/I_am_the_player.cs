using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_the_player : MonoBehaviour
{
    public float speed, punch_force;
    public Transform shoulder, hand;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 move;
    private GameObject Target_Object;
    private bool isJumping;    
    public float action_button_held_down_timer;
    


    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        if (Input.GetButtonUp("Fire1"))
        {
            Get_Target_Object();
            if (Target_Object != null)
            {
                Target_Object.GetComponent<I_am_an_Object>().PunchMe(punch_force, shoulder.rotation);
            }
            StartThrow_Anim();
        }

        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            StartJump_Anim();
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed);

        if (move.x > 0 && move.y == 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (move.x > 0 && move.y > 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 225);
        }
        if (move.x > 0 && move.y < 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 135);
        }
        if (move.x < 0 && move.y == 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (move.x < 0 && move.y > 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 315);
        }
        if (move.x < 0 && move.y < 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 45);
        }
        if (move.x == 0 && move.y > 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (move.x == 0 && move.y < 0)
        {
            StartWalking_Anim();
            shoulder.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (move.x == 0 && move.y == 0) Idle_Anim();
    }


    private void Get_Target_Object()
    {
        Collider2D col = Physics2D.OverlapCircle(hand.position, 0.0f);
        if (col != null) 
            Target_Object = col.gameObject;
    }

    private void Idle_Anim() { anim.SetTrigger("Idle"); }
    private void StartWalking_Anim() { anim.SetTrigger("Start_Walking"); }
    private void StartChargeThrow_Anim() { anim.SetTrigger("Finish_Throw"); }
    private void StartJump_Anim() { anim.SetTrigger("Jump"); }
    private void StartThrow_Anim() 
    {        
        anim.ResetTrigger("Charge_Throw"); 
        anim.SetTrigger("Charge_Throw"); 
    }

}
