using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Grenade"))
        {
            Debug.Log("You hit me!");
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
            Destroy(gameObject, 0.3f);
        }
    }
}
