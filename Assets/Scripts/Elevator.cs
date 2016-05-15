using System;              
using System.Collections;  
using System.Collections.Generic;


public class Elevator : Transportation
{
    protected int index;
    protected int[] floors = {0, 1, 2, 3}; //floors it can reach
    public bool up = false;

    public Elevator() : base()
    {
        maxPeople = 11;
        curPeople = 0;
        baseSpeed = 3;
        index = 0;
    }

    override public void arrived()
    {       
        if(arrivedCB != null)
        {
            arrivedCB(curFloor);
        }
    }
    
    override public void update()
    {
        if(index > floors.Length-2 || index <= 0)
        {
            up = !up;
        }
        
        index = up? index+1: index-1;
    
        curFloor = floors[index];
        Console.WriteLine("elevator: " + curFloor);
		tile.removeFromTile(this);
        tile = World.world.tiles[tile.x, curFloor];
		tile.addToTile(this);
    }

    public int[] getFloors()
    {
        return floors;
    }

    public override string ToString()
    {
        return "elevator";
    }

}
