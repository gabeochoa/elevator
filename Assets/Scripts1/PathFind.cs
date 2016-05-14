using System;           
using System.Linq;   
using System.Collections;  
using System.Collections.Generic;
using Priority_Queue;

public class PathFind                                                                                                                         
{      
    Queue<Tile> path;

    public PathFind(World world, Tile a, Tile b)
    {
        Console.WriteLine("PathFind: start");
        //literally wikipedia
        if(!world.graph.nodes.ContainsKey(a) || !world.graph.nodes.ContainsKey(b))
        {
            Console.WriteLine("PathFind: One of the start or end tiles is bad");
        }

        Node start = world.graph.nodes[a];
        Node end = world.graph.nodes[b];

        List<Node> visited = new List<Node>();
        SimplePriorityQueue<Node> open = new SimplePriorityQueue<Node>();
        open.Enqueue(start, 0);

        Dictionary<Node, Node> from = new Dictionary<Node, Node>();
        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        foreach(Node n in world.graph.nodes.Values)
        {
            dist[n] = -1f;
        }
        dist[start] = 0;
        
        Dictionary<Node, float> huerist = new Dictionary<Node, float>();
        foreach(Node n in world.graph.nodes.Values)
        {
            huerist[n] = -1f;
        }
        huerist[start] = cost_est(start, end);
        
        while(open.Count > 0)
        {
            Node cur = open.Dequeue();
            if(cur == end)//we done
            {
                constructPath(cur, from);
                return;
            }
            visited.Add(cur);
            foreach(Edge e in cur.edges)
            {
                Node nei = e.node;
                if(visited.Contains(nei))
                    continue;
                float gscore = dist[cur] + cost_est(cur, nei);
                if(open.Contains(nei) && gscore >= dist[nei])
                {
                    continue;
                }

                from[nei] = cur;
                dist[nei] = gscore;
                huerist[nei] = dist[nei] + cost_est(nei, end);

                if(!open.Contains(nei))
                {
                    open.Enqueue(nei, huerist[nei]);
                }
            }
        }

        Console.WriteLine("PathFind: eyy, no path");
        return;
    }

    //TODO: replace sqrt and pow with optim
    float cost_est(Node a, Node b)
    {
        return (float) Math.Sqrt( Math.Pow(a.tile.x - b.tile.x, 2) + Math.Pow(a.tile.y-b.tile.y, 2));
    }

    public Tile next()
    {
		if (path.Count == 0)
			return null;
        return path.Dequeue();
    }

    void constructPath(Node cur, Dictionary<Node, Node> from)
    {
        Queue<Tile> mypath = new Queue<Tile>();
        mypath.Enqueue(cur.tile);

        while(from.ContainsKey(cur))
        {
            cur = from[cur];
            mypath.Enqueue(cur.tile);
        }

        path = new Queue<Tile>(mypath.Reverse());
    }

    public int Length()
    {
        if(path == null)
            return 0;
        return path.Count();
    }
}  








