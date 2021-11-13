using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_an_Object : MonoBehaviour
{
    private bool picked_up;
    private Transform target;
    public Transform angle;
    const int Player_Layer = 3, Object_Layer = 6;
    private Rigidbody2D rb;

    private void OnEnable()
    {
        GameManager.PunchObject += PunchMe;
    }
    private void OnDisable() 
    {
        GameManager.PunchObject -= PunchMe;
    }

    private void Start()
    {
        gameObject.layer = Object_Layer;
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (picked_up) transform.position = target.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "Hands") GameManager.OnTouchObject(this.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    if (collision.transform.tag == "Hands") GameManager.OnLeaveObject(this.gameObject);
    }

    public void Pick_Me_Up(Transform t)
    {
        picked_up = true;
        target = t;
        gameObject.layer = Player_Layer;        
    }

    public void Drop_Me()
    {
        picked_up = false;
        gameObject.layer = Object_Layer;        
    }

    public void ThrowMe(float f, Quaternion r)
    {
        angle.rotation = r;
        rb.AddForce(r * Vector2.left * f, ForceMode2D.Impulse);
    }

    public void PunchMe()
    {

    }
}
