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
    public PathFind path;
    Tile nextTile;
    float movement = 0;
	public string name = "";
	bool hasCalled = false;
	bool justgotoff = false;

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
	
	public void randomNextTarget()
	{
		System.Random rnd = new System.Random ();
		int month = rnd.Next (1, World.world.building.floors.Count - 1) + 1;
		int month2 = rnd.Next (1, World.world.building.floors.Count - 1) + 1;
		target = World.world.tiles [month2, month]; //set target 
		path = null; //invalidate any paths
		nextTile = tile; //stay in spot
	}

	public void updateMovement(float deltaTime, float x, float y)
    {
		float dis = (float)Math.Sqrt (Math.Pow (tile.x - x, 2) + Math.Pow (tile.y - y, 2));
		float distFrame = 1 / deltaTime;//should be divided by deltaTime;
		float perc = distFrame / dis;
		movement += perc;
		
		//if we have moved more than one tile this turn, then we are in the next tile
		if (movement >= 1)
		{
			tile = World.world.tiles[(int)x, (int)y];
			movement = 0;
		}
    }
	public void update(float deltaTime)
	{
		if (curTransport == null)
		{
			if (target == tile)
			{
				//if we hit our tile, generate a new random one to go to
				randomNextTarget();
				return;
			}
			//if we dont have a nother tile to goto, or we are at the tile we want to be
			if (nextTile == null || nextTile == tile)
			{
				//if we dont have a pathfinding tile yet
				if (path == null)
				{
					//generate a path from us to target
					path = new PathFind (World.world, tile, target);
					if (path.Length () == 0)
					{
						Console.WriteLine ("Customer: Path not viable");
						path = null;
						return;
						//path not possible
					}
				}
				//if our path is not null, or we have just generated a non null one, get the next tile. 
				nextTile = path.next ();
				if (nextTile == null)
				{
					nextTile = tile;
				}
			}

			//if our next tile is not a shaft/elevator
			if (justgotoff || !nextTile.isShaft)
			{
				justgotoff = false;
				//get the distance to the next tile (should be 1)
				//figure out how many frames itll take to get there
				updateMovement(deltaTime, nextTile.x, nextTile.y);

			} else
			{
				Shaft shft = nextTile.getShaft ();
				//We need to wait for an elevator or whatever
				if (nextTile.hasElevator ())
				{
					//get in
					//Debug.LogError("ELEVATOR Here");
					if (shft.getOnTransport (this))
					{
						//Debug.Log("we are on elevator");
						curTransport = shft.transport;
						curTransport.RegisterArrivedCallback (elevatorArrived);
					}
				} else if (!hasCalled) //to prevent spamming the button
				{
					//tell elevator to come get us
					hasCalled = shft.CallWaiting (this, nextTile.y, (this.tile.y - nextTile.y) == 1);
				} else
				{
				}
			}
		} else
		{
			//we have an elevator
			updateMovement(deltaTime, curTransport.x, curTransport.y);
		}
	}


    public void reset(int floors)
    {
		randomNextTarget ();
        maxWaitingTime = 10f;
        curFloor = 0;
    }

	public void elevatorArrived(int floor)
    {
        curFloor = floor;
		if (curFloor == target.y)
		{
			tile = World.world.tiles[(int)curTransport.x, (int)target.y];
			nextTile = World.world.tiles[(int)curTransport.x, (int)target.y];
			curTransport.UnregisterArrivedCallback (elevatorArrived);
			curTransport = null;
			justgotoff = true;
		}
	}


    public override string ToString()
    {
        return "cust: curFL: "+curFloor + " target: "+ target + " ("+tile.x + " " + tile.y +")";
    }

}  