using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    // Variables
    Rigidbody rb;
    //CarController carController;

    public float BoostAmount;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //carController = GetComponent<CarController>();

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Speed " + carController.CurrentSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpeedBoost"))
        {
            rb.velocity = BoostAmount * transform.forward;
        }

    }
}
