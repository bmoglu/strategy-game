using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierPath : MonoBehaviour
{
    private Stack<Node> path;

    public Stack<Node> Path
    {
        get
        {
            if (path == null)
            {
                GeneratePath();
            }
            return new Stack<Node>(new Stack<Node>(path));
        }
    }

    public bool OneTime { get; set; }
    
    private Soldier soldier;
    private SoldierMove soldierMove;
    private SoldierPathInput soldierPathInput;

    private void Awake()
    {
        soldier = GetComponent<Soldier>();
        soldierMove = GetComponent<SoldierMove>();
        soldierPathInput = GetComponent<SoldierPathInput>();
    }


    //For path finding A* algorithm
    public void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;

            soldier.GridPosition = path.Peek().GridPosition;
            soldierMove.Destination = path.Pop().WorldPosition;
        }
    }

    //Generate path for soldier move
    public void GeneratePath()
    {
        //first position and last position for path
        path = AStar.GetPath(soldierPathInput.FromPoint, soldierPathInput.ToPoint);
    }
}
