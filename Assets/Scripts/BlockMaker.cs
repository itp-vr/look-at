using UnityEngine;
using System.Collections;

public class BlockMaker : MonoBehaviour {

	public GameObject block;
	public int amount;
	public float radius = 5000;
	public float variation = 0.2f;
	private float elevation = 0.5f;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < amount; i++){
			Vector3 coords;
			float polar = Random.Range(Mathf.PI*-20, Mathf.PI*20);
			SphericalCoords.SphericalToCartesian(radius, polar, Random.Range(elevation-variation, elevation+variation),out coords);
			//Debug.Log("coords="+coords);
			Instantiate(block, coords, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
