using UnityEngine;
using System.Collections;

public class throwObject : MonoBehaviour {

    public GameObject leftHand;

    public float floatingTime;
    public float range;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void takeIt()
    {
        //Ausm Abilitysystem geworfene Art Granate ziehen

        GameObject copy = (GameObject)Instantiate(GameObject.Find("grenade"));
        copy.transform.position = leftHand.transform.position;
        copy.transform.parent = leftHand.transform;
    }

    public void throwIt()
    {
        //Sag dem AbilitySystem er soll werfen
    }
}
