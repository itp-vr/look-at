using UnityEngine;
using System.Collections;

public class FaceObject: MonoBehaviour {
	public Transform targetObj;
	//set this to true if you need to orient in the opposite dir.
	public bool flipY = true;
	//set this to true if you want the object to always be on the same polar angle as the target object
	public bool orientPolar;
	private Vector3 initPos;
	private float polar;
	private float radius;
	private float elev;

	void Awake(){
		setSphericalCoords();
	}

	void setSphericalCoords(){
		SphericalCoords.CartesianToSpherical(transform.position, out radius, out polar, out elev);
	}

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (orientPolar){
			float targetPolar;
			//I don't care about any of these
			float r; float e;
			//get point a little bit in front of target.
			Vector3 forward = targetObj.transform.position+targetObj.transform.forward;
			//get direction vector
			Vector3 dir = (forward - targetObj.transform.position).normalized;
			//get polar of target's forward vector
			SphericalCoords.CartesianToSpherical(dir, out r, out targetPolar, out e);
			Vector3 newPos;
			//set the target's polar as the polar for object, and get new position.
			SphericalCoords.SphericalToCartesian(radius, targetPolar, elev, out newPos);
			transform.position = newPos;
		}
		if (flipY){
			transform.rotation = Quaternion.LookRotation(transform.position - targetObj.transform.position);
		} else {
			transform.LookAt(targetObj);
		}
	}
}
