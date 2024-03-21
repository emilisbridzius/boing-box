using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabControl : MonoBehaviour
{
    public GameObject player;
    public PhysicMaterial prefabPhysics;
    public ParticleSystem explosionEffect;
    public float returnSpeed;
    public bool canBeDestroyed, explode;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explode)
        {
            gameObject.SetActive(false);
            StartCoroutine(Explode());
        }
    }

    public IEnumerator ReturnToPlayer()
    {
        canBeDestroyed = true;
        while (Vector3.Distance(gameObject.transform.position, player.transform.position) > 0.1f)
        {
            float step = returnSpeed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
            yield return null;
        }

    }

    public IEnumerator Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        yield return null;
    }
}
