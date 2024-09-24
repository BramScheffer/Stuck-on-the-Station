using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Win : MonoBehaviour
{
    public GameObject wincond;
    // Start is called before the first frame update
    void Start()
    {
       wincond.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void restart()
    {
        SceneManager.LoadScene("Peron1");
    }
    public void ShowCanvas()
    {
       wincond.SetActive(true);
    }

}
