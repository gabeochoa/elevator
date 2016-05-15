using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour {

	public World world;
	public int width;
	public int height;
	// Use this for initialization
	void Start () {
		world = new World (width, height);
	}
	
	// Update is called once per frame
	void Update () {
		world.update (Time.deltaTime);
	}
}
