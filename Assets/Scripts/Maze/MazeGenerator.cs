    using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MazeGenerator
{
    public int width = 18;
    public int height = 10; 
    public GeneratedCell[,] Generate()
    {
        GeneratedCell[,] maze = new GeneratedCell[width,height];
        for(int x = 0; x<width;x++)
        {
            for(int y = 0; y<height;y++)
            {
                maze[x,y] = new GeneratedCell{x=x, y=y};
            }
        }

        for(int x = 0; x<width;x++){
            maze[x,height-1].wallLeft = false;
        }
        for(int y = 0; y<height;y++){
            maze[width-1,y].wallBottom = false;
        }

        
        RemoveWallsOnPath(maze);
        PlaceExit(maze);
        
        return maze;
    }

    void RemoveWallsOnPath(GeneratedCell[,] maze){
        GeneratedCell current = maze[0,0];
        current.wallBottom=false;
        current.visited = true;

        Stack<GeneratedCell> stack = new Stack<GeneratedCell>();
        do{
            List<GeneratedCell> unvisitedNeighbours = new List<GeneratedCell>();
            int x= current.x;
            int y = current.y;
            if(x>0 && !maze[x-1,y].visited) unvisitedNeighbours.Add(maze[x-1,y]);
            if(y>0 && !maze[x,y-1].visited) unvisitedNeighbours.Add(maze[x,y-1]);
            if(x<width-2 && !maze[x+1,y].visited) unvisitedNeighbours.Add(maze[x+1,y]);
            if(y<height-2 && !maze[x,y+1].visited) unvisitedNeighbours.Add(maze[x,y+1]);

            if(unvisitedNeighbours.Count>0){
                GeneratedCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0,unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.visited = true;
                stack.Push(chosen);
                current = chosen;
                current.distanceFromStart = stack.Count;
            }
            else{
                current = stack.Pop();
            }

        }while(stack.Count>0);
    }

    void RemoveWall(GeneratedCell a, GeneratedCell b){
        if(a.x==b.x){
            if(a.y>b.y) a.wallBottom = false;
            else b.wallBottom = false;
        }else{
            if(a.x>b.x) a.wallLeft = false;
            else b.wallLeft = false; 
        }
    }

    void PlaceExit(GeneratedCell[,] maze){
        GeneratedCell furthest = maze[0, 0];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, height - 2].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, height - 2];
            if (maze[x, 0].distanceFromStart > furthest.distanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[width - 2, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[width - 2, y];
            if (maze[0, y].distanceFromStart > furthest.distanceFromStart) furthest = maze[0, y];
        }

        if (furthest.x == 0) furthest.wallLeft = false;
        else if (furthest.y == 0) furthest.wallBottom = false;
        else if (furthest.x == width - 2) maze[furthest.x + 1, furthest.y].wallLeft = false;
        else if (furthest.y == height - 2) maze[furthest.x, furthest.y + 1].wallBottom = false;
    }
}


