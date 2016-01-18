using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

    ManagerSystem manager;
	GameObject player;
	Cell zelle;
	DijkstraSystem dijSys;
	RTSCameraScript cameraScript;
	CameraRotationScript rotationScript;

	bool figurGewaehlt;
    bool spielerAmZug;

	bool angriffAusgewaehlt;
	MovementSystem moveSys;
	bool smokeAusgewaehlt;
	bool molotovAusgewaehlt;
	// Use this for initialization
	void Start () {

        dijSys = (DijkstraSystem) FindObjectOfType (typeof(DijkstraSystem));
        manager = GameObject.Find("Manager").GetComponent<ManagerSystem>();
		rotationScript = (CameraRotationScript)FindObjectOfType (typeof(CameraRotationScript));
	}
	

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) 
		{
            spielerAmZug = manager.getPlayerTurn();  //True = Spieler Eins, False = Spieler zwei
            Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
			if(hit.collider != null)
			{                
				if ((hit.collider.gameObject.tag == "FigurSpieler1" && spielerAmZug) || (hit.collider.gameObject.tag == "FigurSpieler2" && !spielerAmZug)) 
				{
					if(player != hit.collider.gameObject)
					{
                    manager.setSelectedFigurine(hit.collider.gameObject);
                    Debug.Log(hit.collider.gameObject.name);
						player = hit.collider.gameObject;
						figurGewaehlt = true;
						AttributeComponent playerAttr = (AttributeComponent) player.GetComponent(typeof(AttributeComponent));
						Cell currentCell = (Cell) playerAttr.getCurrentCell().GetComponent(typeof(Cell));
						dijSys.executeDijsktra(currentCell, playerAttr.movementRange, playerAttr.attackRange);
						rotationScript.setNewTarget(player);
					}
				}
				if (angriffAusgewaehlt)
				{
					if(hit.collider.gameObject.tag == "FigurSpieler1" && spielerAmZug || hit.collider.gameObject.tag == "FigurSpieler2" && !spielerAmZug)
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
		if (Input.GetMouseButtonDown (1)) {
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
		if (Input.GetKey ("r")) {
			rotationScript.setStartRotation ();
		}
		if (!Input.GetKey ("r")) {
			rotationScript.setStopRotation ();
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

