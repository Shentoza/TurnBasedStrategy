using UnityEngine;
using System.Collections;

/*
    Schießen:
    -          Schießen kostet 1AP
    -          Es kann pro Figur je Zug nur 1AP für das Schießen ausgegeben werden
    -          Schießen kostet Munition
    -          Waffe muss nachgeladen werden, falls Munition leer (kostet an AP)
    -          Grundtrefferwahrscheinlichkeit 75% 
    -          Boni auf Trefferwahrscheinlichkeit durch Waffe
    -          Malus auf Trefferwahrscheinlichkeit durch Rauch
    -          Malus auf Trefferwahrscheinlichkeit durch Deckung hoch
    -          Malus auf Trefferwahrscheinlichkeit durch Deckung niedrig
    -          Boni/Malus auf Trefferwahrscheinlichkeit durch Reichweite (niedrig, mittel, hoch)
    -          Grundangriffsreichweite + Waffenreichweite = actualReichweite
    -          Schadensmalus durch Rüstungswert des Verteidigers
    
    Attributecomponent hat (hat neu = *)

    -          public int hp; //Lebenspunkte
    -          public int ap; //Ausgegebene AP
    -          public int maxMovRange; //Maximale Bewegungsreichweite
    -          public int actMovRange; //Aktuelle Bewegungsreichweite
    -          public float minAccuracy; //Mindest Trefferwahrscheinlichkeit
    -          public int attackRange; //Auslagern in Weapon-Component
    -          *public bool canShoot; // Spieler kann nur 1 mal pro Runde schießen
    -          *public bool highCover; // Spieler ist hinter hoher Deckung    
    -          *public bool lowCover; // // Spieler ist niedriger hoher Deckung
    -          *public bool armored; // Spieler hat Rüstung
    -          public GameObject weapon;
    -          public GameObject[] items; //To-Do: Inventory schreiben
    -          public static int maxMoveAP; //Maximale AP die für Movement ausgegeben werden können
    -          public static int maxShootAP; //Maximale AP die Schießen ausgegeben werden können
    -          Cell cell;
*/

public class ShootingSystem : MonoBehaviour
{
    /*
    * Just dummy values
    * TO-DO: Make sure values are chosen so currentShootingAccuracy ends up between [0,1] when hitchance is calculated
    */
    // Layermask is observing Cells (Change in Inspector)
    public LayerMask mask;

    // Defaults
    private const float DEFAULT_ACCURACY = 0.75f;
    private const float NO_BONUS = 0.0f;

    // Bonuses
    private const float SHORT_RANGE_SHOT_BONUS = 0.15f;

    // Maluses
    private const float LONG_RANGE_SHOT_MALUS = -0.15f;
    private const float SMOKE_MALUS = -0.25f;
    private const float LOW_COVER_MALUS = -0.2f;
    private const float HIGH_COVER_MALUS = -0.4f;

    // Ranges
    private const int SHORT_RANGE = 5;
    private const int MID_RANGE = 10;
    
    private float currentShootingAccuracy;

    private AttributeComponent playerAttr;
    private GameObject currentPlayer;
    private Cell currentPlayerCell;

    private AttributeComponent currentTargetAttr;
    private GameObject currentTarget;
    private Cell currentTargetCell;

    

    
    //Muss noch gepusht werden
    //private WeaponComponent weaponAttr;

