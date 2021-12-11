using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public Transform tankTower, tankGunMuzzle;
    public GameObject grenadePrefab;

    private float elevateSpeed = 15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Rotate the turret - elevating the barrel
            tankTower.Rotate(tankTower.forward, Time.deltaTime * elevateSpeed);
        } else if (Input.GetKey(KeyCode.S))
        {
            // Rotate the turret - decreasing the elevation
            tankTower.Rotate(tankTower.forward, -Time.deltaTime * elevateSpeed);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Fire the gun!
            GameObject newGrenade = Instantiate(grenadePrefab, tankGunMuzzle.position, tankGunMuzzle.rotation);
        } 
    }
}
