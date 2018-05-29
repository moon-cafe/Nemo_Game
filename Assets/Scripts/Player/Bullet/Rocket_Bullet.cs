using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket_Bullet : Projectile {
    [SerializeField]
    private GameObject Boom_Collider;

    public float Radius;

    public float KnockBack_Length;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Range();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {

        base.OnTriggerEnter2D(other);
        if (other.tag == "Monster")
        {
            GameObject tmp_boom = GameManager.Instance.Explosion_Obpool.GetObject(Boom_Collider.name);
            tmp_boom.transform.position = transform.position;
            tmp_boom.GetComponent<CircleCollider2D>().radius =Radius;
            tmp_boom.GetComponent<FireBall_Explosion>().KnockBack_Check = true;
            tmp_boom.transform.rotation = transform.rotation;
            tmp_boom.GetComponent<FireBall_Explosion>().Damage = Damage;
            tmp_boom.GetComponent<FireBall_Explosion>().direct = Direction.normalized;
            tmp_boom.GetComponent<FireBall_Explosion>().KnockBack_Length = KnockBack_Length;
            myrigidbody.velocity = Vector3.zero;
        }
    }
}
