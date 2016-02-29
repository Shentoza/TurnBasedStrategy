using UnityEngine;
using System.Collections;

//To-Do: Lerp von anfangsposition zum ersten ausgewählten Objekt UND lerp zwischen Objekten!

public class CameraRotationScript : MonoBehaviour {

	//Variablen für die Rotation (Tobi)
	public Transform target;
	public Transform oldTarget;

	public GameObject cameraPointer;

	public Vector3 startPosition = new Vector3 (5.5f, 5.0f, -9.0f);

	//Für Zoom
	public float distance = 5.0f;
	public float xSpeed = 120.0f;
	public float ySpeed = 120.0f;

	//Für maximale Rotation auf Z und X achse(Damit die Kamera keine 90 grad bzw 0 grad hat)
	public float yMinLimit = -20f;
	public float yMaxLimit = 80f;

	//Maximale entfernung der Kamera zum Objekt
	public float distanceMin = .5f;
	public float distanceMax = 15f;

	Vector3 distanceToObject = new Vector3 (0.0f, 2.0f, -5.0f);

	//Dient für die Rotation
	private Rigidbody rigidbody;
	
	float x = 0.0f;
	float y = 0.0f;

	//Lerptime für die Anfangs Kamerafahrt
	float lerpTime = 0.0f;

	//Aktivieren und Deaktivieren der Rotation
	bool startRotation = false;
	//Aktiviert/Deaktiviert die anfangs Kamerafahrt
	bool startLerp = true;
	//Aktiviert den Lerp zwischen Figuren
	bool startSwitch = false;
	//aktiviert/deaktiviert target verfolgung
	bool followCameraEnabled = true;


	//Zum scrollen über den bildschirm
	float mousePosX;
	float mousePosY;
	float scrollDistance = 0.99f;
	float scrollSpeed = 15.0f;

	// Use this for initialization
	void Start () {

		//Nimmt rotation und speichert sie in Eulerwinkel
		Vector3 angles = transform.eulerAngles;
		x = angles.y;
		y = angles.x;

		cameraPointer = GameObject.Find ("CameraPointer");
		
		rigidbody = GetComponent<Rigidbody>();
		
		// Make the rigid body not change rotation
		if (rigidbody != null)
		{
			rigidbody.freezeRotation = true;
		}
	}
	
	void LateUpdate () 
	{

		mousePosX = Input.mousePosition.x;
		mousePosY = Input.mousePosition.y;

		//Führt die Kamerafahrt am Anfang durch
		if (startLerp) {
			lerpTime += Time.deltaTime;
			transform.position = Vector3.Lerp(transform.position, new Vector3(5, 10, -15), lerpTime * 0.03f);
			if (transform.position == new Vector3 (5, 10, -15)) {
				startLerp = false;
			}
		}
		//Links scrollen
		if (mousePosX / Screen.width < 1 - scrollDistance && !startRotation) {
			followCameraEnabled = false;
			transform.Translate(transform.right * -scrollSpeed * Time.deltaTime, Space.World);
		}
		//rechts scrollen
		if (mousePosX / Screen.width >= scrollDistance && !startRotation) {
			followCameraEnabled = false;
			transform.Translate (transform.right * scrollSpeed * Time.deltaTime, Space.World);
		}
		//unten scrollen
		if (mousePosY / Screen.height < 1 - scrollDistance && !startRotation) {
			followCameraEnabled = false;
			transform.Translate (new Vector3(transform.forward.x, 0.0f, transform.forward.z) * -scrollSpeed * Time.deltaTime, Space.World);
		}
		//oben scrollen
		if (mousePosY / Screen.height >= scrollDistance && !startRotation) {
			followCameraEnabled = false;
			transform.Translate (new Vector3(transform.forward.x, 0.0f, transform.forward.z) * scrollSpeed * Time.deltaTime, Space.World);
		}

		//Die Rotation um ein Objekt
		if (startRotation && !startLerp && target && followCameraEnabled) {
			x += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.02f;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.02f;

			y = ClampAngle (y, yMinLimit, yMaxLimit);

			Quaternion rotation = Quaternion.Euler (y, x, 0);

			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
			/*
			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) {
				distance -= hit.distance;
			}*/
			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;

			transform.rotation = rotation;
			transform.position = position;
			distanceToObject = transform.position - target.position;
		}

		if (!startLerp && target && followCameraEnabled) {
			lerpTime += Time.deltaTime;
			y = ClampAngle (y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler (y, x, 0);
			
			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
			/*
			RaycastHit hit;
			if (Physics.Linecast (target.position, transform.position, out hit)) {
				distance -= hit.distance;
			}*/
			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance + target.position;
			
			transform.rotation = rotation;
			transform.position = Vector3.Lerp(transform.position, position, lerpTime * 0.03f);
			distanceToObject = transform.position - target.position;
		}

		if (!followCameraEnabled && startRotation) {
			x += Input.GetAxis ("Mouse X") * xSpeed * distance * 0.01f;
			y -= Input.GetAxis ("Mouse Y") * ySpeed * 0.01f;
			
			y = ClampAngle (y, yMinLimit, yMaxLimit);
			
			Quaternion rotation = Quaternion.Euler (y, x, 0);

			distance = Mathf.Clamp (distance - Input.GetAxis ("Mouse ScrollWheel") * 5, distanceMin, distanceMax);

			transform.rotation = rotation;
			Vector3 negDistance = new Vector3 (0.0f, 0.0f, -distance);
			Vector3 position = rotation * negDistance;
		}
	}
	
	//Clampangle für die Rotation um ein Objekt (Damit keiner Werte über/unter 360 entstehen bzw min und max nicht überschritten werden)
	public static float ClampAngle(float angle, float min, float max)
	{
		if (angle < -360F)
			angle += 360F;
		if (angle > 360F)
			angle -= 360F;
		return Mathf.Clamp(angle, min, max);
	}

	//Ändert die Variable zum rotieren auf true(rotieren möglich)
	public void setStartRotation()
	{
		startRotation = true;
	}

	//Ändert die Variable zum rotieren auf false(rotieren nicht möglich)
	public void setStopRotation()
	{
		startRotation = false;
	}

	//Ändert das Ziel der Kamera
	public void setNewTarget(GameObject newTarget)
	{
		if (oldTarget != target) {
			oldTarget = target;
		}
		target = newTarget.transform;
		if (oldTarget)
			distanceToObject = transform.position - oldTarget.position;
		else {
			distanceToObject = new Vector3 (5, 5, 0);
		}
		startSwitch = true;
	}

	public void backToTarget()
	{
		followCameraEnabled = true;
	}
}
