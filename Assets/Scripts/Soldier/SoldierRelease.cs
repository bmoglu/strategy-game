using UnityEngine;

public class SoldierRelease : MonoBehaviour
{
    public bool IsPooled { get; set; }

    //Release the object for reused in future
    private void Release()
    {
        ObjectPool.Instance.ReleaseObject(gameObject);
    }

    private void Update()
    {
        //If gameObject active in hierarchy then send the pool  
        if (gameObject.activeInHierarchy && !IsPooled)
        {
            //5 sec after gameObject send the pool
            Invoke(nameof(Release), 25f);
            //For update control
            IsPooled = true;
        }
    }
}
