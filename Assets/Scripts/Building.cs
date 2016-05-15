using System;              
using System.Collections;  
using System.Collections.Generic;

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
		addShaft (0, Shaft.createEmptyShaft());
		addShaft (World.world.WIDTH-1, Shaft.createElevatorShaft());
        
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

	public void addShaft(int width, Shaft sa)
	{
		Tile t = World.world.tiles [0, 0];
		t.addToTile (sa.transport);
		sa.transport.tile = t;
		shafts.Add(sa);

		for (int i = 0; i < World.world.HEIGHT; i++)
		{
			t = World.world.tiles [width, i];
			t.isFloor = false;
			t.isShaft = true;
			t.removeFromTile ("Floor");
		}
	}

    public void update()
    {
        foreach(Shaft s in shafts)
        {
            s.update();
        }
        foreach(Customer c in customers)
        {
            c.update();
        }
    }   

    public Customer generateRandomCust()
    {
        Random rnd = new Random();
        int month = rnd.Next(1, floors.Count-1) + 1;
        Customer c = new Customer(World.world.tiles[1, month], 10f);
		World.world.tiles[0,0].addToTile(c);
        c.tile =  World.world.tiles[0,0];
        return c;
    }  

}  