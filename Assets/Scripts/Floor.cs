using System;              
using System.Collections;  
using System.Collections.Generic;

public class Floor                                                                                                                         
{                                                                                                                                            
    public enum FloorType{EMPTY, RESIDENTIAL, STOREFRONT};
    Floor.FloorType type;
    int curPop;
    int maxPop;//empty=0    
    public Tile tile;
    
    public Floor(FloorType ty, int max)
    {
        type = ty;
        maxPop = max;
    }

    public static Floor createEmptyFloor()
    {
        Floor f = new Floor(FloorType.EMPTY, 5);
        return f;
    }   

    public static FloorType getRandomType()
    {
        Random rnd = new Random();
        int month = rnd.Next(1, 3);
        switch(month)
        {
            case 1:
            //    return FloorType.EMPTY;
            case 2:
                return FloorType.RESIDENTIAL;
            case 3:
                return FloorType.STOREFRONT;
        }
        return FloorType.EMPTY;
    }                                                                                                                                
}  