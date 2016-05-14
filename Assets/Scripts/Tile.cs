using System;              
using System.Collections;  
using System.Collections.Generic;

public class Tile                                                                                                                         
{                
    public int x {get; protected set;}
    public int y {get; protected set;}
    public ArrayList atLocation;

    public Tile(int x, int y)
    {
        //Console.WriteLine("In tile constructor");
        atLocation = new ArrayList();
        this.x = x;
        this.y = y;
    }

    public void update()
    {
        Console.WriteLine("tiledate");
    }

    public Tile[] getNeighbors()
    {
        List<Tile> tiles = new List<Tile>();
        for(int i = -1; i <= 1; i++) 
        {
            for(int j = -1; j <= 1; j++) 
            {
                if(x != 0 || y != 0) 
                {
                    Tile xxx = World.world.getTiles(i+x, j+y);
                    if(xxx == null)
                        continue;
                    tiles.Add(xxx);
                }
            }
        }
        return tiles.ToArray();
    }

    public override string ToString()
    {
        string a = "Tile: ("+x + " " + y +")";
        foreach(Object e in atLocation)
        {
            if( e.GetType() == typeof(Customer) )
                a += "\n " + ((Customer)e) + "";
            if( e.GetType() == typeof(Elevator) )
                a += "\n " + ((Elevator)e) + "";
            if( e.GetType() == typeof(Floor) )
                a += "\n " + ((Floor) e) + "";
        }
        return a;
    }

}  