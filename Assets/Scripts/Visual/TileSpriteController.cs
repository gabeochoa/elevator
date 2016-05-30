using UnityEngine;
using System;              
using System.Collections;  
using System.Collections.Generic;

public class TileSpriteController : MonoBehaviour {

	public GameObject tilePrefab;

	public GameObject extra;
	//List<GameObject> targets;
	// Use this for initialization
	void Start () {
		
	}

	bool afterInit = false;
	void init()
	{
		GameObject tileObj = new GameObject ();
		tileObj.name = "Tiles";
		tileObj.transform.parent = this.transform;
		//targets = new List<GameObject>();
		for (int i = 0; i < World.world.WIDTH; i++) {
			for (int j = 0; j < World.world.HEIGHT; j++) {
				Tile t = World.world.tiles [i, j];
				GameObject obj = (GameObject) Instantiate (tilePrefab, new Vector3 (t.x, t.y, -0.05f), Quaternion.identity);
				obj.transform.parent = tileObj.transform;
			}
		}
		afterInit = true;	
	}
	// Update is called once per frame
	void Update () {
		if (!afterInit) {
			init ();
		}
		/*targets.Clear();

		foreach(Customer c in World.world.building.customers)
		{
			Tile t = c.target;
			extra = (GameObject) Instantiate (tilePrefab, new Vector3 (t.x,t.y, 0), Quaternion.identity);
			extra.transform.parent = this.transform;
			extra.transform.GetChild (0).transform.localScale = new Vector3 (1, 1, 1);
			extra.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Color.green;
			targets.Add(extra);
		}*/
	}
}
