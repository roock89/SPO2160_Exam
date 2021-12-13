using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    //OnTrigger collects the RigidBody and DieAndRespawn script from the player
    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        DieAndRespawn respawn = other.GetComponent<DieAndRespawn>();

        //If the DieAndRespawn is found, run the HitAndRespawn method of that script and set the players velocity to 0
        if (respawn != null) 
        {
            respawn.HitAndRespawn();
            rb.velocity = Vector3.zero;
        }    
    }
}
