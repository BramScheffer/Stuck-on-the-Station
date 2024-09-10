using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TurretShot : MonoBehaviour
{
    public float damage;
    public float bulletRange;
    public Transform playerCamera;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shoot()
    {
        Ray gunray = new Ray(playerCamera.position, playerCamera.forward);
        if (Physics.Raycast(gunray, out RaycastHit rayInfo, bulletRange))
        {
            if (rayInfo.collider.gameObject.CompareTag("Enemy"))
            {

            }
        }
    }
}
