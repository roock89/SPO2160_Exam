using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishRace : MonoBehaviour
{
    private bool end = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "FinishLine")
        {
            Debug.Log("honeio");
            end = true;
            if (end)
                SceneManager.LoadScene("GameOverScene");
        }
    }
}
