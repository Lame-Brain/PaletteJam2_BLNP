using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_an_Object : MonoBehaviour
{
    private Transform target;
    public Transform angle;
    public GameObject Explosion_PF;
    const int Player_Layer = 3, Object_Layer = 6;
    private float Health = 5;
    private Rigidbody2D rb;
    private bool isMoving;
    private Animator anim;

    private void Start()
    {
        gameObject.layer = Object_Layer;
        anim = gameObject.GetComponent<Animator>(); 
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
            rb.AddForce(r * Vector2.left * 7500000, ForceMode2D.Impulse);
            isMoving = true;            
        }
        if (gameObject.CompareTag("Block"))
        {
            Health -= f;            
            if (Health <= 0)
            {
                anim.SetTrigger("Rubble_Crumble");
                GameManager.PLAYER.GetComponent<AudioSource>().clip = GameManager.PLAYER.FindSound("Rubble_Break2");
                GameManager.PLAYER.GetComponent<AudioSource>().Play();
            }
            else
            {
                anim.SetTrigger("Rubble_Hurt");
                GameManager.PLAYER.GetComponent<AudioSource>().clip = GameManager.PLAYER.FindSound("Rubble_Break");
                GameManager.PLAYER.GetComponent<AudioSource>().Play();
            }
        }
    }
    public void BlastMe()
    {
        anim.SetTrigger("Rubble_Crumble");
        GameManager.PLAYER.GetComponent<AudioSource>().clip = GameManager.PLAYER.FindSound("Rubble_Break2");
        GameManager.PLAYER.GetComponent<AudioSource>().Play();
    }

    public void Land()
    {
        if(gameObject.CompareTag("Block"))GetComponent<BoxCollider2D>().isTrigger = false;
        if(gameObject.CompareTag("Bomb"))GetComponent<CircleCollider2D>().isTrigger = false;
        Collider2D[] col = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0);
        for (int _i = 0; _i < col.Length; _i++)
            if (col[_i].CompareTag("Player"))
            {
                anim.SetTrigger("Rubble_Crumble");
                GameManager.PLAYER.Player_Death();
            }
    }

    private void ShortenFuse()
    {
        if (gameObject.tag == "Bomb") anim.SetTrigger("Short_Fuse");
    }

    public void Boom()
    {
        Instantiate(Explosion_PF, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void DefuseBomb()
    {
        Debug.Log("This bomb is moving? " + isMoving);
        if (!isMoving)
        {
            Destroy(gameObject);
        }
    }
}
