using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierMove : MonoBehaviour
{
    public bool IsActive { get; set; }

    public Vector3 Destination { get; set; }

    private Soldier soldier;
    private SoldierPath soldierPath;

    private void Awake()
    {
        soldier = GetComponent<Soldier>();
        soldierPath = GetComponent<SoldierPath>();
    }

    private void Update()
    {
        //For moving action
        Move();
    }

    //Soldier movement
    private void Move()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, Destination, soldier.Speed * Time.deltaTime);

            if (transform.position == Destination)
            {
                if (soldierPath.Path != null && soldierPath.Path.Count > 0)
                {
                    soldier.GridPosition = soldierPath.Path.Peek().GridPosition;
                    Destination = soldierPath.Path.Pop().WorldPosition;
                }
            }
        }
    }


}
