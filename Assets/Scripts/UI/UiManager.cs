using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UiManager : MonoBehaviour {


    //dummys
    public bool isPlayer1;
   public int player1AP;
   public int player2AP;

    GameObject player1;
    GameObject player2;

    ManagerSystem managerSys;

    public int maxAP;

    GUIStyle style;
    
    // aktionen enum
    int[] activeUnitSkills = {0,3,2,1,3};


	// Use this for initialization
	void Start () {

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        managerSys = GameObject.Find("Manager").GetComponent<ManagerSystem>();

        player1AP = player1.GetComponent<PlayerComponent>().actionPoints;
        player2AP = player2.GetComponent<PlayerComponent>().actionPoints;

        //test angaben
        isPlayer1 = managerSys.getPlayerTurn();


        //getActiveUnitSkills


        //setStyle
        style = new GUIStyle();
        
	}
	

    // Update is called once per frame
    void Update()
    {
        isPlayer1 = managerSys.getPlayerTurn();
        player1AP = player1.GetComponent<PlayerComponent>().actionPoints;
        player2AP = player2.GetComponent<PlayerComponent>().actionPoints;
    }


    public void endTurn()
    {
        managerSys.setPlayerTurn();
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
