using UnityEngine;
using System.Collections;

public class LoadScene : MonoBehaviour {

	//load a new scene

	public void OnClick(string SceneName){
		Application.LoadLevel(SceneName);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
