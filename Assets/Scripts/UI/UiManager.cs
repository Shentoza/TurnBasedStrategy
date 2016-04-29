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
    HealthSystem healthSys;
    public int maxAP;

    GUIStyle style;

    public inputSystem input;

    // aktionen enum
    public AttributeComponent activeUnit;
    public List<Enums.Actions> activeUnitSkills;

    DijkstraSystem dijkstra;

   public Enums.Actions activeSkill = 0;

    public bool figureSelected = false;
    
	// Use this for initialization
	void Start () {

        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
        managerSys = GameObject.Find("Manager").GetComponent<ManagerSystem>();
        inventSys = GameObject.Find("Manager").GetComponent<InventorySystem>();
        healthSys = this.GetComponent<HealthSystem>();
        player1AP = player1.GetComponent<PlayerComponent>().actionPoints;
        player2AP = player2.GetComponent<PlayerComponent>().actionPoints;

        //test angaben
        isPlayer1 = managerSys.getPlayerTurn();       


        //setStyle
        style = new GUIStyle();

        dijkstra = (DijkstraSystem)FindObjectOfType(typeof(DijkstraSystem));

        if (isPlayer1)
            input = player1.GetComponent<inputSystem>();
        else
            input = player2.GetComponent<inputSystem>();

        figureUpdate();
    }
	

    // Update is called once per frame
    void Update()
    {

        // welcher spieler ist am zug
        isPlayer1 = managerSys.getPlayerTurn();
        //beschaffe aktionspunkte
        player1AP = player1.GetComponent<PlayerComponent>().actionPoints;
        player2AP = player2.GetComponent<PlayerComponent>().actionPoints;
        //wähle inputchannel
        if (isPlayer1)
            input = player1.GetComponent<inputSystem>();
        else
            input = player2.GetComponent<inputSystem>();

        figureUpdate();
        if (managerSys.selectedFigurine != null && figureSelected == false)
        {
            figureSelected = true;
            activeUnit = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
        }

        if(managerSys.selectedFigurine == null)
        {
            figureSelected = false;
        }
        
        //beschaffe aktive einheit
        if (figureSelected)
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
        actionCancel();
    }

    public void move() {
        actionCancel();

        activeSkill = Enums.Actions.Move;

        if (isPlayer1)
            player1.GetComponent<PlayerComponent>().useAP();
        else
            player2.GetComponent<PlayerComponent>().useAP();
        Debug.Log("Move Aktion");
        AttributeComponent attr = (AttributeComponent)managerSys.getSelectedFigurine().GetComponent(typeof(AttributeComponent));
        input.cancelActions();
        attr.regenerateMovepoints();
        dijkstra.executeDijsktra(attr.getCurrentCell(), attr.actMovRange, attr.weapon.GetComponent<WeaponComponent>().weaponRange);
    }
    public void hit(){
        actionCancel();
        activeSkill = Enums.Actions.Hit;

        input.angriffAusgewaehlt = true;
    }
    public void shoot()
    {
        actionCancel();
        activeSkill = Enums.Actions.Shoot;
        
        input.angriffAusgewaehlt = true;
    }
    public void reload(){
        actionCancel();
        AttributeComponent attr = managerSys.getSelectedFigurine().GetComponent<AttributeComponent>();
        //Es wird nur nachgeladen wenn das Magazin nicht komplett voll ist
        if (attr.items.getCurrentWeapon().currentBulletsInMagazine < attr.items.getCurrentWeapon().magazineSize)
        {  
            activeSkill = Enums.Actions.Reload;
            inventSys.reloadAmmo(managerSys.getSelectedFigurine());
        }
    }
    public void changeWeapon(){
        actionCancel();
        activeSkill = Enums.Actions.ChangeWeapon;
        // Audiofeedback wenn Waffe gewechselt wird
        AudioManager.playMainClick();

        AttributeComponent attr = managerSys.getSelectedFigurine().GetComponent<AttributeComponent>();
        InventoryComponent inv = managerSys.getSelectedFigurine().GetComponent<InventoryComponent>();
        dijkstra.executeDijsktra(attr.getCurrentCell(), attr.actMovRange, attr.weapon.GetComponent<WeaponComponent>().weaponRange);
        inv.isPrimary = !inv.isPrimary;

        attr.model.GetComponent<WeaponHolding>().swapWeapons();
    }
    public void heal() {
        actionCancel();
        activeSkill = Enums.Actions.Heal;
        HealthSystem healthSystem = GameObject.Find("Manager").GetComponent<HealthSystem>();
        if (inventSys.decreaseMedikits(GameObject.Find("Manager").GetComponent<ManagerSystem>().getSelectedFigurine()) > 0)
        {
            // Audiofeedpack wenn heilen klappt
            AudioManager.playMedikit();

            healthSystem.doHeal(null, activeUnit, HealthSystem.MEDIPACK);
        }
    }

    /*
    * Audio nur für Feedback erst einmal hier drin, eigentliche Audio soll bei ausführender Aktion gespielt werden
    */
    public void molotov() {
        actionCancel();
        activeSkill = Enums.Actions.Molotov;
        input.molotovAusgewaehlt = true;
        AttributeComponent temp = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
        dijkstra.executeDijsktra(temp.getCurrentCell(), 0, temp.attackRange);
    }

    public void grenade(){
        actionCancel();
        activeSkill = Enums.Actions.Grenade;
        input.granateAusgewaehlt = true;
        AttributeComponent temp = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
        dijkstra.executeDijsktra(temp.getCurrentCell(), 0, temp.attackRange);
    }

    public void  smoke(){
        actionCancel();
        activeSkill = Enums.Actions.Smoke;
        input.smokeAusgewaehlt = true;
        AttributeComponent temp = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
        dijkstra.executeDijsktra(temp.getCurrentCell(), 0, temp.attackRange);
    }
    public void teargas()
    {
        actionCancel();
        activeSkill = Enums.Actions.Teargas;
        input.gasAusgewaehlt = true;
    }


    public void actionCancel()
    {
        input.cancelActions();
    }

    public bool isFigureSelected()
    {
        return figureSelected;
    }

    private void figureUpdate()
    {
        //wenn einheit ausgewählt ist, markiere sie
        if (managerSys.selectedFigurine != null && figureSelected == false)
        {
            figureSelected = true;
            activeUnit = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
        }
        else if (managerSys.selectedFigurine == null)
        {
            figureSelected = true;
            activeUnit = null;
        }

        //beschaffe skills der aktiven einheit
        if (figureSelected)
        {
            activeUnit = managerSys.selectedFigurine.GetComponent<AttributeComponent>();
            activeUnitSkills = activeUnit.skills;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            actionCancel();
        }
    }
}
