using System;
using UnityEngine;

public class Node 
{
    //The nodes grid position
    public Point GridPosition { get; private set; }

    //A reference to the tile that this node belongs to
    public TileScript TileRef { get; private set; }

    public Vector2 WorldPosition { get; set; }

    //Reference to the nodes parent
    public Node Parent { get; private set; }

    //G weight
    public int G { get; set; }
    //H weight
    public int H { get; set; }
    //F weight
    public int F { get; set; }

    //Node Constructor
    public Node(TileScript tileRef)
    {
        this.TileRef = tileRef;
        this.GridPosition = tileRef.GridPosition;
        this.WorldPosition = tileRef.WorldPosition;
    }
    
    //Calculate the Node weight
    public void CalculateValues(Node parent, Node goal, int gCost)
    {
        this.Parent = parent;
        this.G = parent.G + gCost;
        this.H = Math.Abs(GridPosition.X - goal.GridPosition.X) + Math.Abs(goal.GridPosition.Y - GridPosition.Y) * 10;
        this.F = G + H;
    }
}
