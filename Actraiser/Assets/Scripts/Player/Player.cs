using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Animator))]
public class Player : MonoBehaviour
{
    public bool verbose = false;
    public bool isGrounded;
    public bool isFalling;
    public bool hasWon;
    public AudioClip deathClip;
    public AudioClip winClip;
    public bool isAttacking;
    public AudioMixerGroup soundFXGroup;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;
    PlayerSounds ps;

    [SerializeField]
    float speed;
    [SerializeField]
    int jumpForce;
    [SerializeField]
    float groundCheckRadius;
    
    int _maxHealth;

    public bool _invincible = false;
    int _score = 0;
    int _lives = 1;
    int _health = 0;
    int _starPoints = 0;

    public int maxLives = 99;

    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            GameManager.instance.score = _score;
            Debug.Log("Score changed to " + _score);
        }
    }

    public int lives
    {
        get { return _lives; }
        set
        {
            _lives = value;
            if (_lives > maxLives)
                _lives = maxLives;

            if (_lives < 0)
            {
            }
            //gameover stuff can go here

            Debug.Log("Lives changed to " + _lives);
        }
    }

    public int starPoints
    {
        get { return _starPoints; }
        set
        {
            _starPoints = value;
            Debug.Log(_starPoints + " Red coins have been collected");
        }
    }

    public int health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (health > _maxHealth) _health = _maxHealth;
            if (GameManager.instance.currentCanvas.healthSegments.Length > 0)
                GameManager.instance.currentCanvas.UpdateHealthbar(health);
            if (health <= 0)
            {
                if (deathClip)
                    GameManager.instance.currentLevel.PlaySoundEffect(deathClip);
                GameManager.instance.lives--;
            }
        }
    }

    public LayerMask isGroundLayer;
    public Transform groundCheck;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerSounds>();

        if (speed <= 0)
        {
            speed = 5.0f;
            if (verbose)
                Debug.Log("Speed value is garbage - setting default speed to 5");
        }

        if(health <= 0)
        {
            _health = 8;
        }

        if (jumpForce <= 0)
        {
            jumpForce = 300;
            if (verbose)
                Debug.Log("Jump Force value is garbage - setting default jump force to 300");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.05f;
            if (verbose)
                Debug.Log("Ground check radius value is garbage - setting default ground check to 0.05");
        }

        if (!groundCheck)
        {
            if (verbose)
                Debug.Log("Ground check transform is not set, please create empty gameobject and assign to groundcheck");
        }

        _maxHealth = health;
        GameManager.instance.currentCanvas.UpdateHealthbar(GameManager.instance.playerInstance.GetComponent<Player>().health);
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasWon)
        {
            float horizontalInput = 0;
            if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                horizontalInput = Input.GetAxis("Horizontal");
                GetComponent<PlayerFire>().attackBoxLeft.SetActive(false);
                GetComponent<PlayerFire>().attackBoxRight.SetActive(false);
                isAttacking = false;
            }

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);

            if (rb.velocity.y < 0) isFalling = true;
            else isFalling = false;

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(Vector2.up * jumpForce);
            }

            Vector2 moveDirection = new Vector2(horizontalInput * speed, rb.velocity.y);
            rb.velocity = moveDirection;

            anim.SetFloat("speed", Mathf.Abs(horizontalInput));
            anim.SetBool("isGrounded", isGrounded);
            anim.SetBool("isFalling", isFalling);



            if (sr.flipX && horizontalInput < 0 || !sr.flipX && horizontalInput > 0)
                sr.flipX = !sr.flipX;
        }
       
    }

    public void TakeDamage(int damage)
    {
        if (!_invincible)
        {
            health -= damage;
        }
    }

    public void ObjectiveCollected()
    {
        GameManager.instance.currentLevel.PauseLevelMusic();
        anim.Play("Win");
        hasWon = true;
        GameManager.instance.currentLevel.PlaySoundEffect(winClip);
        Time.timeScale = 0;
        StartCoroutine("Finish");
    }

    public IEnumerator Finish() 
    {
        yield return new WaitForSecondsRealtime(winClip.length - 1);
        GameManager.instance.EndGame(true);
    }

    // Duration is the time the player will be invincible(Unable to take damage)
    public void BecomeInvincible(float duration)
    {
        StartCoroutine(InvincibilityBoost(duration));
    }

    public IEnumerator InvincibilityBoost(float duration)
    {
        _invincible = true;
        sr.color = Color.yellow;
        yield return new WaitForSeconds(duration);
        sr.color = Color.white;
        _invincible = false;
    }
}
