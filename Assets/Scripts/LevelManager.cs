using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    //Tile prefab
    [SerializeField] private GameObject tile;

    //Tile parent
    [SerializeField] private Transform parentTransform;

    //Tile sprite size x
    public float TileSize => tile.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

    //Map X axis
    public int MapX => 40;

    //Map Y axis
    public int MapY => 20;

    //Tiles Holder
    public Dictionary<Point, TileScript> Tiles { get; set; }

    private Point mapSize;

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

    private void Start()
    {
        //Executes the create level
        CreateLevel();
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        //For map max size
        Camera.main.orthographicSize = 10;

        var worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        
        //For map normal size 
        Camera.main.orthographicSize = 5;

        //Tile creation iteration
        for (var y = 0; y < MapY; y++) // y positions
        {
            for (var x = 0; x < MapX; x++) // x positions
            {
                PlaceTile(x, y, worldStart);
            }
        }

        mapSize = new Point(MapX, MapY);
    }

    //Placing tile
    private void PlaceTile(int x, int y, Vector3 worldStart)
    {
        //Create new tile 
        var newTile = Instantiate(tile, parentTransform).GetComponent<TileScript>();

        //Setup x,y for new tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0));
    }

    //Is it in the map?
    public bool InBounds(Point position)
    {
        return position.X >= 0 && position.Y >= 0 && position.X < mapSize.X && position.Y < mapSize.Y;
    }

    //Generate path for soldier move
    public void GeneratePath()
    {
        //first position and last position for path
        path = AStar.GetPath(new Point(0,0), new Point(3,3));
    }

}
