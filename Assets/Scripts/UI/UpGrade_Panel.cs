using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpGrade_Panel : MonoBehaviour {
    [SerializeField]
    private GameObject Up_Panel;
    [SerializeField]
    private GameObject Clear_Panel;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    public void Exit_Icon_Click()
    {
        Clear_Panel.SetActive(true);
        Up_Panel.SetActive(false);
    }

}
