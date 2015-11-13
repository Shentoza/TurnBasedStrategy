using UnityEngine;
using System.Collections;

public class Cell : MonoBehaviour {

	public GameObject upperNeighbour;
	public GameObject leftNeighbour;
	public GameObject rightNeightbour;
	public GameObject lowerNeighbour;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setNeighbours(GameObject u, GameObject le, GameObject r, GameObject lo)
	{
		upperNeighbour = u;
		leftNeighbour = le;
		rightNeightbour = r;
		lowerNeighbour = lo;

	}
}
