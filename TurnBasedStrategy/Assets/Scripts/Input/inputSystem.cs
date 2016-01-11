using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

    ManagerSystem manager;
	GameObject player;
	Cell zelle;
	bool figurGewaehlt;
    bool SpielerAmZug;

    DijkstraSystem dijSys;
	bool angriffAusgewaehlt;
	MovementSystem moveSys;
	bool smokeAusgewaehlt;
	bool molotovAusgewaehlt;
	// Use this for initialization
	void Start () {

        dijSys = (DijkstraSystem) FindObjectOfType (typeof(DijkstraSystem));
        manager = (ManagerSystem)FindObjectOfType(typeof(ManagerSystem));
	}
	

	// Update is called once per frame
	void Update () {
        SpielerAmZug = manager.getPlayerTurn();
        if (Input.GetMouseButton (0)) 
		{
            //True = Spieler Eins, False = Spieler zwei
            Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
			if(hit.collider != null)
			{
				if ((hit.collider.gameObject.tag == "FigurSpieler1" && SpielerAmZug) || (hit.collider.gameObject.tag == "FigurSpieler2" && !SpielerAmZug)) 
				{
                    manager.setSelectedFigurine(hit.collider.gameObject);
                    Debug.Log(hit.collider.gameObject.name);
					player = hit.collider.gameObject;
					figurGewaehlt = true;
					AttributeComponent playerAttr = (AttributeComponent) player.GetComponent(typeof(AttributeComponent));
					Cell currentCell = (Cell) playerAttr.getCurrentCell().GetComponent(typeof(Cell));
					dijSys.executeDijsktra(currentCell, playerAttr.movementRange, playerAttr.attackRange);
				}
				if (angriffAusgewaehlt)
				{
					if(hit.collider.gameObject.tag == "FigurSpieler1" && SpielerAmZug || hit.collider.gameObject.tag == "FigurSpieler2" && !SpielerAmZug)
					{
						//FühreAngriffAus
					}
					}
				if (smokeAusgewaehlt)
				{
					if(hit.collider.gameObject.tag == "Cell")
					{
						AbilitySystem playerAbilSys = (AbilitySystem) player.GetComponent(typeof(AbilitySystem));
						zelle = (Cell)hit.collider.gameObject.GetComponent(typeof(Cell));
						playerAbilSys.throwSmoke(zelle);
						smokeAusgewaehlt = false;
					}
				}
				if (molotovAusgewaehlt)
				{
					if(hit.collider.gameObject.tag == "Cell")
					{
						AbilitySystem playerAbilSys = (AbilitySystem) player.GetComponent(typeof(AbilitySystem));
						zelle = (Cell)hit.collider.gameObject.GetComponent(typeof(Cell));
						playerAbilSys.throwMolotov(zelle);
						molotovAusgewaehlt = false;
					}
				}
			}
			else
			{
				figurGewaehlt = false;
			}
		}
		if (Input.GetMouseButton (1)) {
			Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
			if(hit.collider != null)
			{
				if(figurGewaehlt && hit.collider.gameObject.tag == "Cell")
				{
					zelle = (Cell)hit.collider.gameObject.GetComponent(typeof(Cell));
					MovementSystem moveSys = (MovementSystem) player.GetComponent(typeof(MovementSystem));
					moveSys.MoveTo(zelle);
				}
			}
			else
			{
				figurGewaehlt = false;
			}
		}
		if (Input.GetKeyDown ("s")) {
			smokeAusgewaehlt = true;
		}
		if (Input.GetKeyDown ("f")) {
			molotovAusgewaehlt = true;
		}

        if(Input.GetKeyDown("n"))
        {
            manager.setPlayerTurn();
        }

	}
}

