using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeScale_Ctrl : MonoBehaviour {
    [SerializeField]
    Text Magni_text;


	// Use this for initialization
	void Start () {
        Magni_text.text ="X "+1;

    }
	
	// Update is called once per frame
	void Update () {
		
	}


    public void Mag_Ctrl()
    {
        if(Time.timeScale==1)
        {
            Time.timeScale = 2;
            Magni_text.text = "X " + 2;
        }
        else if(Time.timeScale == 2)
        {
            Time.timeScale = 1;
            Magni_text.text = "X " + 1;
        }
    }
}
