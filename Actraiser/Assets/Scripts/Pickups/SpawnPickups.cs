using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPickups : MonoBehaviour
{
    public Pickup[] pickupPrefabArray;
    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, 2);
        Debug.Log("Random int = " + randomInt);
        Instantiate(pickupPrefabArray[randomInt], transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
