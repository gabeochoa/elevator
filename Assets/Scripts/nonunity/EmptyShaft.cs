
using System;              
using System.Collections;  
using System.Collections.Generic;


public class EmptyShaft : Transportation
{
    public EmptyShaft() : base()
    {
        maxPeople = 0;
        baseSpeed = 0;
    }

	public override void update(float deltaTime){}
	public override void moveTo(float yval){}
	public override void userEntered(Customer c){}
	public override void userExited(Customer c){}
	public override void arrived(int floor){}
	public override void queue(int floor, bool direction){}

}
