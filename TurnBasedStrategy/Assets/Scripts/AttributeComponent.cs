using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

    public int hp;
    public int ap;
    public int movementRange;
    public float accuracy;
    public float attackRange;
    public GameObject weapon;
    public GameObject[] items;
    public static int maxMoveAP;
    public static int maxShootAP;
    Cell cell;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setCurrentCell(Cell cell)
    {
        this.cell = cell;
    }

    public Cell getCurrentCell()
    {
        return cell;
    }
}
