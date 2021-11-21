using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_a_Lava_Pool : MonoBehaviour
{
    private float doom_timer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            doom_timer = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            doom_timer += Time.deltaTime;
            if (doom_timer > GameManager.PLAYER.Time_before_falling_to_death) GameManager.PLAYER.Melt2Death();
        }

        if (collision.CompareTag("Bomb"))
        {
            collision.GetComponent<I_am_an_Object>().DefuseBomb();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            doom_timer = 0;
    }
}
