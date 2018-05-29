#pragma strict
var text_fx_name : TextMesh;
var fx_prefabs : GameObject[];
var index_fx : int = 0;


private var ray : Ray;
private var ray_cast_hit : RaycastHit;


function Start () {
	text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[ index_fx ].name;
	yield WaitForSeconds(6.0);
	Destroy(GameObject.Find("Instructions"));
}


function Update () {
	if ( Input.GetMouseButtonDown(0) ){
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, ray_cast_hit)){
			if(index_fx == 2 || index_fx == 6 || index_fx == 10 || index_fx == 14 || index_fx == 18 || index_fx == 22)
			Instantiate(fx_prefabs[ index_fx ], Vector3(ray_cast_hit.point.x, 0, ray_cast_hit.point.z), transform.rotation);
			else
			Instantiate(fx_prefabs[ index_fx ], Vector3(ray_cast_hit.point.x, 0.7, ray_cast_hit.point.z - 0.5), transform.rotation);	
		}
	}

	//Change-FX keyboard..	
	if ( Input.GetKeyDown("z") || Input.GetKeyDown("left") ){
		index_fx--;
		if(index_fx <= -1)
			index_fx = fx_prefabs.Length - 1;
		text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[ index_fx ].name;	
	}
	
	if ( Input.GetKeyDown("x") || Input.GetKeyDown("right")){
		index_fx++;
		if(index_fx >= fx_prefabs.Length)
			index_fx = 0;
		text_fx_name.text = "[" + (index_fx + 1) + "] " + fx_prefabs[ index_fx ].name;
	}
	
	if ( Input.GetKeyDown("space") ){
		if(index_fx == 2 || index_fx == 6 || index_fx == 10 || index_fx == 14 || index_fx == 18 || index_fx == 22)
			Instantiate(fx_prefabs[ index_fx ], Vector3(0, 0, 5.0), transform.rotation);
			else
			Instantiate(fx_prefabs[ index_fx ], Vector3(0, 0.7, 5.0 - 0.5), transform.rotation);
	}
	//Hello theere :)	
}