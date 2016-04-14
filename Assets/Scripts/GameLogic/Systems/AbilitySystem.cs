using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour {

    public GameObject smoke;
    public GameObject fire;
    public GameObject explosion;
    public GameObject gas;

    public int effektDauer;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public void throwSmoke(Cell ziel, GameObject figur)
	{
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();
        if (ziel.dij_GesamtKosten <= playerAttr.attackRange) {
            GameObject smokeTmp = Instantiate(smoke);
			smokeTmp.transform.position = new Vector3 (ziel.transform.position.x, ziel.transform.position.y + 0.2f, ziel.transform.position.z);
            EffectComponent ec = smokeTmp.AddComponent<EffectComponent>();
            ArrayList cellList = new ArrayList();
			cellList.Add(ziel);
			if (ziel.upperNeighbour != null)
				cellList.Add(ziel.upperNeighbour);
			if (ziel.lowerNeighbour != null)
				cellList.Add(ziel.lowerNeighbour);
			if (ziel.leftNeighbour != null)
				cellList.Add(ziel.leftNeighbour);
			if (ziel.rightNeighbour != null)
				cellList.Add(ziel.rightNeighbour);
			if (ziel.upperNeighbour.leftNeighbour != null)
				cellList.Add(ziel.upperNeighbour.leftNeighbour);
			if (ziel.upperNeighbour.rightNeighbour != null)
				cellList.Add(ziel.upperNeighbour.rightNeighbour);
			if (ziel.lowerNeighbour.leftNeighbour != null)
				cellList.Add(ziel.lowerNeighbour.leftNeighbour);
			if (ziel.lowerNeighbour.rightNeighbour != null)
				cellList.Add(ziel.lowerNeighbour.rightNeighbour);
            ec.zellenSetzen(cellList);
            ec.setEffekt(Enums.Effects.Smoke);
            ec.setDauer(effektDauer);
        } else {
			Debug.Log ("OutOfRange");
		}
	}

	public void throwMolotov(Cell ziel, GameObject figur)
	{
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();
        if (ziel.dij_GesamtKosten <= playerAttr.attackRange) {
			GameObject fireTmp = Instantiate (fire);
			fireTmp.transform.position = new Vector3 (ziel.transform.position.x, ziel.transform.position.y + 0.2f, ziel.transform.position.z);
            EffectComponent ec = fireTmp.AddComponent<EffectComponent>();
            ArrayList cellList = new ArrayList();
			cellList.Add(ziel);
			if (ziel.upperNeighbour != null)
				cellList.Add(ziel.upperNeighbour);
			if (ziel.lowerNeighbour != null)
				cellList.Add(ziel.lowerNeighbour);
			if (ziel.leftNeighbour != null)
				cellList.Add(ziel.leftNeighbour);
			if (ziel.rightNeighbour != null)
				cellList.Add(ziel.rightNeighbour);
			if (ziel.upperNeighbour.leftNeighbour != null)
				cellList.Add(ziel.upperNeighbour.leftNeighbour);
			if (ziel.upperNeighbour.rightNeighbour != null)
				cellList.Add(ziel.upperNeighbour.rightNeighbour);
			if (ziel.lowerNeighbour.leftNeighbour != null)
				cellList.Add(ziel.lowerNeighbour.leftNeighbour);
			if (ziel.lowerNeighbour.rightNeighbour != null)
				cellList.Add(ziel.lowerNeighbour.rightNeighbour);
            ec.zellenSetzen(cellList);
            ec.setEffekt(Enums.Effects.Fire);
            ec.setDauer(effektDauer);
        } else {
			Debug.Log ("OutOfRange");

		}
	}

    public void throwGrenade(Cell ziel, GameObject figur)
    {
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();
        if (ziel.dij_GesamtKosten <= playerAttr.attackRange)
        {
            GameObject explosionTmp = Instantiate(explosion);
            explosionTmp.transform.position = new Vector3(ziel.transform.position.x, ziel.transform.position.y + 0.2f, ziel.transform.position.z);
            EffectComponent ec = explosionTmp.AddComponent<EffectComponent>();
            ArrayList cellList = new ArrayList();
            cellList.Add(ziel);
			if (ziel.upperNeighbour != null)
            	cellList.Add(ziel.upperNeighbour);
			if (ziel.lowerNeighbour != null)
            	cellList.Add(ziel.lowerNeighbour);
			if (ziel.leftNeighbour != null)
            	cellList.Add(ziel.leftNeighbour);
			if (ziel.rightNeighbour != null)
            	cellList.Add(ziel.rightNeighbour);
			if (ziel.upperNeighbour.leftNeighbour != null)
           		cellList.Add(ziel.upperNeighbour.leftNeighbour);
			if (ziel.upperNeighbour.rightNeighbour != null)
            	cellList.Add(ziel.upperNeighbour.rightNeighbour);
			if (ziel.lowerNeighbour.leftNeighbour != null)
            	cellList.Add(ziel.lowerNeighbour.leftNeighbour);
			if (ziel.lowerNeighbour.rightNeighbour != null)
            	cellList.Add(ziel.lowerNeighbour.rightNeighbour);
            ec.zellenSetzen(cellList);
            ec.setEffekt(Enums.Effects.Explosion);
            ec.setDauer(0);
        }
        else {
            Debug.Log("OutOfRange");
        }
    }

    public void throwGas(Cell ziel, GameObject figur)
    {
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();
        if (ziel.dij_GesamtKosten <= playerAttr.attackRange)
        {
            GameObject gasTmp = Instantiate(gas);
            gasTmp.transform.position = new Vector3(ziel.transform.position.x, ziel.transform.position.y + 0.2f, ziel.transform.position.z);
            EffectComponent ec = gasTmp.AddComponent<EffectComponent>();
            ArrayList cellList = new ArrayList();
			cellList.Add(ziel);
			if (ziel.upperNeighbour != null)
				cellList.Add(ziel.upperNeighbour);
			if (ziel.lowerNeighbour != null)
				cellList.Add(ziel.lowerNeighbour);
			if (ziel.leftNeighbour != null)
				cellList.Add(ziel.leftNeighbour);
			if (ziel.rightNeighbour != null)
				cellList.Add(ziel.rightNeighbour);
			if (ziel.upperNeighbour.leftNeighbour != null)
				cellList.Add(ziel.upperNeighbour.leftNeighbour);
			if (ziel.upperNeighbour.rightNeighbour != null)
				cellList.Add(ziel.upperNeighbour.rightNeighbour);
			if (ziel.lowerNeighbour.leftNeighbour != null)
				cellList.Add(ziel.lowerNeighbour.leftNeighbour);
			if (ziel.lowerNeighbour.rightNeighbour != null)
				cellList.Add(ziel.lowerNeighbour.rightNeighbour);
            ec.zellenSetzen(cellList);
            ec.setEffekt(Enums.Effects.Gas);
            ec.setDauer(1);
        }
        else {
            Debug.Log("OutOfRange");

        }
    }
}
