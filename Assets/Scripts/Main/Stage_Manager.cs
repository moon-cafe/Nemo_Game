using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage_Manager : Singleton<Stage_Manager> {
    public int Stage_Numb;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}



    public void Stage_1_Go()
    {
        SceneManager.LoadScene("Stage1");
        Stage_Numb = 1;
    }


    public void Main_Menu_Move()
    {
        SceneManager.LoadScene("Main");
        
    }


    public void Test_Go()
    {
        SceneManager.LoadScene("test");
        Stage_Numb = 0;
    }
}
