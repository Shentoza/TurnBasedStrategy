using UnityEngine;
using System.Collections;

public class MovementSystem : MonoBehaviour {

    Transform unitTrans;
    AttributeComponent playerAttr;
    Cell currentCell;
    Cell targetCell;
    ArrayList pfad;
    public float secondsPerCell;
    float deltaSum;
    DijkstraSystem dijkstra;

    public int range;
	// Use this for initialization
	void Start () {
        dijkstra = (DijkstraSystem)FindObjectOfType(typeof(DijkstraSystem));
        unitTrans = (Transform)this.gameObject.GetComponent(typeof(Transform));
        playerAttr = (AttributeComponent)this.gameObject.GetComponent(typeof(AttributeComponent));
        currentCell = playerAttr.getCurrentCell();
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
            pfad = dijkstra.getPath(currentCell, targetCell);
        }
    }

    void continueMovement()
    {
        if (targetCell == null)
            return;

        if(currentCell != targetCell)
        {
            Cell nextCell = (Cell)pfad[pfad.Count-1];
            float progress = Mathf.Clamp01(deltaSum / secondsPerCell);
            Debug.Log(progress);
            playerAttr.transform.position = Vector3.Lerp(currentCell.transform.position, nextCell.transform.position, progress);

            if(progress == 1.0f)
            {
                currentCell.setOccupied(null);
                nextCell.setOccupied(this.gameObject);
                playerAttr.setCurrentCell(nextCell);
                pfad.Remove(nextCell);
                deltaSum = 0.0f;
            }
            deltaSum += Time.deltaTime;
        }
    }
}
