using UnityEngine;
using System.Collections;

public class SphericalCoords : MonoBehaviour {
	public float radius;
	public float elevation;
	public float polar;

	public static void SphericalToCartesian(float radius, float polar, float elevation, out Vector3 outCart){
		float a = radius * Mathf.Cos(elevation);
		outCart.x = a * Mathf.Cos(polar);
		outCart.y = radius * Mathf.Sin(elevation);
		outCart.z = a * Mathf.Sin(polar);
	}

	public static void CartesianToSpherical(Vector3 cartCoords, out float outRadius, out float outPolar, out float outElevation){
		if (cartCoords.x == 0)
			cartCoords.x = Mathf.Epsilon;
		outRadius = Mathf.Sqrt((cartCoords.x * cartCoords.x)
		                       + (cartCoords.y * cartCoords.y)
		                       + (cartCoords.z * cartCoords.z));
		outPolar = Mathf.Atan2(cartCoords.z, cartCoords.x);
		if (cartCoords.x < 0)
			outPolar += Mathf.PI;
		outElevation = Mathf.Asin(cartCoords.y / outRadius);
	}

	// Use this for initialization
	void Start () {
		CartesianToSpherical(transform.localPosition, out radius, out polar, out elevation);
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = new Vector3();
		SphericalToCartesian(radius, polar, elevation, out newPos);
		transform.localPosition = newPos;
	}
}
