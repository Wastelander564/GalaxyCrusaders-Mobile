using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject laser;
    public float beamSpeed = 10f;
    public float fireRate = 0.2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InvokeRepeating("Fire", 0.000001f, fireRate);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            CancelInvoke("Fire");
        }
    }

    public void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, 1.2f, 0);
        GameObject beam = Instantiate(laser, startPosition, Quaternion.identity);
        beam.GetComponent<Rigidbody2D>().velocity = new Vector2(0, beamSpeed);
    }
}
