using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject tempPrefab;
    public FirstPersonCam fps;
    public WeaponsManager wepM;
    public float force, size, handgunForce;
    public bool canShoot;

    private void Start()
    {
        canShoot = true;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot && wepM.spawnerEquipped)
        {
            SpawnObject();
        }
        
        if (Input.GetMouseButtonDown(0) && canShoot && wepM.handgunEquipped)
        {
            //FireHandgun();
        }
        
        if (Input.GetMouseButton(0) && canShoot && wepM.gravGunEquipped)
        {
            SuspendObject();
        }
    }

    public void SpawnObject()
    {
        GameObject spawned = Instantiate(tempPrefab, spawnPos.position, spawnPos.rotation);
        Rigidbody spawnRB = spawned.GetComponent<Rigidbody>();
        spawnRB.AddForce(-spawnRB.transform.forward * force, ForceMode.Impulse);
    }

    public void FireHandgun()
    {
        if (fps.viewHit.rigidbody != null)
        {
            Rigidbody rb = fps.viewHit.rigidbody;
            rb.AddForce(transform.forward, ForceMode.Impulse);
        }
    }

    public void SuspendObject()
    {

    }

    public void ScaleObject()
    {

    }
}
