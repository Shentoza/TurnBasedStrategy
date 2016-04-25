using UnityEngine;
using System.Collections;

public class WeaponHolding : MonoBehaviour {

    public GameObject leftHand;
    public GameObject rightHand;

    public GameObject leftHandObjectPrimary;
    public GameObject rightHandObjectPrimary;

    public GameObject leftHandObjectSecondary;
    public GameObject rightHandObjectSecondary;

    public Enums.Effects selectedGrenade;
    public Enums.Stance primaryStance;
    public Enums.Stance secondaryStance;

    bool primary;

    private AbilitySystem ability;
    private GameObject grenadeInstance;
    private ArmoryComponent armory;
    private Animator anim;

    private int animId_iStance;



	// Use this for initialization
	void Start () {
        ability = FindObjectOfType<AbilitySystem>();
        armory = FindObjectOfType<ArmoryComponent>();
        anim = GetComponent<Animator>();
        animId_iStance = Animator.StringToHash("Stance");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public GameObject setLeftHandItem(GameObject item, bool primary)
    {
        GameObject addedItem = null;
        if(primary)
        {
            if (leftHandObjectPrimary != null)
                Destroy(leftHandObjectPrimary);
            addedItem = leftHandObjectPrimary = (GameObject)Instantiate(item, leftHand.transform.position + item.transform.position, item.transform.rotation);
            leftHandObjectPrimary.transform.parent = leftHand.transform;
        }
        else
        {
            if (leftHandObjectSecondary != null)
                Destroy(leftHandObjectSecondary);
            addedItem = leftHandObjectSecondary = (GameObject)Instantiate(item, leftHand.transform.position + item.transform.position, item.transform.rotation);
            leftHandObjectSecondary.transform.parent = leftHand.transform;
        }
        return addedItem;
    }

    public GameObject setRightHandItem(GameObject item, bool primary)
    {
        GameObject addedItem = null;
        if(primary)
        {
            if (rightHandObjectPrimary != null)
                Destroy(rightHandObjectPrimary);
            addedItem = rightHandObjectPrimary = (GameObject)Instantiate(item, rightHand.transform.position + item.transform.position, item.transform.rotation);
            rightHandObjectPrimary.transform.parent = rightHand.transform;
        }
        else
        {
            if (rightHandObjectSecondary != null)
                Destroy(rightHandObjectSecondary);
            addedItem = rightHandObjectSecondary = (GameObject)Instantiate(item, rightHand.transform.position + item.transform.position, item.transform.rotation);
            rightHandObjectSecondary.transform.parent = rightHand.transform;
        }
        return addedItem;
    }

    public void initializeEquip(GameObject itemLeftPrim, GameObject itemRightPrim, GameObject itemLeftSec, GameObject itemRightSec, Enums.Stance primStance, Enums.Stance secondStance)
    {
        primary = true;
        if (itemLeftPrim != null)
            setLeftHandItem(itemLeftPrim,true);

        if (itemRightPrim != null)
            setRightHandItem(itemRightPrim,true);

        if(itemLeftSec != null)


        primaryStance = primStance;
        secondaryStance = secondStance;
    }

    public void grenade_swapToOther()
    {
        if(getActiveStance() == Enums.Stance.Melee1H)
        {

        }
    }


    public void takeIt()
    {
        GameObject[] activeItems = null;
        activeItems = (GameObject[]) getActiveItems().Clone();

        if(getActiveStance() == Enums.Stance.Range2H)
        {

        }
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
        foreach (GameObject g in getActiveItems())
            if (g != null)
                g.SetActive(false);

        primary = !primary;

        foreach (GameObject g in getActiveItems())
            if (g != null)
                g.SetActive(true);

        anim.SetInteger("Stance", (int) getActiveStance());
    }

    private GameObject[] getActiveItems()
    {
        GameObject[] value = { null, null };
        if (primary)
        {
            value[0] = leftHandObjectPrimary;
            value[1] = rightHandObjectPrimary;
        }
        else
        {
            value[0] = leftHandObjectSecondary;
            value[1] = rightHandObjectSecondary;
        }

        return value;
    }

    private Enums.Stance getActiveStance()
    {
        Enums.Stance value = Enums.Stance.None;
        if (primary)
            value = primaryStance;
        else
            value = secondaryStance;

        return value;
    }
}


/*Inventory Component besitzt schon viele der Weapon Gameobjects / Types / Primary Secondary etc. pp. */