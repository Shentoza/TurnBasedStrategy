using UnityEngine;
using System.Collections;

public class BattlefieldCreater : MonoBehaviour {
	
	Transform transformPlane;
	public float sizeX;
	public float sizeZ;
	GameObject[,] Zellen;
	Material material;

	// Use this for initialization
	void Start () {
		transformPlane = (Transform) this.GetComponent (typeof(Transform));
		material = (Material) Resources.Load("MaterialQuad");
		initiateBattlefield ();
	}

	// Update is called once per frame
	void Update () {
	}

	void initiateBattlefield()
	{
		//Bitte kein 1.4 oder ein vielfaches davon benutzen. Danke!
		sizeX = transformPlane.localScale.x;
		sizeZ = transformPlane.localScale.z;

		//Prüft ob die größe der Plane größer 1 ist
		if (sizeX < 1){
			transformPlane.localScale = new Vector3(1, 0, transformPlane.localScale.z);
		}
		if (sizeZ < 1) {
			transformPlane.localScale = new Vector3(transformPlane.localScale.x ,0 ,1);
		}

		//Runden die größe der Plane auf erste nachkommastelle
		sizeX = Mathf.Round (sizeX * 10);
		sizeX = sizeX / 10;

		sizeZ = Mathf.Round (sizeZ * 10);
		sizeZ = sizeZ / 10;

		int sizeZint = (int)(sizeZ * 10);
		int sizeXint = (int)(sizeX * 10);

		transformPlane.localScale = new Vector3 (sizeX, 0, sizeZ);

		Zellen = new GameObject[(int)(sizeXint), (int)(sizeZint)];

		//Verschiebt Plane in den 0 Punkt(Oben links)
		transformPlane.position = new Vector3 (sizeX * 5, 0, sizeZ * -5);

		//Initialisiert alle Zellen
		for (float z = 0; z > -(sizeZint); z--) {
			for (float x = 0; x < (sizeXint); x++) {
				GameObject zelle = GameObject.CreatePrimitive(PrimitiveType.Quad);
				zelle.transform.Rotate(new Vector3(90, 0, 0));
				zelle.AddComponent<Cell>();
                zelle.tag = "Cell";
                zelle.name = x + "|" + -z;
				zelle.transform.position = new Vector3((x + 0.5f), 1, (z - 0.5f));
				MeshRenderer mr = (MeshRenderer)zelle.GetComponent (typeof(MeshRenderer));
				mr.material = material;

				Zellen[(int)x, (int)-z] = zelle;
			}
		}

		//Setzt die Nachbarn der Zellen
		for(int i = 0; i < (sizeZint); i++)
		{
			for(int j = 0; j < (sizeXint); j++)
			{
				Cell currentCell = (Cell) Zellen[j, i].GetComponent(typeof(Cell));
				GameObject upper = i - 1 >= 0 ? Zellen[j, (i-1)]:null;
				GameObject lower = i + 1 < (sizeZint) ? Zellen[j, (i+1)]:null;
				GameObject left = j - 1 >= 0 ? Zellen[(j-1), i]:null;
				GameObject right = j + 1 < (sizeXint) ? Zellen[(j+1), i]:null;

				currentCell.setNeighbours(upper, left, right, lower);
			}
		}

		//Setzt Objekte an richtiche Stelle
		ObjectSetter[] os = FindObjectsOfType (typeof(ObjectSetter)) as ObjectSetter[];
		foreach (ObjectSetter obs in os) 
		{
			obs.move (Zellen);
		}
	}

	public Cell getCell(int x, int y)
	{
		return (Cell)Zellen [x, y].GetComponent (typeof(Cell));
	}
}
