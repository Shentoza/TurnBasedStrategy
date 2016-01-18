using UnityEngine;
using System.Collections;

public class ShootingSystem : MonoBehaviour
{
    // Calculating accuracy with values between 0,1s
    public const float DEFAULT_ACCURACY = 0.75f;

    private AttributeComponent playerAttr;
    //private WeaponComponent weaponAttr;

    // Use this for initialization
    void Start ()
    {
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
    
    public bool shoot(Cell target)
    {
        if (target.dij_GesamtKosten <= playerAttr.attackRange)
        {
            if(playerCanShoot(target))
            {
                float hitChance = chanceOfHittingTarget();
                if(hitChance >= Random.value)
                {

                }
                else
                {
                    return false;
                }

            }
        }

        return false;
    }

    // Can Player shoot target
    private bool playerCanShoot(Cell target)
    {
        if (target.dij_GesamtKosten <= playerAttr.attackRange
            && playerAttr.canShoot
            && playerAttr.ap > 0)
        {
            if(weaponAttr.currentBulletsInMagazine > 0)
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
    private float chanceOfHittingTarget()
    {
        return 0.0f;
    }

    
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
