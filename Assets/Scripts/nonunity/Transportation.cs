using System;              
using System.Collections;  
using System.Collections.Generic;

public abstract class Transportation                                                                                                                         
{                          
    protected int maxPeople;//-1 is infinite, 0 is empty
    protected int curPeople;
    public int curFloor; //-1 for stairs, 0 for empty?
    protected int baseSpeed;//floors per second
    protected float loadDelay;//embark / disembark delay
    public bool canLoad = false;
    protected Tile tile;
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

public abstract class Transportation_new                                                                                                                        
{                          
    protected bool up = true; //false is down
    protected int curFloor; //-1 for stairs, 0 for empty?

    //TODO: Handle slowing/speeding up
    //int maxSpeed;
    //float accelSpeed;
    //float brakeSpeed;
    protected int baseSpeed;//floors per second
    protected bool isMoving = false;

    protected int numPeople;
    protected int maxPeople;//-1 for stairs, 0 for empty?

    Tile destination;

    Action changeOccurred;
    public void changeRegisterCallback(Action cb)
    {
        changeOccurred += cb;
    }
    public void changeUnregisterCallback(Action cb)
    {
        changeOccurred -= cb;
    }

    //not sure if we need this for stairs
    public abstract void moveTo(Tile t);

    public abstract void update(float deltaTime);
    public abstract void userEntered(Customer c);
    public abstract void userExited(Customer c);
    public abstract void arrived(int floor);




}  
















