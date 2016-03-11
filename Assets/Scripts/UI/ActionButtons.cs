using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour {

    UiManager uiM;

    public int bottomSpacing;
    public int width;
    public int height;
    public int spacing;

    //button icons
    public Texture2D Cancel;
    public Texture2D Move;
    public Texture2D Hit;
    public Texture2D Shoot;
    public Texture2D Reload;
    public Texture2D ChangeWeapon;
    public Texture2D Heal;
    public Texture2D Molotov;
    public Texture2D Grenade;
    public Texture2D Smoke;
    public Texture2D Teargas;

    int buttonsToDraw;

    List<Enums.Actions> skills;
    int startPosX;

    
   

	// Use this for initialization
	void Start () {
        uiM = GetComponent<UiManager>();
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnGUI(){
        
        skills = uiM.getActiveUnitSkills();

        buttonsToDraw = skills.Count;
      
        startPosX = Screen.width/2 - (buttonsToDraw * width + (buttonsToDraw - 1) * spacing) / 2;

        for (int i = 0; i < buttonsToDraw; i++)
        {
            drawButton(i);
        //    Debug.Log("button " + i + "erstellt");
        }


    }

    void drawButton(int i){
  
        int posX;
        if (i == 0)
        {
            posX = startPosX;
        }
        else
        {
            posX = startPosX + i * width + i * spacing;
        }
        int posY = Screen.height - height - bottomSpacing;

        if (skills[i] == Enums.Actions.Move)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), new GUIContent(Move, "Move"), uiM.getStyle()))
            {
                uiM.move();
                Debug.Log("move");
            }
        }
        else if (skills[i] == Enums.Actions.Hit)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), new GUIContent(Hit, "Hit"), uiM.getStyle()))
            {
                uiM.hit();
                Debug.Log("hit");
            }
        }







    }
}
