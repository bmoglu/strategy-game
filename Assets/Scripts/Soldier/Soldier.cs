using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Soldier : MonoBehaviour, ISetup
{
    [SerializeField] private float speed;
    public string Name { get; set; }
    public int SizeX { get; set; }
    public int SizeY { get; set; }
    
    private Stack<Node> path;

    public Point GridPosition { get; set; }

    public bool IsActive { get; set; }

    private Vector3 destination;

    private void Awake()
    {
        Name = "Soldier";
        SizeX = 1;
        SizeY = 1;
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
            transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);

            if (transform.position == destination)
            {
                if (path != null && path.Count > 0)
                {
                    GridPosition = path.Peek().GridPosition;
                    destination = path.Pop().WorldPosition;
                }
            }
        }
    }

    //Soldier scale set for down to up
    public void SpawnScale()
    {
        StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1, 1)));

        SetPath(LevelManager.Instance.Path);
    }

    //Scaling enumerator
    private IEnumerator Scale(Vector3 from, Vector3 to)
    {
        IsActive = false;

        float progress = 0;
        while (progress <= 1)
        {
            transform.localScale = Vector3.Lerp(from, to, progress);

            progress += Time.deltaTime;

            yield return null;
        }

        transform.localScale = to;

        IsActive = true;
    }

    //For path finding A* algorithm
    private void SetPath(Stack<Node> newPath)
    {
        if (newPath != null)
        {
            this.path = newPath;

            GridPosition = path.Peek().GridPosition;
            destination = path.Pop().WorldPosition;
        }
    }

}
