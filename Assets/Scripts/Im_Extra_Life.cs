using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Im_Extra_Life : MonoBehaviour
{
    public GameObject extraLifePF;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Instantiate(extraLifePF, transform.position, Quaternion.identity);
            GameManager.Lives++;
            Destroy(gameObject);
        }
    }
}
