using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nomal_Bullet : Projectile {

    

	// Use this for initialization
	protected override void Start () {
        base.Start();
        
	}
	
	// Update is called once per frame
	void Update () {

        Range();
    }



    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if(other.tag=="Monster")
        {
            other.GetComponent<Monster>().Current_HP -= Damage;
            myrigidbody.velocity = Vector3.zero;
        }
    }
}
