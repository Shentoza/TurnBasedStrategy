using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UiManager : MonoBehaviour {


    public bool player1IsActive;
    public int player1AP;
    public int player2AP;
    public Text player1APText;
    public Text player2APText;
   
    public Button endTurnButton;
    public Color colorPlayer1;
    public Color colorPlayer2;

    ColorBlock cb1;
    ColorBlock cb2;

	// Use this for initialization
	void Start () {

        player1IsActive = true;
        player1AP = 3;
        player2AP = 3;


        //endTurnButton Farben vorbereiten (weil button.colors.normalColor nicht funzt umweg über colorblocks)
        cb1 = endTurnButton.colors;
        cb2 = cb1;
        cb1.normalColor = colorPlayer1;
        cb1.highlightedColor = colorPlayer1;
        cb2.normalColor = colorPlayer2;
        cb2.highlightedColor = colorPlayer2;

        updateUi();
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

        updateUi();
    }


    void updateUi()
    {
        player1APText.text = "Player1 AP: " + player1AP;
        player2APText.text = "Player2 AP: " + player2AP;

        if(player1IsActive){
            endTurnButton.colors = cb1;
        }
        else
        {
            endTurnButton.colors = cb2;
        }
        
    }







    // Update is called once per frame
    void Update()
    {
    }
}
