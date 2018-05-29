using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Spot : Singleton<Shot_Spot> {

    public Vector2 shot_spot { get; private set; }
    Vector3 point;
    Ray2D ray;
    RaycastHit2D[] hit;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
            point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            ray  = new Ray2D(point, (transform.position - point));

            hit = Physics2D.RaycastAll(ray.origin, ray.direction);

            for(int i=0;i<hit.Length;i++)
            {
                if(hit[i].collider.tag=="Center")
                {
                    shot_spot = hit[i].point;
                }
            }
       
	}
}
