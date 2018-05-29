using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Closeing_Ctrl : Singleton<Closeing_Ctrl> {
    //[HideInInspector]
    //public bool Closing_Start;


    [SerializeField]
    private Text Kill_text;
    [SerializeField]
    private Text player_damage_text;
    [SerializeField]
    private Text coin_text;
    [SerializeField]
    private Text Ex_text;
    [SerializeField]
    private Text Lv_text;
    public float t_t;

    private Image content;

    [SerializeField]
    private GameObject Up_Pannel;
    [SerializeField]
    private GameObject Closing_Pannel;

    private float[] tmp_t=new float[5];
    public int k;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Text_Manager();
	}


    private void Text_Manager()
    {
        t_t += Time.deltaTime;
        if (t_t < 1.5f &&tmp_t[tmp_t.Length-1]!=1)
        {
            if (k <=3)
                tmp_t[k] = Mathf.Lerp(0, 1, t_t * 1);
            

            Kill_text.text = "Monster Kill : " + (int)(Round_Manager.Instance.Monster_Kill_Count * tmp_t[0]);
            player_damage_text.text = "Player Damage : " + (int)(Player.Instance.Total_Damage * tmp_t[1]);
            coin_text.text = "Get Coin : " + (int)(Round_Manager.Instance.coin * tmp_t[2]);
            
            Ex_text.text = "+ " + (int)(Round_Manager.Instance.Ex * tmp_t[3])+" Ex";
            
        }
        else
        {
            t_t = 0;
            if (k==-1)
            {
                for(int i=0;i<tmp_t.Length;i++)
                {
                    tmp_t[i] = 0;
                }

                Kill_text.text = "Monster Kill : " + 0;
                player_damage_text.text = "Player Damage :" + 0;
                coin_text.text = "Get Coin : " + 0;
                Ex_text.text = "+ " + 0;
                Lv_text.text = "Lv. " + Player.Instance.Player_Level;
            }
            if (k<tmp_t.Length-1)
                k++;
          
        }
        Lv_text.text = "Lv. " + (int)(Player.Instance.Player_Level * 1);
    }


    public int Coin_Calculate()
    {
        int tmp=0;
        int x, y;
        if(Monster_Respon.Instance.Round_Count<=5)
        {
            x = 1;y = 3;
        }
        else if(Monster_Respon.Instance.Round_Count <= 10)
        {
            x = 2;y = 4;
        }
        else
        {
            x = 0;y = 0;
        }


        for(int i=0;i<Round_Manager.Instance.Monster_Kill_Count;i++)
        {
            tmp += Random.Range(x, y);
        }
        return tmp;
    }
    public int Ex_Culculate()
    {
        int tmp = 1;
        float Scale = 1;
        if (Monster_Respon.Instance.round_stack > 1)
        {
            for (int i = 0; i < Monster_Respon.Instance.round_stack; i++)
            {
                Scale *= 1.2f;
            }
        }
        tmp = (int)(tmp * Round_Manager.Instance.Monster_Kill_Count * Scale);
        return tmp;
    }


    public void Up_Panel_Go()
    {
        Up_Pannel.SetActive(true);
        Closing_Pannel.SetActive(false);
        
    }

}
