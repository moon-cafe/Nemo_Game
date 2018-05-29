using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour {
    [HideInInspector]
    public Vector2 dir;
    [HideInInspector]
    public float Alpha_n;

    private bool alpha_ctrl;

    private SpriteRenderer sp_render;


	// Use this for initialization
	void Start () {
        sp_render = transform.GetChild(0).GetComponent<SpriteRenderer>();
        Alpha_n = 255;
	}
	
	// Update is called once per frame
	void Update () {
        if (Alpha_n > 210)
        {
            alpha_ctrl = true;
        }
        else if (Alpha_n < 40)
            alpha_ctrl = false;


        if (alpha_ctrl)
        {
            Alpha_n -= Time.deltaTime * 200;
        }
        else
            Alpha_n += Time.deltaTime * 200;



        sp_render.color = new Color32(255,0,0, (byte)Alpha_n);




        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
