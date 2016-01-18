using UnityEngine;
using System.Collections;

public class ObjectSetter : MonoBehaviour {


	public int x;
	public int z;
	Transform objecttrans;
	GameObject zelle;
	Transform zelletrans;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	public void move(GameObject[,] Zellen)
	{
		objecttrans = (Transform)this.gameObject.GetComponent (typeof(Transform));
		AttributeComponent objectAttr = (AttributeComponent)this.gameObject.GetComponent (typeof(AttributeComponent));
		zelle = Zellen[x, z];
		zelletrans = (Transform) zelle.GetComponent (typeof(Transform));
		Cell zellecell = (Cell)zelle.GetComponent (typeof(Cell));
		zellecell.setOccupied (this.gameObject);
		objecttrans.position = new Vector3 (zelletrans.position.x, objecttrans.position.y, zelletrans.position.z);
		objectAttr.setCurrentCell (zellecell);

	}
}
