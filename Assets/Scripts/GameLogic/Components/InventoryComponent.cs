using UnityEngine;
using System.Collections;

public class InventoryComponent : MonoBehaviour {

    public WeaponComponent primary;
    public WeaponComponent secondary;
    public GameObject equipment;
    public int amountSmokes;
    public int amountGrenades;
    public int amountMolotovs;
    public int amountMediKits;
    public int amountMines;
    public int amountMagazines;
    public int bulletsInMagazine;


	// Use this for initialization
	void Start () {
        amountSmokes = 0;
        amountMolotovs = 0;
        amountMediKits = 0;
        amountMines = 0;
        amountMagazines = 0;
        bulletsInMagazine = 0;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
