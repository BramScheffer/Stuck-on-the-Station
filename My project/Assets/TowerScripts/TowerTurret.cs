using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerTurret : MonoBehaviour
{
    public Transform turret;
    public Transform rotatingGunHolder;
    public float rotationSmooth;
    public Transform enemy;
    public FindEnemy find;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        enemy = find.FindEnem();

            if (enemy != null)
            {
                RotateGunHolder();             
            }
        
    }
    void RotateGunHolder()
    {
        Vector3 direction = enemy.position - turret.position;
        direction.y = 0;
        Quaternion rotation = Quaternion.LookRotation(direction);
        rotatingGunHolder.rotation = Quaternion.Slerp(rotatingGunHolder.rotation, rotation, rotationSmooth);
    }
}
