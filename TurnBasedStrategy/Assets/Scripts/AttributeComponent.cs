using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

	Cell currentCell;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setCurrentCell(Cell currCell)
	{
		currentCell = currCell;
	}

	public Cell getCurrentCell()
	{
		return currentCell;
	}
}
