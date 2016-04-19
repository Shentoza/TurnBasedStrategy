using UnityEngine;
using System.Collections;

public class WeaponHolding : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    GameObject leftHandObject;
    GameObject rightHandObject;

    AbilitySystem ability;

    public Enums.Effects selectedGrenade;
    public Enums.PrimaryWeapons weaponPrim;
    public Enums.SecondaryWeapons weaponSecond;

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

    public void setLeftHandItem(GameObject item)
    {
        if (leftHandObject != null)
            Destroy(leftHandObject);

        leftHandObject = (GameObject) Instantiate(item,leftHand.transform.position + item.transform.position,item.transform.rotation);
        leftHandObject.transform.parent = leftHand.transform;

    }

    public void setRightHandItem(GameObject item)
    {
        if (leftHandObject != null)
            Destroy(leftHandObject);

        leftHandObject = (GameObject)Instantiate(item, leftHand.transform.position + item.transform.position, item.transform.rotation);
        leftHandObject.transform.parent = leftHand.transform;
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
