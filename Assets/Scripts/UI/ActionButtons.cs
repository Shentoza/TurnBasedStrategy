using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ActionButtons : MonoBehaviour {

    UiManager uiM;

    public int bottomSpacing;
    public int width;
    public int height;
    public int spacing;

    //button icons
    public Texture2D buttonTextur0;
    public Texture2D buttonTextur1;
    public Texture2D buttonTextur2;
    public Texture2D buttonTextur3;
    public Texture2D buttonTextur4;
    public Texture2D buttonTextur5;

    int buttonsToDraw;
    int[] skills;
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
      
  
        buttonsToDraw = skills.GetLength(0);
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

        if (skills[i] == 0)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), buttonTextur0, uiM.getStyle()))
            {
                // uiM.getActiveUnit().move();
                uiM.shoot();
                Debug.Log("action0");
            }
        }else if (skills[i] == 1)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), buttonTextur1, uiM.getStyle()))
            {
                uiM.heal();
            }
        }else if (skills[i] == 2)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), buttonTextur2, uiM.getStyle()))
            {
                // uiM.getActiveUnit().action2();
                uiM.reload();
            }
        }else if (skills[i] == 3)
        {
            if (GUI.Button(new Rect(posX, posY, width, height), buttonTextur3, uiM.getStyle()))
            {
                // uiM.getActiveUnit().action3();
                Debug.Log("action3");
            }
        }







    }
}
