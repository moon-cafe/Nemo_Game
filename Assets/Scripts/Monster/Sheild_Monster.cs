using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheild_Monster :  Monster{

    private bool Invincibility;
    [SerializeField]
    private GameObject Sheild;

    private float time_t;

    // Use this for initialization
    protected override void Start () {
        base.Start();
        time_t = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Invincibility)
            Current_HP = Hp;

        Move();
        Hp_Manager();
	}


    private void Sheild_Setting()
    {
        if (Sheild.activeSelf == true)
        {
            Invincibility = true;
        }
        else
        {
            Invincibility = false;
            time_t += Time.deltaTime;
            if(time_t >3)
            {
                Sheild.SetActive(true);
                time_t = 0;
            }

        }
    }


    public void Sheild_Respon()
    {
        time_t = 0;
        Sheild.SetActive(true);
    }



    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if(other.tag=="B&B")
        {
            if(Sheild.activeSelf)
            {
                Sheild.SetActive(false);
            }
        }
    }
}
