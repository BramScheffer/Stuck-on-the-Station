using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenager : MonoBehaviour
{
    public static BuildMenager Instance;
    public GameObject turretToBuild;
    public GameObject standardTurretPrefab;
    public GameObject enrgieTowerPrefab;
    public GameObject barbedWirePrefab;
    public bool traps;


    void Awake()
    {
        if (Instance == null)
        {
            Debug.Log("more then one buildmanager in scene!");
        }
        Instance = this;

    }


    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }
    public GameObject GetTurretToBuild()
    {
        return turretToBuild;

    }
    public void energietower()
    {
        turretToBuild = enrgieTowerPrefab;
        traps = false;
    }
    public void Turrettower()
    {
        turretToBuild = standardTurretPrefab;
        traps = false;
    }
    public void BarbedWire()
    { 
        traps = true;
        turretToBuild = barbedWirePrefab;
        
    }
}
