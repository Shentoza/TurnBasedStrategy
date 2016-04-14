using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

    //Stuff vom Manager
    ManagerSystem manager;
    DijkstraSystem dijSys;
    PlayerAssistanceSystem assist;
    AbilitySystem abilSys;
    Animator anim;

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

        manager = (ManagerSystem)managerObj.GetComponent(typeof(ManagerSystem));
        dijSys = (DijkstraSystem)managerObj.GetComponent(typeof(DijkstraSystem));
        assist = (PlayerAssistanceSystem)managerObj.GetComponent(typeof(PlayerAssistanceSystem));
        abilSys = (AbilitySystem)managerObj.GetComponent(typeof(AbilitySystem));


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
                        manager.setSelectedFigurine(hit.collider.gameObject);
                        assist.ClearThrowPath();
                        assist.ClearWalkPath();
                        player = hit.collider.gameObject;
						figurGewaehlt = true;
						attr = (AttributeComponent) player.GetComponent(typeof(AttributeComponent));
                        movement = (MovementSystem)player.GetComponent(typeof(MovementSystem));
                        Cell currentCell = (Cell) attr.getCurrentCell().GetComponent(typeof(Cell));
						dijSys.executeDijsktra(currentCell, attr.actMovRange, attr.weapon.GetComponent<WeaponComponent>().weaponRange);
						rotationScript.setNewTarget(player);

                        //Fuer Animationen
                        anim = (Animator) player.GetComponent(typeof(Animator));
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
						abilSys.throwSmoke(selectedCell, player);
                        cancelActions();
                    }
				}
				if (molotovAusgewaehlt)
				{
                    if(selectedCell != null && figurGewaehlt)
                    { 
						abilSys.throwMolotov(selectedCell, player);
                        cancelActions();
                    }
				}
                if (gasAusgewaehlt)
                {
                    if (selectedCell != null && figurGewaehlt)
                    {
                        abilSys.throwGas(selectedCell, player);
                        cancelActions();
                    }

                }
                if (granateAusgewaehlt)
                {
                    if (selectedCell != null && figurGewaehlt)
                    {
                        abilSys.setThrowDestination(selectedCell);
                        abilSys.throwGrenade(selectedCell, player);
                        anim.SetTrigger("Throw");
                        granateAusgewaehlt = false;
                    }

                }
            }
			else
			{
				figurGewaehlt = false;
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
            cancelActions();
        }
	}

    public void cancelActions()
    {
        Debug.Log("Cancel Actions");
        angriffAusgewaehlt = false;
        molotovAusgewaehlt = false;
        smokeAusgewaehlt = false;
        movementAusgewaehlt = false;
    }
}

