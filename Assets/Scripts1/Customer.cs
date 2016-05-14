using System;              
using System.Collections;  
using System.Collections.Generic;

public class Customer                                                                                                                         
{      
	public Tile target{ get; protected set; }
    float maxWaitingTime;
    int curFloor;
    public Tile tile;
    public bool ontransport;
    Transportation curTransport;
    PathFind path;
    Tile nextTile;
    float movement = 0;
	public string name = "";

    float lerp(float v0, float v1, float t) {
      return (1-t)*v0 + t*v1;
    }

    public float x {
        get
        {
            return lerp(tile.x, nextTile.x, movement);
        } 
    }

    public float y {
        get
        {
            return lerp(tile.y, nextTile.y, movement);
        } 
    }

    public Customer(Tile targ, float max)
    {
        target = nextTile = targ;
        maxWaitingTime = max;
        curFloor = 0;
        curTransport = null;
    }

    public void updateMovement()
    {
		if (target == tile) 
		{
			Random rnd = new Random();
			int month = rnd.Next(1, World.world.building.floors.Count-1) + 1;
			int month2 = rnd.Next(1, World.world.building.floors.Count-1) + 1;
			target = World.world.tiles [month2, month];
			path = null;
			nextTile = tile;
			return;
		}
        if(nextTile == null || nextTile == tile)
        {
            if(path == null)
            {
                path = new PathFind(World.world, tile, target);
                if(path.Length() == 0)
                {
                    Console.WriteLine("Customer: Path not viable");
                    path = null;
                    return;
                }
            }
            nextTile = path.next();
			if (nextTile == null) 
			{
				nextTile = tile;
			}
        }
        
        float dis = (float) Math.Sqrt(Math.Pow(tile.x - nextTile.x, 2) + Math.Pow(tile.y - nextTile.y, 2));
        float distFrame = 1/1;//should be divided by deltaTime;
        float perc = distFrame / dis;
        movement += perc;

        if(movement >= 1)
        {
            tile = nextTile;
            movement = 0;
        }
/*
        if(curTransport == null )
        {
            //search
            foreach (Shaft s in World.world.building.shafts)//shafts for building im in;
            {
                if(s.type == Shaft.ShaftType.ELEVATOR)
                {
                    //Get on elevator
                    if(s.transport.curFloor == curFloor)
                    {
                        Console.WriteLine("get on");
                        curTransport = s.transport;
                    }
                }
            }
        }
        else
        {
            //move up / down based on whatever
            curFloor = curTransport.curFloor;
        }
        }
        else if(curTransport != null)
        {
            //we have reached our current floor
            curTransport = null;
            Console.WriteLine("get off");
        }
        */

    }
    public void update()
    {
        updateMovement();
        Console.WriteLine("cust: curFL: "+curFloor + " target: "+ target + " ("+x + " " + y +")");
    }

    public void reset(int floors)
    {
        Random rnd = new Random();
        int month = rnd.Next(1, floors);
        target = World.world.tiles[1, month];
        maxWaitingTime = 10f;
        curFloor = 0;
    }

    public void elevatorArrived(int floor)
    {
        //if elevator floor type == target
        //get off elevator
        //else
        //update our current floor
        curFloor = floor;
    }


    public override string ToString()
    {
        return "cust: curFL: "+curFloor + " target: "+ target + " ("+tile.x + " " + tile.y +")";
    }

}  