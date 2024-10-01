using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    public float money;
    public TMP_Text moneyTxt;
    
    // Start is called before the first frame update
    void Start()
    {
        money = 300;
    }

    // Update is called once per frame
    void Update()
    {
        print(money);
        moneyTxt.text = money.ToString();
    }
    public void SmallZombie()
    {
        money += 10f;
    }
    public void BigZombie()
    {
        money += 30f;
    }
}
