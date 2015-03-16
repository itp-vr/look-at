using UnityEngine;
using System.Collections;
using System;

public class BlockSelect : MonoBehaviour {

	public LookingAt lookingAt;
	public GameObject grabber;
	private bool looking;
	private float lookPct;
	public float lookTime = 2;
	private Rigidbody rbody;
	private bool grabbed;

	void OnEnable () {
		lookingAt.OnLookingAt += HandleOnLookingAt;
		lookingAt.OnLookedAway += HandleOnLookedAway;
	}
	
	void OnDisable () {
		lookingAt.OnLookingAt -= HandleOnLookingAt;
		lookingAt.OnLookedAway -= HandleOnLookedAway;
	}

	void HandleOnLookedAway (GameObject seenObj) {
		if (seenObj.Equals (gameObject)) {
			if (!grabbed) {
				looking = false;
				GetComponent<Renderer> ().material.color = Color.white;
			} else {
				grabber.GetComponent<LineRenderer> ().enabled = false;
				grabbed = false;
			}
		}
	}
	
	void HandleOnLookingAt (GameObject seenObj, float timeSeen) {
		if (seenObj.Equals (gameObject)) {
			if (!looking) {
				looking = true;
			}
			lookPct = timeSeen / lookTime;
			if (lookPct < 1) {
				//Debug.Log("look pct = "+lookPct);
				GetComponent<Renderer> ().material.color = Color.Lerp (Color.white, Color.red, lookPct);
			} else {
				if (!grabbed) {
					grabber.GetComponent<LineRenderer> ().enabled = true;
					grabbed = true;
					//Debug.Log("grabbing ");
					FixedJoint grabJoint = grabber.GetComponent<FixedJoint> ();
					if (grabJoint != null)
						Destroy (grabJoint);
					grabJoint = grabber.AddComponent<FixedJoint> ();
					grabJoint.breakForce = 4000;
					grabJoint.breakTorque = 4000;
					grabJoint.connectedBody = rbody;
					GetComponent<Renderer> ().material.color = Color.green;
				}
			}
		}
	}

	void Awake () {
		rbody = gameObject.GetComponent<Rigidbody> ();
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (grabbed) {
			LineRenderer line = grabber.GetComponent<LineRenderer> ();
			if (line.enabled) {
				line.SetPosition (0, grabber.transform.position);
				line.SetPosition (1, transform.position);
			}
		}
	}
}
