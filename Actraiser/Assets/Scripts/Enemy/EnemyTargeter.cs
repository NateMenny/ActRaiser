using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyTargeter : Enemy
{
    Rigidbody2D rb;

    [SerializeField] float speed;
    [SerializeField] bool spriteFacesRight;

    float timeSinceLastFire = 0;
    float attackSpeed = 0.3f;
    float ogSpeed;

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

        ogSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Death"))
        {
            
            Transform playerTran = GameManager.instance.playerInstance.transform;
            Vector2 direction = playerTran.position - transform.position;
            if (Vector2.Distance(playerTran.position, transform.position) < GetComponent<CircleCollider2D>().radius)
            {
                speed = 0;
            }
            else
            {
                speed = ogSpeed;
            }
            rb.velocity = direction.normalized * speed;

            if (rb.velocity.x != 0)
            {
                if (rb.velocity.x / -1 == Mathf.Abs(rb.velocity.x))
                {
                    // Moving Left
                    if (spriteFacesRight)
                        sr.flipX = true;
                    else
                        sr.flipX = false;
                }
                else
                {
                    // Moving Right
                    if (spriteFacesRight)
                      sr.flipX = false;
                    else
                        sr.flipX = true;
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

    public void SpawnMe()
    {
        anim.Play("Spawn");
    }

    private void OnTriggerStay2D(Collider2D collision)
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
