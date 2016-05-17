using System;              
using System.Collections;  
using System.Collections.Generic;


public class Graph
{
    public Dictionary<Tile, Node> nodes;

    public Graph(World w)
    {
        nodes = new Dictionary<Tile, Node>();
        for(int i=0; i<w.WIDTH; i++)
        {
            for(int j=0; j<w.HEIGHT; j++)
            {
                Tile t = w.tiles[i,j];
                Node n = new Node(t);
                nodes.Add(t, n);
            }
        }

        int edcount = 0;
        foreach(Tile t in nodes.Keys)
        {
            Node node = nodes[t];
            Tile[] neigh = t.getNeighbors();
            List<Edge> edges = new List<Edge>();
            foreach(Tile n in neigh)
            {
                Edge e = new Edge();
                //TODO: Maybe update cost for elevators
                // such that there exists an edge
                // to any accesable floor
                // where the cost is the time
                e.cost = 1;
                e.node = nodes[n];
                edges.Add(e);
                edcount++;
            }
            node.edges = edges.ToArray();
        }

        Console.WriteLine("Graph N:"+nodes.Count + " E:" + edcount);
    }
   
}
