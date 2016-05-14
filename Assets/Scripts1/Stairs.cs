
using System;              
using System.Collections;  
using System.Collections.Generic;

public class Stairs : Transportation
{
    public Stairs() : base()
    {
        maxPeople = -1;
        curPeople = 0;
        baseSpeed = 1;
    }
    override public void arrived()
    {
        if(arrivedCB != null)
        {
            arrivedCB(-1);
        }
    }
    override public void update(){}
}
