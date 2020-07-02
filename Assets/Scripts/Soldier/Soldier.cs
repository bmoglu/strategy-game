using System.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Soldier : MonoBehaviour, ISetup
{
    public int Speed { get; set; }
    public string Name { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    
    public Point GridPosition { get; set; }

    private SoldierMove soldierMove;

    private void Awake()
    {
        Name = "Soldier";
        SizeX = 1;
        SizeY = 1;
        Speed = 1;

        soldierMove = GetComponent<SoldierMove>();
       
    }

    private void Start()
    {
        soldierMove.IsActive = false;
        SpawnScale();
    }

    //Soldier scale set for down to up
    public void SpawnScale()
    {
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1)));
    }

    //Scaling enumerator
    private IEnumerator Scale(Vector3 from, Vector3 to)
    {
        soldierMove.IsActive = false;

        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;
    }

}
