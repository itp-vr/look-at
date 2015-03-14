using UnityEngine;
using System.Collections;
//add UI library
using UnityEngine.UI;

public class SimpleLookReceiver : MonoBehaviour {

	private LookingAt lookingAt;
	private Image image;
	private Text text;
		//register to delegate event when object is enabled
	void OnEnable() {
		lookingAt.OnLookingAt += HandleOnLookingAt;
		lookingAt.OnLookedAway += HandleOnLookedAway;
	}

	//deregister when object is disabled
	void OnDisable() {
		lookingAt.OnLookingAt -= HandleOnLookingAt;
		lookingAt.OnLookedAway -= HandleOnLookedAway;
	}

	void HandleOnLookedAway (GameObject lookedAtObj) {
		//check to see if we were looked at
		if (lookedAtObj.Equals(gameObject)){
			image.color = Color.white;
			text.text = "Look At Me";
		}
	}

	void HandleOnLookingAt (GameObject lookedAtObj, float timeLooked) {
		//check to see if we were looked at
		if (lookedAtObj.Equals(gameObject)){
			image.color = Color.red;
			text.text = "Looking for \n"+timeLooked.ToString("0.00")+" seconds";
		}
		
	}

	void Awake(){
		//find LookSensor tag.  MAKE SURE YOU TAG THE OBJECT WITH LookedAt Script
		lookingAt = GameObject.FindGameObjectWithTag("LookSensor").GetComponent<LookingAt>();
		image = GetComponent<Image>();
		text = GetComponentInChildren<Text>();
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
