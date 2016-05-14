using System;              
using System.Collections;  
using System.Collections.Generic;

public class World                                                                                                                         
{                      
    public static World world;

    public Tile[,] tiles;  
    public int WIDTH = 40;
    public int HEIGHT = 40;                                                                                                                    
    //List<Building> buildings;
    public Building building {get; protected set;}//TODO: add support for multiple buildings, added Micro skillz
    
    public Graph graph;
    
	public World(int w, int h)
    {
		WIDTH = w;
		HEIGHT = h;
        Console.WriteLine("In world constructor");
        World.world = this;
        tiles = new Tile[WIDTH, HEIGHT];
        for(int i=0; i< WIDTH; i++)
        {
            for(int j=0; j <HEIGHT; j++)
            {
                tiles[i,j] = new Tile(i,j);
            }
        }

        building = new Building();
        graph = new Graph(this);
    }

    public void update()
    {
        Console.WriteLine("update");
          /*  
        for(int i=0; i< WIDTH; i++)
        {
            for(int j=0; j <HEIGHT; j++)
            {
                Console.WriteLine(tiles[i,j]);
            }
        }
        */
        building.update();
    }

    public Tile getTiles(int x, int y)
    {
        if( x < 0 || x >= WIDTH || y < 0 || y >= HEIGHT)
        {
            return null;
        }
        return tiles[x,y];
    }

}  









