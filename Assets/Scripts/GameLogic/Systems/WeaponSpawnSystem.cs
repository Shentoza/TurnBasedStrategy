using UnityEngine;
using System.Collections;

public class WeaponSpawnSystem : MonoBehaviour
{
    private const int WEAPONSLOTS = 5;

    public GameObject schlagstock;
    public GameObject pistole;
    public GameObject maschinengewehr;
    public GameObject riotGun;
    public GameObject scharfschuetzengewehr;

    private GameObject[] weapons;

    private GameObject weaponCopy;

    private bool weaponChanged;

    private int nextWeapon;
    // Use this for initialization
    void Start()
    {
        weaponChanged = false;
        nextWeapon = 1;

        weapons = new GameObject[] { pistole, schlagstock, maschinengewehr, riotGun, scharfschuetzengewehr };
        weaponCopy = Instantiate(schlagstock, transform.position, transform.rotation) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(nextWeapon);
        if (weaponChanged)
        {
            changeWeapon();
        }
        if (Input.GetKeyDown("c"))
        {
            weaponChanged = true;
        }

    }

    private void changeWeapon()
    {
        Destroy(weaponCopy);

        weaponCopy = Instantiate(weapons[nextWeapon], transform.position, transform.rotation) as GameObject;

        nextWeapon++;
        if(nextWeapon == WEAPONSLOTS)
        {
            nextWeapon = 0;
        }
        weaponChanged = false;
    }
}
