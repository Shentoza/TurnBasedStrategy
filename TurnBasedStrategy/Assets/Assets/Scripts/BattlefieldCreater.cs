using UnityEngine;
using System.Collections;

public class BattlefieldCreater : MonoBehaviour {

	Transform transformPlane;
	float sizeX;
	float sizeZ;
	GameObject[,] Zellen;

	// Use this for initialization
	void Start () {
		transformPlane = (Transform) this.GetComponent (typeof(Transform));
		initiateBattlefield ();
	}

	// Update is called once per frame
	void Update () {
	}

	void initiateBattlefield()
	{
		sizeX = transformPlane.localScale.x;
		sizeZ = transformPlane.localScale.y;

		if (sizeX < 1){
			transformPlane.localScale = new Vector3(1, 0, transformPlane.localScale.z);
		}
		if (sizeZ < 1) {
			transformPlane.localScale = new Vector3(transformPlane.localScale.x ,0 ,1);
		}

		sizeX = Mathf.Round (sizeX * 10);
		sizeX = sizeX / 10;

		sizeZ = Mathf.Round (sizeZ * 10);
		sizeZ = sizeZ / 10;

		Zellen = new GameObject[(int)(sizeX * 10), (int)(sizeZ * 10)];

		transformPlane.position = new Vector3 (sizeX * 5, 0, sizeZ * -5);

		for (float z = 0; z > -(sizeZ*10); z--) {
			for (float x = 0; x < (sizeX*10); x++) {
				GameObject zelle = GameObject.CreatePrimitive(PrimitiveType.Quad);
				zelle.transform.Rotate(new Vector3(90, 0, 0));
				zelle.AddComponent<Cell>();
				zelle.transform.position = new Vector3((x + 0.5f), 0 , (z - 0.5f));

				Zellen[(int)x, (int)-z] = zelle;
			}
		}
		for(int i = 0; i < (sizeZ * 10); i++)
		{
			for(int j = 0; j < (sizeX*10); j++)
			{
				Cell currentCell = (Cell) Zellen[i, j].GetComponent(typeof(Cell));
				GameObject upper = j - 1 >= 0 ? Zellen[i, (j-1)]:null;
				GameObject lower = j + 1 < (sizeX * 10) ? Zellen[i, (j+1)]:null;
				GameObject left = i - 1 >= 0 ? Zellen[(i-1), j]:null;
				GameObject right = i + 1 < (sizeZ * 10) ? Zellen[(i+1), j]:null;

				currentCell.setNeighbours(upper, left, right, lower);
			}
		}
	}
}
