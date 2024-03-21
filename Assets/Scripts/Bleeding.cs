using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bleeding : MonoBehaviour
{
    public CharacterJoint joint;
    public Transform bloodPos;
    public GameObject bloodSpurt;
    public bool blood;

    private void Start()
    {
        joint = transform.GetComponent<CharacterJoint>();
        bloodPos = transform.parent.GetChild(0);
        blood = false;
    }

    private void Update()
    {
        if (joint == null && !blood)
        {
            SpawnBlood();
        }
    }

    void SpawnBlood()
    {
        blood = true;
        Instantiate(bloodSpurt, bloodPos.transform.position, bloodPos.transform.rotation, bloodPos.transform);
    }
}
