using UnityEngine;

public class SoldierTileInteraction : MonoBehaviour
{
    //When soldier stay the tile , set tile full and not walkable
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TileScript>())
        {
            other.gameObject.GetComponent<TileScript>().IsEmpty = false;
            other.gameObject.GetComponent<TileScript>().Walkable = false;
        }
    }

    //When soldier exit the tile , set empty and walkable
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<TileScript>())
        {
            other.gameObject.GetComponent<TileScript>().IsEmpty = true;
            other.gameObject.GetComponent<TileScript>().Walkable = true;
        }
    }
}
