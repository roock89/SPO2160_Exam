using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GhostHolder : ScriptableObject
{
    public bool isRecording;
    public bool isReplaying;
    public float recordFrequency;

    public List<float> timeStamp;
    public List<Vector3> position;
    public List<Vector3> rotation;
    // Start is called before the first frame update
    void Start()
    {
        
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
