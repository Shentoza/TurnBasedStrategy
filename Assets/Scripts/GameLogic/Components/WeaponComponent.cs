using UnityEngine;
using System.Collections;

public class WeaponComponent : MonoBehaviour {

    public bool isPrimary;
    public int damage;
    public int weaponRange;
    public int currentBulletsInMagazine;
    public int magazineSize;
    public int rangeMod;
    public float weaponAccuracy;
    public string name;

	// Use this for initialization
	void Start ()
    {
        /* Testing */
        damage = 8;
        weaponRange = 2;
        currentBulletsInMagazine = 3;
        magazineSize = 5;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
