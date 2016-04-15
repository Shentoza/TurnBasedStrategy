using UnityEngine;
using System.Collections;

public class throwObject : MonoBehaviour {

    public GameObject leftHand;
    AbilitySystem ability;

    public Enums.Effects selectedGrenade;
    GameObject grenadeInstance;

    ArmoryComponent armory;

    public float floatingTime;
    public float range;

	// Use this for initialization
	void Start () {
        ability = (AbilitySystem)FindObjectOfType(typeof(AbilitySystem));
        armory = (ArmoryComponent)FindObjectOfType(typeof(ArmoryComponent));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeIt()
    {
        //Ausm Abilitysystem geworfene Art Granate ziehen
        switch(selectedGrenade)
        {
            case Enums.Effects.Explosion:
                grenadeInstance = (GameObject)Instantiate(armory.grenade);
                break;
            case Enums.Effects.Fire:
                grenadeInstance = (GameObject)Instantiate(armory.molotov);
                break;
            case Enums.Effects.Gas:
                grenadeInstance = (GameObject)Instantiate(armory.smoke);
                break;
            case Enums.Effects.Smoke:
                grenadeInstance = (GameObject)Instantiate(armory.gas);
                break;
        }
        grenadeInstance.transform.position = leftHand.transform.position;
        grenadeInstance.transform.parent = leftHand.transform;
    }

    public void throwIt()
    {
        ability.throwIt(grenadeInstance);
    }

    public void setNextGrenade(Enums.Effects type)
    {
        selectedGrenade = type;
    }
}
