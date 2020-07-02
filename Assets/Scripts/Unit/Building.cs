using UnityEngine;

public class Building : MonoBehaviour, ISetup
{
    public string Name { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }

    private bool _firstTime = true;

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Tile"))
        {
            //Tile full or empty?
            if (col.gameObject.GetComponent<TileScript>().IsEmpty == false)
            {
                Destroy(gameObject);
               
                //User feedback
                AnimationController.Instance.NotifyUser();

                _firstTime = false;
                return;
            }

            _firstTime = false;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Tile"))
        {
            other.gameObject.GetComponent<TileScript>().IsEmpty = false;
            other.gameObject.GetComponent<TileScript>().Walkable = false;
            
            //To avoid unnecessary controls
            gameObject.GetComponent<Collider2D>().isTrigger= true;
        }
    }

    private void Release()
    {
        ObjectPool.Instance.ReleaseObject(gameObject);
    }

}




