using UnityEngine;
using System.Collections;

public class ManagerSystem : MonoBehaviour {

    private int rounds;             //Spiegelt Rundenzahl wieder
    private bool isPlayer1;         //Spieler1 an der Reihe
    GameObject player1;
    GameObject player2;
    GameObject selectedFigurine;
    inputSystem inputP1;
    inputSystem inputP2;
	// Use this for initialization
	void Start () {
        rounds = 0;
        isPlayer1 = true;
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        inputP1 = player1.GetComponent<inputSystem>();
        inputP1.enabled = true;
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
        return player1;
    }

    //Legt fest, welcher Spieler am Zug ist
    public void setPlayerTurn()
    {
        isPlayer1 = !isPlayer1;
        if(isPlayer1) //wenn Spieler 1 dran ist
        {
            player1.GetComponent<inputSystem>().enabled = true; //Aktiviere InputSys von Spieler1
            player2.GetComponent<inputSystem>().enabled = false;
        }
        else
        {
            player1.GetComponent<inputSystem>().enabled = false;
            player2.GetComponent<inputSystem>().enabled = true; //Aktiviere InputSys von Spieler2
        }

    }

    public void setSelectedFigurine(GameObject selected)
    {
        selectedFigurine = selected;
    }


   
}
