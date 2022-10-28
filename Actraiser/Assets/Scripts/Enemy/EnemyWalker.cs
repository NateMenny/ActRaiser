using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] bool spriteFacesRight;
  
    float timeSinceLastFire = 0;
    float attackSpeed = 0.5f;

    public override void Death()
    {
        speed = 0;
        base.Death();
        anim.SetBool("Death", true);
    }
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 5.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death"))
        {
            if (spriteFacesRight)
            {
                if (!anim.GetBool("Death") && !anim.GetBool("Squish"))
                {
                    if (sr.flipX)
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }
            }
            else
            {

                if (!anim.GetBool("Death") && !anim.GetBool("Squish"))
                {
                    if (!sr.flipX)
                    {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }
            }
        }
    }

    public void IsSquished()
    {
        anim.SetBool("Squish", true);
        rb.velocity = Vector2.zero;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            sr.flipX = !sr.flipX;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !anim.GetBool("Death"))
        {
            if (Time.time >= timeSinceLastFire + attackSpeed)
            {
                timeSinceLastFire = Time.time;
                collision.gameObject.GetComponent<Player>().TakeDamage(1);
            }
        }
    }
}
