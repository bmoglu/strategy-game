using UnityEngine;

public class Hover : Singleton<Hover>
{
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        //Access sprite renderer 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Execute
        FollowMouse();
    }

    //Following mouse position
    private void FollowMouse()
    {
        if (spriteRenderer.enabled)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    //Activation for sprites
    public void Activate(Sprite sprite)
    {
        this.spriteRenderer.sprite = sprite;
        spriteRenderer.enabled = true;
    }

    //DeActivation for sprites
    public void DeActivate()
    {
        spriteRenderer.enabled = false;
        GameManager.Instance.ClickedButton = null;
    }
}
