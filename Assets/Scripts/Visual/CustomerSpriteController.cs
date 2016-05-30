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
		float red,gre, blu;
		System.Random rnd = new System.Random();
		foreach(Customer c in World.world.building.customers)
		{
			GameObject obj = (GameObject) Instantiate (CustomerPrefab, new Vector3 (c.x, c.y, 0), Quaternion.identity);
			obj.transform.parent = this.transform;
			obj.name = "Customer" + i;
			c.name = "Customer" + i;
			i += 1;
			red = rnd.Next (0,100)/100f;
			gre = rnd.Next (0,100)/100f;
			blu = rnd.Next (0,100)/100f;
			obj.GetComponent<MeshRenderer>().material.color = new Color(red, gre, blu);
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
