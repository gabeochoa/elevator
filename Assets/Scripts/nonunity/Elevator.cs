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
	bool isAtFloor;

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
		loadDelay = 2f;
		isAtFloor = false;

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
		if (!isAtFloor)
		{
			updateTime -= baseSpeed * deltaTime;
			if (updateTime < 0)
			{
				if (index > floors.Count - 2 || index <= 0)
				{
					up = !up;
				}
				index = up ? index + 1 : index - 1;
				curFloor = floors [index];
				tile.removeFromTile (this);
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
					nextTile = World.world.tiles [tile.x, curFloor2];
				}
				isAtFloor = true;
			}
		} 
		else
		{
			//allow people to get on
			loadDelay -= deltaTime;
			if (loadDelay <= 0)
			{
				loadDelay = 2f;
				isAtFloor = false;
			}
			tile = World.world.tiles [tile.x, curFloor];
			tile.addToTile (this);
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

public class Elevator_new : Transportation_new
{
    int MAX_PEOPLE = 4;
    int WAIT_TIME = 2;
    int currentWait;

    public Elevator_new()
    {
        x = tile.x;
        y = tile.y;
        up = true;
        curFloor = 0;
        baseSpeed = MAX_SPEED;
        isMoving = false;
        numPeople = 0;
        maxPeople = MAX_PEOPLE;
        velocity = 0;

        destination = World.world.building.floors[0].tile;
    }

    public float brakeDist()
    {
        //d = (v^2) / (2a)
        return ((velocity*velocity) / (2 * BRAKE_SPD));
    }

    public override void update(float deltaTime)
    {
        //hmm
        velocity = MathUtil.Clamp(velocity, 0, MAX_SPEED);
        moveTo(y + (velocity*deltaTime));
        int posdiff = (int) (destination.y - y);
        if(posdiff != 0)
        {
            //not done moving
            if(Math.Sign(posdiff) == Math.Sign(velocity))
            {
                if(brakeDist() <= Math.Abs(posdiff))
                {
                    //welp we cant stop in time, try our best
                    velocity -= Math.Abs(posdiff) * BRAKE_SPD * deltaTime;
                }
                else if(brakeDist()*2 <= Math.Abs(posdiff))
                {
                    // time to slowdown
                    velocity -= Math.Abs(posdiff) * BRAKE_SPD * deltaTime;
                }
                else
                {
                    // we can gain speed
                    velocity += Math.Abs(posdiff) * ACCEL_SPD * deltaTime;
                }
            }
            else if(Math.Sign(velocity) == 0)
            {
                //we should move please
                velocity += Math.Sign(posdiff) * ACCEL_SPD * deltaTime;
            }
            else
            {
                //rip us : going wrong way
                velocity -= Math.Sign(velocity) * BRAKE_SPD * deltaTime;
            }
        }

        if(isMoving && posdiff == 0 && velocity < 1)
        {
            moveTo(destination.y);
            velocity = 0;
            isMoving = false;
            arrived(destination.y);
        }

    }

    public override void moveTo(float yval)
    {
        y = yval;
        if(changeOccurred != null)
            changeOccurred();
    }

    public override void userEntered(Customer c)
    {

        if(changeOccurred != null)
            changeOccurred();
    }

    public override void userExited(Customer c)
    {

        if(changeOccurred != null)
            changeOccurred();
    }

    public override void arrived(int floor)
    {

    }
}





















