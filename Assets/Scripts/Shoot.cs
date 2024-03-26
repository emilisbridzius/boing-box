using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform spawnPos, fireEffectPos;
    public ParticleSystem hitEffect, fireEffect;
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
            RemoveStoppedParticles();
            SpawnObject();
        }
        
        if (Input.GetMouseButtonDown(0) && canShoot && wepM.handgunEquipped)
        {
            RemoveStoppedParticles();
            FireHandgun();
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
            rb.AddForce(transform.forward * handgunForce, ForceMode.Impulse);
            Instantiate(hitEffect, fps.viewHit.point, Quaternion.identity);
            Instantiate(fireEffect, fireEffectPos.transform.position, Quaternion.identity);
        }
    }

    public void SuspendObject()
    {
        if (fps.viewHit.rigidbody != null)
        {
            Rigidbody rb = fps.viewHit.rigidbody;
        }
    }

    public void ScaleObject()
    {

    }

    public void RemoveStoppedParticles()
    {
        ParticleSystem[] effects = FindObjectsOfType<ParticleSystem>();
        foreach (ParticleSystem effect in effects)
        {
            if (effect.isStopped)
            {
                Destroy(effect.gameObject);
            }
        }
    }
}
