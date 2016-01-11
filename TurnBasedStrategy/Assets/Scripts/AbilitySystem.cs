using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour {

	AttributeComponent playerAttr;

	// Use this for initialization
	void Start () {
		playerAttr = (AttributeComponent)this.gameObject.GetComponent (typeof(AttributeComponent));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void throwSmoke(Cell ziel)
	{
		if (ziel.dij_GesamtKosten <= playerAttr.attackRange) {
			GameObject smoke = Instantiate (GameObject.Find ("Smoke_Sub"));
			smoke.transform.position = new Vector3 (ziel.transform.position.x, ziel.transform.position.y + 1, ziel.transform.position.z);
			ziel.smoked = true;
			ziel.upperNeighbour.smoked = true;
			ziel.lowerNeighbour.smoked = true;
			ziel.leftNeighbour.smoked = true;
			ziel.rightNeighbour.smoked = true;
			ziel.upperNeighbour.leftNeighbour.smoked = true;
			ziel.upperNeighbour.rightNeighbour.smoked = true;
			ziel.lowerNeighbour.leftNeighbour.smoked = true;
			ziel.lowerNeighbour.rightNeighbour.smoked = true;
		} else {
			Debug.Log ("OutOfRange");
		}
	}

	public void throwMolotov(Cell ziel)
	{
		if (ziel.dij_GesamtKosten <= playerAttr.attackRange) {
			GameObject fire = Instantiate (GameObject.Find ("Molotov"));
			fire.transform.position = new Vector3 (ziel.transform.position.x, ziel.transform.position.y + 1, ziel.transform.position.z);
			ziel.setOnFire = true;
			ziel.upperNeighbour.setOnFire = true;
			ziel.lowerNeighbour.setOnFire = true;
			ziel.leftNeighbour.setOnFire = true;
			ziel.rightNeighbour.setOnFire = true;
			ziel.upperNeighbour.leftNeighbour.setOnFire = true;
			ziel.upperNeighbour.rightNeighbour.setOnFire = true;
			ziel.lowerNeighbour.leftNeighbour.setOnFire = true;
			ziel.lowerNeighbour.rightNeighbour.setOnFire = true;
		} else {
			Debug.Log ("OutOfRange");

		}
	}


}
