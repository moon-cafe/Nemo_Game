using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle_Destroy : MonoBehaviour {

    private float time_t;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        time_t += Time.deltaTime;

        if (gameObject.name == "Explosion_No.3")
        {
            if (time_t > 1)
            {
                gameObject.SetActive(false);
                time_t = 0;
            }
        }
        else if (gameObject.name == "Explosion_No.7")
        {
            if (time_t > 4)
            {
                gameObject.SetActive(false);
                time_t = 0;
            }
        }
        else if (gameObject.name == "Holl")
        {
            if (time_t > 10)
            {
                gameObject.SetActive(false);
                time_t = 0;
            }
        }
        else
        {
            if (time_t > 2)
            {
                gameObject.SetActive(false);
                time_t = 0;
            }
        }
	}
}
