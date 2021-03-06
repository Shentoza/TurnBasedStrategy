﻿using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

    //Stuff vom Manager
    ManagerSystem manager;

    DijkstraSystem dijSys;
    PlayerAssistanceSystem assist;
    AbilitySystem abilSys;

    //Ausgewählte Figur
    GameObject player;
    //Stuff von der Aktuell gewählten Figur
    AttributeComponent attr;
    MovementSystem movement;
	CameraRotationScript rotationScript;

	bool figurGewaehlt;
    bool spielerAmZug;

    //Maske für Raycast
    public LayerMask Cellmask;
    //Aktuelle Zelle über die man hovert
    Cell selectedCell;

    //letzte angewählte Zelle zu der man moven kann
    Cell selectedMovementCell;
    bool changedSelectedCell;
    bool changedSelectedMovementCell;


    //Bools welche Aktion aktuell ausgewählt is
    public bool movementAusgewaehlt;
	public bool angriffAusgewaehlt;
    public bool smokeAusgewaehlt;
    public bool molotovAusgewaehlt;
    public bool gasAusgewaehlt;
    public bool granateAusgewaehlt;
   

	// Use this for initialization
	void Start () {
        GameObject managerObj = GameObject.Find("Manager");
        manager = managerObj.GetComponent<ManagerSystem>();
        dijSys = managerObj.GetComponent<DijkstraSystem>();
        assist = managerObj.GetComponent<PlayerAssistanceSystem>();
        abilSys = managerObj.GetComponent<AbilitySystem>();


        selectedCell = selectedMovementCell =  null;
        changedSelectedCell = changedSelectedMovementCell = false;

        rotationScript = (CameraRotationScript)FindObjectOfType (typeof(CameraRotationScript));
	}
	

	// Update is called once per frame
	void Update () {
        Ray mouseOver = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hover;
        Physics.Raycast(mouseOver, out hover, Mathf.Infinity,Cellmask);
        
        //Cell getroffen?
        if(hover.collider != null)
        {
            Cell tmp = (Cell)hover.collider.gameObject.GetComponent(typeof(Cell));
            changedSelectedCell = selectedCell != tmp;

            //Cellkomponent vorhanden?  und Haben wir die alte Zelle getroffen, oder noch garkeine ausgewählt?
            if(tmp != null && (changedSelectedCell || selectedCell == null))
            {
                if (selectedCell != null)
                {
                    if (figurGewaehlt && !movement.moving)
                        dijSys.colorCell(selectedCell, attr.actMovRange, attr.weapon.GetComponent<WeaponComponent>().weaponRange);
                    else
                        dijSys.resetSingleCell(selectedCell);
                }
                    selectedCell = tmp;
                    dijSys.highlightSingleCell(selectedCell);

                if(movementAusgewaehlt)
                {
                    if(selectedCell.dij_GesamtKosten <= attr.actMovRange)
                    {
                        changedSelectedMovementCell = selectedMovementCell != selectedCell;
                        selectedMovementCell = selectedCell;
                    }
                }
            }
        }


        if (Input.GetMouseButtonDown (0)) 
		{
            spielerAmZug = manager.getPlayerTurn();  //True = Spieler Eins, False = Spieler zwei
            Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

            Physics.Raycast(clicked, out hit, Mathf.Infinity);
			if(hit.collider != null)
			{                
				if (((hit.collider.gameObject.tag == "FigurSpieler1" && spielerAmZug) 
                    || (hit.collider.gameObject.tag == "FigurSpieler2" && !spielerAmZug)) 
                    && !angriffAusgewaehlt) 
				{
                    //Neuer Spieler angeklickt
					if(player != hit.collider.gameObject)
					{
                        selectFigurine(hit.collider.gameObject);
					}
				}
				if (angriffAusgewaehlt && figurGewaehlt)
				{
					if((hit.collider.gameObject.tag == "FigurSpieler2" && spielerAmZug)
                        || hit.collider.gameObject.tag == "FigurSpieler1" && !spielerAmZug)
					{
                        manager.shoot(player, hit.collider.gameObject);
                        angriffAusgewaehlt = false;
					}
                    else
                    {
                        Debug.Log("Kann nicht angegriffen werden");
                    }
                }
				if (smokeAusgewaehlt)
				{
                    if (selectedCell != null && figurGewaehlt)
                    {
                        StartCoroutine(abilSys.throwGrenade(selectedCell, player, Enums.Effects.Smoke));
						smokeAusgewaehlt = false;
					}
				}
				if (molotovAusgewaehlt)
				{
                    if(selectedCell != null && figurGewaehlt)
                    {
                        StartCoroutine(abilSys.throwGrenade(selectedCell, player, Enums.Effects.Fire));
                        molotovAusgewaehlt = false;
				}
				}
                if (gasAusgewaehlt)
                {
                    if (selectedCell != null && figurGewaehlt)
                    {
                        StartCoroutine(abilSys.throwGrenade(selectedCell, player, Enums.Effects.Gas));
                        gasAusgewaehlt = false;
                    }

                }
                if (granateAusgewaehlt)
                {
                    if (selectedCell != null && figurGewaehlt)
                    {
                        StartCoroutine(abilSys.throwGrenade(selectedCell, player, Enums.Effects.Explosion));
                        granateAusgewaehlt = false;
                    }

                }
            }
		}

        //Wenn begonnen wird rechts zu klicken
        if(Input.GetMouseButtonDown(1))
        {
            if (figurGewaehlt && selectedCell.dij_GesamtKosten <= attr.actMovRange)
            {
                movementAusgewaehlt = true;
                selectedMovementCell = selectedCell;
                ArrayList path = dijSys.getPath(attr.getCurrentCell(), selectedMovementCell);
                assist.PaintWalkPath(path);
            }
        }

        
        if (Input.GetMouseButton(1))
        {
            if (movementAusgewaehlt && changedSelectedMovementCell)
            { 
                ArrayList path = dijSys.getPath(attr.getCurrentCell(), selectedMovementCell);
                assist.PaintWalkPath(path);
            }
        }

        if (Input.GetMouseButtonUp (1)) {
            if (movementAusgewaehlt)
            {
                if(movement.MoveTo(selectedMovementCell))
                {
                    movementAusgewaehlt = false;
                    assist.ClearWalkPath();
                    dijSys.resetDijkstra();
                }
            }
        }

        if(Input.GetKeyDown("a") && player != null)
        {
            angriffAusgewaehlt = !angriffAusgewaehlt;
            if (angriffAusgewaehlt)
                Debug.Log("Angriff ausgewählt");
            else
                Debug.Log("Kein Angriff");
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
        if(Input.GetKeyDown("g"))
        {
            gasAusgewaehlt = true;
        }
        if (Input.GetKeyDown("d"))
        {
            granateAusgewaehlt = true;
        }
        if(Input.GetKeyDown("space"))
        {
			rotationScript.switchCamera();
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isActionSelected()){
                cancelActions();

                Debug.Log("esc 1");
             }
            else if(figurGewaehlt)
            {
                player = null;
                manager.setSelectedFigurine(null);
                figurGewaehlt = false; 
                dijSys.resetDijkstra();

                //ui manager informieren
                    // scheinbar redundant
               // uiManager.deselect();

                Debug.Log("esc 2");
            }
        }
	}

    public void cancelActions()
    {
        angriffAusgewaehlt = false;
        molotovAusgewaehlt = false;
        smokeAusgewaehlt = false;
        movementAusgewaehlt = false;
        gasAusgewaehlt = false;
        GameObject.Find("UiManager(Clone)").GetComponent<UiManager>().activeSkill = Enums.Actions.Cancel;
    }

    public void selectFigurine(GameObject figurine)
    {
        assist.ClearThrowPath();
        assist.ClearWalkPath();
        dijSys.resetDijkstra();
        player = figurine;
        if (figurine == null)
        {
            player = null;
            rotationScript.setNewTarget(null);
            figurGewaehlt = false;
        }
        else
        {
            attr = (AttributeComponent)player.GetComponent(typeof(AttributeComponent));
            movement = (MovementSystem)player.GetComponent(typeof(MovementSystem));
            Cell currentCell = (Cell)attr.getCurrentCell().GetComponent(typeof(Cell));
            dijSys.executeDijsktra(currentCell, attr.actMovRange, attr.items.getCurrentWeapon().weaponRange);
            manager.setSelectedFigurine(figurine);
            figurGewaehlt = true;
            rotationScript.setNewTarget(player);
        }
        
    }

    public bool isActionSelected()
    {
        return granateAusgewaehlt || movementAusgewaehlt || angriffAusgewaehlt || molotovAusgewaehlt ||
                gasAusgewaehlt || smokeAusgewaehlt;
    }
}

