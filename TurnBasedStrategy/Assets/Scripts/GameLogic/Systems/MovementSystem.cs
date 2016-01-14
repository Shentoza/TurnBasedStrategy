using UnityEngine;
using System.Collections;
using System;

public class MovementSystem : MonoBehaviour {

    AttributeComponent playerAttr;
    DijkstraSystem dijkstra;

    public float secondsPerCell;

    ArrayList pfad;
    Cell startingCell;
    Cell targetCell;
    float yHeight;
    bool yHeightSet = false;

    float deltaSum;

    private float turningSpeed = 360.0f;
    private float startAngle;
    private bool startAngleSet;
    private float turningDirection;
    
	// Use this for initialization
	void Start () {
        dijkstra = (DijkstraSystem)FindObjectOfType(typeof(DijkstraSystem));
        playerAttr = (AttributeComponent)this.gameObject.GetComponent(typeof(AttributeComponent));
        startAngle = 0.0f;
        startAngleSet = false;
    }
	
	// Update is called once per frame
	void Update () {
        continueMovement();
	}

    public void MoveTo(Cell target)
    {
        if(target.dij_GesamtKosten <= playerAttr.actMovRange)
        {
            if(!yHeightSet)
            { 
                yHeight = playerAttr.transform.position.y - playerAttr.getCurrentCell().transform.position.y;
                yHeightSet = true;
            }
            targetCell = target;
            startingCell = playerAttr.getCurrentCell();
            pfad = dijkstra.getPath(playerAttr.getCurrentCell(), target);
            dijkstra.colorAllCells(true, 0, 0);
            playerAttr.actMovRange -= target.dij_GesamtKosten;
        }
    }

    void continueMovement()
    {
        if (targetCell == null)
            return;

        if(playerAttr.getCurrentCell() != targetCell)
        {
            Cell currentCell = playerAttr.getCurrentCell();

            Cell nextCell = (Cell)pfad[pfad.Count-1];

            if (!checkRotation(currentCell,nextCell))
                return;

            float progress = Mathf.Clamp01(deltaSum / secondsPerCell);

            float parabelY = 0.0f;
            //Startet?
            if(playerAttr.getCurrentCell() == startingCell)
            {
                //Nächste Zelle das Ziel?
                if(nextCell == targetCell)
                {
                    parabelY = -6.0f * progress * progress + 6.0f * progress +yHeight;
                }
                else
                {
                    parabelY = -4.5f * progress * progress + 5.25f * progress + yHeight;
                }
            }
            else
            {
                //Nächste Zelle das Ziel?
                if(nextCell == targetCell)
                {
                    parabelY = parabelY = -4.5f * progress * progress + 3.75f * progress + 0.75f + yHeight;
                }
                else
                {
                    parabelY = parabelY = -3.0f * progress * progress + 3.0f * progress + 0.75f + yHeight;
                }
            }
            Vector3 yVector = new Vector3(0, parabelY, 0);
            Vector3 moveVector = Vector3.Lerp(currentCell.transform.position, nextCell.transform.position, progress);
            playerAttr.transform.position = moveVector + yVector;

            if (progress == 1.0f)
            {
                if (currentCell == targetCell)
                    playerAttr.transform.position = targetCell.transform.position;
                currentCell.setOccupied(null);
                currentCell = nextCell;

                nextCell.setOccupied(this.gameObject);
                playerAttr.setCurrentCell(nextCell);
                deltaSum = 0.0f;
                pfad.RemoveAt(pfad.Count - 1);

                //Zielerreicht
                if(currentCell == targetCell)
                {
                    //MovementRange abziehen(?)
                    dijkstra.executeDijsktra(currentCell, playerAttr.actMovRange, playerAttr.attackRange);
                }
            }
            deltaSum += Time.deltaTime;
        }
    }

    /*
    Returns: True wenn er in die Richtung des Pfades schaut, false sonst
    Interpoliert zwischen der aktuellen Position, und der RIchtung des Pfades und dreht Playerattr.
    */
    public bool checkRotation(Cell currentCell, Cell targetCell)
    {

        //Richtung, in die der Pfad von currentCell zu  targetCell schaut.
        Vector3 walkingDirection = targetCell.transform.position - currentCell.transform.position;

        //Aktuelle Richtung in die wir schauen
        Vector3 facingDirection = playerAttr.transform.forward;


        //Winkel zwischen unseren zwei Vektoren
        float angle = Vector3.Angle(walkingDirection.normalized, facingDirection);

        //Schaue ich schon in die passende Richtung?
        if (angle != 0.0f)
        {
            if (!startAngleSet)
            {
                if (Vector3.Cross(walkingDirection.normalized, facingDirection).y < 0.0f)
                {
                    turningDirection = 1.0f;
                }
                else
                {
                    turningDirection = -1.0f;
                }

                startAngle = angle;
                startAngleSet = true;
            }

            float yRotation = Mathf.Clamp(Time.deltaTime * turningSpeed * turningDirection, -angle, angle);
            angle += yRotation;
            Vector3 euler = playerAttr.transform.rotation.eulerAngles;
            euler.y += yRotation;
            playerAttr.transform.rotation = Quaternion.Euler(euler);
        }
        else
            startAngleSet = false;

        return angle == 0.0f;
    }
}
