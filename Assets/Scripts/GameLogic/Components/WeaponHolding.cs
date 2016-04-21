using UnityEngine;
using System.Collections;

public class WeaponHolding : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftHandObjectPrimary;
    public GameObject rightHandObjectPrimary;

    public GameObject leftHandObjectSecondary;
    public GameObject rightHandObjectSecondary;

    AbilitySystem ability;

    public Enums.Effects selectedGrenade;
    public Enums.Stance primaryStance;
    public Enums.Stance secondaryStance;

    GameObject grenadeInstance;
    ArmoryComponent armory;

	// Use this for initialization
	void Start () {
        ability = (AbilitySystem)FindObjectOfType(typeof(AbilitySystem));
        armory = (ArmoryComponent)FindObjectOfType(typeof(ArmoryComponent));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject setLeftHandItem(GameObject item)
    {
        if (leftHandObjectPrimary != null)
            Destroy(leftHandObjectPrimary);

        leftHandObjectPrimary = (GameObject) Instantiate(item,leftHand.transform.position + item.transform.position,item.transform.rotation);
        leftHandObjectPrimary.transform.parent = leftHand.transform;

        return leftHandObjectPrimary;

    }

    public GameObject setRightHandItem(GameObject item)
    {
        if (rightHandObjectPrimary != null)
            Destroy(rightHandObjectPrimary);

        rightHandObjectPrimary = (GameObject)Instantiate(item, rightHand.transform.position + item.transform.position, item.transform.rotation);
        rightHandObjectPrimary.transform.parent = rightHand.transform;

        return rightHand;
    }

    public void initializeEquip(GameObject addedItemLeft, GameObject addedItemRight, Enums.Stance primStance, Enums.Stance secondStance)
    {
        if (addedItemLeft != null)
            setLeftHandItem(addedItemLeft);

        if (rightHandObjectPrimary != null)
            setRightHandItem(addedItemRight);

        primaryStance = primStance;
        secondaryStance = secondStance;
    }

    public void takeIt()
    {
        if(leftHandObjectPrimary != null)
            leftHandObjectPrimary.SetActive(false);
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
        if (leftHandObjectPrimary != null)
            leftHandObjectPrimary.SetActive(true);
    }

    public void setNextGrenade(Enums.Effects type)
    {
        selectedGrenade = type;
    }

    public void swapWeapons()
    {

    }
}


/*Inventory Component besitzt schon viele der Weapon Gameobjects / Types / Primary Secondary etc. pp. */