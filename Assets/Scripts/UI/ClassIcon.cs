using UnityEngine;
using System.Collections;

public class ClassIcon : MonoBehaviour {

    public int width;
    public int height;
    public float figureOffset;
    public float barOffset;


    int baseX;
    int baseY;

    public GameObject unit;

    public Texture2D iconRiot;
    public Texture2D iconSoldier;
    public Texture2D iconSupport;

    public Enums.Prof prof;

    public Texture2D iconToShow;



    // Use this for initialization
    void Start()
    {
        Debug.Log("Icon at start " + iconToShow);
        unit = this.transform.gameObject;
        prof = unit.GetComponent<AttributeComponent>().profession;

        if (prof == Enums.Prof.Riot)
            iconToShow = iconRiot;
        if (prof == Enums.Prof.Soldier)
            iconToShow = iconSoldier;
        if (prof == Enums.Prof.Support)
            iconToShow = iconSupport;

        Debug.Log(iconToShow);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    void OnGUI()
    {

        if (Input.GetKey("space") && iconToShow != null)
        {

            int xOffset = ((10 + 9) / 10) * width / 2;

            Vector3 worldPosition = unit.transform.position;
            worldPosition.y += figureOffset;
            Vector3 position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(worldPosition);

            GUI.DrawTexture(new Rect(position.x - xOffset + 10 / 10 * (width + barOffset), Screen.height - position.y, width, height), iconToShow);

        }

    }
}
