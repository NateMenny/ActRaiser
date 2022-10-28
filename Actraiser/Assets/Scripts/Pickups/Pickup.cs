using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    enum CollectibleType
    {
        INVINCIBILITY = 0,
        SCORE = 1,
        LIFE = 2,
        HEALTH = 3,
        OBJECTIVE = 4,
    }

    [SerializeField] CollectibleType currentCollectible;
    public int collectibleValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player curPlayer = collision.gameObject.GetComponent<Player>();

            switch (currentCollectible)
            {
                case CollectibleType.INVINCIBILITY:
                    curPlayer.BecomeInvincible(collectibleValue);
                    break;
                case CollectibleType.SCORE:
                    curPlayer.score += collectibleValue;
                    break;
                case CollectibleType.LIFE:
                    Debug.Log("Life Collected");
                    curPlayer.lives+= collectibleValue;
                    break;
                case CollectibleType.HEALTH:
                    curPlayer.health += 1;
                    break;
                case CollectibleType.OBJECTIVE:
                    curPlayer.ObjectiveCollected();
                    break;
            }

            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player curPlayer = collision.gameObject.GetComponent<Player>();

            switch (currentCollectible)
            {
                case CollectibleType.INVINCIBILITY:
                    curPlayer.BecomeInvincible(collectibleValue);
                    break;
                case CollectibleType.SCORE:
                    curPlayer.score += collectibleValue;
                    break;
                case CollectibleType.LIFE:
                    Debug.Log("Life Collected");
                    curPlayer.lives+= collectibleValue;
                    break;
                case CollectibleType.HEALTH:
                    curPlayer.health += collectibleValue;
                    break;
                case CollectibleType.OBJECTIVE:
                    curPlayer.ObjectiveCollected();
                    break;
            }

            Destroy(gameObject);
        }
    }
}
