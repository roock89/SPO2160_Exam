using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GhostComunicationController : MonoBehaviour
{
    public string filePath;
    private string writePath = @"E:\MyTest.txt";
    public GhostHolder loadedGhost;
    public GhostHolder recordedGhost;
    public bool isLoading;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator uploadGhostData(int scoreID)
    {
        isLoading = true;

       
        isLoading = false;    
        yield return null;
    }
}
