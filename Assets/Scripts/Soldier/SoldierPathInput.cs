using UnityEngine;

public class SoldierPathInput : MonoBehaviour
{
    public Point FromPoint { get; set; }
    public Point ToPoint { get; set; }

    private SoldierMove soldierMove;
    private SoldierPath soldierPath;
    private SoldierTileInteraction soldierTileInteraction;
    private void Awake()
    {
        soldierMove = GetComponent<SoldierMove>();
        soldierPath = GetComponent<SoldierPath>();
        soldierTileInteraction = GetComponent<SoldierTileInteraction>();
    }

    private void Update()
    {
        //When clicked mouse left button
        if (Input.GetMouseButtonDown(0))
        {
            //Create Ray
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            
            //Hit Control
            if (!hit.collider) return;

            //Is it soldier?
            if (!hit.collider.GetComponent<Soldier>()) return;
           
            //Set current position
            FromPoint = soldierTileInteraction.SoldierPoint;
           
        }

        //When clicked mouse right button
        if (Input.GetMouseButtonDown(1))
        {
            //Create Ray
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //Hit Control
            if (!hit.collider) return;

            //Is it Soldier?
            if (!hit.collider.GetComponent<TileScript>()) return;
            
            //Access Tile Script
            var tile = hit.collider.GetComponent<TileScript>();

            //Is tile empty and walkable?
            if(tile.IsEmpty && tile.Walkable) ToPoint = tile.GridPosition;

            soldierPath.SetPath(soldierPath.Path);

            soldierMove.IsActive = true;
            
        }
    }
}
