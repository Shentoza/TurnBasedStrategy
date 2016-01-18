using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour {

    GameObject[] figurines; //Alle Figuren über die ein Spieler verfügt
    int actionPoints; //Anzahl an verfügbaren Aktionspunkten
    int maxAP; //Maxcap für AP

	// Use this for initialization
	void Start () {
        if (this.gameObject.name == "player1")
            maxAP = (figurines.Length + 2) * 2;
        else if (this.gameObject.name == "player2")
            maxAP = (figurines.Length + 4) * 2;

        regenerateAP();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //Löscht Spielfigur
    public void removeFigurine(GameObject figurine)
    {
        for(int i = 0; i < figurines.Length; i++)
        {
            if(figurines[i] == figurine)
            {
                GameObject.Destroy(figurine);
                break;
            }
        }
    }

    //Füllt die AP für den Spieler wieder auf
    public void regenerateAP()
    {
        if(this.gameObject.name == "player1")
        {
            if (actionPoints + figurines.Length + 2 > maxAP)
                actionPoints = maxAP;
            else
                actionPoints = actionPoints + figurines.Length + 2;

        }
        if(this.gameObject.name == "player2")
        {
            if (actionPoints + figurines.Length + 4 > maxAP)
                actionPoints = maxAP;
            else
                actionPoints = actionPoints + figurines.Length + 4;
        }
    }
}
