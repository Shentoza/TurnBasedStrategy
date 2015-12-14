using UnityEngine;
using System.Collections;

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
    
	// Use this for initialization
	void Start () {
        dijkstra = (DijkstraSystem)FindObjectOfType(typeof(DijkstraSystem));
        playerAttr = (AttributeComponent)this.gameObject.GetComponent(typeof(AttributeComponent));
    }
	
	// Update is called once per frame
	void Update () {
        continueMovement();
        //Debug.Log("Player:"+playerAttr.transform.position+"  Cell:"+playerAttr.getCurrentCell().transform.position);
	}

    public void MoveTo(Cell target)
    {
        if(target.dij_GesamtKosten <= playerAttr.movementRange)
        {
            if(!yHeightSet)
            { 
                yHeight = playerAttr.transform.position.y - playerAttr.getCurrentCell().transform.position.y;
                yHeightSet = true;
            }
            targetCell = target;
            startingCell = playerAttr.getCurrentCell();
            pfad = dijkstra.getPath(playerAttr.getCurrentCell(), target);
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
            }
            deltaSum += Time.deltaTime;
        }
    }
}
