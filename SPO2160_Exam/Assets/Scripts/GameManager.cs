using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager iAmTheManager;
    public GhostHolder ghostInputData;
    public string GhostDataURL;

    // Start is called before the first frame update
    void Start()
    {
        if(iAmTheManager == null)
            iAmTheManager = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void getGhostData(int scoreID)
    {
        StartCoroutine(downloadGhostData(scoreID));
    }

    private IEnumerator downloadGhostData(int scoreID)
    {
        Debug.Log("Test score script: " + GhostDataURL);

        List<IMultipartFormSection> formData = new List<IMultipartFormSection>();

        UnityWebRequest www = UnityWebRequest.Post(GhostDataURL, formData);
        yield return www.SendWebRequest();
        // Debug.Log(www.downloadHandler.text);
        string[] inputs = new string[2];
        // Split string using space, semi colon, comma, dot
        if (www.downloadHandler.text.Length > 1)
        {
            Debug.Log("More than one character received");
            //Split string into two parts using string.split
            inputs = www.downloadHandler.text.Split(';');

            string[] positions = inputs[0].Split(':');
            foreach(string pos in positions)
            {
                string[] f = pos.Split(',');
                float x = float.Parse(f[0]);
                float y = float.Parse(f[1]);
                float z = float.Parse(f[2]);
                Vector3 _pos = new Vector3(x, y, z);
                ghostInputData.position.Add(_pos);
            }
            string[] rotations = inputs[1].Split(':');
            foreach(string rot in rotations)
            {
                string[] q = rot.Split(',');
                float x = float.Parse(q[0]);
                float y = float.Parse(q[1]);
                float z = float.Parse(q[2]);
                Vector3 _rot = new Vector3(x, y, z);
                ghostInputData.rotation.Add(_rot);
            }
            ghostInputData.isReplaying = true;
            ghostInputData.isRecording = false;
        }
        else
        {
            Debug.LogWarning("No replayable ghost data was found");
        }

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
    }
}
