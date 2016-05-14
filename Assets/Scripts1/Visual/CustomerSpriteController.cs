using UnityEngine;
using System.Collections;

public class CustomerSpriteController : MonoBehaviour {

	public GameObject CustomerPrefab;


	// Use this for initialization
	void Start () {
	}
	bool afterInit = false;
	void init()
	{
		int i = 0;
		foreach(Customer c in World.world.building.customers)
		{
			GameObject obj = (GameObject) Instantiate (CustomerPrefab, new Vector3 (c.x, c.y, 0), Quaternion.identity);
			obj.transform.parent = this.transform;
			obj.name = "Customer" + i;
			c.name = "Customer" + i;
			i += 1;
		}
		afterInit = true;	
	}
	// Update is called once per frame
	void Update () {
		if (!afterInit) {
			init ();
		}
		foreach (Customer c in World.world.building.customers) 
		{
			//Debug.Log (c.name);
			GameObject cust = GameObject.Find (c.name);
			cust.transform.position = new Vector3 (c.x, c.y, 0);
		}
	}
}
