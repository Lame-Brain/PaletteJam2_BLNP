using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class I_am_the_player : MonoBehaviour
{
    public float speed, punch_force;
    public Transform shoulder, hand;
    public bool airControl;
    public float Time_before_falling_to_death, Time_before_melting_in_lava;
    public List<AudioClip> SoundFX = new List<AudioClip>();
    //public AudioClip jumpSound;
    //public AudioClip deathSound;
    //public AudioClip pitSound;
    //public AudioClip lavaSound;
    //public AudioClip kickSound;
    //public AudioClip powerupSound;
    //public AudioClip spawnSound;
    //public AudioClip BombSpawn_SFX;
    //public AudioClip RubbleSpawn_SFX;
    public AudioMixer audiomixer;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private AudioSource sfxaudio;
    private Vector2 move;
    private GameObject Target_Object;
    private bool isJumping, isKicking;    
    public float action_button_held_down_timer;

    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        sfxaudio = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        GameManager.PLAYER = this;
        GameManager.SetAudioMixer(audiomixer); //Passes the Audiomixer to the Gamemanager (debug)
    }

    private void Update()
    {
        if (!isKicking) move.x = Input.GetAxis("Horizontal");
        if (!isKicking) move.y = Input.GetAxis("Vertical");
        WalkStuff();

        if (Input.GetButtonUp("Fire1") && !isJumping && !isKicking)
        {
            SetKick(true);
            sfxaudio.clip = FindSound("Player_Kick"); ////<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            sfxaudio.Play();

            Get_Target_Object();
            if (Target_Object != null)
            {                
                    Target_Object.GetComponent<I_am_an_Object>().PunchMe(punch_force, shoulder.rotation);                
            }
        }

        if (Input.GetButtonDown("Jump") && !isJumping && !isKicking)
        {
            SetJump(true);
            sfxaudio.clip = FindSound("Player_Jump"); ////<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            sfxaudio.Play();
        }

    }

    public void SetJump(bool b)
    {
        anim.SetBool("Jumping", b);
        isJumping = b;
    }
    public void SetKick(bool b)
    {
        anim.SetBool("Kicking", b);
        isKicking = b;
    }

    private void FixedUpdate()
    {
        if(!isKicking) rb.MovePosition(rb.position + move * speed);
        if(!isKicking && airControl) rb.MovePosition(rb.position + move * (speed / 2));
    }

    private void WalkStuff()
    {
        if (move.x == 0 && move.y > 0) //moving up
        {
            anim.SetInteger("Move Y", 1);
            anim.SetInteger("Move X", 0);
            shoulder.rotation = Quaternion.Euler(0, 0, 270);
        }
        if (move.x == 0 && move.y < 0) // moving down
        {
            anim.SetInteger("Move Y", -1);
            anim.SetInteger("Move X", 0);
            shoulder.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (move.x > 0 && move.y == 0) //moving right
        {
            anim.SetInteger("Move Y", 0);
            anim.SetInteger("Move X", 1);
            sprite.flipX = false;
            shoulder.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (move.x < 0 && move.y == 0) //Moving left
        {
            anim.SetInteger("Move Y", 0);
            anim.SetInteger("Move X", -1);
            sprite.flipX = true;
            shoulder.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (move.x > 0 && move.y < 0) //moving down and right
        {
            anim.SetInteger("Move Y", -1);
            anim.SetInteger("Move X", 1);
            sprite.flipX = false;
            shoulder.rotation = Quaternion.Euler(0, 0, 135);
            //Debug.Log("Moving down and right");
        }
        if (move.x > 0 && move.y > 0) //moving up and right
        {
            anim.SetInteger("Move Y", 1);
            anim.SetInteger("Move X", 1);
            sprite.flipX = false;
            shoulder.rotation = Quaternion.Euler(0, 0, 225);
        }
        if (move.x < 0 && move.y > 0) //moving down and left
        {
            anim.SetInteger("Move Y", 1);
            anim.SetInteger("Move X", -1);
            sprite.flipX = true;
            shoulder.rotation = Quaternion.Euler(0, 0, 315);
        }
        if (move.x < 0 && move.y < 0) // moving up and left
        {
            anim.SetInteger("Move Y", -1);
            anim.SetInteger("Move X", -1);
            sprite.flipX = true;
            shoulder.rotation = Quaternion.Euler(0, 0, 45);
        }
        if (move.x == 0 && move.y == 0) //Not moving
        {
            anim.SetTrigger("Idle");
            anim.SetInteger("Move Y", 0);
            anim.SetInteger("Move X", 0);
            shoulder.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void Get_Target_Object()
    {
        Collider2D col = Physics2D.OverlapCircle(hand.position, 0.0f);
        if (col != null)
            if ((col.CompareTag("Block") || col.CompareTag("Bomb"))) Target_Object = col.gameObject;
        if (col == null)
            Target_Object = null;
    }

    public AudioClip FindSound(string _name)
    {
        AudioClip result = null;
        foreach (AudioClip _ac in SoundFX) if (_ac.name == _name) result = _ac;
        return result;
    }
    
    public void PlayerLifeUp()
    {
        sfxaudio.clip = FindSound("1Up_Collect"); 
        sfxaudio.Play();
    }

    public void Fall2Death()
    {
        anim.SetTrigger("Falls");        
        sfxaudio.clip = FindSound("Player_Pitfall"); 
        sfxaudio.Play();
        Destroy(gameObject);
    }

    public void Melt2Death()
    {
        anim.SetTrigger("Melts");
    }
}
