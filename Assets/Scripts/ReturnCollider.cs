using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpherePrefab") && other.GetComponent<PrefabControl>().canBeDestroyed)
        {
            Destroy(other.gameObject);
        }
    }
}
