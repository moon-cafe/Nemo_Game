using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide_Monster : Monster
{
    [HideInInspector]
    public GameObject line_check;

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Hp_Manager();
    }

    protected override void Hp_Manager()
    {
        if (Current_HP <= 0)
        {
            if (line_check != null)
            {
                line_check.SetActive(false);
            }
        }

        base.Hp_Manager();
    }



    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag == "Player" && line_check != null)
        {
            line_check.SetActive(false);
        }

       
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.name == "Line")
        {
            if (line_check == null)
            {
                line_check = other.gameObject;
            }
        }
    }
}
