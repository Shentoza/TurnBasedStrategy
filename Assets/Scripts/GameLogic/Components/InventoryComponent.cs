using UnityEngine;
using System.Collections;

public class InventoryComponent : MonoBehaviour {


    //Inventar (primärwaffe, sekundärwaffe, equipment, equipment) siehe Enums.cs    
    public Enums.PrimaryWeapons primaryWeaponType;
    public Enums.SecondaryWeapons secondaryWeaponType;
    public Enums.Equipment utility1;
    public Enums.Equipment utility2;

    //Primärwaffe
    public WeaponComponent primary;
    //Sekundärwaffe
    public WeaponComponent secondary;
    //Ist Primärwaffeausgewählt? 
    public bool isPrimary;
    public GameObject equipment;
    //Anzahl Rauchgranaten
    public int amountSmokes;
    //Anzahl Granaten
    public int amountGrenades;
    //Anzahl Molotovs
    public int amountMolotovs;
    //Anzahl Medikits
    public int amountMediKits;
    //Anzahl Minen
    public int amountMines;
    //Anzahl Magazine
    public int amountMagazines;



	// Use this for initialization
	void Start () {
        isPrimary = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
