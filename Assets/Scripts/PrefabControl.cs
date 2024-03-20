using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabControl : MonoBehaviour
{
    public GameObject player;
    public float returnSpeed;
    public bool canBeDestroyed;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
    }

    IEnumerator ReturnToPlayer()
    {
        canBeDestroyed = true;
        while (Vector3.Distance(gameObject.transform.position, player.transform.position) > 0.1f)
        {
            float step = returnSpeed * Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, player.transform.position, step);
            yield return null;
        }

    }
}
