using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_the_player : MonoBehaviour
{
    public float speed, punch_force;
    public Transform shoulder, hand;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private Vector2 move;
    private GameObject Target_Object;
    public bool isJumping, isKicking;    
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
        sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        GameManager.PLAYER = this;
    }

    private void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");
        WalkStuff();

        if (Input.GetButtonUp("Fire1"))
        {
            if (anim.GetInteger("Move Y") < 0)
            {
                Debug.Log("should be kicking");
                SetJump(false);
                move.x = 0; move.y = 0;
                //anim.SetTrigger("Kick(Up)");
            }

            Get_Target_Object();
            if (Target_Object != null)
            {
                //Target_Object.GetComponent<I_am_an_Object>().PunchMe(punch_force, shoulder.rotation);
            }
            //Debug.Log("should be kicking");
            //if(anim.GetInteger("Move X") != 0) anim.SetTrigger("Kick(Side)");
            
            //else anim.SetTrigger("Kick(Down)");
        }

        if (Input.GetButtonDown("Jump"))
        {
            SetJump(true);
        }
    }

    public void SetJump(bool b)
    {
        anim.SetBool("Jumping", b);
        isJumping = b;
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed);

    }

    private void WalkStuff()
    {
        if (move.x > 0 && move.y == 0) //moving right
        {
            anim.SetInteger("Move Y", 0);
            anim.SetInteger("Move X", 1);
            sprite.flipX = false;
            shoulder.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (move.x > 0 && move.y > 0) //moving right and up
        {
            anim.SetInteger("Move Y", 1);
            anim.SetInteger("Move X", 1);
            sprite.flipX = false;
            shoulder.rotation = Quaternion.Euler(0, 0, 225);
        }
        if (move.x > 0 && move.y < 0) //moving down and right
        {
            anim.SetInteger("Move Y", -1);
            anim.SetInteger("Move X", 1);
            sprite.flipX = false;
            shoulder.rotation = Quaternion.Euler(0, 0, 135);
        }
        if (move.x < 0 && move.y == 0) //Moving left
        {
            anim.SetInteger("Move Y", 0);
            anim.SetInteger("Move X", -1);
            sprite.flipX = true;
            shoulder.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (move.x < 0 && move.y > 0) //moving down and left
        {
            anim.SetInteger("Move Y", -1);
            anim.SetInteger("Move X", -1);
            sprite.flipX = true;
            shoulder.rotation = Quaternion.Euler(0, 0, 315);
        }
        if (move.x < 0 && move.y < 0) // moving up and left
        {
            anim.SetInteger("Move Y", 1);
            anim.SetInteger("Move X", -1);
            sprite.flipX = true;
            shoulder.rotation = Quaternion.Euler(0, 0, 45);
        }
        if (move.x == 0 && move.y > 0) // moving down
        {
            anim.SetInteger("Move Y", -1);
            anim.SetInteger("Move X", 0);
            shoulder.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (move.x == 0 && move.y < 0) //moving right
        {
            anim.SetInteger("Move Y", 1);
            anim.SetInteger("Move X", 0);
            shoulder.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (move.x == 0 && move.y == 0) //Not moving
        {
            anim.SetTrigger("Idle");
            anim.SetInteger("Move Y", 0);
            anim.SetInteger("Move X", 0);
            shoulder.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void Get_Target_Object()
    {
        Collider2D col = Physics2D.OverlapCircle(hand.position, 0.0f);
        if (col != null) 
            Target_Object = col.gameObject;
        if (col == null)
            Target_Object = null;
    }
    

}
