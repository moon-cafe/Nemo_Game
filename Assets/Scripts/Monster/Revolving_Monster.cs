using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revolving_Monster : Monster {
    

	// Use this for initialization
	protected override void Start () {
       
        base.Start();
	}
	
	// Update is called once per frame
	void Update () {
        Hp_Manager();
	}

    public void Re_Respon()
    {
        Current_HP = 80;
        Start();
    }


    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        
    }
}
