using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {
    //생성 관련 오브젝트풀.
    public Object_Pool Monster_Obpool;
    public Object_Pool Bullet_Obpool;
    public Object_Pool hit_Obpool;
    public Object_Pool Explosion_Obpool;
    public Object_Pool Ect_Obpool;

    public int Round_Numb;
    public bool Game_Over_Check;
    

    // Use this for initialization
    void Awake()
    {
        Time.timeScale = 1;
        Monster_Obpool = transform.GetChild(0).GetComponent<Object_Pool>();
        Bullet_Obpool = transform.GetChild(1).GetComponent<Object_Pool>();
        hit_Obpool = transform.GetChild(2).GetComponent<Object_Pool>();
        Explosion_Obpool = transform.GetChild(3).GetComponent<Object_Pool>();
        Ect_Obpool= transform.GetChild(4).GetComponent<Object_Pool>();
     
    }

	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}






    public void Game_ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
