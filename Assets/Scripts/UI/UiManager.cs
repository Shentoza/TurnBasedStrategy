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

    InventorySystem inventSys;
    ManagerSystem managerSys;

    public int maxAP;

    GUIStyle style;

    inputSystem input;

    // aktionen enum
    int[] activeUnitSkills = {0,1,2,3,4};


	// Use this for initialization
	void Start () {

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        managerSys = GameObject.Find("Manager").GetComponent<ManagerSystem>();
        inventSys = GameObject.Find("Manager").GetComponent<InventorySystem>();

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
        if (isPlayer1)
            input = player1.GetComponent<inputSystem>();
        else
            input = player2.GetComponent<inputSystem>();
    }


    public void endTurn()
    {
        managerSys.setPlayerTurn();
    }

    public void shoot()
    {
        input.angriffAusgewaehlt = true;
    }

    public void heal()
    {

    }

    //Weitergabe an InventorySystem
    public void reload()
    {
        inventSys.reloadAmmo(GameObject.Find("Manager").GetComponent<ManagerSystem>().getSelectedFigurine());
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
