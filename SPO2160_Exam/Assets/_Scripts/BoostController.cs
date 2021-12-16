using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    // Variables
    Rigidbody rb;
    //CarController carController;

    public float BoostAmount;
    public float JumpAmount;
    public float SuperBoostAmount;
    public GameObject wallOfChina, goldenRoad;
    public float wallActiveTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            rb.velocity += (BoostAmount / 5) * transform.forward;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("JumpZone"))
        {
            rb.velocity += (JumpAmount / 5) * transform.up;
            //rb.velocity = 0f * transform.position;
        }

        if (other.CompareTag("WallController"))
        {
            StopCoroutine(WallTimer());
            StartCoroutine(WallTimer());
            Destroy(other.gameObject);
        }

        if (other.CompareTag("GoldenRoadController"))
        {
            goldenRoad.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("SuperSpeedZone"))
        {
            rb.velocity += (SuperBoostAmount / 5) * transform.forward;
            rb.velocity += -10 * transform.up;
        }
        
    }

    IEnumerator WallTimer()
    {
        wallOfChina.SetActive(true);
        yield return new WaitForSeconds(wallActiveTime);
        wallOfChina.SetActive(false);
    }
}
