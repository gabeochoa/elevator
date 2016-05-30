using System;              
using System.Collections;  
using System.Collections.Generic;

using UnityEngine;

public class Building                                                                                                                         
{                                                                                                                                            
    //Action onGameOver;

    public List<Shaft> shafts {get; protected set;}
    public List<Floor> floors;
    public List<Customer> customers;

    int happinessLevel; // 
    int maxPeople;

    public Building()
    {
		floors = new List<Floor>();

		for (int i = 0; i < World.world.HEIGHT; i++) 
		{
			addFloor (i);
		}

        shafts = new List<Shaft>();
		addShaft (0, Shaft.ShaftType.EMPTY);
		addShaft (2, Shaft.ShaftType.ELEVATOR);
		addShaft (World.world.WIDTH-1, Shaft.ShaftType.ELEVATOR);
        
        customers = new List<Customer>();
        customers.Add(generateRandomCust());
    }

	public void addFloor(int height)
	{
		Floor a = Floor.createEmptyFloor ();
		for (int j = 0; j < World.world.WIDTH; j++)
		{
			Tile t = World.world.tiles [j, height];
			if (t.isShaft)
				continue;
			t.addToTile (a);
		}
		floors.Add (a);
	}

	public void addShaft(int width, Shaft.ShaftType satype)
	{
		Tile t = World.world.tiles [width, 0];
        Shaft sa = null;
        switch(satype)
        {
            case Shaft.ShaftType.EMPTY:
                sa = Shaft.createEmptyShaft(t);
            break;
            case Shaft.ShaftType.ELEVATOR:
                sa = Shaft.createElevatorShaft(t);
            break;
            default:
                sa = Shaft.createEmptyShaft(t);
            break;
        }
        t.addToTile (sa.transport);
		shafts.Add(sa);

		for (int i = 0; i < World.world.HEIGHT; i++)
		{
			t = World.world.tiles [width, i];
			t.addToTile(sa);
			t.isFloor = false;
			t.isShaft = true;
			t.removeFromTile ("Floor");
		}
	}

    public void update(float deltaTime)
    {
        foreach(Shaft s in shafts)
        {
            s.update(deltaTime);
        }
        foreach(Customer c in customers)
		{
			Debug.Log (c);
			c.update(deltaTime);
		}

    }   

    public Customer generateRandomCust()
    {
		System.Random rnd = new System.Random();
        int month = rnd.Next(1, floors.Count-1) + 1;
        Customer c = new Customer(World.world.tiles[1, month], 10f);
		World.world.tiles[0,0].addToTile(c);
        c.tile =  World.world.tiles[0,0];
        return c;
    }  

}  