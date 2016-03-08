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

    ArmoryComponent armory;


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

        armory = GameObject.Find("Armory").GetComponent<ArmoryComponent>();
        items = this.GetComponent<InventoryComponent>();

        profession = (Enums.Prof)i;


        if (profession == Enums.Prof.Riot)
        {
       
            items.primary = Instantiate(armory.Pipe).GetComponent<WeaponComponent>();

        }
        else if (profession == Enums.Prof.Soldier)
        {
            items.primary = Instantiate(armory.Pistol).GetComponent<WeaponComponent>();
            items.amountGrenades = 1;
        }
        else if (profession == Enums.Prof.HeavyGunner)
        {
            items.primary = Instantiate(armory.MG).GetComponent<WeaponComponent>();
        }
        else if (profession == Enums.Prof.Support)
        {
            items.primary = Instantiate(armory.AssaultRifle).GetComponent<WeaponComponent>();
            items.secondary = Instantiate(armory.Pistol).GetComponent<WeaponComponent>();
            items.amountMediKits = 2;
        }
        if (profession == Enums.Prof.Sniper)
        {
            items.primary = Instantiate(armory.Sniper).GetComponent<WeaponComponent>();
            items.secondary = Instantiate(armory.Pistol).GetComponent<WeaponComponent>();
            items.amountGrenades = 1;
        }
    }

    public void setEquip(Vector4 v)
    {
        armory = GameObject.Find("Armory").GetComponent<ArmoryComponent>();

        items = this.GetComponent<InventoryComponent>();
        

        //primärwaffe
        items.primaryWeaponType = (Enums.PrimaryWeapons)v.x;
        if (items.primaryWeaponType == Enums.PrimaryWeapons.Pipe)
        {

           items.primary = Instantiate(armory.Pipe).GetComponent<WeaponComponent>();
            
        }
        else if (items.primaryWeaponType == Enums.PrimaryWeapons.Shotgun)
        {

            items.primary = Instantiate(armory.Shotgun).GetComponent<WeaponComponent>();
        }
        else if (items.primaryWeaponType == Enums.PrimaryWeapons.HuntingRifle)
        {

            items.primary = Instantiate(armory.HuntingRifle).GetComponent<WeaponComponent>();
        }
        else if (items.primaryWeaponType == Enums.PrimaryWeapons.AssaultRifle)
        {

            items.primary = Instantiate(armory.AssaultRifle).GetComponent<WeaponComponent>();
        }
        else if (items.primaryWeaponType == Enums.PrimaryWeapons.MG)
        {

            items.primary = Instantiate(armory.MG).GetComponent<WeaponComponent>();
        }
        else if (items.primaryWeaponType == Enums.PrimaryWeapons.Sniper)
        {

            items.primary = Instantiate(armory.Sniper).GetComponent<WeaponComponent>();
        }


        //sekundärwaffe
        items.secondaryWeaponType = (Enums.SecondaryWeapons)v.y;
        if (items.secondaryWeaponType == Enums.SecondaryWeapons.Pistol)
        {

            items.secondary = Instantiate(armory.Pistol).GetComponent<WeaponComponent>();
        }
        else if (items.secondaryWeaponType == Enums.SecondaryWeapons.Mortar)
        {

            items.secondary = Instantiate(armory.Mortar).GetComponent<WeaponComponent>();
        } 
        else if (items.secondaryWeaponType == Enums.SecondaryWeapons.RPG)
        {

            items.secondary = Instantiate(armory.RPG).GetComponent<WeaponComponent>();
        }



        //utility1
        items.utility1 = (Enums.Equipment)v.z;
        if (items.utility1 == Enums.Equipment.Kevlar)
        {
            hp += 10;
        }
        else if (items.utility1 == Enums.Equipment.Helmet)
        {
            hp += 10;
        }
        else if (items.utility1 == Enums.Equipment.SuicideBelt)
        {

        }
        else if (items.utility1 == Enums.Equipment.Scope)
        {

        }
        else if (items.utility1 == Enums.Equipment.MediPack)
        {
            items.amountMediKits = 2;
        }
        else if (items.utility1 == Enums.Equipment.Mine)
        {
            items.amountMines = 2;
        }
        else if (items.utility1 == Enums.Equipment.Rocks)
        {

        }
        else if (items.utility1 == Enums.Equipment.Mollotov)
        {
            items.amountMolotovs = 2;
        }
        else if (items.utility1 == Enums.Equipment.Grenade)
        {
            items.amountGrenades = 1;
        }
        else if (items.utility1 == Enums.Equipment.SmokeGreneade)
        {
            items.amountSmokes = 2;
        }




        //utility 2
        items.utility2 = (Enums.Equipment)v.w;
        if (items.utility2 == Enums.Equipment.Kevlar)
        {
            hp += 10;
        }
        else if (items.utility2 == Enums.Equipment.Helmet)
        {
            hp += 10;
        }
        else if (items.utility2 == Enums.Equipment.SuicideBelt)
        {

        }
        else if (items.utility2 == Enums.Equipment.Scope)
        {

        }
        else if (items.utility2 == Enums.Equipment.MediPack)
        {
            items.amountMediKits = 2;
        }
        else if (items.utility2 == Enums.Equipment.Mine)
        {
            items.amountMines = 2;
        }
        else if (items.utility2 == Enums.Equipment.Rocks)
        {

        }
        else if (items.utility2 == Enums.Equipment.Mollotov)
        {
            items.amountMolotovs = 2;
        }
        else if (items.utility2 == Enums.Equipment.Grenade)
        {
            items.amountGrenades = 1;
        }
        else if (items.utility2 == Enums.Equipment.SmokeGreneade)
        {
            items.amountSmokes = 2;
        }

        
    }

}
