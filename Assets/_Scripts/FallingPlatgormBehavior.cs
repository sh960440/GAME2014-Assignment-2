/*******************
File name: FallingPlatform.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: A class controls the falling platform.
Revision History:
2020/12/13
 - Added Start function
 - Added FixedUpdate function
 - Added OnCollisionEnter2D function
 - Added Fall function

Class:
    FallingPlatgormBehavior
Functions:
    Start
    FixedUpdate
    OnCollisionEnter2D
    Fall
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatgormBehavior : MonoBehaviour
{
    private Vector3 startPoint;
    private float destroyTimer = 0;
    private float resetTime = 8.0f;
    private bool isFalling = false;


    void Start()
    {
        startPoint = transform.position;
    }

    void FixedUpdate()
    {
        if (isFalling)
        {
            Fall();
            destroyTimer += Time.deltaTime;
            if (destroyTimer >= resetTime)
            {
                transform.position = startPoint;
                isFalling = false;
                destroyTimer = 0.0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlayerStepsOn());
        }
    }

    IEnumerator PlayerStepsOn() {
        yield return new WaitForSeconds(2.0f);
        isFalling = true;
    }

    private void Fall()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 4.0f, transform.position.z);
    }
}
