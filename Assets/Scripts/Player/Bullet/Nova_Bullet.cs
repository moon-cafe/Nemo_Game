using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nova_Bullet : Projectile {
    [SerializeField]
    private GameObject Black_Holl;


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
            GameObject tmp_holl = Instantiate(Black_Holl);
            tmp_holl.transform.position = transform.position;
            tmp_holl.transform.name = "Holl";
            tmp_holl.GetComponent<CircleCollider2D>().radius = 8f;
            tmp_holl.transform.rotation = Quaternion.AngleAxis(180, Vector3.right);
            
            myrigidbody.velocity = Vector3.zero;
        }
    }
}
