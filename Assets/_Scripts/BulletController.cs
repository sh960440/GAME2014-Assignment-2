/*******************
File name: BulletController.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/10/13
Program description: A class for the bullet's movement and bound checking.
Revision History:
2020/12/13
 - Added Start function
 - Added Update function
 - Added _Move function
 - Added OnTriggerEnter2D function
 - Added _CheckBounds function

Class:
    BulletController
Functions:
    Start
    Update
    _Move
    OnTriggerEnter2D
    _CheckBounds
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletController : MonoBehaviour
{
    public float speed;
    public float horizontalBoundary;
    public BulletManager bulletManager;

    // Start is called before the first frame update
    void Start()
    {
        bulletManager = FindObjectOfType<BulletManager>();
    }

    // Update is called once per frame
    void Update()
    {
        _Move();
        _CheckBounds();
    }

    private void _Move()
    {
        transform.position += new Vector3(speed, 0.0f, 0.0f);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("LOS"))
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }

    private void _CheckBounds()
    {
        if (transform.position.x > horizontalBoundary || transform.position.x < -horizontalBoundary)
        {
            bulletManager.ReturnBullet(gameObject);
        }
    }
}
