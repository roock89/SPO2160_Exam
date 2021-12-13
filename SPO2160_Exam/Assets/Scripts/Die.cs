using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        Debug.Log("Bongos");
        DieAndRespawn respawn = other.GetComponent<DieAndRespawn>();
        Debug.Log(respawn);
        if (respawn != null) 
        {
            respawn.HitAndRespawn();
            rb.velocity = Vector3.zero;
            Debug.Log(other.name + " ran into " + name);
            Debug.Log("rb");
        }    
    }
}
