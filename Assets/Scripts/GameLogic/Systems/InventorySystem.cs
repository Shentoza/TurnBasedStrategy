using UnityEngine;
using System.Collections;

public class InventorySystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    //Wird durch reloadAktion aufgerufen
    public void reloadAmmo(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMagazines--;
    }

    //Wird durch GranatAktion aufgerufen
    public void decreaseGrenades(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountGrenades--;
    }

    //Wird durch RauchgranatenAktion aufgerufen
    public void decreaseSmokegrenades(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountSmokes--;
    }

    //Wird durch MolotovAktion aufgerufen
    public void decreaseMolotovs(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMolotovs--;
    }

    //Wird durch MedikitAktion aufgerufen
    public void decreaseMedikits(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMediKits--;
    }

    //Wird durch MinenAktion aufgerufen
    public void decreaseMines(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMines--;
    }

}
