using UnityEngine;
using System.Collections;


public class SelfDestruct : MonoBehaviour {
	public float selfdestruct_in = 4; // Setting this to 0 means no selfdestruct.

	void Start () {
		if ( selfdestruct_in != 0){
            StartCoroutine(selfDestroy());
            
		}
	}

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(selfdestruct_in);
        gameObject.SetActive(false);
    }
}
