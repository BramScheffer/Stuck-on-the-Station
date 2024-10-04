using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    public Color hoverColor;
    private Renderer rend;
    private Color startColor;
    private GameObject turret;
    public Vector3 positionOffset;
    public CameraSwitch camswitch;
    public bool Barbed;
    public Money mn;


    // Reference to the EnergyManager
    private EnergyManager energyManager;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;

        // Use FindObjectOfType if FindAnyObjectByType is not available
        energyManager = FindObjectOfType<EnergyManager>();

        if (energyManager == null)
        {
            Debug.LogError("EnergyManager not found in the scene!");
        }
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there!");
            return;
        }

        if (camswitch.inBuy)
        {
            if (!Barbed && !BuildMenager.Instance.traps && !BuildMenager.Instance.energy)
            {
                if (mn.money >= 200f)
                {
                    GameObject turretToBuild = BuildMenager.Instance.GetTurretToBuild();
                    turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
                    mn.money -= 200;
                }
               
            }
            if (Barbed && BuildMenager.Instance.traps && !BuildMenager.Instance.energy)
            {
                if (mn.money >= 100f)
                {

                    GameObject turretToBuild = BuildMenager.Instance.GetTurretToBuild();
                    turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
                    mn.money -= 100f;
                }
            }
            if (!Barbed && !BuildMenager.Instance.traps && BuildMenager.Instance.energy)
            {
                if (mn.money >= 150f)
                {
                    GameObject turretToBuild = BuildMenager.Instance.GetTurretToBuild();
                    turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
                    energyManager.energyIncreaseAmount += 0.15f; // Increase energy amount}
                    mn.money -= 150;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}