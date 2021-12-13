using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public float Timer, LapTime;
    public bool Passed, RoundOver, Goal, GhostLap,PlayerLap;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(RoundOver == true)
        {
            LapTime = Timer;
            
            Passed = false;
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
        if(other.tag == "Player" &&Goal == true)
        {
            RoundOver = true;
                PlayerLap = true;
        }
        if(other.tag == "Player")
        {
           // Ghost.currentCheckPoint++;
           this.gameObject.GetComponent<Checkpoint>().Passed = true;
        }
        if(other.tag == "Ghost" && Goal == true)
        {
            GhostLap = true;
        }
    }
}
