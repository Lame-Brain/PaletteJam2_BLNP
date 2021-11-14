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
            }            
        }
    }

    public void PunchMe(float f, Quaternion r)
    {
        if (gameObject.CompareTag("Bomb")) 
        {
            angle.rotation = r;
            rb.AddForce(r * Vector2.left * 5000000, ForceMode2D.Impulse);
            isMoving = true;
        }
        if (gameObject.CompareTag("Block"))
        {
            Health -= f;
            if(Health <= 0)
            {
                //GameManager.OnBlockDeath(transform.position);
                Debug.Log("Block dead");
                Destroy(gameObject);
            }
            else
            {
                //Call Block hit animation trigger
            }
        }
    }

    private void ShortenFuse()
    {

    }
}
