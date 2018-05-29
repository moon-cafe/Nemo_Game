using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Bullet : Projectile {
    [SerializeField]
    private GameObject Bullet;

    private float Split_Damage;

    [HideInInspector]
    public GameObject Pass_Monster;

    [HideInInspector]
    public int Split_Count;

    
    // Use this for initialization
    protected override void Start () {
        base.Start();
        if (gameObject.name == "Bullet_No.4")
        {
            Split_Damage = Damage;
            Split_Count = 1;
        }
    }
	
	// Update is called once per frame
	void Update () {
        //if(gameObject.name== "Bullet_No.4")
            Range();
	}



    new void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.tag=="Monster" )
        {
            Monster O_Count = other.gameObject.GetComponent<Monster>();

            if (Pass_Monster == null)
            {
                Split(O_Count, other);
                /*GameObject tmp_1 = GameManager.Instance.Bullet_Obpool.GetObject(Bullet.name);
                tmp_1.GetComponent<Magic_Bullet>().Pass_Monster = other.gameObject;
                tmp_1.transform.position = transform.position;
                tmp_1.GetComponent<Magic_Bullet>().direction_setting(Direction);
                tmp_1.GetComponent<Magic_Bullet>().Start_Check = true;
                tmp_1.GetComponent<Magic_Bullet>().Bullet_Speed = 0.7f;
                gameObject.SetActive(false);*/
                //GameObject particle_tmp = GameManager.Instance.Explosion_Obpool.GetObject(GetComponent<ETFXProjectileScript>().impactParticle.name);
                //particle_tmp.transform.position = transform.position;
                //particle_tmp.transform.rotation = transform.rotation;
                //particle_tmp.transform.localScale = transform.localScale;
                //other.GetComponent<Monster>().Current_HP -= Damage;
            }
            else if (O_Count.Monster_numb != Pass_Monster.GetComponent<Monster>().Monster_numb)
            {
               
                if (Split_Count == 1)
                {
                    Split(O_Count,other);
                }
                else if(Split_Count==2)
                {
                    Split(O_Count, other);
                }
                else
                {
                    gameObject.SetActive(false);
                    GameObject particle_tmp = GameManager.Instance.Explosion_Obpool.GetObject(GetComponent<ETFXProjectileScript>().impactParticle.name);
                    particle_tmp.transform.position = transform.position;
                    particle_tmp.transform.rotation = transform.rotation;
                    particle_tmp.transform.localScale = transform.localScale;
                    O_Count.Current_HP -= Split_Damage;
                }
            }
            //other.GetComponent<Monster>().Current_HP -= Split_Damage;
            //Debug.Log(other.GetComponent<Monster>().Current_HP);
            /*GameObject tmp_1 = GameManager.Instance.Bullet_Obpool.GetObject(Bullet.name);
            tmp_1.transform.position = transform.position;
            tmp_1.transform.rotation = transform.rotation;
            tmp_1.GetComponent<Magic_Bullet>().Pass_Monster = other.gameObject;
            Debug.Log(tmp_1.GetComponent<Magic_Bullet>().Pass_Monster);*/
            
        }
       
    }


    private void Split(Monster o_Count,Collider2D other)
    {
        GameObject[] tmp_bull = new GameObject[3];
        for (int i = 0; i < tmp_bull.Length; i++)
        {
            tmp_bull[i] = GameManager.Instance.Bullet_Obpool.GetObject(Bullet.name);
            tmp_bull[i].GetComponent<Magic_Bullet>().Pass_Monster = other.gameObject;
            tmp_bull[i].transform.rotation = transform.rotation;

            if (transform.name == "Bullet_No.4")
            {

                tmp_bull[i].GetComponent<Magic_Bullet>().Bullet_Speed = 1f;
                //tmp_bull[i].transform.localScale = transform.localScale;
                tmp_bull[i].GetComponent<Magic_Bullet>().Split_Damage = Damage;
            }
            tmp_bull[i].GetComponent<Magic_Bullet>().Split_Count = Split_Count + 1;
            tmp_bull[i].transform.position = transform.position;
            //tmp_bull[i].transform.localScale *= 0.7f;
            tmp_bull[i].GetComponent<Magic_Bullet>().Split_Damage *= 0.7f;

            if (i == 0)
            {
                //tmp_bull[i].transform.rotation = transform.rotation;
                //tmp_bull[i].transform.rotation = Quaternion.AngleAxis(-30, Vector2.down);
                tmp_bull[i].GetComponent<Magic_Bullet>().direction_setting(Direction - new Vector3(0.3f, -0.3f));
            }
            else if (i == 1)
            {
                //tmp_bull[i].transform.rotation = transform.rotation;

                tmp_bull[i].GetComponent<Magic_Bullet>().direction_setting(Direction - new Vector3(-0.3f, 0.3f));
            }
            else if (i == 2)
            {
                //tmp_bull[i].transform.rotation = transform.rotation;
                // tmp_bull[i].transform.rotation = Quaternion.AngleAxis(30, Vector2.down);
                tmp_bull[i].GetComponent<Magic_Bullet>().direction_setting(Direction);
            }
            tmp_bull[i].GetComponent<Magic_Bullet>().Start_Check = true;
            tmp_bull[i].GetComponent<Magic_Bullet>().Bullet_Speed = 1f;
        }


        gameObject.SetActive(false);
        GameObject particle_tmp = GameManager.Instance.Explosion_Obpool.GetObject(GetComponent<ETFXProjectileScript>().impactParticle.name);
        particle_tmp.transform.position = transform.position;
        particle_tmp.transform.rotation = transform.rotation;
        particle_tmp.transform.localScale = transform.localScale;
        o_Count.Current_HP -= Split_Damage;
    }
}
