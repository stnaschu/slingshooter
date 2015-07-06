using UnityEngine;
using System.Collections;

public class followCam : MonoBehaviour {

	static public followCam S; //single

	public GameObject poi; // point of interest

	private float camZ;

	public float easing = 0.05f;


	// awake is what's being called when the start button is pressed

	void Awake(){
		S = this;
		camZ = transform.position.z;

	}

	void Update(){
		// if there is no poi just get outta here!
		if (poi == null){
			return;
		}

		Vector3 destination = poi.transform.position;

		destination.x = Mathf.Max (0.0f, destination.x);
		destination.y = Mathf.Max (0.0f, destination.y);
	

		destination = Vector3.Lerp (transform.position, destination, easing);

		destination.z = camZ;

		transform.position = destination;

		GetComponent<Camera> ().orthographicSize = 10 + destination.y;
	}
}