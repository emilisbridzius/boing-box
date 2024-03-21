using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabControl : MonoBehaviour
{
    public GameObject player;
    public PhysicMaterial prefabPhysics;
    public ParticleSystem explosionEffect;
    public MeshFilter mesh;
    public MeshRenderer mat;
    public Rigidbody rb;
    public Collider col;
    public float returnSpeed;
    public bool canBeDestroyed, explode;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        mesh = transform.GetComponent<MeshFilter>();
        mat = transform.GetComponent<MeshRenderer>();
        rb = transform.GetComponent<Rigidbody>();
        col = transform.GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (explode)
        {
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
        mesh = null;
        mat = null;
        rb = null;
        col = null;
        var expl = Instantiate(explosionEffect, transform.position, transform.rotation);
        expl.Play();
        while (explosionEffect.isPlaying)
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
