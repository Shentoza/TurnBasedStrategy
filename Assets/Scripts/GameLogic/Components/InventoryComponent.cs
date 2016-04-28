using UnityEngine;
using System.Collections;

public class InventoryComponent : MonoBehaviour {


    //Inventar (primaerwaffe, sekundaerwaffe, equipment1, equipment2) siehe Enums.cs    
    public Enums.PrimaryWeapons primaryWeaponType;
    public Enums.SecondaryWeapons secondaryWeaponType;
    public Enums.Equipment utility1;
    public Enums.Equipment utility2;

    //Primärwaffe
    public WeaponComponent primary;
    //Sekundärwaffe
    public WeaponComponent secondary;
    //Ist Primärwaffe ausgewählt? 
    public bool isPrimary;
    //Anzahl Rauchgranaten
    public int amountSmokes;
    //Anzahl Teargas
    public int amountTeargas;
    //Anzahl Granaten
    public int amountGrenades;
    //Anzahl Molotovs
    public int amountMolotovs;
    //Anzahl Medikits
    public int amountMediKits;
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
