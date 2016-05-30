using UnityEngine;
using System.Collections;

public class GraphVisualizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnDrawGizmos ()
	{
		if (World.world == null || World.world.building.customers == null)
			return;

		//Draw path

		Vector3 a;Vector3 b;
		float red,gre,blu;
		System.Random rnd = new System.Random ();
		
		foreach(Customer c in World.world.building.customers )
		{
			a = new Vector3 (c.tile.x, c.tile.y, 2);
			b = new Vector3 (c.tile.x, c.tile.y, 2);
			red = rnd.Next (0,100)/100f;
			gre = rnd.Next (0,100)/100f;
			blu = rnd.Next (0,100)/100f;
			if (c.path != null)
			{
				foreach (Tile n in c.path.path)
				{
					a = b;
					b = new Vector3 (n.x, n.y, 2);

					Debug.Log(red);
					Debug.DrawLine (a, b, new Color(red, gre, blu));
				}
			}
		}
		if (World.world.graph == null || World.world.graph.nodes == null)
			return;

		foreach (Node n in World.world.graph.nodes.Values) 
		{
			foreach (Edge e in n.edges) 
			{
				Debug.DrawLine((new Vector3(n.tile.x, n.tile.y, 1.5f)), 
					(new Vector3(e.node.tile.x, e.node.tile.y, 1.5f))
					,Color.grey);
			}
		}
	}

}
