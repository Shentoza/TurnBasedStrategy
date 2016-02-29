using UnityEngine;
using System.Collections;

public class InventorySystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void decreaseAmmo(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();

        inventory.bulletsInMagazine--;
    }

    public void reloadAmmo(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMagazines--;
    }

    public void decreaseGrenades(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountGrenades--;
    }

    public void decreaseSmokegrenades(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountSmokes--;
    }

    public void decreaseMolotovs(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMolotovs--;
    }

    public void decreaseMedikits(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMediKits--;
    }

    public void decreaseMines(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMines--;
    }
}
