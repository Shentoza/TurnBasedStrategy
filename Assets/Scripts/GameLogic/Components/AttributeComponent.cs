using UnityEngine;
using System.Collections;

public class AttributeComponent : MonoBehaviour {

    public int hp; //Lebenspunkte
    public int ap; //Ausgegebene AP
    public int maxMovRange; //Maximale Bewegungsreichweite
    public int actMovRange; //Aktuelle Bewegungsreichweite
    public float minAccuracy; //Mindest Trefferwahrscheinlichkeit
    public int attackRange; //Auslagern in Weapon-Component
    public bool canShoot; // Spieler kann nur 1 mal pro Runde schießen
    public GameObject weapon;
    public GameObject[] items; //To-Do: Inventory schreiben
    public static int maxMoveAP; //Maximale AP die für Movement ausgegeben werden können
    public static int maxShootAP; //Maximale AP die Schießen ausgegeben werden können
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
