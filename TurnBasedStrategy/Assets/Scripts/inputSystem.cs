using UnityEngine;
using System.Collections;

public class inputSystem : MonoBehaviour {

	GameObject player;
	DijkstraSystem dijSys;
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
				if (hit.collider.gameObject.tag == "Player") 
				{
					player = hit.collider.gameObject;
					AttributeComponent playerAttr = (AttributeComponent) player.GetComponent(typeof(AttributeComponent));
					Cell currentCell = (Cell) playerAttr.getCurrentCell().GetComponent(typeof(Cell));
					dijSys.executeDijsktra(currentCell);
				}
			}
		}
	}
}
