using UnityEngine;
using System.Collections;

public class ObjectSetter : MonoBehaviour {


	public int x;
	public int z;
	BattlefieldCreater bc;
	Transform objecttrans;
	GameObject zelle;
	Transform zelletrans;

	// Use this for initialization
	void Start () {
		bc = (BattlefieldCreater)GameObject.Find ("Plane").GetComponent (typeof(BattlefieldCreater));
		objecttrans = (Transform)this.gameObject.GetComponent (typeof(Transform));
	}
	
	// Update is called once per frame
	void Update () {
	
	}	

	public void move()
	{
		zelle = bc.getZellen () [x, z];
		zelletrans = (Transform) zelle.GetComponent (typeof(Transform));
		objecttrans.position = new Vector3 (zelletrans.position.x, objecttrans.position.y, zelletrans.position.z);
	}
}
