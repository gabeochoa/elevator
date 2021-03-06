using System;              
using System.Collections;  
using System.Collections.Generic;

using UnityEngine;

public class Shaft                                                                                                                         
{                                                                                                                                            
               
    public enum ShaftType{EMPTY, STAIR, ELEVATOR};
    public Shaft.ShaftType type {get; protected set;}     
    public Transportation transport;  

    public Shaft(ShaftType ty, Transportation tr)
    {
        type = ty;
        transport = tr;
    }

    public void update(float deltaTime)
    {
        transport.update(deltaTime);
    }
	
    public static Shaft createEmptyShaft(Tile t)
    {
        Shaft s = new Shaft(ShaftType.EMPTY, new EmptyShaft());
        s.transport.setTile(t);
        return s;
    }    
    public static Shaft createElevatorShaft(Tile t)
    {
        Shaft s = new Shaft(ShaftType.ELEVATOR, new Elevator(t));
        return s;
    }

	public bool CallWaiting(Customer cust, int floor, bool direction)
	{
		//Debug.LogError ("callwaiting " + floor);
		//TODO: tell elevator that someone wants to get on
		return transport.queue (floor, direction);
	}
	public bool getOnTransport(Customer cust)
	{
		return transport.userEntered(cust);
	}
	public void getOffTransport(Customer cust)
	{
		transport.userExited(cust);
	}
}  