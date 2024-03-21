using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    public GameObject handgun, gravGun;
    public bool handgunEquipped, gravGunEquipped, spawnerEquipped;

    private void Start()
    {
        spawnerEquipped = true;
        handgunEquipped = false;
        gravGunEquipped = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipSpawner();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipHandgun();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipGravGun();
        }
    }

    public void EquipSpawner()
    {
        spawnerEquipped = true;
        handgunEquipped = false;
        gravGunEquipped = false;

    }

    public void EquipHandgun()
    {
        handgunEquipped = true;
        spawnerEquipped = false;
        gravGunEquipped = false;

    }

    public void EquipGravGun()
    {
        gravGunEquipped = true;
        spawnerEquipped = false;
        handgunEquipped = false;

    }
}
