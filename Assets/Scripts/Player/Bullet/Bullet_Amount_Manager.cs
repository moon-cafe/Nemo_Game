using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet_Amount_Manager : Singleton<Bullet_Amount_Manager> {
    [SerializeField]
    private GameObject[] Bullet_UI;

    private float[] Bullet_Colldown_t=new float[10];
    [SerializeField]
    private float[] Bullet_Colldown=new float[10];

    [SerializeField]
    private Player player;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!GameManager.Instance.Game_Over_Check &&!Round_Manager.Instance.Closing_Start)
        Bullet_Charge();
	}


    public void Bullet_Charge()
    {
       

        for(int i=1;i<Bullet_Colldown.Length;i++)
        {

            Bullet_UI[i].transform.GetChild(1).GetComponent<Image>().fillAmount =Bullet_Colldown_t[i]/Bullet_Colldown[i];
            Bullet_Colldown_t[i] += Time.deltaTime;
            if (Bullet_Colldown_t[i]>Bullet_Colldown[i])
            {
                Bullet_Colldown_t[i] = 0;
                player.Bullet_Amount[i] += 1;
               
            }
            Bullet_UI[i].transform.GetChild(0).GetComponent<Text>().text = "" + player.Bullet_Amount[i];
        }

    }
    public void Bullet_Cool_Reset()
    {
        for(int i=0;i<Bullet_Colldown.Length;i++)
        {
            Bullet_Colldown_t[i] = 0;
        }
    }


    public void Bullet_numb(int numb)
    {
        player.Bullet_Number = numb;
        Debug.Log(numb);
        player.Atk_t = 99;
    }
}
