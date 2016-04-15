using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public int Id;

		void OnTriggerEnter (Collider col)
		{
			if(col.gameObject.tag == "ANN")
			{
			if(col.gameObject.GetComponent<ANNCarFitnessModule>().iTargetWaypoint == Id)
			{
				col.gameObject.GetComponent<ANNCarFitnessModule>().fFitness++;
				col.gameObject.GetComponent<ANNCarFitnessModule>().iTargetWaypoint++;
				col.gameObject.GetComponent<CarController>().iFramesSinceLastWaypoint = 0;

				Debug.Log("booom");


			}
			}
}
}
