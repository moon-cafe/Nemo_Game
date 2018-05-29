using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_text : Singleton<Coin_text> {
    [SerializeField]
    private Text Coin_Text;

    private int Coin;

    public int coin
    {
        get
        {
            return Coin;
        }
    }

    private int pre_Coin;

    private float time_t;

    private bool Coin_Change;


	// Use this for initialization
	void Start () {
        Coin = 1000;
        Coin_Text.text = coin + " $";
	}
	
	// Update is called once per frame
	void Update () {
        if (Coin_Change == true)
        {
            time_t += Time.deltaTime;
            if (time_t < 1.5f)
                Coin_Text.text = (int)Mathf.Lerp(pre_Coin, Coin, time_t * 2) + " $";
            else
            {
                time_t = 0;
                Coin_Change = false;
            }
        }
       
    }

    public void Coin_Plus(int plus)
    {
        pre_Coin = Coin;
        Coin += plus;
        Coin_Change = true;
        Debug.Log(Coin);

        
    }




}
