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
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't built there!");
            return;
        }

        GameObject turretToBuild = BuildMenager.Instance.GetTurretToBuild();
       turret = Instantiate(turretToBuild, transform.position + positionOffset, transform.rotation);
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
