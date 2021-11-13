using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_a_Block : MonoBehaviour
{
    private bool picked_up;
    private Transform target;

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
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void Drop_Me()
    {
        picked_up = false;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
