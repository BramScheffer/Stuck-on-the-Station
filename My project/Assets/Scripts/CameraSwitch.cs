using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraSwitch : MonoBehaviour
{
    public Camera playCam;
    public Camera buyCam;
    private Camera activeCamera;
    public bool camswitched;
    public bool inBuy;
    public bool dead = false;
    public GameObject buyscreen;
    // Start is called before the first frame update
    void Start()
    {
        activeCamera = playCam;
        playCam.enabled = true;
        buyCam.enabled = false;
        camswitched = false;
        Cursor.lockState = CursorLockMode.Locked;
        buyscreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) // Detects a single press of the "G" key
        {
            if (!camswitched)
            {
                camswitched = true;
                SwitchCamera();
            }
        }       
    }


    public void SwitchCamera()
    {
       if (!dead)
        {
            if (activeCamera == playCam)
            {
                playCam.enabled = false;
                buyCam.enabled = true;
                activeCamera = buyCam;
                camswitched = false;
                Cursor.lockState = CursorLockMode.Confined;
                inBuy = true;
                buyscreen.SetActive(true);


            }
            else
            {
                playCam.enabled = true;
                buyCam.enabled = false;
                activeCamera = playCam;
                camswitched = false;
                Cursor.lockState = CursorLockMode.Locked;
                inBuy = false;
                buyscreen.SetActive(false);

            }
        }// Switch active camera     
    }
    public void death()
    {
        playCam.enabled = false;
        buyCam.enabled = true;
        activeCamera = buyCam;
        inBuy = false;
        Cursor.lockState = CursorLockMode.Confined;
        dead = true;
    }
}
