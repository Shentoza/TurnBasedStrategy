using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

    ManagerSystem manager;
    PlayerAssistanceSystem assist;
	GameObject player;

	Cell selectedCell;
    Cell selectedMovementCell;
    bool changedSelectedCell;
    bool changedSelectedMovementCell;

    public DijkstraSystem dijSys;
    AttributeComponent attr;
    public AbilitySystem abilSys;
    MovementSystem movement;
	CameraRotationScript rotationScript;

	bool figurGewaehlt;
    bool spielerAmZug;

    public bool movementAusgewaehlt;
	public bool angriffAusgewaehlt;
    public bool smokeAusgewaehlt;
    public bool molotovAusgewaehlt;
    public LayerMask Cellmask;
    public bool gasAusgewaehlt;
    public bool granateAusgewaehlt;

    MovementSystem moveSys;

	// Use this for initialization
	void Start () {
        selectedCell = null;
        dijSys = (DijkstraSystem) FindObjectOfType (typeof(DijkstraSystem));
        manager = GameObject.Find("Manager").GetComponent<ManagerSystem>();
		rotationScript = (CameraRotationScript)FindObjectOfType (typeof(CameraRotationScript));
        assist = (PlayerAssistanceSystem)GameObject.Find("Manager").GetComponent(typeof(PlayerAssistanceSystem));
        changedSelectedCell = false;
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
						smokeAusgewaehlt = false;
					}
				}
				if (molotovAusgewaehlt)
				{
                    if(selectedCell != null && figurGewaehlt)
                    { 
						abilSys.throwMolotov(selectedCell, player);
						molotovAusgewaehlt = false;
					}
				}
                if (gasAusgewaehlt)
                {
                    if (selectedCell != null && figurGewaehlt)
                    {
                        abilSys.throwGas(selectedCell, player);
                        gasAusgewaehlt = false;
                    }

                }
                if (granateAusgewaehlt)
                {
                    if (selectedCell != null && figurGewaehlt)
                    {
                        abilSys.throwGrenade(selectedCell, player);
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
                movement.MoveTo(selectedMovementCell);
                movementAusgewaehlt = false;
                assist.ClearWalkPath();
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

