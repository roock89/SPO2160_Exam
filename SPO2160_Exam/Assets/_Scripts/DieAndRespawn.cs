using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{

    public Transform respawnPoint;

    //HitAndRespawn makes the player's transform the same as the checpoints transform
    public void HitAndRespawn()
    {
        transform.position = respawnPoint.transform.position;
        transform.rotation = respawnPoint.rotation;
    }
}
