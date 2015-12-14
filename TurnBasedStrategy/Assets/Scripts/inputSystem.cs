using UnityEngine;
using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {
	
	GameObject player;
	bool figurGewaehlt;
	bool SpielerAmZug; //True = Spieler Eins, False = Spieler zwei
	DijkstraSystem dijSys;
	bool angriffAusgewaehlt;
	MovementSystem moveSys;
	// Use this for initialization
	void Start () {
		dijSys = (DijkstraSystem) FindObjectOfType (typeof(DijkstraSystem));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0)) 
		{
			Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
			if(hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "FigurSpieler1" && SpielerAmZug || hit.collider.gameObject.tag == "FigurSpieler2" && !SpielerAmZug) 
				{
					player = hit.collider.gameObject;
					figurGewaehlt = true;
					AttributeComponent playerAttr = (AttributeComponent) player.GetComponent(typeof(AttributeComponent));
					Cell currentCell = (Cell) playerAttr.getCurrentCell().GetComponent(typeof(Cell));
					dijSys.executeDijsktra(currentCell,playerAttr.movementRange,(int)playerAttr.attackRange);
				}
				if (angriffAusgewaehlt)
				{
					if(hit.collider.gameObject.tag == "FigurSpieler1" && SpielerAmZug || hit.collider.gameObject.tag == "FigurSpieler2" && !SpielerAmZug)
					{
						//FühreAngriffAus
					}
				}
			}
		}
		if (Input.GetMouseButton (1)) {
			Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
			if(figurGewaehlt && hit.collider.gameObject.tag == "Cell")
			{
				Cell zelle = (Cell)hit.collider.gameObject.GetComponent(typeof(Cell));
				MovementSystem moveSys = (MovementSystem) player.GetComponent (typeof(MovementSystem));
				MovementSystem.MoveTo(zelle);				
			}
		}
	}
}

