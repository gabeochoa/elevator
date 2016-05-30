 using System;              
using System.Collections;  
using System.Collections.Generic;
using Priority_Queue;


using UnityEngine;

public class Elevator : Transportation
{
    int MAX_PEOPLE = 4;
	List<Customer> onElevator;
    //int WAIT_TIME = 2;
    int currentWait;

    SimplePriorityQueue<int> buttonsPressed = new SimplePriorityQueue<int>();

	public Elevator(Tile t)
    {
		new List<Customer> ();
		tile = t;
		tile.addToTile (this);
        x = tile.x;
        y = tile.y;
        up = true;
        curFloor = 0;
        baseSpeed = MAX_SPEED;
        isMoving = true;
        numPeople = 0;
        maxPeople = MAX_PEOPLE;
        velocity = 0;
		destination = World.world.tiles [(int)x, 0];
    }

    public float brakeDist()
    {
		//d = vi*t + .5at^2
		//vf2 = vi2 + 2ad
		//velocity^2 = 2*BRAKE_SPD * D
        //d = (v^2) / (2a)
        return ((velocity*velocity) / (2 * BRAKE_SPD));
    }

    public override void update(float deltaTime)
    {
		if (destination == null)
		{
			//just go to the end of the direction we were going
            if(buttonsPressed.Count == 0)
                return;
            destination = World.world.tiles [(int)x, buttonsPressed.Dequeue()];
		}
		velocity = MathUtil.Clamp(velocity, -MAX_SPEED, MAX_SPEED);
        moveTo(y + (velocity*deltaTime));
        
        int posdiff = (int) (destination.y - y);
		int velSign = Math.Sign (velocity);
		int dirSign = Math.Sign (posdiff);
        if(posdiff != 0)
        {
			isMoving = true;
			//not done moving
			if(dirSign == velSign)
            {
				float mydist = brakeDist ();
				if(mydist*2f > Math.Abs(posdiff))
                {
                    // time to slowdown
					velocity -= dirSign * BRAKE_SPD * deltaTime;
                }
                else
                {
                    // we can gain speed
					velocity += dirSign * ACCEL_SPD * deltaTime;
                }
            }
            else if(velSign == 0)
            {
                //we should move please
				float acceleration = Math.Min(Math.Abs(posdiff), ACCEL_SPD);
				velocity += dirSign * acceleration * deltaTime;
            }
            else
            {
                //rip us : going wrong way
				velocity -= velSign * BRAKE_SPD * deltaTime;
				if(Math.Sign(velocity) != velSign) {
					velocity = 0;
				}
            }
        }
		//Debug.Log (isMoving + " " + posdiff +" " + velocity);
		if(isMoving && posdiff == 0 && Math.Abs(velocity) < BRAKE_SPD)
        {
			//Debug.Log ("inside");
			moveTo(destination.y); //TODO: if this is changed to just 'y' the elevator moves at the correct speed
            velocity = 0;
            isMoving = false;
            arrived(destination.y);//however this is then not accurate i think
        }
    }

    public override void moveTo(float yval)
    {
		y = yval;
        if(changeOccurred != null)
            changeOccurred();
    }

    public override bool userEntered(Customer c)
    {
		destination = c.target;
        numPeople++;
        if(changeOccurred != null)
            changeOccurred();
		return true;
    }

    public override void userExited(Customer c)
    {
        numPeople--;
		c.tile = destination;
        if(numPeople == 0)
            destination = null;
        if(changeOccurred != null)
            changeOccurred();
    }

    public override void arrived(int floor)
    {
		if(arrivedCB != null)
			arrivedCB (floor);
		//do whatever
		//Debug.Log("arrived at " + floor);
        //choose next destination

		//if (floor == World.world.HEIGHT - 1 || floor == 0)
		//	up = !up;
		//floor = !up ? floor + 1 : floor - 1;
		//destination = World.world.tiles[tile.x, floor];

		destination = null;
		tile.removeFromTile (this);
		tile = World.world.tiles [(int)x, (int)y];
		tile.addToTile (this);
    }

	public override bool queue(int floor, bool direction)
	{
        buttonsPressed.Enqueue(floor, Math.Abs(floor - y));
		if (destination == null)
		{	
            //Debug.LogError ("queue: " + floor);
        	//someone has called this elevator and wants to use it
			//TODO: handle multiple people, right now we will just go directly there
			destination = World.world.tiles [(int)x, (int)floor];
			return true;
		}
		return false;
	}
}




/*
public class Elevator_OLD : Transportation_OLD
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
	
	public Elevator_OLD() : base()
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
*/
















