using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    public Checkpoint[] CheckPoints;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] checkPoint = GameObject.FindGameObjectsWithTag("checkPoint");
        CheckPoints = new Checkpoint[checkPoint.Length];
        for (int i = 0; i < checkPoint.Length; i++)
        {
            CheckPoints[i] = checkPoint[i].GetComponent<Checkpoint>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Checkpoint.currentCheckpoint == CheckPoints.Length)
            SceneManager.LoadScene("GameOverScene");
    }
   
}
