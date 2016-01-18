using UnityEngine;
using System.Collections;

public class ManagerSystem : MonoBehaviour {

    private int rounds;             //Spiegelt Rundenzahl wieder
    private bool isPlayer1;         //Spieler1 an der Reihe
    GameObject player1;
    GameObject player2;
    GameObject selectedFigurine;    //Aktuell ausgewählte Spielfigur


	// Use this for initialization
	void Start () {
        rounds = 0;
        isPlayer1 = true;
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        player2.GetComponent<inputSystem>().enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Runde wird inkrementiert && AP werden wieder aufgefüllt
    void nextRound()
    {
        player1.GetComponent<PlayerComponent>().regenerateAP(); //Füllt AP von Spieler1 wieder auf
        player2.GetComponent<PlayerComponent>().regenerateAP(); //Füllt AP von Spieler2 wieder auf
        rounds++;
    }

    //Liefer true, wenn Spieler1 am Zug
    public bool getPlayerTurn()
    {
        return isPlayer1;
    }

    //Legt fest, welcher Spieler am Zug ist
    public void setPlayerTurn()
    {
        isPlayer1 = !isPlayer1;
        if(isPlayer1) //wenn Spieler 1 dran ist
        {
            //To-Do: Mit UI verknüpfen 
            Debug.Log("Spieler1 ist am Zug"); 
            player1.GetComponent<inputSystem>().enabled = true; //Aktiviere InputSys von Spieler1
            player2.GetComponent<inputSystem>().enabled = false;
        }
        else
        {
            //To-Do: Mit UI verknüpfen 
            Debug.Log("Spieler2 ist am Zug");
            player1.GetComponent<inputSystem>().enabled = false;
            player2.GetComponent<inputSystem>().enabled = true; //Aktiviere InputSys von Spieler2
        }

        GameObject.Find("Plane").GetComponent<DijkstraSystem>().resetDijkstra();

    }

    public void setSelectedFigurine(GameObject selected)
    {
        selectedFigurine = selected;
    }


   
}
