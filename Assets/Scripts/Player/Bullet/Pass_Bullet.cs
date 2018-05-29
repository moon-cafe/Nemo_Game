using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pass_Bullet : Projectile
{
    [SerializeField]
    private int Pass_Check_Number;

    private int P_count;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        Range();
        if (Range_Over_Check)
        {
            P_count = 0;
            Range_Over_Check = false;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Monster")
        {
            P_count++;
            if (P_count>Pass_Check_Number)
            {
                gameObject.SetActive(false);
                myrigidbody.velocity = Vector3.zero;
                P_count = 0;
            }

            
            
            
            GameObject particle_tmp = GameManager.Instance.Explosion_Obpool.GetObject(GetComponent<ETFXProjectileScript>().impactParticle.name);
            particle_tmp.transform.position = transform.position;
            particle_tmp.transform.rotation = transform.rotation;
            

            other.GetComponent<Monster>().Current_HP -= Damage;
        }
    }

}
