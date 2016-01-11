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

    public void setPlayerTurn()
    {
        isPlayer1 = !isPlayer1;
        if(isPlayer1)
        {
            player1.GetComponent<inputSystem>().enabled = true;
            player2.GetComponent<inputSystem>().enabled = false;
            Debug.Log("Spieler1 am Zug");
        }
        else
        {
            player1.GetComponent<inputSystem>().enabled = false;
            player2.GetComponent<inputSystem>().enabled = true;
            Debug.Log("Spieler2 am Zug");
        }

    }

    public void setSelectedFigurine(GameObject selected)
    {
        selectedFigurine = selected;
    }


   
}
