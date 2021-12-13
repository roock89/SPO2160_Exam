using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieAndRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    public void HitAndRespawn()
    {
        Debug.Log("I respawned");
        transform.position = respawnPoint.transform.position;
    }
}
