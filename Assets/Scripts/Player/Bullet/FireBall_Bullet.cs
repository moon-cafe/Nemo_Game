using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Bullet : Projectile {
    [SerializeField]
    private GameObject Boom_Collider;


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
        if (other.tag == "Monster")
        {
            GameObject tmp_boom = GameManager.Instance.Explosion_Obpool.GetObject(Boom_Collider.name);
            tmp_boom.transform.position = transform.position;
            tmp_boom.GetComponent<CircleCollider2D>().radius = 1;
            tmp_boom.transform.rotation = transform.rotation;
            tmp_boom.GetComponent<FireBall_Explosion>().Damage = Damage;
            myrigidbody.velocity = Vector3.zero;
        }
    }

}
