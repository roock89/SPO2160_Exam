using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GhostHolder : ScriptableObject
{
    public bool isRecording;
    public bool isReplaying;
    public bool ResetGhost;
    public float recordFrequency = 20;
    public float lapTime;

    public List<float> timeStamp;
    public List<Vector3> position;
    public List<Vector3> rotation;
    // Start is called before the first frame update
    void Start()
    {
        if(ResetGhost == true)
        {
            ClearReplay();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ClearReplay()
    {
        timeStamp.Clear();
        position.Clear();
        rotation.Clear();
    }
}
