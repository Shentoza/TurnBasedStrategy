using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class UiManager : MonoBehaviour {


    //dummys
   public bool isPlayer1;
   public int player1AP;
   public int player2AP;

    GameObject player1;
    GameObject player2;

    InventorySystem inventSys;
    ManagerSystem managerSys;
    public int maxAP;

    GUIStyle style;

    inputSystem input;

    // aktionen enum
    public AttributeComponent activeUnit;
    public List<Enums.Actions> activeUnitSkills;

    DijkstraSystem dijkstra;


	// Use this for initialization
	void Start () {

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        managerSys = GameObject.Find("Manager").GetComponent<ManagerSystem>();
        inventSys = GameObject.Find("Manager").GetComponent<InventorySystem>();

        player1AP = player1.GetComponent<PlayerComponent>().actionPoints;
        player2AP = player2.GetComponent<PlayerComponent>().actionPoints;

        //test angaben
        isPlayer1 = managerSys.getPlayerTurn();       


        //getActiveUnitSkills
        activeUnit = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
        activeUnitSkills = activeUnit.skills;

        //setStyle
        style = new GUIStyle();

        dijkstra = (DijkstraSystem)FindObjectOfType(typeof(DijkstraSystem));

    }
	

    // Update is called once per frame
    void Update()
    {
        isPlayer1 = managerSys.getPlayerTurn();
        player1AP = player1.GetComponent<PlayerComponent>().actionPoints;
        player2AP = player2.GetComponent<PlayerComponent>().actionPoints;
        if (isPlayer1)
            input = player1.GetComponent<inputSystem>();
        else
            input = player2.GetComponent<inputSystem>();

        //beschaffe aktive einheit
        if (activeUnit)
        {
            activeUnit = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
            activeUnitSkills = activeUnit.skills;
        }
    }


   

    // verhindert das zu viele waffenoptionen angezeigt werden
    public List<Enums.Actions> getActiveUnitSkills()
    {
        List<Enums.Actions> activeSkills = new List<Enums.Actions>();
       
       //kann gehen
        if (activeUnitSkills.Contains(Enums.Actions.Move))
        {
            activeSkills.Add(Enums.Actions.Move);
        }

       //hat Primärwaffe angelegt
        if (activeUnit.items.isPrimary)
        {
            //Schlagwaffe
            if (activeUnitSkills.Contains(Enums.Actions.Hit))
            {
                activeSkills.Add(Enums.Actions.Hit);
            }
            //Schusswaffe
            else
            {
                if (activeUnitSkills.Contains(Enums.Actions.Shoot))
                {
                    activeSkills.Add(Enums.Actions.Shoot);
                    activeSkills.Add(Enums.Actions.Reload);
                }
            }
        }
        //Sekundärwaffe Angelegt
        else
        {
            // schusswaffe
            if (activeUnit.items.secondaryWeaponType != Enums.SecondaryWeapons.None)
            {
                if (activeUnitSkills.Contains(Enums.Actions.Shoot))
                {
                    activeSkills.Add(Enums.Actions.Shoot);
                }
            }
        }

       //können waffen gewechselt werden
        if (activeUnitSkills.Contains(Enums.Actions.ChangeWeapon))
        {
            activeSkills.Add(Enums.Actions.ChangeWeapon);
        }

        //Heal
        if (activeUnitSkills.Contains(Enums.Actions.Heal))
        {
            activeSkills.Add(Enums.Actions.Heal);
        }

        //Molotov
        if (activeUnitSkills.Contains(Enums.Actions.Molotov))
        {
            activeSkills.Add(Enums.Actions.Molotov);
        }

        //Grenade
        if (activeUnitSkills.Contains(Enums.Actions.Grenade))
        {
            activeSkills.Add(Enums.Actions.Grenade);
        }

        //Smoke
        if (activeUnitSkills.Contains(Enums.Actions.Smoke))
        {
            activeSkills.Add(Enums.Actions.Smoke);
        }

        //Teargas
        if (activeUnitSkills.Contains(Enums.Actions.Teargas))
        {
            activeSkills.Add(Enums.Actions.Teargas);
        }


        return activeSkills;
    }

    public GUIStyle getStyle()
    {
        return style;
    }



    public void endTurn()
    {
        managerSys.setPlayerTurn();
    }

    public void move() {
        Debug.Log("Move Aktion");
        AttributeComponent attr = (AttributeComponent)managerSys.getSelectedFigurine().GetComponent(typeof(AttributeComponent));
        input.cancelActions();
        attr.regenerateMovepoints();
        input.dijSys.executeDijsktra(attr.getCurrentCell(), attr.actMovRange, attr.weapon.GetComponent<WeaponComponent>().rangeMod);
    }
    public void hit(){
        shoot();
    }
    public void shoot()
    {
        input.cancelActions();
        input.angriffAusgewaehlt = true;
    }
    public void reload(){
        input.cancelActions();
        inventSys.reloadAmmo(GameObject.Find("Manager").GetComponent<ManagerSystem>().getSelectedFigurine());
    }
    public void changeWeapon(){
        input.cancelActions();

        AttributeComponent attr = (AttributeComponent)managerSys.getSelectedFigurine().GetComponent(typeof(AttributeComponent));
        InventoryComponent inv = (InventoryComponent)managerSys.getSelectedFigurine().GetComponent(typeof(InventoryComponent));
        dijkstra.executeDijsktra(attr.getCurrentCell(), attr.actMovRange, attr.weapon.GetComponent<WeaponComponent>().rangeMod);
        inv.isPrimary = !inv.isPrimary;
    }
    public void heal() {
        input.cancelActions();
    }
    public void molotov() {
        input.cancelActions();
    }
    public void grenade(){
        input.cancelActions();
    }
    public void  smoke(){
        input.cancelActions();
    }
    public void teargas()
    {
        input.cancelActions();
    }



}
