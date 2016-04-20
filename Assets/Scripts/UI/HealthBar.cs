using UnityEngine;
using System.Collections;



public class HealthBar : MonoBehaviour {

    public int width;
    public int height;
    public int figureOffset;
    public int barOffset;


    int baseX;
    int baseY;   
    public GameObject unit;

    public Texture2D hpTexture;
    int hp;

    public int xOffset = 0;
    public int yOffset = 0;

	// Use this for initialization
	void Start () {
        unit = this.transform.gameObject;
        hpTexture = unit.GetComponentInParent<PlayerComponent>().teamColor;
	}
	

	// Update is called once per frame
	void Update () {

        hp = unit.GetComponent<AttributeComponent>().hp;

	}


    void OnGUI()
    {
        if (GameObject.Find("Manager").GetComponent<ManagerSystem>().uiManagerSet)
        {
            UiManager uim = GameObject.Find("Manager").GetComponent<ManagerSystem>().uiManager.GetComponent<UiManager>();

            bool d = false;// uim.activeUnit.gameObject == this.gameObject;          

            if (Input.GetKey("space") | uim.activeSkill == Enums.Actions.Shoot | d)
            {

                xOffset = 0;
                yOffset = 0;


                int hpBars = (unit.GetComponent<AttributeComponent>().hp + 9) / 10;

                if (hpBars <= 5)
                {
                    xOffset = hpBars * (width + barOffset) / 2;
                }
                else
                {
                    xOffset = 5 * (width + barOffset) / 2;
                    yOffset = ((hpBars + 4) / 5) * (height + barOffset);
                }
                //bestimme hp leisten position
                Vector3 worldPosition = unit.transform.position;
                worldPosition.y += figureOffset;
                Vector3 position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(worldPosition);



              
                for (int i = 0; i <= hpBars; i ++)
                {
                   
                    GUI.DrawTexture(new Rect(position.x - xOffset + (i%5 * (width + barOffset)), (Screen.height - position.y) - yOffset + (i/5 *(height+barOffset)), width, height), hpTexture);
                    // Debug.Log("positionX: " + position.x + "position.y: " + position.y);

                    
                }

            }

        }
    }
}
