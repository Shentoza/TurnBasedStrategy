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
    public bool highCover; // Spieler ist hinter hoher Deckung
    public bool lowCover; // Spieler ist niedriger hoher Deckung
    public bool armored; // Spieler hat Rüstung
    public GameObject armor;
    public GameObject weapon;
    public InventoryComponent items; //Inventory
    public static int maxMoveAP; //Maximale AP die für Movement ausgegeben werden können
    public static int maxShootAP; //Maximale AP die Schießen ausgegeben werden können
    Cell cell;

    public Enums.Prof profession = 0;

	// Use this for initialization
	void Start ()
    {
        
        hp = 10;
        ap = 2;
        canShoot = true;
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

    public void setProf(int i)
    {
        profession = (Enums.Prof)i;


        // TO DO: zuordnung von waffen / equip
    }

    public void setEquip(Vector4 v)
    {
        items = this.GetComponentInParent<InventoryComponent>();
        items.primaryWeaponType = (Enums.PrimaryWeapons)v.x;
        items.secondaryWeaponType = (Enums.SecondaryWeapons)v.y;
        items.utility1 = (Enums.Equipment)v.z;
        items.utility2 = (Enums.Equipment)v.w;


        // TO DO: zuordnung von waffen / equip
    }

}
