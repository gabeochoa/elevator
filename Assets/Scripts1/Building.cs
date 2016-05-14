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
			Floor a = Floor.createEmptyFloor ();
			World.world.tiles [1, i].atLocation.Add (a);
			a.tile = World.world.tiles [1, i];
			floors.Add (a);
		}

        shafts = new List<Shaft>();
        Shaft sa = Shaft.createEmptyShaft();
        Shaft sb = Shaft.createElevatorShaft();

        World.world.tiles[0,0].atLocation.Add(sa.transport);
        World.world.tiles[2,0].atLocation.Add(sb.transport);
        sa.transport.tile = World.world.tiles[0,0];
        sb.transport.tile = World.world.tiles[2,0];

        shafts.Add(sa);
        shafts.Add(sb);
        
        customers = new List<Customer>();
        customers.Add(generateRandomCust());
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
        World.world.tiles[0,0].atLocation.Add(c);
        c.tile =  World.world.tiles[0,0];
        return c;
    }  

}  