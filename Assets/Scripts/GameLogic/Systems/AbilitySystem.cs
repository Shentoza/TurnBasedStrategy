using UnityEngine;
using System.Collections;

public class AbilitySystem : MonoBehaviour {

    public GameObject smoke;
    public GameObject fire;
    public GameObject explosion;
    public GameObject gas;


    //Shit für animationen zum drehen
    private float turningSpeed = 360.0f;
    private float startAngle;
    private bool startAngleSet;
    private float turningDirection;


    //Grenade Stuff
    private GameObject throwing_Object;
    private float throwing_TimePerCell = 0.25f;
    private float throwing_TimeNeeded;
    private float throwing_TimeSum;
    private float throwing_Length;
    private Vector3 throwing_StartPos;
    private Vector3 throwing_DestinationPos;
    private Cell throwing_DestinationCell;
    private bool throwing_Active = false;
    private Enums.Effects throwing_effect;
    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (throwing_Active)
            continousThrowing(Time.deltaTime);
	}

	public void throwSmoke(Cell ziel, GameObject figur)
	{
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();

        checkRotation(ziel, playerAttr);

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
            ec.setDauer(3);

            //Für Wurfanimation
            playerAttr.anim.SetTrigger("Throw");

        } else {
			Debug.Log ("OutOfRange");
		}
	}

	public void throwMolotov(Cell ziel, GameObject figur)
	{
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();

        //Dreht Figur in Wurfrichtung
        checkRotation(ziel, playerAttr);

        //Setzt effekte und "wirft" die eigentliche physik
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
            ec.setDauer(3);

            //Für Wurfanimation
            playerAttr.anim.SetTrigger("Throw");


        } else {
			Debug.Log ("OutOfRange");

		}
	}

    public void throwGrenade(Cell ziel, GameObject figur)
    {
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>();


        checkRotation(ziel, playerAttr);

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

            //Für Wurfanimation
            playerAttr.anim.SetTrigger("Throw");

        }
        else {
            Debug.Log("OutOfRange");
        }
    }

    public void throwGas(Cell ziel, GameObject figur)
    {
        AttributeComponent playerAttr = figur.GetComponent<AttributeComponent>(); 

        checkRotation(ziel, playerAttr);

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

            //Für Wurfanimation
            playerAttr.anim.SetTrigger("Throw");

        }
        else {
            Debug.Log("OutOfRange");

        }
    }

    public bool checkRotation(Cell targetCell, AttributeComponent playerAttr)
    {

        Cell currentCell = (Cell)playerAttr.getCurrentCell();

        //Richtung, in die der Pfad von currentCell zu  targetCell schaut.
        Vector3 walkingDirection = targetCell.transform.position - currentCell.transform.position;

        //Aktuelle Richtung in die wir schauen
        Vector3 facingDirection = playerAttr.transform.forward;

        //Winkel zwischen unseren zwei Vektoren
        float angle = Vector3.Angle(walkingDirection.normalized, facingDirection);

        //Schaue ich schon in die passende Richtung?
        if (angle != 0.0f)
        {
            if (!startAngleSet)
            {
                if (Vector3.Cross(walkingDirection.normalized, facingDirection).y < 0.0f)
                {
                    turningDirection = 1.0f;
                }
                else
                {
                    turningDirection = -1.0f;
                }

                startAngle = angle;
                startAngleSet = true;
            }

            float yRotation = Mathf.Clamp(Time.deltaTime * turningSpeed * turningDirection, -angle, angle);
            angle += yRotation;
            Vector3 euler = playerAttr.transform.rotation.eulerAngles;
            euler.y += angle;
            playerAttr.transform.rotation = Quaternion.Euler(euler);
        }
        else
            startAngleSet = false;

        return angle == 0.0f;
    }

    public void throwIt(GameObject grenade)
    {
        throwing_Object = grenade;
        throwing_StartPos = throwing_Object.transform.position;
        throwing_DestinationPos = throwing_DestinationCell.gameObject.transform.position;

        Vector3 baselength = throwing_DestinationPos - throwing_StartPos;
        baselength.y = 0.0f;

        throwing_TimeSum = 0.0f;
        throwing_TimeNeeded = throwing_TimePerCell * baselength.magnitude;

        throwing_Active = true;
        
    }
    void continousThrowing(float deltaTime)
    {
        if (throwing_TimeSum <= throwing_TimeNeeded)
        {
            throwing_TimeSum += deltaTime;
        }

        float progress = Mathf.Clamp01(throwing_TimeSum / throwing_TimeNeeded);
        float yValue = -20 * (progress * progress) + 20 * progress;

        //Debug.Log(progress);
        Vector3 result = Vector3.Lerp(throwing_StartPos, throwing_DestinationPos, progress);
        result.y += yValue;
        throwing_Object.transform.position = result;
        if(progress == 1.0f)
        {
            throwing_Active = false;
            GameObject.Destroy(throwing_Object);
            Debug.Log("BOOM");
        }


    }

    public void setThrowDestination(Cell destination)
    {
        throwing_DestinationCell = destination;
    }

}
