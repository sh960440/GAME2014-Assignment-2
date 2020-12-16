/*******************
File name: FruitBehavoir.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: A class controls the behavoir of the pickup item.
Revision History:
2020/12/13
 - Added Start function
 - Added Update function
 - Added _toggle function

Class:
    FruitBehavior
Functions:
    Start
    Update
    _toggle
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{
    public RaycastHit2D hit;
    public bool isGrounded;
    public Vector3 groundLevel;
    public Rigidbody2D rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGrounded)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, 5.0f);
            if (hit)
            {
                groundLevel = hit.point + Vector2.up;
                isGrounded = true;
                rigidbody2D.gravityScale = 0;
            }
        }
        _toggle();
    }

    private void _toggle()
    {
        if (isGrounded)
        {
            transform.position = new Vector3(transform.position.x, groundLevel.y + Mathf.PingPong(Time.time, 1), 0.0f);
        }
    }
}
