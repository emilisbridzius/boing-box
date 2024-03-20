using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject tempPrefab;
    public float force, size;
    public bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            GameObject spawned = Instantiate(tempPrefab, spawnPos.position, spawnPos.rotation);
            Rigidbody spawnRB = spawned.GetComponent<Rigidbody>();
            spawnRB.AddForce(spawnRB.transform.forward * force, ForceMode.Impulse);
        }
    }
}
