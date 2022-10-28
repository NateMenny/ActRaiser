using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(Player))]
public class PlayerFire : MonoBehaviour
{
    public bool verbose;
    public AudioClip fireClip;
    public AudioMixerGroup soundFXGroup;

    SpriteRenderer sr;
    Animator anim;
    PlayerSounds ps;

    public GameObject attackBoxLeft;
    public GameObject attackBoxRight;

    //This is an important distinction from the speed that is on the projectile itself
    //paying attention to how we use this variable is key to solving the problem of the lab
    public float projectileSpeed;

    public Projectile projectilePrefab;

    // Start is called before the first frame update
    void Start()
    {

        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        ps = GetComponent<PlayerSounds>();

        if (projectileSpeed <= 0)
            projectileSpeed = 7.0f;

        if (!attackBoxLeft || !attackBoxRight || !projectilePrefab)
            if (verbose)
                Debug.Log("Inspector values not set");
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<Player>().hasWon)
        {
            if (Input.GetButtonDown("Fire1") && GetComponent<Player>().isGrounded && GetComponent<Player>().isAttacking == false)
            {
                GetComponent<Player>().isAttacking = true;
                anim.SetTrigger("Attack");
                FireProjectile();
            }
        }
    }

    void FireProjectile()
    {
        //Flipped is right
        if (sr.flipX)
        {
            attackBoxRight.SetActive(true);
        }
        else
        {
            attackBoxLeft.SetActive(true);
        }

        if (fireClip)
            GameManager.instance.currentLevel.PlaySoundEffect(fireClip);
        
    }
}
