
using UnityEngine;
using UnityEngine.SceneManagement;

public class LOSE : MonoBehaviour
{
    public GameObject losecond;
    // Start is called before the first frame update
    void Start()
    {
        losecond.SetActive(false);
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
        losecond.SetActive(true);
    }

}

