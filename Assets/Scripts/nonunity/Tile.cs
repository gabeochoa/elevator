using System;              
using System.Collections;  
using System.Collections.Generic;

public class Tile                                                                                                                         
{                
    public int x {get; protected set;}
	public int y {get; protected set;}
	public int width { get; protected set; }
	public int height { get; protected set; }
	int[,] nei = new int[,]{ {-1, 0}, {1, 0}, {0, -1}, {0, 1}};

	protected ArrayList atLocation;

	public bool isFloor = false;
	public bool isShaft = false;

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


	public bool isValidNeighbor(Tile temp, Tile ths, bool left)
	{
		if (left) //left right connection, connect to all
			return (temp != null);
		bool a = (temp != null && !(temp.isFloor && this.isFloor));
		return a;
	}

	public Tile[] getNeighbors()
    {
        List<Tile> tiles = new List<Tile>();
		/*
		Tile temp = World.world.getTiles(x-1, y);
		if (temp != null)
			tiles.Add (temp);
		 temp = World.world.getTiles(x, y-1);
		if (temp != null && !(temp.isFloor && this.isFloor))
			tiles.Add (temp);
		 temp = World.world.getTiles(x+1, y);
		if (temp != null)
			tiles.Add (temp);
		 temp = World.world.getTiles(x, y+1);
		if (temp != null && !(temp.isFloor && this.isFloor))
			tiles.Add (temp);
		*/
		for(int i = 0; i < nei.Length/2; i++) 
        {
			Tile xxx = World.world.getTiles(nei[i,0]+x, nei[i,1]+y);
			if (!isValidNeighbor(xxx, this, (nei[i,0]!=0)))
                continue;
            tiles.Add(xxx);
        }
        
        return tiles.ToArray();
    }

    public override string ToString()
    {
		string a = "Tile: (" + x + " " + y + ")";
		foreach(System.Object e in atLocation)
        {
            if( e.GetType() == typeof(Customer) )
                a += "\n " + ((Customer)e) + "";
            if( e.GetType() == typeof(Elevator) )
                a += "\n " + ((Elevator)e) + "";
			if( e.GetType() == typeof(Floor))
                a += "\n " + ((Floor) e) + "";
        }
        return a;
    }

	public void addToTile( System.Object o)
	{
		if (o.GetType () == typeof(Floor) || o.GetType () == typeof(Shaft) )
		{
			//Debug.Log ("what the heck");
		}
		atLocation.Add (o);
	}

	public void addToTile(Floor f)
	{
		isFloor = true;
		atLocation.Add (f);
	}

	public void addToTile(Shaft s)
	{
		isShaft = true;
		atLocation.Add (s);
	}

	public void removeFromTile( string name)
	{
		if (name == "Floor")
		{
			foreach(System.Object o in atLocation)
			{
				if(o.GetType() == typeof(Floor))
				{
					atLocation.Remove (o);
					break;
				}
			}
		}
	}

	public void removeFromTile( System.Object o)
	{
		if (o.GetType () == typeof(Floor))
			isFloor = false;
		if (o.GetType () == typeof(Shaft))
			isShaft = false;
		
		atLocation.Remove (o);
	}
}  