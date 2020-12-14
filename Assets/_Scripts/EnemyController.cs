using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement")]
    public float runForce;
    public Rigidbody2D rigidbody2D;
    public Transform lookInFrontPoint;
    public Transform lookAheadPoint;
    public LayerMask collisionGroundLayer;
    public LayerMask collisionWallLayer;

    public bool isGroundAhead;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _LookInFront();
        _LookAhead();
        _Move();
    }

    private void _LookInFront()
    {
        var wallHit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, collisionWallLayer);
        if (wallHit)
        {
            if (!wallHit.collider.CompareTag("Player"))
            {
                if (transform.rotation.z == 0.0f)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
                }
            }
        }

        Debug.DrawLine(transform.position, lookInFrontPoint.position, Color.red);
    }

    private void _LookAhead()
    {
        var groundHit = Physics2D.Linecast(transform.position, lookAheadPoint.position, collisionGroundLayer);
        if (groundHit)
        {
            isGroundAhead = true;
        }
        else
        {
            isGroundAhead = false;
        }

        Debug.DrawLine(transform.position, lookAheadPoint.position, Color.green);
    }

    private void _Move()
    {
        if (isGroundAhead)
        {
            rigidbody2D.AddForce(Vector2.left * runForce * Time.deltaTime * transform.localScale.x);  

            rigidbody2D.velocity *= 0.90f;
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
        }
       
    }
}
