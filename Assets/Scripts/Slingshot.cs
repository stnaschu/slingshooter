using UnityEngine;
using System.Collections;

public class Slingshot : MonoBehaviour {

	public float velocityMultiplier = 4.0f;

	private GameObject launchPoint;
	public GameObject prefabProjectile;

	private bool aimingMode;
	private Vector3 launchPos;

	private GameObject projectile;




	void Awake(){

		Transform launchPointTrans = transform.Find("launchPoint");
		launchPoint = launchPointTrans.gameObject;
		launchPoint.SetActive (false);

		launchPos = launchPointTrans.position;
	}

	void OnMouseEnter(){
		launchPoint.SetActive (true);
		//print ("Yaay, the mouse has entered!");
	}

	void OnMouseExit(){
		launchPoint.SetActive (false);
		//print ("Ooh no, the mouse has exited!");
	}

	void OnMouseDown(){
		aimingMode = true;
		projectile = Instantiate (prefabProjectile)as GameObject;
		projectile.transform.position = launchPos;
		projectile.GetComponent<Rigidbody>().isKinematic = true;
	}
    void Update(){


		// if we are not in aiming mode then do nothing
		if(aimingMode == false){
			return;
		}

		// get the current mouse position in screen coordinates
		Vector3 mousePos2D = Input.mousePosition;

		// convert mouse position to 3d world space
		mousePos2D.z = - Camera.main.transform.position.z;
		Vector3 mousePos3D = - Camera.main.ScreenToWorldPoint (mousePos2D);
	 
		// find the difference between the launch position and my mouse position
		Vector3 mouseDelta = launchPos - mousePos3D;

		// move the projectile to this new postion
		float radius = GetComponent<SphereCollider>().radius;
		mouseDelta = Vector3.ClampMagnitude (mouseDelta, radius);

		projectile.transform.position = launchPos + mouseDelta;

		// if the mousebutton has been released, 
		if(Input.GetMouseButtonUp(0)){

		// exit aimingmode
			aimingMode = false;

		// stop the projectile from being kinematic
		projectile.GetComponent<Rigidbody>().isKinematic = false;

		// set the projectiles velocity to the negative mouseDelta
		projectile.GetComponent<Rigidbody>().velocity = -mouseDelta * velocityMultiplier;

		followCam.S.poi = projectile;
		}
	}
}

