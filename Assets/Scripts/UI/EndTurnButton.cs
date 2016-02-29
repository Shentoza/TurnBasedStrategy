using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class EndTurnButton : MonoBehaviour {

    //ref object
    UiManager uiM;

   
    //end Turn button
    public Texture2D iconPlayer1EndTurn;
    public Texture2D iconPlayer2EndTurn;


    public int width;
    public int height;


	// Use this for initialization
	void Start () {
        uiM = GetComponent<UiManager>();
	}
	

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnGUI()
    {

        if (uiM.isPlayer1)
        {
            if (GUI.Button(new Rect(Screen.width-width, Screen.height-height, width, height), iconPlayer1EndTurn, uiM.getStyle()))
            {
                endTurn();
            }

        }
        else if (GUI.Button(new Rect(Screen.width - width, Screen.height - height, width, height), iconPlayer2EndTurn, uiM.getStyle()))
        {
            endTurn();
        }
       
    }



    //test
    void endTurn() {

        uiM.endTurn();
    
    }


}
