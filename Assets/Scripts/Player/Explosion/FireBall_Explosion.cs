using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Explosion : MonoBehaviour {
    public int Damage;


    public bool KnockBack_Check;


    public Vector2 direct;

    //시간변수
    private float Set_Active_t;
    private float Mystic_t;



    public bool Mystic_check;

    public float KnockBack_Length;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!Mystic_check)
        {
            Set_Active_t += Time.deltaTime;
            if (Set_Active_t > 0.3f)
            {
                Set_Active_t = 0;
                gameObject.SetActive(false);
                KnockBack_Check = false;
            }
        }
        else
        {
            Set_Active_t += Time.deltaTime;
            if (Set_Active_t > 4f)
            {
                Set_Active_t = 0;
                gameObject.SetActive(false);
                KnockBack_Check = false;
                Mystic_check = false;
            }
        }

	}


    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Monster")
        {
            other.GetComponent<Monster>().Current_HP -= Damage;

            if(KnockBack_Check==true &&other.GetComponent<Rigidbody2D>()!=null &&!other.GetComponent<Monster>().Hole_Check)
            {
                //Ray2D ray = new Ray2D(transform.position, (other.transform.position - transform.position));
                //RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction);
                direct = other.transform.position - transform.position;

                other.GetComponent<Rigidbody2D>().AddForce(direct.normalized * KnockBack_Length/Vector2.Distance(other.transform.position,transform.position));
                other.GetComponent<Monster>().Move_Check = false;
                other.GetComponent<Monster>().Knock_D = direct.normalized;
                other.GetComponent<Monster>().KnockBack_Length = KnockBack_Length;


            }
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag=="Monster" &&Mystic_check)
        {

            Mystic_t += Time.deltaTime;
            if (Mystic_t > 0.5f)
            {
                Mystic_t = 0;
                other.GetComponent<Monster>().Current_HP -= Damage;

            }
        }
    }
}
