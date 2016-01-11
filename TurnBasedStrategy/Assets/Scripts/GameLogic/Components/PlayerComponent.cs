using UnityEngine;
using System.Collections;

public class PlayerComponent : MonoBehaviour {

    GameObject[] figurines;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void removeFigurine(GameObject figurine)
    {
        for(int i = 0; i < figurines.Length; i++)
        {
            if(figurines[i] == figurine)
            {
                GameObject.Destroy(figurine);
                break;
            }
        }
    }
}
