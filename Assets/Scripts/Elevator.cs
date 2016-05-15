using System;              
using System.Collections;  
using System.Collections.Generic;


public class Elevator : Transportation
{
    protected int index;
    protected List<int> floors; //floors it can reach
    public bool up = false;
	float updateTime = 1; 
	Tile nextTile;

	float lerp(float v0, float v1, float t) {
		return (1-t)*v0 + t*v1;
	}
	
	public float x {
		get
		{
			return lerp(tile.x, nextTile.x, updateTime);
		} 
	}
	
	public float y {
		get
		{
			return lerp(tile.y, nextTile.y, updateTime);
		} 
	}

    public Elevator() : base()
    {
        maxPeople = 11;
        curPeople = 0;
        baseSpeed = 3;
        index = 0;
		nextTile = tile;

		floors = new List<int> ();
		for (int i=0; i<World.world.HEIGHT; i++)
		{
			floors.Add(i);
		}
    }

    override public void arrived()
    {       
        if(arrivedCB != null)
        {
            arrivedCB(curFloor);
        }
    }
    
    override public void update(float deltaTime)
	{
		updateTime -= baseSpeed * deltaTime;
		if(updateTime < 0)
		{
			if (index > floors.Count - 2 || index <= 0)
			{
				up = !up;
			}
			index = up ? index + 1 : index - 1;
			curFloor = floors [index];
			tile.removeFromTile (this);
			tile = World.world.tiles[tile.x, curFloor];
			tile.addToTile(this);
			updateTime = 1f;
			//Calculate next spot
			{
				bool upp = up;
				if (index > floors.Count - 2 || index <= 0)
				{
					upp = !upp;
				}
				int index2 = upp ? index + 1 : index - 1;
				int curFloor2 = floors [index2];
				nextTile = World.world.tiles[tile.x, curFloor2];
			}
		}
		Console.WriteLine("elevator: " + curFloor);
    }

    public int[] getFloors()
    {
        return floors.ToArray();
    }

    public override string ToString()
    {
        return "elevator";
    }

}
