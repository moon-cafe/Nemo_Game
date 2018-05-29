using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Round_Manager : Singleton<Round_Manager> {
    [HideInInspector]
    public int Monster_Kill_Count;
    [HideInInspector]
    public bool RoundClear;
    private bool ClearEffectEnd;


    [HideInInspector]
    public bool Last;

    private float Clear_t;
    [SerializeField]
    private GameObject Clear_Text;
    [SerializeField]
    private GameObject Result_Screen;


    float fire_t;

    public float Fire_Max_t;
    [SerializeField]
    private GameObject[] Fire_Prefabs;

    [HideInInspector]
    public bool Closing_Start;

    public int coin;

    public int Ex;

    // Use this for initialization
    void Start () {
        Monster_Kill_Count = 0;

	}
	
	// Update is called once per frame
	void Update () {
        if (Last&& !GameManager.Instance.Game_Over_Check)
        {
            RoundClear = Round_Clear_Check();
            Round_Clear();
        }
        
	}
    public void Clear_Ctrl()
    {
        if (!ClearEffectEnd)
        {
            Clear_t += Time.deltaTime;
            //Fire_Max_t = 0;
        }
        if(Clear_t>4&&!ClearEffectEnd)
        {
           
            Clear_Text.SetActive(true);
            
        }
        if(Clear_t>8)
        {
            ClearEffectEnd = true;
            Clear_Text.SetActive(false);
            Result_Screen.SetActive(true);
            Player.Instance.Ctrl_Check = false;
            Closing_Start = true;
            
            coin =Closeing_Ctrl.Instance.Coin_Calculate();
            Coin_text.Instance.Coin_Plus(coin);

            Ex = Closeing_Ctrl.Instance.Ex_Culculate();
            StartCoroutine(ex_up());
            Clear_t = 0;
        }
        if (Clear_t > 4)
        {
            FireWork();
        }
    }
    IEnumerator ex_up()
    {
        yield return new WaitForSeconds(4.5f);
        Player.Instance.Player_Current_Ex_Setting(Ex);
    }


    private void FireWork()
    {
        Fire_Max_t += Time.deltaTime;
        fire_t += Time.deltaTime;
        if (fire_t > 0.05f && Fire_Max_t < 10)
        {
            float x = Random.Range(-11, 11);
            float y = Random.Range(-6, 6);
            int numb = Random.Range(0, Fire_Prefabs.Length);

            GameObject tmp = GameManager.Instance.Ect_Obpool.GetObject(Fire_Prefabs[numb].name);
            tmp.transform.position = new Vector2(x, y);
            tmp.transform.rotation = transform.rotation;
            fire_t = 0;
        }
    }


    public void Round_Clear()
    {
        if (RoundClear)
        {
            Clear_Ctrl();
            
           
        }
    }


    public bool Round_Clear_Check()
    {
        for (int i = 0; i < GameManager.Instance.Monster_Obpool.pooledObjects.Count; i++)
        {
            if (GameManager.Instance.Monster_Obpool.pooledObjects[i].activeSelf)
            {
                return false;
            }
            
                
        }
        return true;
    }


    public void Round_Reset()
    {
        Clear_t = 0;
        Last = false;
        RoundClear = false;
        ClearEffectEnd = false;
        if (Result_Screen.activeSelf)
        {
            Closing_Start = false;
            Closeing_Ctrl.Instance.k = -1 ;
        }
        Fire_Max_t = 0;
        Monster_Kill_Count = 0;
        Player.Instance.Total_Damage = 0;
        Result_Screen.SetActive(false);
        
    }


    public void Go_Main_Menu()
    {
        SceneManager.LoadScene("Main");
    }
}
