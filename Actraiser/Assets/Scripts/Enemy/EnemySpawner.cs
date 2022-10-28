using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject spawnObj;
    [SerializeField] Transform spawnTran;
    [SerializeField] bool spawnsOnTrigger;

    private void Start()
    {
        if (!spawnObj) spawnsOnTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (spawnsOnTrigger)
            {
                Instantiate(spawnObj, spawnTran.position, spawnTran.rotation);
            }
        }
    }
}
