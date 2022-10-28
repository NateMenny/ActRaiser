using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float minXClamp = -1.43f;
    public float maxXClamp = 151.1f;
    public float minYClamp = -1.43f; //This amount is the amount added to player position
    public float maxYClamp = 0.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        if (GameManager.instance.playerInstance)
        {
            player = GameManager.instance.playerInstance.transform;
        }
        if (player)
        {
            Vector3 cameraTransform;

            cameraTransform = transform.position;

            cameraTransform.x = player.transform.position.x;
            cameraTransform.x = Mathf.Clamp(cameraTransform.x, minXClamp, maxXClamp);
            cameraTransform.y = player.transform.position.y;
            cameraTransform.y = Mathf.Clamp(cameraTransform.y, player.position.y + minYClamp, maxYClamp);

            transform.position = cameraTransform;
        }
    }
}
