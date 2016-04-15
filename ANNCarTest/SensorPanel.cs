using UnityEngine;
using System.Collections;

public class SensorPanel : MonoBehaviour {

	public float fValue;
	public Ray ray;
	public RaycastHit hit;



	void Update()
	{

		ray.origin = new Vector3 (transform.position.x, transform.position.y, transform.position.z + .5f);
		ray.direction = transform.forward;


	if (Physics.Raycast (ray, out hit ))
		{
			fValue = hit.distance;
		}

	}
}
