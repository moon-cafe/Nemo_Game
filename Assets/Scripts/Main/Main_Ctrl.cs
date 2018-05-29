using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_Ctrl : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Screen.SetResolution(2560, 1440, true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Stage_Select_Scene()
    {
        SceneManager.LoadScene("Main_Stage_Select");
    }


    public void Game_Exit()
    {
        Application.Quit();
    }
}
