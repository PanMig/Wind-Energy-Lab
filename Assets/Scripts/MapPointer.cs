using TMPro;
using UnityEngine;
using UnityEngine.UI;
using goedle_sdk;


public class MapPointer : MonoBehaviour {

    [Header("GUI components")]
    [SerializeField] private Image infoPanel;
    private TextMeshProUGUI textPro;

    [Space]
    public Sprite[] pointerSprites;
	SpriteRenderer spriteRend;
    private Vector2 panelPos;
    private Vector3 mousePos;

    [SerializeField] private int cost;
    [SerializeField] private int subAreaType;

    void Start()
	{
		spriteRend = GetComponent<SpriteRenderer>();
		this.spriteRend.sprite = pointerSprites[0];

        //panel 
        textPro = infoPanel.GetComponentInChildren<TextMeshProUGUI>();
        EnableInfoPanel(false);

	}

	void OnMouseEnter()
	{
		this.spriteRend.sprite = pointerSprites[1];
        //PlacePanelNextToSprite();
        EnableInfoPanel(true);  
	}

	void OnMouseExit()
	{
		this.spriteRend.sprite = pointerSprites[0];
        EnableInfoPanel(false);
	}

    private void OnMouseDown()
    {
        GameManager.instance.IncrementCost(cost);
        GameManager.instance.areaInstallationCost = GameManager.cost;
        GameManager.instance.SetSubArea(subAreaType);
        GameManager.instance.LoadLevel("TurbineSelection");
        GoedleAnalytics.track("select.turbine",gameObject.name);
    }


    void PlacePanelNextToSprite()
    {
        mousePos = Input.mousePosition;
        panelPos.x = mousePos.x;
        panelPos.y = mousePos.y + 170.0f;
        panelPos.x = Mathf.Clamp(panelPos.x, 0, Screen.width);
        panelPos.y = Mathf.Clamp(panelPos.y, 0, Screen.height);
        infoPanel.transform.position = panelPos;
    }

    void EnableInfoPanel( bool visible )
    {
        if(visible == true)
        {
            infoPanel.enabled = true;
            textPro.enabled = true;
        }
        else
        {
            infoPanel.enabled = false;
            textPro.enabled = false;
        }
        
    }

}
