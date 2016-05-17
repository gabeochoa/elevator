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

    public Tile tile;
	public string name;
    protected Action<int> arrivedCB;

    public abstract void arrived();
    public abstract void update(float deltaTime);

    public void registerArrivedCB(Action<int> reg)
    {
        arrivedCB += reg;
    }
    public void unregisterArrivedCB(Action<int> reg)
    {
        arrivedCB -= reg;
    }

    public override string ToString()
    {
        return "transportation";
    }


}  
