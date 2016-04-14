using UnityEngine;
using System.Collections;

public class ObjectSetter : MonoBehaviour {


	public int x;
	public int z;

	Transform objecttrans;
	GameObject zelle;
	Transform zelletrans;
    //float gridHeight;

	// Use this for initialization
	void Start () {
        //BattlefieldCreater battlefield = (BattlefieldCreater)GameObject.FindObjectOfType(typeof(BattlefieldCreater));
        //gridHeight = battlefield.gridHeight;
	}
	
	// Update is called once per frame
	void Update () {
	
	}	


	//Setzt das Objekt an dem das Script hängt auf die Zelle
	public void move(GameObject[,] Zellen)
	{
		//Objekt Transform und AttributeComponent
		objecttrans = (Transform)this.gameObject.GetComponent (typeof(Transform));
		AttributeComponent objectAttr = (AttributeComponent)this.gameObject.GetComponent (typeof(AttributeComponent));

		zelle = Zellen[x, z];
		zelletrans = (Transform) zelle.GetComponent (typeof(Transform));
		Cell zellecell = (Cell)zelle.GetComponent (typeof(Cell));
		zellecell.setOccupied (this.gameObject);
		objecttrans.position = new Vector3 (zelletrans.position.x, objecttrans.position.y, zelletrans.position.z);
		objectAttr.setCurrentCell (zellecell);
	}

	public void moveObject(GameObject[,] Zellen)
	{
		Transform objectTrans = (Transform)this.gameObject.GetComponent (typeof(Transform));
		ObjectComponent objectComp = (ObjectComponent)this.gameObject.GetComponent (typeof(ObjectComponent));

		zelle = Zellen [x, z];
		Cell cell = (Cell)zelle.GetComponent (typeof(Cell));

		if (objectComp.isOccupant) 
		{
			cell.setOccupied (this.gameObject);
		}
		if (objectComp.keineDeckung) 
		{
			cell.keineDeckung = true;
		}
		if (objectComp.niedrigeDeckung) 
		{
			cell.niedrigeDeckung = true;
		}
		if (objectComp.hoheDeckung) 
		{
			cell.hoheDeckung = true;
		}
		if (objectComp.sizeX > 1) 
		{
			for (int i = 0; i < objectComp.sizeX; i++) 
			{

			}
		}
	}
}
