
using System;              
using System.Collections;  
using System.Collections.Generic;


public class EmptyShaft : Transportation
{
    public EmptyShaft() : base()
    {
        maxPeople = 0;
        curPeople = 0;
        baseSpeed = 0;
    }

    override public void arrived(){}
    override public void update(float deltaTime){}
}
