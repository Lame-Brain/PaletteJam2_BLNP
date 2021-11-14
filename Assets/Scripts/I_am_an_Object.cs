using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_an_Object : MonoBehaviour
{
    private Transform target;
    public Transform angle;
    public GameObject Death_Effect;
    const int Player_Layer = 3, Object_Layer = 6;
    private float Health = 5;
    private Rigidbody2D rb;
    private bool isMoving;

    private void Start()
    {
        gameObject.layer = Object_Layer;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isMoving)
        {
            if (rb.velocity == Vector2.zero)
            {
                isMoving = false;
                ShortenFuse();
                Debug.Log("stopped moving");
            }            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Hands") GameManager.OnTouchObject(this.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Hands") GameManager.OnLeaveObject(this.gameObject);
    }

    public void PunchMe(float f, Quaternion r)
    {
        if (gameObject.CompareTag("Bomb")) 
        {
            angle.rotation = r;
            rb.AddForce(r * Vector2.left * 5000000, ForceMode2D.Impulse);
            isMoving = true;
            Debug.Log("started moving");
        }
        if (gameObject.CompareTag("Block"))
        {
            Health -= f;
            if(Health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ShortenFuse()
    {

    }
}
