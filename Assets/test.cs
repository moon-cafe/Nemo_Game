using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	float 보간비율 = 0.0f;
	float 회전속도 = 2.0f;

	bool 카메라_회전 = false;
	bool 카메라가_위에_있음 = false;

	Quaternion curQuatCamera;
	Quaternion nextQuatCamera;

	// Update is called once per frame
	void Update () {
		if (카메라_회전) {
			보간비율 += Time.deltaTime * 회전속도;
			transform.rotation = Quaternion.Slerp (curQuatCamera, nextQuatCamera, 보간비율);

			if (보간비율 >= 1.0f) {
				보간비율 = 0.0f;
				카메라_회전 = false;
			}
		}
		else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			카메라_회전 = true;
			curQuatCamera = transform.rotation;
			nextQuatCamera = transform.rotation * Quaternion.Euler (0, 90, 0);

			if (카메라가_위에_있음) {
				카메라가_위에_있음 = false;
				nextQuatCamera *= Quaternion.Euler (0, 0, -90);
			}
		}
		else if (Input.GetKeyDown (KeyCode.RightArrow)) {
			카메라_회전 = true;
			curQuatCamera = transform.rotation;
			nextQuatCamera = transform.rotation * Quaternion.Euler (0, -90, 0);

			if (카메라가_위에_있음) {
				카메라가_위에_있음 = false;
				nextQuatCamera *= Quaternion.Euler (0, 0, 90);
			}
		}
		else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			카메라_회전 = true;
			curQuatCamera = transform.rotation;
			nextQuatCamera = transform.rotation * Quaternion.Euler (90, 0, 0);

			if (카메라가_위에_있음) {
				카메라가_위에_있음 = false;
				nextQuatCamera *= Quaternion.Euler (0, 0, 180);
			} else {
				카메라가_위에_있음 = true;
			}
		}
		else if (카메라가_위에_있음 && Input.GetKeyDown (KeyCode.DownArrow)) {
			카메라_회전 = true;
			curQuatCamera = transform.rotation;
			nextQuatCamera = transform.rotation * Quaternion.Euler (-90, 0, 0);

			카메라가_위에_있음 = false;
		}
	}
}
