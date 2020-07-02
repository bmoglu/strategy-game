using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{

    [SerializeField]  private GameObject[] objectPrefabs;

    private List<GameObject> pooledObjects = new List<GameObject>();

    public GameObject GetObject(string type, Transform parentTransform)
    {
        //If object pooled then reuse it from the pool
        foreach (var po in pooledObjects)
        {
            if (po.name == type && !po.activeInHierarchy)
            {
                po.SetActive(true);
                po.GetComponent<SoldierRelease>().IsPooled = false;
                return po;
            }
        }

        //Which type? ( We have only 1 type now )
        switch (type)
        {
            case "Soldier":
                var newObject = Instantiate(objectPrefabs[0],parentTransform);
                newObject.name = type;
                pooledObjects.Add(newObject);
                return newObject;
        }
        
        return null;
    }

    public void ReleaseObject(GameObject go)
    {
        go.SetActive(false);
    }
}
