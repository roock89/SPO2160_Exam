using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public int number;

    private GameManager _gm;
    private void Start()
    {
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        number = int.Parse(gameObject.name);
    }
    public void SendNumber()
    {
        // _gm.SelectNumber(number);
        gameObject.SetActive(false);
    }
}