using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Player : Singleton<Player> {
   [SerializeField]
    private Stat Player_Stat;
    [HideInInspector]
    public float Atk_t;
    [HideInInspector]
    public bool Player_hit;

    //private GameObject Bullet;

    [SerializeField]
    private GameObject[] Bullet_Prefabs;

    [HideInInspector]
    public int Bullet_Number;

    [HideInInspector]
    public bool Monster_hit;
    [HideInInspector]
    public GameObject monster;

    [SerializeField]
    private GameObject[] Hit_particle;

    [SerializeField]
    private GameObject GameOver_Text;
    [HideInInspector]
    public int hit_damage;

    [SerializeField]
    private Transform parent;
   
    public int[] Bullet_Amount=new int[10];


    public int Total_Damage;

    public bool Ctrl_Check;


    public int Player_Level;

    [SerializeField]
    private Stat player_Ex;

    private int Remain_Ex;

    
    // Use this for initialization
    void Start () {
        Player_Stat.Initialize();
        
        Player_Stat.Atk_Speed = 0.3f;
        Monster_hit = false;
        Player_Stat.MaxVal = 100;
        Player_Stat.CurrentValue = Player_Stat.MaxVal;
        Bullet_Number = 0;
        Bullet_Reset();
        Ctrl_Check = true;


        Player_Level = PlayerPrefs.GetInt("Level");
        Player_Max_Ex_Setting();
        player_Ex.CurrentValue= PlayerPrefs.GetFloat("Current_Ex");
        player_Ex.bar.fillAmount= PlayerPrefs.GetFloat("Ex_Bar");
        player_Ex.Initialize();
        if (Player_Level==0)
            Player_Level = 1;
        
	}

    public void Bullet_Reset()
    {
        for (int i = 0; i < Bullet_Amount.Length; i++)
        {
            Bullet_Amount[i] = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        attack();
        hit_check();
        KeyBorde_Input();
        Status();
        //if(player_Ex !=null)
            Ex_Ctrl();
	}

    private void Ex_Ctrl()
    {
        if(player_Ex.CurrentValue>=player_Ex.MaxVal)
        {
            Debug.Log("a");
            
        }

    }


    private void hit_check()
    {


        //몬스터 거리로 쟀을때 사용했던코드
    /*    if(Monster_hit == true)
        {
            
            Player_Stat.CurrentValue -= 5;
            Monster_hit = false;
        }
        if(Player_hit==true)
        {
            int hit_number;
            if (monster.name== "Monster_No.1")
            {
                hit_number = Random.Range(0, 2);

                monster.GetComponent<Monster>().Hit_Particle(Hit_particle[hit_number]);
            }
            else if (monster.name == "Monster_No.2")
            {
                hit_number = Random.Range(2, 4);

                monster.GetComponent<Monster>().Hit_Particle(Hit_particle[hit_number]);
            }
            else if (monster.name == "Monster_No.3")
            {
                hit_number = Random.Range(4, 7);

                monster.GetComponent<Monster>().Hit_Particle(Hit_particle[hit_number]);
            }
            else if (monster.name == "Monster_No.4")
            {
                hit_number = Random.Range(7, 9);

                monster.GetComponent<Monster>().Hit_Particle(Hit_particle[hit_number]);
            }
            Player_hit = false;
        }
        */
        if(Player_hit)
        {
            Player_Stat.CurrentValue -= hit_damage;
            Total_Damage += hit_damage;
            Player_hit = false;
        }
    }


    private void attack()
    {
        Atk_t += Time.deltaTime;

        if (Input.GetMouseButton(0)&& !EventSystem.current.IsPointerOverGameObject() &&Ctrl_Check)
        {
            if (Atk_t > Player_Stat.Atk_Speed)
            {

                if (Bullet_Amount[Bullet_Number] > 0)
                {
                    Vector2 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //Debug.Log(Shot_Spot.Instance.shot_spot);
                    GameObject Bullet = GameManager.Instance.Bullet_Obpool.GetObject(Bullet_Prefabs[Bullet_Number].name);
                    Projectile bull_tmp = Bullet.GetComponent<Projectile>();
                    bull_tmp.transform.SetParent(parent);
                    Bullet.transform.position = Shot_Spot.Instance.shot_spot;
                    Bullet.transform.LookAt(new Vector3(mouse_position.x, mouse_position.y, 0));
                    bull_tmp.direction_setting(mouse_position);
                    bull_tmp.Start_Check = true;
                    switch (Bullet_Number)
                    {
                        //노말
                        case 0:
                            bull_tmp.Bullet_Speed = 1;
                            Player_Stat.Atk_Speed = 0.3f; break;
                        //레이저
                        case 1:
                            bull_tmp.Bullet_Speed = 2;
                            Player_Stat.Atk_Speed = 0.2f;
                            break;
                        //노바 스몰
                        case 2:
                            bull_tmp.Bullet_Speed = 0.3f;
                            Player_Stat.Atk_Speed = 0.2f;
                            break;
                        //파이어볼
                        case 3:
                            bull_tmp.Bullet_Speed = 1.5f;
                            Player_Stat.Atk_Speed = 0.3f;
                            break;
                        //매직
                        case 4:
                            bull_tmp.Bullet_Speed = 1.5f;
                            Player_Stat.Atk_Speed = 0.2f;
                            break;
                        //로켓
                        case 5:
                            bull_tmp.Bullet_Speed = 2f;
                            Player_Stat.Atk_Speed = 0.5f;
                            break;
                        //소울
                        case 6:
                            bull_tmp.Bullet_Speed = 1.5f;
                            Player_Stat.Atk_Speed = 0.1f;
                            break;
                        //미스틱
                        case 7:
                            bull_tmp.Bullet_Speed = 1f;
                            Player_Stat.Atk_Speed = 1.5f;
                            break;
                        //뉴클리어
                        case 8:
                            bull_tmp.Bullet_Speed = 2;
                            Player_Stat.Atk_Speed = 0.5f;
                            break;
                        //빅노바
                        case 9:
                            bull_tmp.Bullet_Speed = 2;
                            Player_Stat.Atk_Speed = 10f;
                            break;
                    }
                    if(Bullet_Number !=0)
                        Bullet_Amount[Bullet_Number] -= 1;
                    Atk_t = 0;
                }
            }
        }
    }

    private void KeyBorde_Input()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Bullet_Number = 0;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Bullet_Number = 1;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Bullet_Number = 2;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Bullet_Number = 3;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Bullet_Number = 4;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Bullet_Number = 5;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            Bullet_Number = 6;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Bullet_Number = 7;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            Bullet_Number = 8;
            Atk_t = 99;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            Bullet_Number = 9;
            Atk_t = 99;
        }

    }


    private void Status()
    {
        if(Player_Stat.CurrentValue<=0)
        {
            GameOver_Text.SetActive(true);
            //Time.timeScale = 0;
            Ctrl_Check = false;
            GameManager.Instance.Game_Over_Check = true;
        }
    }



    public void Stat_Setting(int hp,int atk_dmg, float atk_spd,float range )
    {

    }
    public void Hp_Pull()
    {
        Player_Stat.CurrentValue = Player_Stat.MaxVal;
    }

    
    public void Hp_Change(int C_Hp)
    {

        Player_Stat.CurrentValue += C_Hp;

        if (Player_Stat.CurrentValue > Player_Stat.MaxVal)
        {
            Player_Stat.CurrentValue = Player_Stat.MaxVal;
        }
        else if (Player_Stat.CurrentValue<0)
        {
            Player_Stat.CurrentValue = 0;
        }

    }

    public void Player_Max_Ex_Setting()
    {
        player_Ex.MaxVal = 40;
        for(int i=1; i<30;i++)
        {
            if(Player_Level==i)
            {
                for(int j=1;j<i;j++)
                {
                    player_Ex.MaxVal = player_Ex.MaxVal + (30 * 1.5f);
                }
            }
        }
    }


    public void Player_Current_Ex_Setting(int ex)
    {
        player_Ex.bar.lerpSpeed = 5* ex/player_Ex.MaxVal;
        if (player_Ex.CurrentValue+ex >= player_Ex.MaxVal)
        {
            Debug.Log(player_Ex.CurrentValue);
            Remain_Ex = (int)(ex + (player_Ex.CurrentValue - player_Ex.MaxVal));
            player_Ex.CurrentValue = player_Ex.MaxVal;
            StartCoroutine(Ex_Bar());
            
            //Player_Current_Ex_Setting(tmp);
        }
        else
            player_Ex.CurrentValue += ex;

        Debug.Log(Player_Level);
        Debug.Log(player_Ex.CurrentValue);
        Debug.Log(player_Ex.MaxVal);
        PlayerPrefs.SetInt("Level", Player_Level);
        PlayerPrefs.SetFloat("Current_Ex", player_Ex.CurrentValue);
        PlayerPrefs.SetFloat("Ex_Bar", player_Ex.bar.fillAmount);
    }

    IEnumerator Ex_Bar()
    {
        yield return new WaitForSeconds(5/player_Ex.bar.lerpSpeed);
        player_Ex.CurrentValue = 0;
        player_Ex.bar.content.fillAmount = 0;
        player_Ex.bar.fillAmount = 0;
        Player_Level++;
        Player_Max_Ex_Setting();
        Player_Current_Ex_Setting(Remain_Ex);
        Remain_Ex = 0;
        PlayerPrefs.SetInt("Level", Player_Level);
        PlayerPrefs.SetInt("Current_Ex", (int)player_Ex.CurrentValue);
    }
}
