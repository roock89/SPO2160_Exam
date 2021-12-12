using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager iAmTheManager;
    public string ghostInputData;
    // Start is called before the first frame update
    void Start()
    {
        if(iAmTheManager == null)
            iAmTheManager = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
