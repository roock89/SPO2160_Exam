using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Die : MonoBehaviour
{
    public int timesFallen = 0, lives = 3;
    public GhostHolder ghost;
    public Rigidbody PlayerRigidbody;
    public DieAndRespawn ButtonRespawn;
    public Text healthText;
    private void Update()
    {
        //Press R to reset to previous checkpoint.
        if (Input.GetKey(KeyCode.R))
        {
                ButtonRespawn.HitAndRespawn();
                PlayerRigidbody.velocity = Vector3.zero;
               // timesFallen++;
        }
        healthText.text = "lives: " + lives;
    }
    //OnTrigger collects the RigidBody and DieAndRespawn script from the player
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            DieAndRespawn respawn = other.GetComponent<DieAndRespawn>();
            timesFallen++;
            lives--;

            //If the DieAndRespawn is found, run the HitAndRespawn method of that script and set the players velocity to 0
            if (respawn != null)
            {
                respawn.HitAndRespawn();
                rb.velocity = Vector3.zero;
            }
            if (timesFallen >= 3)
            {
                // Remove ghost data
                ghost.ClearReplay();


                // Reset score.laptime
                Score.LapTime = 0;

                // Send player over to Game Over scene
                SceneManager.LoadScene(2);
            }
        }
    }
}
