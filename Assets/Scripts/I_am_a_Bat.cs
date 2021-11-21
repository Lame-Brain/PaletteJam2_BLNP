using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_a_Bat : MonoBehaviour
{
    public float speed;

    private Vector2 destination;

    private void Awake()
    {
        speed = Random.Range(speed - .05f, speed + .05f);
    }

    private void Start()
    {
        PickDestination();   
    }    

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, speed);
        if (Vector2.Distance(transform.position, destination) < .25f) PickDestination();
    }

    private void PickDestination()
    {
        float _x = GameManager.PUZZLE.half_horz;
        float _y = GameManager.PUZZLE.half_vert;
        destination = new Vector2(Random.Range(-_x, _x), Random.Range(-_y, _y));
    }

    public void Bat_Goes_Boom()
    {
        GetComponent<AudioSource>().Stop();
        GameManager.PLAYER.GetComponent<AudioSource>().clip = GameManager.PLAYER.FindSound("Bat_Death");
        GameManager.PLAYER.GetComponent<AudioSource>().Play();
        GetComponent<Animator>().SetTrigger("Death");
    }
}
