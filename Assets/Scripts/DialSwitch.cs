using UnityEngine;
using System.Collections;
using System;
public class DialSwitch : MonoBehaviour {

	public GameObject offBG;
	public GameObject onBG;
	public GameObject dial;
	public LookingAt lookingAt;
	public float lookTime;
	private float lookPct;
	private bool looking;
	private bool flipped;
	public bool on = false;

	public event Action<bool> OnSwitchChange;

	void OnEnable() {
		lookingAt.OnLookingAt += HandleOnLookingAt;
		lookingAt.OnLookedAway += HandleOnLookedAway;
	}

	void OnDisable(){
		lookingAt.OnLookingAt -= HandleOnLookingAt;
		lookingAt.OnLookedAway -= HandleOnLookedAway;
	}

	void HandleOnLookedAway (GameObject seenObj) {
		if (seenObj.Equals(gameObject)){
			looking = false;
			flipped = false;
			dial.SetActive(false);
			dial.GetComponent<Renderer>().material.SetFloat("_Cutoff", 1);
		}
	}

	void HandleOnLookingAt (GameObject seenObj, float timeSeen) {
		if (seenObj.Equals(gameObject)){
			if (!looking && !flipped){
				looking = true;
				dial.SetActive(true);
			}
			lookPct = timeSeen / lookTime;
			if (lookPct < 1 ){
				//Debug.Log("look pct = "+lookPct);
				dial.GetComponent<Renderer>().material.SetFloat("_Cutoff", 1-lookPct);
			} else {
				if (!flipped){
					on = !on;
					flipped = true;
					//Debug.Log("FLIP SWITCH");
					dial.SetActive(false);
					setSwitchView();
					//do flip switch
					if (OnSwitchChange != null)OnSwitchChange(on);
				}
			}
		}
	}

	private void setSwitchView(){
		onBG.SetActive(on);
		offBG.SetActive(!on);
	}

	void Awake(){
		dial.SetActive(false);
		setSwitchView();
	}

	void Start () {}

	void Update () {}
}
