using Palmmedia.ReportGenerator.Core.Common;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject menu, crosshair;
    public Transform player;
    public ParticleSystem despawnEffect;
    public FirstPersonCam camControl;
    public Shoot shoot;
    public PrefabControl prefabControl;
    public ReturnCollider returnCol;

    public Slider forceSlider, sizeSlider, bounceSlider;
    public TextMeshProUGUI forceText, sizeText, bounceText;
    public Toggle gravityToggle;
    public InputField forceInput, sizeInput;
    public Dropdown pms;

    public GameObject poleM;

    public float returnSpeed;

    public void Start()
    {
        menu.SetActive(false);
        crosshair.SetActive(true);
        shoot.tempPrefab.GetComponent<Rigidbody>().useGravity = true;
        shoot.tempPrefab.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        bounceSlider.value = prefabControl.prefabPhysics.bounciness;
        pms = FindObjectOfType<Dropdown>();
        pms.onValueChanged.AddListener(delegate {
            DropdownValueChanged(pms);
        });
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !menu.activeInHierarchy)
        {
            menu.SetActive(true);
            crosshair.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            camControl.canLook = false;
            shoot.canShoot = false;
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && menu.activeInHierarchy)
        {
            menu.SetActive(false);
            crosshair.SetActive(true);
            Cursor.lockState= CursorLockMode.Locked;
            Cursor.visible = false;
            camControl.canLook = true;
            shoot.canShoot = true;
            Time.timeScale = 1;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !menu.activeInHierarchy)
        {
            ReturnAllPrefabs();
        }
    }

    public void ResetProps()
    {

    }

    public void ResetSpawns()
    {
        prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
        foreach(GameObject go in prefabs)
        {
            despawnEffect.Play();
            Destroy(go);
        }
    }

    public void ForceSlider()
    {
        shoot.force = forceSlider.value;
        forceText.text = shoot.force.ToString("F0");
    }

    public void ForceInput()
    {
        string forceInputTemp = forceInput.ToString();
        float forceFloat = forceInputTemp.ParseLargeInteger();
        shoot.force = forceFloat;
    }

    public void SizeSlider()
    {
        shoot.size = sizeSlider.value;
        Vector3 newScale = new Vector3(shoot.size, shoot.size, shoot.size);
        prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
        foreach(GameObject go in prefabs)
        {
            go.transform.localScale = newScale;
        }
        shoot.tempPrefab.transform.localScale = newScale;
        sizeText.text = shoot.size.ToString("F1");
    }

    public void BounceSlider()
    {
        prefabControl.prefabPhysics.bounciness = bounceSlider.value;
        prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
        foreach (GameObject go in prefabs)
        {
            go.GetComponent<PrefabControl>().prefabPhysics.bounciness = bounceSlider.value;
        }
        bounceText.text = bounceSlider.value.ToString("F1");
    }

    public void DropdownValueChanged(Dropdown change)
    {
        if (change.value == 0)
        {
            SphereSelect();
        }
        else if (change.value == 1)
        {
            CubeSelect();
        }
        else if (change.value == 2)
        {
            PoleSelect();
        }
    }

    public void SphereSelect()
    {
        prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
        foreach (GameObject go in prefabs)
        {
            var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            shoot.tempPrefab.GetComponent<MeshFilter>().mesh = sphere.GetComponent<MeshFilter>().sharedMesh;
        }
    }

    public void CubeSelect()
    {
        prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
        foreach (GameObject go in prefabs)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shoot.tempPrefab.GetComponent<MeshFilter>().mesh = cube.GetComponent<MeshFilter>().sharedMesh;
        }
    }

    public void PoleSelect()
    {

    }

    public void ToggleSpawnedGravity()
    {
        if (shoot.tempPrefab.GetComponent<Rigidbody>().useGravity == true)
        {
            prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
            foreach (GameObject go in prefabs)
            {
                go.GetComponent<Rigidbody>().useGravity = false;
            }
            shoot.tempPrefab.GetComponent<Rigidbody>().useGravity = false;
            gravityToggle.isOn = false;
        }
        else if (shoot.tempPrefab.GetComponent<Rigidbody>().useGravity == false)
        {
            prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
            foreach (GameObject go in prefabs)
            {
                go.GetComponent<Rigidbody>().useGravity = true;
            }
            shoot.tempPrefab.GetComponent<Rigidbody>().useGravity = true;
            gravityToggle.isOn = true;
        }
    }

    public void ReturnAllPrefabs()
    {
        prefabs = GameObject.FindGameObjectsWithTag("SpherePrefab");
        foreach (GameObject go in prefabs)
        {
            go.GetComponent<PrefabControl>().StartCoroutine("ReturnToPlayer");
        }
    }
}
