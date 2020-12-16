/*******************
File name: LOS.cs
Author: Shun min Hsieh
Student Number: 101212629
Date last Modified: 2020/12/13
Program description: A class deals with the enemy's line of sight.
Revision History:
2020/12/13
 - Added Start function
 - Added FixedUpdate function
 - Added OnTriggerEnter2D function

Class:
    LOS
Functions:
    Start
    FixedUpdate
    OnTriggerEnter2D
*******************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LOS : MonoBehaviour
{
    public Collider2D collidesWith;
    public ContactFilter2D contactFilter;
    public List<Collider2D> colliders;
    public Transform entity;

    //private BoxCollider2D LOSCollider;
    private PolygonCollider2D LOSCollider;


    // Start is called before the first frame update
    void Start()
    {
        LOSCollider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = entity.position;
        transform.localScale = entity.localScale;
        Physics2D.GetContacts(LOSCollider, contactFilter, colliders);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        collidesWith = other;
    }
}
