using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlaceObject : MonoBehaviour
{
    //Some color for full and empty
    private readonly Color32 _fullColor = new Color32(255, 118, 188, 255);
    private readonly Color32 _emptyColor = new Color32(96, 255, 90, 255);

    private SpriteRenderer _spriteRenderer;
    private TileScript tileScript;
    
    private void Start()
    {
        //Reference TileScript
        tileScript = GetComponent<TileScript>();
        //Reference Tile's sprite renderer
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //When mouse over the tile
    private void OnMouseOver()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && GameManager.Instance.ClickedButton != null)
        {
            if (tileScript.IsEmpty)
            {
                ColorTile(_emptyColor);
            }

            if (!tileScript.IsEmpty)
            {
                ColorTile(_fullColor);
            }
            else if (Input.GetMouseButtonDown(0))
            {
                PlaceBuilding();
            }
        }
    }

    //When mouse exit the tile
    private void OnMouseExit()
    {
        ColorTile(Color.white);
    }

    //Place to tile 
    private void PlaceBuilding()
    {
        var building = Instantiate(GameManager.Instance.ClickedButton.BuildingPrefab, transform.position, Quaternion.identity);
        
        building.transform.SetParent(transform);

        ColorTile(Color.white);

        //When place any building deActivate sprite
        Hover.Instance.DeActivate();
    }

    //For tile coloration
    private void ColorTile(Color newColor)
    {
        _spriteRenderer.color = newColor;
    }
}
