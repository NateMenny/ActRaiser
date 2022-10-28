using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] float projectileForce;
    [SerializeField] float projectileFireRate;

    float timeSinceLastFire;
    float distanceToPlayer;
    
    public Transform projectileSpawnPointRight;
    public Transform projectileSpawnPointLeft;
    public Projectile projectilePrefab;
    public Transform playerTransform;

    


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (projectileForce <= 0)
            projectileForce = 7.0f;
        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;

        if (!projectilePrefab)
        {
            if (verbose)
                Debug.Log("Projectile Prefab has not been set on " + name);
        }
        if (projectileSpawnPointLeft)
        {
            if (verbose)
                Debug.Log("Projectile Spawn Point Left has not been set on " + name);
        }
        if (projectileSpawnPointRight)
        {
            if (verbose)
                Debug.Log("Projectile Spawn Point Right has not been set on " + name);
        }
        if (!playerTransform)
        {
            if (verbose)
                Debug.Log("PlayerTransform has not been set for " + name);
        }
    }

    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = transform.position.x - playerTransform.position.x;
        if (!anim.GetBool("Fire"))
        {
            //Check direction
            if (distanceToPlayer > 0) sr.flipX = true; //If left
            else sr.flipX = false;// If Right
            if (Mathf.Abs(distanceToPlayer) < 4.5f)// Check distance from player
            {
                //HINT: CHECK SOMETHING PRIOR TO THIS TO DETERMINE WHICH DIRECTION TO FIRE - MAYBE YOU CAN ALSO INCLUDE DISTANCE
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                    anim.SetBool("Fire", true);
            }
        }
    }

    public void Fire()
    {
        if (sr.flipX == false)
        {
            timeSinceLastFire = Time.time;
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPointRight.position, projectileSpawnPointRight.rotation);
            temp.speed = projectileForce;
        }
        else
        {
            timeSinceLastFire = Time.time;
            Projectile temp = Instantiate(projectilePrefab, projectileSpawnPointLeft.position, projectileSpawnPointLeft.rotation);
            temp.speed = -projectileForce;
        }
    }

    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }
}
