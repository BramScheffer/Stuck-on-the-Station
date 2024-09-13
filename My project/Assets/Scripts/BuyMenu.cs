using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyMenu : MonoBehaviour
{
    public BuildMenager blman;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Energietower()
    {
        blman.energietower();
    }
    public void TurretTower()
    {
        blman.Turrettower();
    }
}
