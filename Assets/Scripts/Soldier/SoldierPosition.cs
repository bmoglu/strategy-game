using UnityEngine;

public class SoldierPosition : MonoBehaviour
{
    private void Start()
    {
        RandomSpawn();
    }

    //For randomize soldier position
    private void RandomSpawn()
    {
        // X axis between this values ==> (-12, 13)
        // Y axis between this values ==> (-8, 10)
        var rndX = Random.Range(-12, 13);
        var rndY = Random.Range(-8, 10);
        
        transform.position = new Vector3(rndX, rndY, 0);
    }

    //When soldier enter the tile , if tile full then randomize position
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<TileScript>())
        {
            //If tile full
            if (col.gameObject.GetComponent<TileScript>().IsEmpty == false)
            {
                RandomSpawn();
            }
        }
    }
}
