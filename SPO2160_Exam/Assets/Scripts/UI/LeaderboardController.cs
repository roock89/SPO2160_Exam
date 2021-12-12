using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardController : MonoBehaviour
{
    public GameObject _highScorePanel;
    public Transform ScrollContent;
    private readonly Vector3 StartPos = new Vector3(0, 641, 0); 
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 10; i++)
        {
            GameObject spawn = Instantiate(_highScorePanel);
            spawn.transform.SetParent(ScrollContent, false);
            spawn.transform.localPosition = StartPos + new Vector3(0, i, 0) * -200;
        }
    }
}
