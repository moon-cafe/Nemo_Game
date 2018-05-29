using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Monster_Respon : Singleton<Monster_Respon>
{
    private float time_t;

    private float Respon_time;

    [SerializeField]
    private GameObject[] monster_Prefabs;

    [SerializeField]
    private Transform Center;

    private float Max_BoxSize_x;
    private float Max_BoxSize_y;

    [SerializeField]
    private Transform parent;
    //몬스터 리스폰 몇마리인지 카운트
    private int Respon_Count;

    //라운드 시작관리 조건
    private bool Round_Start;
    //지금이 몇라운드인지 체크
    public int Round_Count { get; private set; }

    //라운드 몬스터 생성
    private int Max_Monster_Count;

    //리스폰 몬스터 한계 넘버
    private int Respon_Max_Numb;

    [SerializeField]
    private GameObject line_prefab;

    [SerializeField]
    private Text Round_text;

    [SerializeField]
    private Sprite[] Wing_Image;

    private int Round_Stack;


    public int round_stack
    {
        get
        {
            return Round_Stack;
        }
    }
    // Use this for initialization
    void Start()
    {
        Max_BoxSize_x = 25;
        Max_BoxSize_y = 25;
        Round_Start = false;
        Round_Stack = 0;
        Round_next();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Round_Start && !GameManager.Instance.Game_Over_Check)
            Respon();

    }
    private void Round_Ctrl()
    {
        if (Respon_Count > Max_Monster_Count)
        {
            Round_Start = false;

        }
    }








    private void Respon()
    {
        time_t += Time.deltaTime;

        if (time_t > 0.7f && (Respon_Count < Max_Monster_Count || Max_Monster_Count == 0))
        {
            Respon_Count++;

            int monster_number = Random.Range(0, Respon_Max_Numb);


            GameObject monster_tmp = GameManager.Instance.Monster_Obpool.GetObject(monster_Prefabs[monster_number].name);
            monster_tmp.GetComponent<Monster>().Monster_numb = Respon_Count;
            if (Max_Monster_Count == 0 || Respon_Count == Max_Monster_Count)
            {
                monster_tmp.GetComponent<Monster>().Last_Monster = true;
            }
            else
            {
                monster_tmp.GetComponent<Monster>().Last_Monster = false;
            }
            monster_tmp.GetComponent<Monster>().Move_Check = true;
            monster_tmp.GetComponent<Monster>().Respon_Check = true;
            //monster_tmp.GetComponent<Monster>().myrigid.velocity = Vector2.zero;
            monster_tmp.transform.SetParent(parent);
            monster_tmp.transform.position = Box_Calculation();
            monster_tmp.GetComponent<Monster>().Current_HP = monster_tmp.GetComponent<Monster>().Hp;

            int round = round_stack * 5 + Round_Count;
            float Scale = 1;
            switch (monster_number)
            {
                //세모-기본
                case 0: monster_tmp.GetComponent<Monster>().MoveSpeed = 1;
                    for(int i=0;i<round;i++)
                    {
                        Scale *= 1.1f;
                    }
                    monster_tmp.GetComponent<Monster>().Monster_Damage *=Scale;
                    break;
                //오각형 -기본업
                case 1: monster_tmp.GetComponent<Monster>().MoveSpeed = 0.5f; break;
                //별 - 스피드업
                case 2: monster_tmp.GetComponent<Monster>().MoveSpeed = 2.5f; break;
                //ㄷ - 체력업
                case 3: monster_tmp.GetComponent<Monster>().MoveSpeed = 0.5f; break;
                //동그라미4개 -회전
                case 4: monster_tmp.GetComponent<Monster>().MoveSpeed = 0.2f; monster_tmp.GetComponent<Monster>().Respon(); break;
                //팩맨 - 실드
                case 5:
                    monster_tmp.GetComponent<Monster>().MoveSpeed = 1f;
                    monster_tmp.GetComponent<Sheild_Monster>().Sheild_Respon();
                    break;
                //아무고토없음 - 은신
                case 6:
                    monster_tmp.GetComponent<Monster>().MoveSpeed = 4f;
                    Ray2D ray = new Ray2D(monster_tmp.transform.position, (Center.position - monster_tmp.transform.position));

                    RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction);

                    monster_tmp.GetComponent<Hide_Monster>().line_check = null;

                    GameObject line = GameManager.Instance.Ect_Obpool.GetObject(line_prefab.name);
                    line.GetComponent<Line>().dir = Center.transform.position - monster_tmp.transform.position;
                    //line.GetComponent<LineRenderer>().SetPosition(0, transform.InverseTransformPoint(ray.origin));
                    line.transform.position = monster_tmp.transform.position;

                    for (int i = 0; i < hit.Length; i++)
                    {

                        if (hit[i].collider.tag == "Center")
                        {
                            line.transform.GetChild(0).localScale = new Vector3(0.2f, hit[i].distance * 0.9f, 1);

                        }
                    }


                    //line.transform.SetParent(monster_tmp.transform);

                    break;
                //윙
                case 7:
                    int tmp_t = Random.Range(0, 2);
                    if (tmp_t == 0)
                    {
                        monster_tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Wing_Image[tmp_t];
                        monster_tmp.GetComponent<Monster>().MoveSpeed = 0.2f;
                        monster_tmp.GetComponent<Wing>().Creat_cool = 5f;
                    }
                    else if (tmp_t == 1)
                    {
                        monster_tmp.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Wing_Image[tmp_t];
                        monster_tmp.GetComponent<Monster>().MoveSpeed = 0.7f;
                        monster_tmp.GetComponent<Wing>().Creat_cool = 4f;
                    }
                    break;
                case 8:
                    monster_tmp.GetComponent<Monster>().MoveSpeed = 0.9f;
                    monster_tmp.GetComponent<Ottoogi>().Resurrection = true;
                    break;
            }


            monster_tmp.transform.rotation = transform.rotation;



            time_t = 0;
        }
    }



    // 몬스터 랜덤위치 생성하기위한 함수 만들기.
    private Vector3 Box_Calculation()
    {
        float x = Random.Range(-Max_BoxSize_x, Max_BoxSize_x);
        float y = Random.Range(-Max_BoxSize_y, Max_BoxSize_y);
        Vector3 Respon_Position = Vector3.zero;
        int bearing = Random.Range(0, 4);
        // 0왼쪽 1위쪽 2오른쪽 3아래
        switch (bearing)
        {
            case 0:
                x = -Max_BoxSize_x;
                break;
            case 1:
                y = Max_BoxSize_y;
                break;
            case 2:
                x = Max_BoxSize_x;
                break;
            case 3:
                y = -Max_BoxSize_y;
                break;
        }


        Vector3 point = new Vector3(x, y);

        Ray2D ray = new Ray2D(point, (Center.position - point));

        RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction);

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.tag == "Respon_Circle")
            {
                Respon_Position = hit[i].point;
            }
        }

        return Respon_Position;

    }

    public void Round_next()
    {
        Round_Count++;
        Round_Start = true;
        Round_Manager.Instance.Last = false;
        Round_Manager.Instance.Closing_Start = false;
       

        if (Round_Count != 0)
        {
            Round_Count = Round_Count % 5;
            if (Round_Count == 0)
            {
                Round_Count = 5;
                
            }
            if (Round_Stack > 3)
                Max_Monster_Count = 20 * Round_Count * (int)(Round_Stack * 0.3f);
            else
                Max_Monster_Count = 20 * Round_Count;

        }
        else if (Round_Count == 0)
            Max_Monster_Count = 0;

        if (SceneManager.GetActiveScene().name == "test")
            Round_Count = 0;


        if (Round_Stack < 1)
        {
            switch (Round_Count)
            {

                case 1:
                    Respon_time = 0.7f;
                    Respon_Max_Numb = 3;
                    break;
                case 2:
                    Respon_time = 0.6f;
                    Respon_Max_Numb = 4;
                    break;
                case 3:
                    Respon_time = 0.5f;
                    Respon_Max_Numb = 4;
                    break;
                case 4:
                    Respon_time = 0.4f;
                    Respon_Max_Numb = 4;
                    break;
                case 5:
                    Respon_time = 0.25f;
                    Respon_Max_Numb = 4;
                    break;

                case 0:
                    Respon_time = 0.7f;
                    Respon_Max_Numb = monster_Prefabs.Length;
                    break;
            }
        }
        else
        {
            switch (Round_Count)
            {

                case 1:
                    Respon_time = 0.7f;
                    for (int i = 0; i < Round_Stack; i++)
                    {
                        Respon_time *= 0.9f;
                    }
                    Respon_Max_Numb = 4 + Round_Stack;
                    break;
                case 2:
                    Respon_time = 0.6f;
                    for (int i = 0; i < Round_Stack; i++)
                    {
                        Respon_time *= 0.9f;
                    }
                    Respon_Max_Numb = 4 + Round_Stack;
                    break;
                case 3:
                    Respon_time = 0.5f;
                    for (int i = 0; i < Round_Stack; i++)
                    {
                        Respon_time *= 0.9f;
                    }
                    Respon_Max_Numb = 4 + Round_Stack;
                    break;
                case 4:
                    Respon_time = 0.4f;
                    for (int i = 0; i < Round_Stack; i++)
                    {
                        Respon_time *= 0.9f;
                    }
                    Respon_Max_Numb = 4 + Round_Stack;
                    break;
                case 5:
                    Respon_time = 0.25f;
                    for (int i = 0; i < Round_Stack; i++)
                    {
                        Respon_time *= 0.9f;
                    }
                    Respon_Max_Numb = 4 + Round_Stack;
                    break;
            }
        }
        if (Respon_Max_Numb > monster_Prefabs.Length)
            Respon_Max_Numb = monster_Prefabs.Length;


        Respon_Count = 0;
        Debug.Log(Max_Monster_Count);
        Round_text.text = "Round " + (Round_Count + (Round_Stack * 5));

        if(Round_Count==5)
        {
            Round_Stack++;
        }

        Round_Manager.Instance.Round_Reset();
        Player.Instance.Bullet_Reset();
        Player.Instance.Hp_Pull();
        Player.Instance.Ctrl_Check = true;
        Player.Instance.Bullet_Number = 0;
        Bullet_Amount_Manager.Instance.Bullet_Cool_Reset();
        Player.Instance.Player_Max_Ex_Setting();

    }




}

