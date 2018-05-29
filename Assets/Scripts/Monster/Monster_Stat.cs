using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Stat : Singleton<Monster_Stat>  {
    [HideInInspector]
    public float MoveSpeed;
    
    public float Hp;
    [HideInInspector]
    public float Current_HP;

    [HideInInspector]
    public float Amour;

    public float Monster_Damage;

    [SerializeField]
    protected Transform Center;

    
}
