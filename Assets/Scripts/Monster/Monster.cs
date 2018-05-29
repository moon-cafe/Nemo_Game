using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Monster : Monster_Stat {

    
    [HideInInspector]
    public GameObject Hit_particle;

    [SerializeField]
    protected GameObject[] Player_hit_particle;

    [HideInInspector]
    public int Monster_numb;

    [HideInInspector]
    public Rigidbody2D myrigid;

    protected float t;
    [HideInInspector]
    public bool Move_Check;

    [HideInInspector]
    public Vector2 Knock_D;
    [HideInInspector]
    public float KnockBack_Length;

    [SerializeField]
    protected GameObject[] Destroy_Particle;

    [HideInInspector]
    public bool Respon_Check;

    public bool Main_Check;

    [SerializeField]
    private GameObject[] Circles;
    [HideInInspector]
    public bool Hole_Check;

    private Transform holl;
    [HideInInspector]
    public bool Last_Monster;

    // Use this for initialization
    protected virtual void Start () {
        Hp = Current_HP;

        myrigid = GetComponent<Rigidbody2D>();
        t = 0;
        Move_Check = true;
	}
	
	// Update is called once per frame
	void Update () {

        Move();
        Hp_Manager();
	}

    public void Respon()
    {
        if(Respon_Check &&Circles[0]!=null)
        {
            for (int i = 0; i < 3; i++)
            {
                Circles[i].SetActive(true);
                Circles[i].GetComponent<Revolving_Monster>().Re_Respon();
            }
        }
    }

    protected void Move()
    {
        if (!GameManager.Instance.Game_Over_Check)
        {
            if (Move_Check)
            {
                transform.position = Vector2.MoveTowards(transform.position, Center.position, Time.deltaTime * MoveSpeed);

                
                if (Main_Check)
                    transform.Rotate(0, 0, 3);
                //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


                /* if(Vector3.Distance(transform.position,Center.position)<0.5f)
                 {
                     Player.Instance.monster = gameObject;
                     Player.Instance.Player_hit = true;
                 }*/
            }
            else if (Hole_Check == true)
            {
                if (transform.name != "Circle")
                {

                    transform.position = Vector2.MoveTowards(transform.position, holl.transform.position, 2 * Time.deltaTime);
                }
               

                
            }
          


            if (Move_Check == false && Hole_Check == false)
            {
                if (myrigid.velocity != Vector2.zero)
                    myrigid.AddForce(-KnockBack_Length * 0.03f * Knock_D);
                t += Time.deltaTime;

                if (t > 0.5f)
                {
                    myrigid.velocity = Vector2.zero;
                    t = 0;
                    Move_Check = true;

                }
            }

            Vector2 dir = Center.transform.position - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            if (!Main_Check)
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
    }
    protected virtual void Hp_Manager()
    {
        if (Current_HP <= 0)
        {

            int numb = Random.Range(0, 2);

            GameObject des_tmp = GameManager.Instance.hit_Obpool.GetObject(Destroy_Particle[numb].name);
            des_tmp.transform.position = transform.position;
            des_tmp.transform.rotation = transform.rotation;
            Round_Manager.Instance.Monster_Kill_Count++;
            if(Last_Monster)
            {
                Round_Manager.Instance.Last = true;
            }

            gameObject.SetActive(false);
        }
    }

    /*public void Hit_Particle(GameObject hit)
    {
        gameObject.SetActive(false);
        GameObject tmp_particle = GameManager.Instance.hit_Obpool.GetObject(hit.name);
        tmp_particle.transform.position = Center.position;
        tmp_particle.transform.rotation = Center.rotation;
        Player.Instance.Monster_hit = true;

    }*/

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            int hit_number;

            hit_number = Random.Range(0, 2);

            GameObject tmp_particle = GameManager.Instance.hit_Obpool.GetObject(Player_hit_particle[hit_number].name);
            tmp_particle.transform.position = other.transform.position;
            tmp_particle.transform.rotation = other.transform.rotation;
            other.GetComponent<Player>().Player_hit = true;
            other.GetComponent<Player>().hit_damage = (int)Monster_Damage;
            if (Last_Monster)
            {
                Round_Manager.Instance.Last = true;
            }
            gameObject.SetActive(false);
        }
        if (other.name == "Holl")
        {

            Move_Check = false;
            Hole_Check = true;
            holl = other.transform;
        }

    }

   /* public void OnTriggerStay2D(Collider2D other)
    {
       
        if (other.name == "Holl")
        {
            
            Move_Check = false;
            if (transform.name != "Circle")
            {
                
                transform.position = Vector2.MoveTowards(transform.position, other.transform.position, 2 * Time.deltaTime);
            }
                Vector2 dir = Center.transform.position - transform.position;

                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            
            if (!Main_Check)
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            else
                transform.Rotate(0, 0, 1.5f);

            Hole_Check = true;
        }
    }*/


    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.name=="Holl")
        {
            Hole_Check = false;
        }
    }
    
}
