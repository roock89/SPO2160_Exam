using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostRecorder : MonoBehaviour
{

    public GhostHolder ghost;
    private float timer;
    private float timeValue;
    private void Awake()
    {
        if (ghost.isRecording)
        {
            ghost.ClearReplay();
            timeValue = 0;
            timer = 0;
        }
    }
 

    // Update is called once per frame
    void Update()
    {
        timer += Time.unscaledDeltaTime;
        timeValue += Time.unscaledDeltaTime;
        if(ghost.isRecording &timer >= 1 / ghost.recordFrequency)
        {
            ghost.timeStamp.Add(timeValue);
            ghost.position.Add(this.transform.position);
            ghost.rotation.Add(this.transform.eulerAngles);
            timer = 0;
        }
    }
}
