using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{
    public static float LapTime;
}
public class Checkpoint : MonoBehaviour
{
    public int checkpointNumb;
    public Checkpoint goalCheckpointMEGA;
    public bool checkpointMEGA = false;
    public bool isGoldenRoad = false;
    public static int currentCheckpoint;
    public int maxlap = 3, currentlap;
    public float Timer;
    public bool Passed, RoundOver, Goal, GhostLap,PlayerLap;
    public RoundController RC;
    // Start is called before the first frame update
    void Start()
    {
        currentCheckpoint = 0;
        if(Goal == true)
        {
            RC = GameObject.Find("CheckpointParent").GetComponent<RoundController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentlap>= maxlap)
        {
            //Insert gameover here, canvas text and buttons.
        }
        //LapTime is the final time of the entire lap. after that it resets the lap and allows the player to do more laps.
        //if you want you can show the laptime on the game over screen.
        if(RoundOver == true)
        {
            Score.LapTime = Timer;
            
            Passed = false;
            currentlap++;
            RoundOver = false;
            Timer = 0;
        }
        if(Passed == false)
        { 
            Timer += Time.deltaTime;
         }
       
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if(PlayerLap == true && GhostLap == false)
        {
            //Insert code for if player is faster than ghost,
        }
        if(GhostLap == true && PlayerLap == false)
        {
            //Insert code for what happens if player is slower than ghost.
        }
        if(GhostLap == true && PlayerLap == true)
        {
            //insert code to show time.
        }

        if (other.tag == "Player")
        {
            // Ghost.currentCheckPoint++;
            if (currentCheckpoint == checkpointNumb)
            {
                Passed = true;
                currentCheckpoint++;
            }

            if (isGoldenRoad == true)
            {
                goalCheckpointMEGA.checkpointMEGA = true;
            }
        }


        if (Goal == true && currentCheckpoint > 10 && other.tag == "Player" || checkpointMEGA == true && other.tag == "Player" && Goal == true)
        {

            RoundOver = true;
            PlayerLap = true;
            checkpointMEGA = false;

        }
       
        if(other.tag == "Ghost" && Goal == true)
        {
            GhostLap = true;
        }

        {

        //Find the DieAndRespawn script on the player            
        DieAndRespawn respawn = other.GetComponent<DieAndRespawn>();
        if (respawn != null)
            {
                //Set the respawn point the same as the transform of the checkpoint
                respawn.respawnPoint = gameObject.transform;
            }    
        }
    }
}
