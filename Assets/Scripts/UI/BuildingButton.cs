using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    [SerializeField] private GameObject buildingPrefab;

    [SerializeField] private Sprite sprite;
    public GameObject BuildingPrefab => buildingPrefab;

    public Sprite Sprite => sprite;
}
