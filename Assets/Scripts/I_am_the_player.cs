using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_the_player : MonoBehaviour
{
    public float speed;
    public Transform shoulder, hand;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 move;
    private GameObject Target_Object;
    private bool holding_object, took_action, isJumping;    
    public float action_button_held_down_timer;
    


    private void OnEnable()
    {
        GameManager.OnTouchObject += Add_Interaction_Object;
        GameManager.OnLeaveObject += Drop_Interaction_Object;
    }

    private void OnDisable()
    {
        GameManager.OnTouchObject -= Add_Interaction_Object;
        GameManager.OnLeaveObject -= Drop_Interaction_Object;
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

        if (Input.GetButtonUp("Fire1") && !holding_object && Target_Object != null && !took_action)
        {
            if (Target_Object.tag == "Bomb")
            {
                Target_Object.GetComponent<I_am_an_Object>().Pick_Me_Up(hand);
                holding_object = true;
            }
            else if (Target_Object.tag == "Block")
            {
                StartThrow_Anim();
                GameManager.PunchObject();
            }
            took_action = true;
        }

        if (Input.GetButtonDown("Fire1") && holding_object)
        {
            action_button_held_down_timer = 0;
            took_action = true;
            StartChargeThrow_Anim();
        }

        if (Input.GetButton("Fire1") && holding_object && !took_action)
        {
            action_button_held_down_timer += Time.deltaTime;
            if (action_button_held_down_timer > 1.5f) action_button_held_down_timer = 1.5f;

        }

        if (Input.GetButtonUp("Fire1") && holding_object && !took_action)
        {
            Target_Object.GetComponent<I_am_an_Object>().Drop_Me();
            Target_Object.GetComponent<I_am_an_Object>().ThrowMe(action_button_held_down_timer * 5000, shoulder.rotation);
            Target_Object = null;
            took_action = true;
            holding_object = false;
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
        took_action = false;

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


    private void Add_Interaction_Object(GameObject t)
    {
        if (!holding_object)
        {
            Target_Object = t;
        }
    }

    private void Drop_Interaction_Object(GameObject t)
    {
        if (!holding_object) Target_Object = null;
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
