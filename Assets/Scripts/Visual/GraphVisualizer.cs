using UnityEngine;
using System.Collections;

public class GraphVisualizer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnDrawGizmos ()
	{
		if (World.world == null)
			return;

		//Draw path
		Vector3 a = new Vector3 (World.world.building.customers [0].tile.x, World.world.building.customers [0].tile.y, 2);
		Vector3 b = new Vector3 (World.world.building.customers [0].tile.x, World.world.building.customers [0].tile.y, 2);
		if (World.world.building.customers [0].path != null)
		{
			foreach (Tile n in World.world.building.customers[0].path.path)
			{
				a = b;
				b = new Vector3 (n.x, n.y, 2);
				Debug.DrawLine (a, b, Color.red);
			}
		}

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
