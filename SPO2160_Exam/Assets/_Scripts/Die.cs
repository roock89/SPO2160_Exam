using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Die : MonoBehaviour
{
    public int timesFallen = 0;

    //OnTrigger collects the RigidBody and DieAndRespawn script from the player
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            DieAndRespawn respawn = other.GetComponent<DieAndRespawn>();
            timesFallen++;

            //If the DieAndRespawn is found, run the HitAndRespawn method of that script and set the players velocity to 0
            if (respawn != null)
            {
                respawn.HitAndRespawn();
                rb.velocity = Vector3.zero;
            }
            if (timesFallen >= 3)
            {
                // Remove ghost data



                // Reset score.laptime
                Score.LapTime = 0;

                // Send player over to Game Over scene
                SceneManager.LoadScene("GameOverScene");
            }
        }
    }
}
