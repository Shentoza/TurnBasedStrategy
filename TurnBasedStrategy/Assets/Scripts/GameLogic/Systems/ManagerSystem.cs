using UnityEngine;
using System.Collections;

public class ManagerSystem : MonoBehaviour {

    private int rounds;             //Spiegelt Rundenzahl wieder
    private GameObject[] order;     //Zugreihenfolge der Spielfiguren
    private bool isPlayer1;         //Spieler1 an der Reihe
    private int pointerOnFigurine;  //Pointer für aktuelle Spielfigur
    GameObject player1;
    GameObject player2;

	// Use this for initialization
	void Start () {
        player1 = GameObject.Find("player1");
        player2 = GameObject.Find("player2");
        rounds = 0;
        isPlayer1 = true;
        pointerOnFigurine = 0;
       // setOrder(); Reihenfolge von Spielfiguren initialisieren
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Runde wird inkrementiert
    void nextRound()
    {
        rounds++;
        pointerOnFigurine = 0;
    }

    //Nächste Figur ist am Zug
    void nextFigurine()
    {
        pointerOnFigurine++;
        isPlayer1 = !isPlayer1;
    }

    /*
    //Füllt Array abwechselnd von jedem Spieler mit einer Figur
    void setOrder()
    {
        int j = 0;
        for(int i = 0; i < player1.getFigurines().length; i+=2)
        {
            order[i] = player1.getFigurines()[j];
            j++;
        }

        j = 0;

        for(int i = 1; i < player2.getFigurines().length; i+=2)
        {
            if (i < player1.getFigurines().length + 1)
                order[i] = player2.getFigurines()[j];
            else
                order[i - 1] = player2.getFigurines()[j];
            j++;        
        }

    } */
}
