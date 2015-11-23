﻿using UnityEngine;
using System.Collections;

public class RTSCameraScript : MonoBehaviour {

    public GameObject target1;
    public GameObject target2;
    public GameObject currentTarget;
    public Vector3 velocity = new Vector3(0, 0, 0);
    public float distance = 15.0f;
    float mousePosX;
    float mousePosY;
    float scrollDistance = 0.98f;
    float scrollSpeed = 15.0f;
    Vector3 oldCamera;
    float xRot = 27.9f;
    Vector3 constantPos = new Vector3(0,10, -15);
    float lerpTime;
    Quaternion constantRot;
    float minDistance = 20.0f;
    float maxDistance = 60.0f;
    float zoomSpeed = 5.0f;
    bool isZoomed = false;
    Camera cam;
    Vector3 lastCamPos;

    float rotationSpeed = 20.0f;

    float currentAngle;



    // Use this for initialization
    void Start () {
        cam = this.gameObject.GetComponent<Camera>();
       // constantPos = this.transform.position;
        constantRot = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        mousePosX = Input.mousePosition.x;
        mousePosY = Input.mousePosition.y;

        oldCamera = cam.transform.position;

        if(mousePosX/Screen.width < 1-scrollDistance)
        {
            
            cam.transform.Translate(transform.right * -scrollSpeed * Time.deltaTime, Space.World);
            clearTarget();
        }

        if(mousePosX/Screen.width >= scrollDistance)
        {
            transform.Translate(transform.right * scrollSpeed * Time.deltaTime, Space.World);
            clearTarget();
        }

        if (mousePosY/Screen.height < 1-scrollDistance)
        {
            cam.transform.Translate(transform.up * -scrollSpeed * Time.deltaTime);
            clearTarget();
        }

        if (mousePosY/Screen.height >= scrollDistance)
        {
            cam.transform.Translate(transform.up * scrollSpeed * Time.deltaTime);
            clearTarget();
        }

        cam.transform.position = new Vector3(transform.position.x, oldCamera.y, transform.position.z);

        if(Input.GetMouseButtonDown(0))
        {
            if (currentTarget == target1)
            {
                currentTarget = target2;
                lerpTime = 0.0f;
                //lastCamPos = cam.transform.position;
                setDefinedRot();
            }
            else
            {
                currentTarget = target1;
                lerpTime = 0.0f;
                //lastCamPos = cam.transform.position;
                setDefinedRot();
            }
        }

        if(currentTarget != null)
        {
            lerpTime += Time.deltaTime;
            Vector3 temp = currentTarget.transform.position + constantPos;
            cam.transform.position = Vector3.Lerp(cam.transform.position, temp, lerpTime);
        }

        if(Input.GetKey("z"))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, minDistance, Time.deltaTime);
        }

        if(Input.GetKey("u"))
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxDistance, Time.deltaTime);
        }

       // this.transform.position = //camPos eintragen
        //this.transform.LookAt(currentTarget.transform.position);

        if(Input.GetKey("i"))
        {
            setDefinedRot();
        }

        if(Input.GetMouseButton(1))
        {
            currentAngle += rotationSpeed * Time.deltaTime;

            Quaternion q = Quaternion.Euler(0, currentAngle, 0);
            Vector3 direction = q * Vector3.forward;
            transform.position = currentTarget.transform.position - direction * distance;
            transform.position += new Vector3(0, 10, 0);
            transform.LookAt(currentTarget.transform.position);
            Debug.Log(currentTarget);
            //transform.RotateAround(currentTarget.transform.position, currentTarget.transform.up, rotationSpeed * Time.deltaTime);
        }

        if(Input.GetMouseButtonUp(1))
        {
            setDefinedRot();
        }

    }

    //Objekt ändern auf das die Kamera ausgerichtet ist
    public void changeTarget(GameObject newTarget)
    {     
        currentTarget = newTarget;
    }


    public void setDefinedRot()
    {
        cam.transform.rotation = constantRot;
    }

    public void clearTarget()
    {
        currentTarget = null;
    }

}
