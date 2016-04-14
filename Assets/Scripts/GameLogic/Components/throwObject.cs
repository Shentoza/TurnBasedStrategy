using UnityEngine;
using System.Collections;

public class throwObject : MonoBehaviour {

    public GameObject leftHand;
    AbilitySystem ability;

    public Enums.Effects selectedGrenade;

    GameObject

    Cell destination;
    GameObject grenade;

    public float floatingTime;
    public float range;

	// Use this for initialization
	void Start () {
        ability = (AbilitySystem)FindObjectOfType(typeof(AbilitySystem));
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setThrowPath(Cell destination)
    {
        this.destination = destination;
    }

    public void takeIt()
    {
        //Ausm Abilitysystem geworfene Art Granate ziehen

        grenade = (GameObject)Instantiate(GameObject.Find("grenade"));
        grenade.transform.position = leftHand.transform.position;
        grenade.transform.parent = leftHand.transform;
    }

    public void throwIt()
    {
        //Vector3 vec3 = grenade.transform.position;
        //grenade.transform.parent = null;
        //grenade.transform.position = vec3;
        ability.throwIt(grenade);
    }
}
