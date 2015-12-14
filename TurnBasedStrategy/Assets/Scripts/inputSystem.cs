using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

	GameObject player;
	DijkstraSystem dijSys;
    public Material begebarMat;
	// Use this for initialization
	void Start () {
		dijSys = (DijkstraSystem) FindObjectOfType (typeof(DijkstraSystem));
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
			if(hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "Player") 
				{
					player = hit.collider.gameObject;
					AttributeComponent playerAttr = (AttributeComponent) player.GetComponent(typeof(AttributeComponent));
                    MovementSystem moveSystem = (MovementSystem)player.GetComponent(typeof(MovementSystem));
					Cell currentCell = (Cell) playerAttr.getCurrentCell().GetComponent(typeof(Cell));
                    //ToDo: fill in attackSystem
					dijSys.executeDijsktra(currentCell,playerAttr.movementRange,playerAttr.attackRange);
				}
                else if (hit.collider.gameObject.tag == "Cell")
                {
                    Cell targetCell = (Cell)hit.collider.gameObject.GetComponent(typeof(Cell));
                    

                    //DEBUG
                    MeshRenderer meshRend = (MeshRenderer)targetCell.gameObject.GetComponent(typeof(MeshRenderer));
                    meshRend.material = begebarMat;
                }
			}
		}
        if(Input.GetMouseButtonDown(1))
        {

            Ray clicked = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast (clicked, out hit);
            if (hit.collider != null)
            {
                if(hit.collider.gameObject.tag == "Cell" && player != null)
                {
                    MovementSystem moveSystem = (MovementSystem)player.GetComponent(typeof(MovementSystem));
                    Cell targetCell = (Cell) hit.collider.gameObject.GetComponent(typeof(Cell));
                    moveSystem.MoveTo(targetCell);
                }
            }
        }
	}
}
