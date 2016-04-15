using UnityEngine;
using System.Collections;

public class ANNCarFitnessModule : MonoBehaviour {

	public float fFitness;
	public CarController cCarController;
	public int iTargetWaypoint;


	public void Reset()
	{
		fFitness = 0;
		iTargetWaypoint = 0;
	}

}
