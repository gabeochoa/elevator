using System;              
using System.Collections;  
using System.Collections.Generic;


public class Node
{
    public Tile tile;
    public Edge[] edges;
    
    public Node(Tile t)
    {
        tile = t;
    }
}
