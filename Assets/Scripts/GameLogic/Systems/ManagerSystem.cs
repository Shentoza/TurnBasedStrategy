using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ManagerSystem : MonoBehaviour {

    public GameObject unit;
    public GameObject uiManager;

    public List<GameObject> unitListP1;
    public List<GameObject> unitListP2;

    
    public GameObject activeUnitMarker;

    CameraRotationScript cam;
    private int rounds;             //Spiegelt Rundenzahl wieder
    private bool isPlayer1;         //Spieler1 an der Reihe
    GameObject player1;
    GameObject player2;
    public GameObject selectedFigurine;    //Aktuell ausgewählte Spielfigur
    int roundHalf;  //1 wenn Spieler1 seinen Turn beendet, 2 wenn Spieler2 seinen Turn beendet;

    private ShootingSystem shootingSys;

    GameObject plane;

	// Use this for initialization
	void Start () {

        rounds = 0;
        isPlayer1 = true;
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player2.GetComponent<inputSystem>().enabled = false;
        cam = GameObject.Find("Main Camera").GetComponent<CameraRotationScript>();
        shootingSys = (ShootingSystem)this.gameObject.GetComponent(typeof(ShootingSystem));

        plane = GameObject.Find("Plane");
    }
	
	// Update is called once per frame
	void Update () {

        activeUnitMark();

	}

    public void loadUI()
    {
        Instantiate(uiManager);
        selectedFigurine = unitListP1[0];
        isPlayer1 = true;
    }


    public void shoot(GameObject attacker, GameObject target)
    {
        if(shootingSys.shoot(attacker, target))
        {
            Debug.Log("Getroffen");
        }
        else
        {
            Debug.Log("Nicht getroffen");
        }
    }
    //Runde wird inkrementiert && AP werden wieder aufgefüllt
    void nextRound()
    {



        player1.GetComponent<PlayerComponent>().regenerateAP(); //Füllt AP von Spieler1 wieder auf
        player2.GetComponent<PlayerComponent>().regenerateAP(); //Füllt AP von Spieler2 wieder auf
        rounds++;
        Debug.Log("Runde: " + rounds);
    }

    //Liefer true, wenn Spieler1 am Zug
    public bool getPlayerTurn()
    {
        return isPlayer1;
    }

    //Legt fest, welcher Spieler am Zug ist
    public void setPlayerTurn()
    {
        roundHalf++;
        if(roundHalf == 2)
        {
            nextRound();
            roundHalf = 0;
        }
        isPlayer1 = !isPlayer1;
        if(isPlayer1) //wenn Spieler 1 dran ist
        {
            //To-Do: Mit UI verknüpfen 
            Debug.Log("Spieler1 ist am Zug");
            setSelectedFigurine(unitListP1[0]);               //Wählt das erste Child von Spieler2
            cam.setNewTarget(selectedFigurine);                 //Gibt der Kamera ein neues Target
            player1.GetComponent<inputSystem>().enabled = true;                         //Aktiviere InputSys von Spieler1
            player2.GetComponent<inputSystem>().enabled = false;
        }
        else
        {
            //To-Do: Mit UI verknüpfen 
            setSelectedFigurine(unitListP2[0]);             //Wählt das erste Child von Spieler2
            cam.setNewTarget(selectedFigurine);              //Gibt der Kamera ein neues Target
            player1.GetComponent<inputSystem>().enabled = false;
            player2.GetComponent<inputSystem>().enabled = true;                      //Aktiviere InputSys von Spieler2
        }

        plane.GetComponent<DijkstraSystem>().resetDijkstra();

    }

    public void setSelectedFigurine(GameObject selected)
    {
        selectedFigurine = selected;
    }

    public GameObject getSelectedFigurine()
    {
        return selectedFigurine;
    }


    public void addUnit(int team)
    {
        GameObject tmp = Instantiate(unit);
        
        if (team == 1)
        {
            tmp.transform.SetParent(player1.transform);
            unitListP1.Add( tmp );
        }
        else if (team == 2)
        {
            tmp.transform.SetParent(player2.transform);
            unitListP2.Add(tmp );
        }
    }


    public void activeUnitMark()
    {
        if (selectedFigurine)
        {
            activeUnitMarker.transform.position = selectedFigurine.transform.position;
            activeUnitMarker.transform.rotation = selectedFigurine.transform.rotation;
        }
        

    }
}
