using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_the_player : MonoBehaviour
{
    public float speed;
    public SpriteRenderer sprite;
    public GameObject hands;

    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 move;

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

        //anim.SetBool("moving", move != Vector2.zero);
        //anim.SetBool("moving up", move.y > 0);
        if (move.x > 0) sprite.flipX = true;
        if (move.x < 0) sprite.flipX = false;

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + move * speed);
    }

}
