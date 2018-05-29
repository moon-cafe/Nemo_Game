using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wing : Monster {
    float Creat_t;
    [HideInInspector]
    public float Creat_cool;

    [SerializeField]
    private GameObject small_M;

    
    

	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        Move();
        Hp_Manager();
        Small_Monster_Create();
	}

   

    private void Small_Monster_Create()
    {
        Creat_t += Time.deltaTime;
        if(Creat_t>Creat_cool&&!GameManager.Instance.Game_Over_Check)
        {
            if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "Wing")
            {
                GameObject Small_Monster = GameManager.Instance.Monster_Obpool.GetObject(small_M.name);
                Small_Monster.transform.position = transform.position;
                Small_Monster.GetComponent<Monster>().Current_HP = Small_Monster.GetComponent<Monster>().Hp;
                Small_Monster.GetComponent<Monster>().Move_Check = true;
                Small_Monster.GetComponent<Monster>().Last_Monster = false;
                Small_Monster.GetComponent<Monster>().MoveSpeed = 2;
            
            }
            else if (transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name == "Wing2")
            {
                for (int i = 0; i < 2; i++)
                {
                    GameObject Small_Monster = GameManager.Instance.Monster_Obpool.GetObject(small_M.name);
                    Small_Monster.transform.position = transform.position;
                    Small_Monster.GetComponent<Monster>().Current_HP = Small_Monster.GetComponent<Monster>().Hp;
                    
                    Small_Monster.GetComponent<Monster>().Last_Monster = false;
                    Small_Monster.GetComponent<Monster>().MoveSpeed = 2;

                    float tmp_x = Random.Range(-0.2f, 0.2f);
                    float tmp_y = Random.Range(-0.2f, 0.2f);
                    Vector2 dir = Center.transform.position - transform.position;
                    Small_Monster.GetComponent<Rigidbody2D>().AddForce(-dir.normalized - new Vector2(tmp_x, tmp_y) * 200);
                    Small_Monster.GetComponent<Monster>().Knock_D = -dir.normalized - new Vector2(tmp_x, tmp_y);
                    Small_Monster.GetComponent<Monster>().KnockBack_Length = 200;
                    Small_Monster.GetComponent<Monster>().Move_Check = false;
                }
            }
            Creat_t = 0;
        }

    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

}
