using System;              
using System.Collections;  
using System.Collections.Generic;

public abstract class Transportation                                                                                                                       
{                          
    protected bool up = true; //false is down
    protected int curFloor; //-1 for stairs, 0 for empty?

    //TODO: dynamic accel?
    protected int MAX_SPEED = 1;
    protected float ACCEL_SPD = 0.2f;
    protected float BRAKE_SPD = 0.5f;

    protected int baseSpeed;//floors per second
    protected bool isMoving = false;

    protected int numPeople;
    protected int maxPeople;//-1 for stairs, 0 for empty?

    protected float velocity;

    public float x{get; protected set;}
    public float y{get; protected set;}
	protected Tile tile;
    protected Tile destination;
	public string name;

    protected Action changeOccurred;
    public void changeRegisterCallback(Action cb)
    {
        changeOccurred += cb;
    }
    public void changeUnregisterCallback(Action cb)
    {
        changeOccurred -= cb;
    }

    //not sure if we need this for stairs
	public abstract void moveTo(float yval);
	public abstract bool queue (int floor, bool direction);

    public abstract void update(float deltaTime);
    public abstract bool userEntered(Customer c);
    public abstract void userExited(Customer c);
    public abstract void arrived(int floor);
	public void setTile(Tile t)
	{
		tile = t;
	}


	protected Action<int> arrivedCB;
	public void RegisterArrivedCallback(Action<int> func)
	{
		arrivedCB += func;
	}	

	public void UnregisterArrivedCallback(Action<int> func)
	{
		arrivedCB -= func;
	}
}  


/*
public abstract class Transportation_OLD                                                                                                                        
{                          
	protected int maxPeople;//-1 is infinite, 0 is empty
	protected int curPeople;
	public int curFloor; //-1 for stairs, 0 for empty?
	protected int baseSpeed;//floors per second
	protected float loadDelay;//embark / disembark delay
	public bool canLoad = false;
	public Tile tile{get; protected set;}
	public string name;
	protected Action<int> arrivedCB;
	public abstract void arrived();
	public abstract void update(float deltaTime);
	public override string ToString()
	{
		return "transportation";
	}
	
	public void setTile(Tile t)
	{
		tile = t;
	}
	
}  
*/












