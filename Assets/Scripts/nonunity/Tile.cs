using System;              
using System.Collections;  
using System.Collections.Generic;

public class Tile                                                                                                                         
{                
    public int x {get; protected set;}
	public int y {get; protected set;}
	public int width { get; protected set; }
	public int height { get; protected set; }

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

    public Tile[] getNeighbors()
    {
        List<Tile> tiles = new List<Tile>();

		Tile xxx = World.world.getTiles(x-1, y);
		if (xxx != null)
			tiles.Add (xxx);
		 xxx = World.world.getTiles(x, y-1);
		if (xxx != null && !(xxx.isFloor && this.isFloor))
			tiles.Add (xxx);
		 xxx = World.world.getTiles(x+1, y);
		if (xxx != null)
			tiles.Add (xxx);
		 xxx = World.world.getTiles(x, y+1);
		if (xxx != null && !(xxx.isFloor && this.isFloor))
			tiles.Add (xxx);
		/*
        for(int i = -1; i <= 1; i++) 
        {
            for(int j = -1; j <= 1; j++) 
            {
				if(i != 0 || j != 0) 
                {
                    Tile xxx = World.world.getTiles(i+x, j+y);
					if (xxx == null)//|| (isFloor && xxx.isFloor) )
                        continue;
                    tiles.Add(xxx);
                }
            }
           
        }
         */
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