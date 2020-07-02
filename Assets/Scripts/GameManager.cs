using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private Transform _soldier;
    public BuildingButton ClickedButton { get; set; }

    private void Awake()
    {
        _soldier = GameObject.Find("SoldierHolder").GetComponent<Transform>();
    }

    private void Update()
    {
        HandleEscape();
    }

    public void PickBuilding(BuildingButton buildingButton)
    {
        this.ClickedButton = buildingButton;
        Hover.Instance.Activate(buildingButton.Sprite);
    }

    
    private void HandleEscape()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Hover.Instance.DeActivate();
        }
    }

    public void Spawn()
    {
        StartCoroutine(SpawnSoldier());
    }

    private IEnumerator SpawnSoldier()
    {
        var soldierIndex = 0;

        string type = string.Empty;

        switch (soldierIndex)
        {
            case 0:
                type = "Soldier";
                break;
        }

        //Request from pool
        ObjectPool.Instance.GetObject(type, _soldier).GetComponent<Soldier>();

        yield return new WaitForSeconds(2.5f);

    }

}
