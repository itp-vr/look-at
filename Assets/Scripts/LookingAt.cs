using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
public class LookingAt : MonoBehaviour {
	public string layerMaskName;
	public Vector3 lookOffset;
	private int layerMask;
	private GameObject currentGameObject;
	private float lookTime;
	private float firstLookTime;
	private bool isLooking;

	public event Action<GameObject, float> OnLookingAt;
	public event Action<GameObject> OnLookedAway;

	// Use this for initialization
	void Start () {
		if (layerMaskName != null && layerMaskName.Length > 0){
			layerMask = LayerMask.NameToLayer(layerMaskName);
		} else {
			layerMask = int.MaxValue;
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 fwd = transform.TransformDirection(Vector3.forward);
		RaycastHit hit;
		if (Physics.Raycast(transform.position+lookOffset, fwd, out hit, layerMask)){
			if (!isLooking || currentGameObject != hit.transform.gameObject){
				isLooking= true;
				firstLookTime = Time.time;
				if (currentGameObject != null && currentGameObject != hit.transform.gameObject) OnLookedAway(currentGameObject);
			}
			float lookTime = Time.time - firstLookTime;
			currentGameObject = hit.transform.gameObject;
			if (OnLookingAt != null) OnLookingAt(currentGameObject, lookTime);
			Debug.DrawLine(transform.position, hit.point);
			LineRenderer line = GetComponent<LineRenderer>();
			if (line != null){
				line.enabled = true;
				line.SetPosition(0,transform.position);
				line.SetPosition(1,hit.point);
			}
		} else {
			if (isLooking){
				isLooking = false;
				LineRenderer line = GetComponent<LineRenderer>();
				if (line != null){
					line.enabled = false;
				}
				if (currentGameObject != null && OnLookedAway != null)OnLookedAway(currentGameObject);
			}
		}
		
	}
}
