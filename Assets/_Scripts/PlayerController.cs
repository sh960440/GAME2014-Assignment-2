using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

[System.Serializable]
public enum ImpulseSounds
{
    JUMP,
    HIT1,
    HIT2,
    HIT3,
    DIE,
    THROW,
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
    //public Animator livesHUD;

    //public AudioSource[] sounds;

    //public Transform parent;

    public BulletManager bulletManager;
    public Transform bulletSpawnPoint;

    private Rigidbody2D m_rigidBody2D;
    private SpriteRenderer m_spriteRenderer;
    private Animator m_animator;
    private RaycastHit2D groundHit;
    private float firingDelay = 0;

    void Start()
    {
        Scoreboard.score = 0;
        lives = 5;

        m_rigidBody2D = GetComponent<Rigidbody2D>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_animator = GetComponent<Animator>();

        //sounds = GetComponents<AudioSource>();
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
                    // move right
                    m_rigidBody2D.AddForce(Vector2.right * horizontalForce * Time.deltaTime);
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.RUN);
                }
                else if (joystick.Horizontal < -joystickHorizontalSensitivity)
                {
                    // move left
                    m_rigidBody2D.AddForce(Vector2.left * horizontalForce * Time.deltaTime);
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.RUN);
                }
                else
                {
                    m_animator.SetInteger("AnimState", (int)PlayerAnimationType.IDLE);
                }
            }

            if ((joystick.Vertical > joystickVerticalSensitivity) && (!isJumping))
            {
                // jump
                m_rigidBody2D.AddForce(Vector2.up * verticalForce);
                m_animator.SetInteger("AnimState", (int) PlayerAnimationType.JUMP);
                isJumping = true;

                //sounds[(int) ImpulseSounds.JUMP].Play();
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
            // score++
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            LoseLife();
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        
    }

    public void LoseLife()
    {
        lives -= 1;

        //sounds[(int) ImpulseSounds.DIE].Play();
        
        //livesHUD.SetInteger("LivesState", lives);

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
        if (firingDelay >= 0.5f)
        {
            bulletManager.GetBullet(bulletSpawnPoint.position);
            firingDelay = 0; // Reset the timer
            //GetComponent<AudioSource>().clip = soundclips[0];
            //GetComponent<AudioSource>().Play();
        }
    }
}
