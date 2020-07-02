using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class UIController : MonoBehaviour
    { 
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private Text unitName;
        [SerializeField] private Image unitImage;
        [SerializeField] private GameObject rightPanel;
        [SerializeField] private GameObject productionPanel;
        [SerializeField] private Text dimentionText;

        private Building buildingGo;
        private Soldier soldierGo;
        private string _nameHolder;
        private int _sizeX, _sizeY;
        private void LateUpdate()
        {
            #region Ray creation and to decite which object

            //When clicked Unit or Soldier
            if (!Input.GetMouseButtonDown(0)) return;

            //Create Ray
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            //Hit Control
            if (!hit.collider) return;

            //Is that building or soldier?
            if (hit.collider.gameObject.GetComponent<Building>())
            {
                buildingGo = hit.collider.gameObject.GetComponent<Building>();
                _nameHolder = buildingGo.Name;
                _sizeX = buildingGo.SizeX;
                _sizeY = buildingGo.SizeY;
            }
            else if (hit.collider.gameObject.GetComponent<Soldier>())
            {
                soldierGo = hit.collider.gameObject.GetComponent<Soldier>();
                _nameHolder = soldierGo.Name;
                _sizeX = soldierGo.SizeX;
                _sizeY = soldierGo.SizeY;
            }
            else return;

            #endregion

            SetCanvasObjects();
        }

        private void SetCanvasObjects()
        {
            //Then do some ui things
            switch (_nameHolder)
            {
                case "PowerPlant":
                    unitName.text = "Power\nPlant";
                    unitImage.sprite = sprites[0];
                    rightPanel.SetActive(true);
                    productionPanel.SetActive(false);
                    dimentionText.text = buildingGo.SizeX + "x" + buildingGo.SizeY;
                    break;
                case "Barrack":
                    unitName.text = "Barrack";
                    unitImage.sprite = sprites[1];
                    rightPanel.SetActive(true);
                    productionPanel.SetActive(true);
                    dimentionText.text = buildingGo.SizeX + "x" + buildingGo.SizeY; ;
                    break;
                case "Soldier":
                    unitName.text = "Soldier";
                    unitImage.sprite = sprites[2];
                    rightPanel.SetActive(true);
                    productionPanel.SetActive(false);
                    dimentionText.text = soldierGo.SizeX + "x" + soldierGo.SizeY; ;
                    break;
            }
        }
    }
}
