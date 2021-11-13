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
    private bool holding_object, took_action;


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
        //anim = sprite.gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        move.x = Input.GetAxis("Horizontal");
        move.y = Input.GetAxis("Vertical");

        if (Input.GetButtonUp("Jump") && !holding_object && Target_Object != null && !took_action)
        {
            Target_Object.GetComponent<I_am_a_Block>().Pick_Me_Up(hand);
            holding_object = true;
            Debug.Log("Picking up " + Target_Object.name);
            took_action = true;
        }

        if (Input.GetButtonUp("Jump") && holding_object && !took_action)
        {
            Target_Object.GetComponent<I_am_a_Block>().Drop_Me();
            Target_Object = null;
            took_action = true;
        }
    }

    private void FixedUpdate()
    {
        took_action = false;

        rb.MovePosition(rb.position + move * speed);

        if (move.x > 0 && move.y == 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (move.x > 0 && move.y > 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 225);
        }
        if (move.x > 0 && move.y < 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 135);
        }
        if (move.x < 0 && move.y == 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (move.x < 0 && move.y > 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 315);
        }
        if (move.x < 0 && move.y < 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 45);
        }
        if (move.x == 0 && move.y > 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (move.x == 0 && move.y < 0)
        {
            shoulder.rotation = Quaternion.Euler(0, 0, 90);
        }
    }


    private void Add_Interaction_Object(GameObject t)
    {
        if (!holding_object)
        {
            Target_Object = t;
            Debug.Log("Found a " + Target_Object.name);
        }
    }

    private void Drop_Interaction_Object(GameObject t)
    {
        if (!holding_object) Target_Object = null;
    }
}
