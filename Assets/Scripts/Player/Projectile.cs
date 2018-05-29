using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public Vector3 Direction { get; private set; }
    

    [HideInInspector]
    public Rigidbody2D myrigidbody;
    [SerializeField]
    protected Vector3 start_position;
    [HideInInspector]
    public bool Start_Check;
    [HideInInspector]
    public float Bullet_Speed;
    [SerializeField]
    protected int Damage;
    //범위벗어나는지 체크
    protected bool Range_Over_Check;

    //임시저장 게임오브젝트
    protected GameObject tmp;

    // Use this for initialization
    protected virtual void Start() {
        myrigidbody = GetComponent<Rigidbody2D>();
 
	}
	
    protected void Range()
    {
        /* Ray2D ray = new Ray2D(transform.position, (Direction - transform.position));

         RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction);

         Vector3 dir= Vector3.zero;

         for (int i = 0; i < hit.Length; i++)
         {
             if (hit[i].collider.tag == "Wall")
             {
                 dir = hit[i].point;
             }
         }*/

        if(Start_Check)
        {
            myrigidbody.AddForce(Direction.normalized * Bullet_Speed*500);
            Start_Check = false;
        }

        if (Vector3.Distance(start_position, transform.position) > 15)
        {
            Range_Over_Check = true;
            gameObject.SetActive(false);
            myrigidbody.velocity = Vector3.zero;
        }
        
        //myrigidbody.velocity = (dir) * 10;
        //transform.position = Vector2.MoveTowards(transform.position, Direction, 20 * Time.deltaTime);
    }


    public void direction_setting(Vector3 direction)
    {
        Direction = direction;
        
    }


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag=="Monster")
        {
            gameObject.SetActive(false);
            GameObject particle_tmp = GameManager.Instance.Explosion_Obpool.GetObject(GetComponent<ETFXProjectileScript>().impactParticle.name);
            particle_tmp.transform.position = transform.position;
            particle_tmp.transform.rotation = transform.rotation;
            particle_tmp.transform.localScale = transform.localScale;
            
            /*if (gameObject.name == "Bullet_No.3")
            {
                particle_tmp.GetComponent<FireBall_Explosion>().Damage = Damage;
            }*/



            if (gameObject.name== "Bullet_No.7")
            {
                particle_tmp.transform.rotation =  Quaternion.AngleAxis(90,Vector3.right);
            }
            if (gameObject.name == "Bullet_No.8")
            {
                particle_tmp.transform.rotation = Quaternion.AngleAxis(90, Vector3.right);
            }
        }
    }
}
