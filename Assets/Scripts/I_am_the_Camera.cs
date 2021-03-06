using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_the_Camera : MonoBehaviour
{
    private Transform target;
    private bool FollowTarget = true;
    private float Camera_Move_Speed = .5f;
    private float how_long_should_the_screen_shake = .5f;

    private bool screen_shaking;
    private float screen_shake_amount;
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (FollowTarget && !screen_shaking)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, target.position.y, transform.position.z), Camera_Move_Speed * Time.deltaTime);
        }

        if (screen_shaking)
        {
            transform.position = new Vector3(transform.position.x + Random.Range(-screen_shake_amount / 2, screen_shake_amount / 2), 
                                             transform.position.y + Random.Range(-screen_shake_amount / 2, screen_shake_amount / 2), 
                                             transform.position.z);
        }
    }

    public void ShakeScreen(float f)
    {
        screen_shake_amount = f;
        StartCoroutine(ShakeScreen_CR());
    }

    IEnumerator ShakeScreen_CR()
    {
        screen_shaking = true;
        yield return new WaitForSeconds(how_long_should_the_screen_shake);
        screen_shaking = false;
        screen_shake_amount = 0;
    }
}
