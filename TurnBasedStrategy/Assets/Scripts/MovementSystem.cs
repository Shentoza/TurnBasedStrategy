using UnityEngine;
using System.Collections;

public class MovementSystem : MonoBehaviour {

    AttributeComponent playerAttr;
    DijkstraSystem dijkstra;

    public float secondsPerCell;

    ArrayList pfad;
    Cell targetCell;

    float deltaSum;
    
	// Use this for initialization
	void Start () {
        dijkstra = (DijkstraSystem)FindObjectOfType(typeof(DijkstraSystem));
        playerAttr = (AttributeComponent)this.gameObject.GetComponent(typeof(AttributeComponent));
	}
	
	// Update is called once per frame
	void Update () {
        continueMovement();
	}

    public void MoveTo(Cell target)
    {
        if(target.dij_GesamtKosten <= range)
        {
            targetCell = target;
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
            Debug.Log(progress);
            playerAttr.transform.position = Vector3.Lerp(currentCell.transform.position, nextCell.transform.position, progress);

            if(progress == 1.0f)
            {
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
