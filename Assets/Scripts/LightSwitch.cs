using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour {

	public DialSwitch dialswitch;
	public Light bulbLight;
	public GameObject glass;
	private float maxIntensity = 0.4f;
	private float minIntensity = 0f;
	void OnEnable(){
		dialswitch.OnSwitchChange += HandleOnSwitchChange;
	}

	void HandleOnSwitchChange (bool on) {
		bulbLight.enabled = on;
		if (on) SetIntensity(glass, maxIntensity); 
		else SetIntensity(glass, minIntensity);
	}
	
	private void SetIntensity(GameObject obj, float amount) {
		Material mat = obj.GetComponent<Renderer>().material;
		Color color =  mat.GetColor("_EmissionColorUI");
		mat.SetColor("_EmissionColor", EvalFinalEmissionColor(color, mat.GetFloat("_EmissionScaleUI")*amount));
	}
	
	Color EvalFinalEmissionColor(Color color, float emissionScale) {
		if (emissionScale < 0.0f) {
			emissionScale = 0.0f;
		}
		return color * Mathf.LinearToGammaSpace(emissionScale);
	}

		void OnDisable(){
			dialswitch.OnSwitchChange -= HandleOnSwitchChange;
	}

	void Awake(){
		//should only be one switch so this is ok
		if (dialswitch == null) dialswitch = GetComponentInChildren<DialSwitch>();
		//there's only one light so this is ok
		if (bulbLight == null) bulbLight = GetComponentInChildren<Light>();
		bulbLight.enabled = dialswitch.on;
		if (dialswitch.on) SetIntensity(glass, maxIntensity); 
		else SetIntensity(glass, minIntensity);
	}

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}
}
