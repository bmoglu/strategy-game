using System;
using System.Collections.Generic;
using System.Linq;

public static class AStar
{
    private static Dictionary<Point, Node> nodes;

    //Create Nodes
    private static void CreateNodes()
    {
        nodes = new Dictionary<Point, Node>();

        foreach (TileScript tile in LevelManager.Instance.Tiles.Values)
        {
            nodes.Add(tile.GridPosition, new Node(tile));
        }
    }

    public static Stack<Node> GetPath(Point start, Point goal)
    {
        if (nodes == null)
        {
            CreateNodes();
        }

        //Create an open list to be used with the A* algorithm
        HashSet<Node> openList = new HashSet<Node>();

        //Create an closed list to be used with the A* algorithm
        HashSet<Node> closedList = new HashSet<Node>();

        Stack<Node> finalPath = new Stack<Node>();
    
        //Finds the start node and creates a reference to it called current node
        Node currentNode = nodes[start];

        //1.Add the start node to the openList
        openList.Add(currentNode);

        while (openList.Count > 0)
        {
            //2.Runs through all neighbors
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    Point neighborPos = new Point(currentNode.GridPosition.X - x, currentNode.GridPosition.Y - y);

                    if (LevelManager.Instance.InBounds(neighborPos) && LevelManager.Instance.Tiles[neighborPos].Walkable && neighborPos != currentNode.GridPosition)
                    {
                        var gCost = 0;

                        //[14] [10] [14]
                        //[10] [XX] [10]
                        //[14] [10] [14]
                        //
                        if (Math.Abs(x - y) == 1) // straight movement = 10
                        {
                            gCost = 10;
                        }
                        else //Diagonal movement = 14 
                        {
                            if (!ConnectedDiagonally(currentNode,nodes[neighborPos]))
                            {
                                continue;
                            }

                            gCost = 14;
                        }

                        //3.Adds the neighbor to the openList
                        Node neighbor = nodes[neighborPos];


                        if (openList.Contains(neighbor))
                        {
                            if (currentNode.G + gCost < neighbor.G)
                            {
                                neighbor.CalculateValues(currentNode, nodes[goal], gCost);
                            }
                        }
                        else if (!closedList.Contains(neighbor))
                        {
                            openList.Add(neighbor);
                            neighbor.CalculateValues(currentNode, nodes[goal], gCost);
                        }
                    }
                }
            }

            //5.Moves the current node from the open list to the closed list
            openList.Remove(currentNode);
            closedList.Add(currentNode);

            if (openList.Count > 0)
            {
                //Sorts list by f value then take first element
                currentNode = openList.OrderBy(n => n.F).First();
            }

            //Found the goal point
            if (currentNode == nodes[goal])
            {
                while (currentNode.GridPosition != start)
                {
                    finalPath.Push(currentNode);
                    currentNode = currentNode.Parent;
                }
                
                break;
            }
        }

        return finalPath;
    }

    //Can soldier walk diagonally the tile?
    private static bool ConnectedDiagonally(Node currentNode,Node neighbor)
    {
        var direction = neighbor.GridPosition - currentNode.GridPosition;

        var first = new Point(currentNode.GridPosition.X + direction.X, currentNode.GridPosition.Y);

        var second = new Point(currentNode.GridPosition.X, currentNode.GridPosition.Y + direction.Y);

        if (LevelManager.Instance.InBounds(first) && !LevelManager.Instance.Tiles[first].Walkable)
        {
            return false;
        }

        if (LevelManager.Instance.InBounds(second) && !LevelManager.Instance.Tiles[second].Walkable)
        {
            return false;
        }

        return true;
    }
}
