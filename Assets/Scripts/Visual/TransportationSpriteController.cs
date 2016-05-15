﻿using UnityEngine;
using System.Collections;

public class TransportationSpriteController : MonoBehaviour {
	
	public GameObject ElevatorPrefab;
	
	// Use this for initialization
	void Start () {
	}
	bool afterInit = false;
	void init()
	{
		int i = 0;
		foreach(Shaft c in World.world.building.shafts)
		{
			if(c.type == Shaft.ShaftType.ELEVATOR)
			{
				GameObject obj = (GameObject) Instantiate (ElevatorPrefab, new Vector3 (c.transport.tile.x, c.transport.tile.y, 0), Quaternion.identity);
				obj.transform.parent = this.transform;
				obj.name = "Elevator" + i;
				c.transport.name = "Elevator"+i;
				i+=1;
			}
		}
		afterInit = true;	
	}
	// Update is called once per frame
	void Update () {
		if (!afterInit) 
		{
			init ();
		}
		foreach (Shaft c in World.world.building.shafts)
		{
			if (c.type == Shaft.ShaftType.ELEVATOR) 
			{
				//Debug.Log (c.name);
				GameObject elev = GameObject.Find (c.transport.name);
				elev.transform.position = new Vector3 (c.transport.tile.x, c.transport.tile.y, 0);
			}
		}
	}
}
