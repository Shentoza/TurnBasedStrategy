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

                int xOffset = ((hp + 9) / 10) * width / 2;

                //bestimme hp leisten position
                Vector3 worldPosition = unit.transform.position;
                worldPosition.y += figureOffset;
                Vector3 position = GameObject.Find("Main Camera").GetComponent<Camera>().WorldToScreenPoint(worldPosition);


                for (int i = 0; i < hp; i += 10)
                {
                    GUI.DrawTexture(new Rect(position.x - xOffset + i / 10 * (width + barOffset), Screen.height - position.y, width, height), hpTexture);
                    // Debug.Log("positionX: " + position.x + "position.y: " + position.y);
                }

            }

        }
    }
}
