using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ottoogi : Monster {
    [HideInInspector]
    public bool Resurrection;

    private bool Die;

    private float Die_Time;
	// Use this for initialization
	protected override void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        if(!Die)
            Move();

        Hp_Manager();
	}


    new private void Hp_Manager()
    {
        if (Current_HP <= 0)
        {
            if(Resurrection)
            {
                Die = true;
                Die_Time += Time.deltaTime;
                transform.GetComponent<Collider2D>().enabled = false;
                int numb = Random.Range(0, 2);
               
                if (myrigid.velocity!=Vector2.zero)
                {
                    myrigid.velocity = Vector2.zero;
                }
                if(Die_Time>2)
                {
                    transform.GetComponent<Collider2D>().enabled = true;
                    Die = false;
                    Resurrection = false;
                    Current_HP = Hp;
                    Die_Time = 0;
                }
            }
            else if (!Resurrection)
            {
                int numb = Random.Range(0, 2);

                GameObject des_tmp = GameManager.Instance.hit_Obpool.GetObject(Destroy_Particle[numb].name);
                des_tmp.transform.position = transform.position;
                des_tmp.transform.rotation = transform.rotation;
                Round_Manager.Instance.Monster_Kill_Count++;
                if (Last_Monster)
                {
                    Round_Manager.Instance.Last = true;
                }

                gameObject.SetActive(false);
            }
        }
    }

}
