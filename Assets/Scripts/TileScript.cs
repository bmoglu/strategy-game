using UnityEngine;

public class TileScript : MonoBehaviour
{
    //For grid position
    public Point GridPosition { get; private set; }

    //For game world position
    public Vector2 WorldPosition
    {
        get
        {
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x/2),transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2));
        } 
    }

    //Is tile Empty? ( flag )
    public bool IsEmpty { get;  set; }

    //Is tile Walkable? ( flag )
    public bool Walkable { get; set; }

    //For tile setup (Grid Position, World Position, Walkable, IsEmpty)
    public void Setup(Point gridPosition, Vector3 worldPos)
    {
        Walkable = true;
        IsEmpty = true;
        this.GridPosition = gridPosition;
        transform.position = worldPos;
        LevelManager.Instance.Tiles.Add(gridPosition, this);

    }

}
