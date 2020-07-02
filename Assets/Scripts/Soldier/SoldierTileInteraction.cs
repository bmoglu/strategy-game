using UnityEngine;

public class SoldierTileInteraction : MonoBehaviour
{
    public Point SoldierPoint { get; set; }

    //When soldier stay the tile , set tile full and not walkable
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TileScript>())
        {
            var tile = other.gameObject.GetComponent<TileScript>();
            tile.IsEmpty = false;
            tile.Walkable = false;
            SoldierPoint = tile.GridPosition;
        }
    }

    //When soldier exit the tile , set empty and walkable
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TileScript>())
        {
            var tile = other.gameObject.GetComponent<TileScript>();
            tile.IsEmpty = true;
            tile.Walkable = true;
        }
    }
}
