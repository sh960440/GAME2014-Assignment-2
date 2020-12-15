﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[System.Serializable]
public enum ImpulseSounds
{
    JUMP,
    DIE,
    SHOOT,
    FRUIT
}

public class PlayerController : MonoBehaviour
{
    [Header("Controls")]
    public Joystick joystick;
    public float joystickHorizontalSensitivity;
    public float joystickVerticalSensitivity;
    public float horizontalForce;
    public float verticalForce;

    [Header("Platform Detection")]
    public bool isGrounded = false;
    public bool isJumping;
    public Transform spawnPoint;
    public LayerMask collisionGroundLayer;

    public int lives;
    public Text scoreText;

    public AudioSource audioSource;
    public AudioClip[] soundEffects;

    public Transform parent;

    public BulletManager bulletManager;
    public Transform bulletSpawnPoint;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private RaycastHit2D groundHit;
    private float firingDelay = 0;
    private bool facingRight = true;

    void Start()
    {
        Scoreboard.score = 0;
        Scoreboard.remainingFruits = 16;
        lives = 5;

        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        //sounds = GetComponents<AudioSource>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        _IsGroundBelow();
        _Move();
        
        firingDelay += Time.deltaTime;

        scoreText.text = Scoreboard.score.ToString();
    }

    private void _IsGroundBelow()
    {
        groundHit = Physics2D.CircleCast(transform.position - new Vector3(0.0f, 0.65f, 0.0f), 0.4f, Vector2.down, 0.4f, collisionGroundLayer);

        
        isGrounded = (groundHit) ? true : false;
    }

    void _Move()
    {
        if (isGrounded)
        {
            if (!isJumping)
            {
                if (joystick.Horizontal > joystickHorizontalSensitivity)
                {
                    m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                    facingRight = true;

                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.RUN);
                }
                else if (joystick.Horizontal < -joystickHorizontalSensitivity)
                {
                    m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                    facingRight = false;

                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.RUN);
                }
                else
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.IDLE);
                }
            }

            if ((joystick.Vertical > joystickVerticalSensitivity) && (!isJumping))
            {
                m_rigidBody2D.AddForce(Vector2.up * verticalForce);
                m_animator.SetInteger("AnimState", (int) PlayerAnimationType.JUMP);
                isJumping = true;

                audioSource.clip = soundEffects[(int)ImpulseSounds.JUMP];
                audioSource.Play();
            }
            else
            {
                isJumping = false;
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // respawn
        if (other.gameObject.CompareTag("DeathPlane"))
        {
            LoseLife();
        }

        if (other.gameObject.CompareTag("Fruits"))
        {
            other.gameObject.SetActive(false);
            Scoreboard.score += 100;
            audioSource.clip = soundEffects[(int)ImpulseSounds.FRUIT];
            audioSource.Play();
            var remainingFruits = FindObjectsOfType<FruitBehavior>();
            if (remainingFruits.Length <= 0)
            {
                SceneManager.LoadScene("GameoverScreen");
            } 
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LoseLife();
        }

        if (other.gameObject.CompareTag("Moving Platform"))
        {
            other.gameObject.GetComponent<MovingPlatformBehavior>().isActive = true;
            transform.SetParent(other.gameObject.transform);
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Moving Platform"))
        {
            other.gameObject.GetComponent<MovingPlatformBehavior>().isActive = false;
            transform.SetParent(parent);
        }
    }

    public void LoseLife()
    {
        lives -= 1;

        audioSource.clip = soundEffects[(int)ImpulseSounds.DIE];
        audioSource.Play();
        

        if (lives > 0)
        {
            transform.position = spawnPoint.position;

            //FindObjectOfType<GameController>().ResetAllPlatforms();
        }
        else
        {
            SceneManager.LoadScene("GameoverScreen");
        }
    }

    public void Fire()
    {
        if (firingDelay >= 1.0f)
        {
            if (facingRight)
            {
                bulletManager.GetBullet(bulletSpawnPoint.position, 1);
                firingDelay = 0;
            }
            else
            {
                bulletManager.GetBullet(bulletSpawnPoint.position, -1);
                firingDelay = 0;
            }
            audioSource.clip = soundEffects[(int)ImpulseSounds.SHOOT];
            audioSource.Play();
        }
    }
}
