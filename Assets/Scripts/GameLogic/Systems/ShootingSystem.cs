using UnityEngine;
using System.Collections;

public class ShootingSystem : MonoBehaviour
{
    // Calculating accuracy with values between 0,1s
    public const float DEFAULT_ACCURACY = 0.75f;
    public const float NO_BONUS = 0.0f;

    // Bonuses
    public const float SHORT_RANGE_SHOT_BONUS = 0.15f;

    // Maluses
    public const float LONG_RANGE_SHOT_MALUS = -0.15f;
    public const float SMOKE_MALUS = -0.25f;
    public const float LOW_COVER_MALUS = -0.2f;
    public const float HIGH_COVER_MALUS = -0.4f;

    // Ranges
    // Dummy Werte
    public const int SHORT_RANGE = 5;
    public const int MID_RANGE = 10;
    
    private float currentShootingAccuracy;

    private AttributeComponent playerAttr;

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
   
    /*
        public int hp; //Lebenspunkte
        public int ap; //Ausgegebene AP
        public int maxMovRange; //Maximale Bewegungsreichweite
        public int actMovRange; //Aktuelle Bewegungsreichweite
        public float minAccuracy; //Mindest Trefferwahrscheinlichkeit
        public int attackRange; //Auslagern in Weapon-Component
        public bool canShoot; // Spieler kann nur 1 mal pro Runde schießen
        public GameObject weapon;
        public GameObject[] items; //To-Do: Inventory schreiben
        public static int maxMoveAP; //Maximale AP die für Movement ausgegeben werden können
        public static int maxShootAP; //Maximale AP die Schießen ausgegeben werden können
        Cell cell;
    */
    public bool shoot(Cell target)
    {
        if (target.dij_GesamtKosten <= playerAttr.attackRange)
        {
            if(playerCanShoot(target))
            {
                float hitChance = chanceOfHittingTarget(target);
                if(hitChance >= Random.value)
                {
                    //TO-DO: Erfolgreicher Treffer
                    playerAttr.ap--;
                    return true;
                }
                else
                {
                    //TO-DO: Nicht erfolgreicher Treffer Treffer
                    return false;
                }
            }
        }

        return false;
    }

    // Can Player shoot target
    private bool playerCanShoot(Cell target)
    {
        if (target.dij_GesamtKosten <= playerAttr.attackRange // + weaponAttr.weaponRange
            && playerAttr.canShoot
            && playerAttr.ap > 0)
        {
            if(true) // In if einsetzen : weaponAttr.currentBulletsInMagazine > 0
            {
                return true;
            }
            else
            {
                //TO-DO: Hinweis zum Nachladen anzeigen (Nachlade Button highlighten etc.)
                return false;
            }
        }

        return false;
    }

    // Calculate chance of hitting enemy, clamp to [0,1]
    private float chanceOfHittingTarget(Cell target)
    {
        //currentShootingAccuracy += weaponAttr.weaponAccuracy;

        if(smokeIsObstructingVision())
        {
            currentShootingAccuracy += ShootingSystem.SMOKE_MALUS;
        }

        if(targetIsCovered(target))
        {
            float coverMalus = generateCoverMalus(target);
            currentShootingAccuracy += coverMalus;
        }

        float distanceBonusOrMalus = generateDistanceMalus(target);
        currentShootingAccuracy += distanceBonusOrMalus;

        if(targetHasArmor(target))
        {
            float armorMalus = generateArmorMalus(target);
            currentShootingAccuracy += armorMalus;
        }

        return currentShootingAccuracy;
    }

    private bool smokeIsObstructingVision()
    {
        // TO-DO: Check if smoke is obstructing view
        return false;
    }

    private bool targetIsCovered(Cell target)
    {
        // TO-DO: Check if target is covered
        return false;
    }

    private float generateCoverMalus(Cell target)
    {
        /*
        if (target.highCover)
        {
            return ShootingSystem.HIGH_COVER_MALUS;
        }
        */
        return ShootingSystem.LOW_COVER_MALUS;
        
    }

    private float generateDistanceMalus(Cell target)
    {
        /*
        int distance = distanceTo(target);
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
        */

        // Default 
        return NO_BONUS;

    }

    private int distanceTo(Cell target)
    {
        //TO-DO generate distance to target cell
        return 0;
    }

    private bool targetHasArmor(Cell target)
    {
        /*
        if(target.armored)
        {
            return true;
        }
        */

        return false;
    }

    private float generateArmorMalus(Cell target)
    {
        //TO-DO generate armor malus
        return NO_BONUS;
    }
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
    */

}