    // Use this for initialization
    void Start ()
    {
        currentShootingAccuracy = DEFAULT_ACCURACY;

        playerAttr = (AttributeComponent)this.gameObject.GetComponent(typeof(AttributeComponent));
        //weaponAttr = (WeaponComponent)this.gameObject.GetComponent(typeof(WeaponComponent));
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
   
    public bool shoot(GameObject target)
    {
        currentPlayer = this.gameObject;
        currentPlayerCell = this.gameObject.GetComponent<AttributeComponent>().getCurrentCell();

        currentTargetAttr = (AttributeComponent)target.GetComponent(typeof(AttributeComponent));
        currentTarget = target;
        currentTargetCell = target.GetComponent<AttributeComponent>().getCurrentCell();

        if (playerCanShoot())
        {
            float hitChance = chanceOfHittingTarget();
            if(hitChance >= Random.value)
            {
                //TO-DO: Erfolgreicher Treffer
                playerAttr.ap--;
                return true;
            }
            else
            {
                //TO-DO: Nicht erfolgreicher Treffer 
                return false;
            }
        }        

        return false;
    }

    // Can player shoot target
    private bool playerCanShoot()
    {
        if (currentTargetCell.dij_GesamtKosten <= playerAttr.attackRange // + weaponAttr.weaponRange
            && playerAttr.canShoot
            && playerAttr.ap > 0)
        {
            if(true) // In if einsetzen : weaponAttr.currentBulletsInMagazine > 0
            {
                Debug.Log("Keine Kugeln im Magazin vorhanden. Bitte nachladen.");
                return true;
            }
            else
            {
                // TO-DO: Hinweis zum Nachladen anzeigen (?) (Nachlade Button highlighten etc.)
                return false;
            }
        }

        return false;
    }

    // Calculate chance of hitting enemy, clamped to [0,1]
    private float chanceOfHittingTarget()
    {
        //currentShootingAccuracy += weaponAttr.weaponAccuracy;

        if(smokeIsObstructingVision())
        {
            currentShootingAccuracy += ShootingSystem.SMOKE_MALUS;
        }

        if(targetIsCovered())
        {
            float coverMalus = generateCoverMalus();
            currentShootingAccuracy += coverMalus;
        }

        float distanceBonusOrMalus = generateDistanceBonusOrMalus();
        currentShootingAccuracy += distanceBonusOrMalus;

        if(targetHasArmor())
        {
            float armorMalus = generateArmorMalus();
            currentShootingAccuracy += armorMalus;
        }

        return currentShootingAccuracy;
    }

    /* SMOKE related */
    private bool smokeIsObstructingVision()
    {
        if (currentPlayerCell.smoked)
        {
            return true;
        }
        else
        {
            float length = Vector3.Magnitude(currentTargetCell.transform.position - currentPlayerCell.transform.position);
            Ray raycast = new Ray(currentPlayerCell.transform.position - Vector3.up / 8, currentTargetCell.transform.position - currentPlayerCell.transform.position);

            //Mask ist die Maske, die nur Objekte des Layers Cell betrachet
            RaycastHit[] hits = Physics.RaycastAll(raycast, length, mask);
            if (hits.Length > 0)
            {
                // Debug
                foreach (RaycastHit hit in hits)
                    Debug.Log(hit.collider.name + " is smoked");

                return true;
            }
        }

        return false;
    }

    /* COVER related */
    private bool targetIsCovered()
    {
        if (currentTargetAttr.highCover || currentTargetAttr.lowCover)
            return true;

        return false;
    }

    private float generateCoverMalus()
    {        
        if (currentTargetAttr.highCover)
        {
            return ShootingSystem.HIGH_COVER_MALUS;
        }
        
        return ShootingSystem.LOW_COVER_MALUS;        
    }

    /* DISTANCE related */
    private float generateDistanceBonusOrMalus()
    {        
        float distance = Vector3.Magnitude(currentTargetCell.transform.position - currentPlayerCell.transform.position);

        // Short Range Bonus
        if (distance <= ShootingSystem.SHORT_RANGE)
        {
            return ShootingSystem.SHORT_RANGE_SHOT_BONUS;
        }
        // Long Range Malus
        else if (distance > ShootingSystem.MID_RANGE)
        {
            return ShootingSystem.LONG_RANGE_SHOT_MALUS;
        }        

        // Default 
        return NO_BONUS;
    }

    /* ARMOR related */
    private bool targetHasArmor()
    {
        if(currentTargetAttr.armored)        
            return true;          

        return false;
    }

    private float generateArmorMalus()
    {
        //TO-DO generate armor malus
        //NEED: Armorclass, Armorvalues etc.
        return NO_BONUS;
    }
}
