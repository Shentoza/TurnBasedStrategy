using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UiManager : MonoBehaviour {


    //dummys
    public bool player1IsActive;
    public int player1AP;
    public int player2AP;
    public int maxAP;

    GUIStyle style;
    
    // aktionen enum
    int[] activeUnitSkills = {0,3,2,1,3};


	// Use this for initialization
	void Start () {



        //test angaben
        player1IsActive = true;
        player1AP = 3;
        player2AP = 3;

        //getActiveUnitSkills


        //setStyle
        style = new GUIStyle();
        
	}
	

    // Update is called once per frame
    void Update()
    {
        
    }


    public void endTurn()
    {
        player1IsActive = !player1IsActive;
        Debug.Log("turn ended");


        if (player1IsActive)
        {
            player1AP += 2;

        }
        else
        {
            player2AP += 2;
        }


    }

    public int[] getActiveUnitSkills()
    {
       
        return activeUnitSkills;
    }

    public GUIStyle getStyle()
    {
        return style;
    }
    
}
