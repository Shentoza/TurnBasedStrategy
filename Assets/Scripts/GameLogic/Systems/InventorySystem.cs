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
        Debug.Log("Herll");
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        if(inventory.amountMagazines > 0)
        {
            if (GameObject.Find("Manager").GetComponent<ManagerSystem>().getPlayerTurn())
            {
                if (GameObject.Find("Player1").GetComponent<PlayerComponent>().actionPoints > 0)
                {
                    GameObject.Find("Player1").GetComponent<PlayerComponent>().useAP();
                }
            }
            else
            {
                if (GameObject.Find("Player2").GetComponent<PlayerComponent>().actionPoints > 0)
                {
                    GameObject.Find("Player2").GetComponent<PlayerComponent>().useAP();
                }
            }

            inventory.amountMagazines--;
            WeaponComponent weapon = inventory.primary;
            weapon.currentBulletsInMagazine = weapon.magazineSize;
        }
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

   /* 
    * //Wird durch MinenAktion aufgerufen
    public void decreaseMines(GameObject figurine)
    {
        InventoryComponent inventory = figurine.GetComponent<InventoryComponent>();
        inventory.amountMines--;
    }
    */
}
