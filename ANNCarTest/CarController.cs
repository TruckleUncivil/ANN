using UnityEngine;
using System.Collections;

public class CarController : MonoBehaviour {

	public float fBaseDrag;
	public float fAcceleration;
	public float fSoftBrakeStrength;
	public float fTurnSpeed;


	public GameObject goSensorPanel;

	public bool bIsAgent;
	public Brain cBrain;
	public SensorControl cSensorControl;
	public int iBestNeuronIndex;
	public float iBestNeuronOutput;
	public int iFramesSinceLastWaypoint;
	public int iFramesPerWaypointCutoff;
	//Controller inputs

	void Update()
	{


				if (bIsAgent) {

			iFramesSinceLastWaypoint++;
			if(iFramesSinceLastWaypoint > iFramesPerWaypointCutoff)
			{
				GameObject.Find("_ExperimentManager").GetComponent<CarExperimentControl>().TestNextGenome();
				iFramesSinceLastWaypoint = 0;

			}

						GetAutoInputs ();
				} else {

						//Accelerate
						if (Input.GetKey (KeyCode.UpArrow)) {
								Accelerate ();

						}
						if (Input.GetKey (KeyCode.DownArrow)) {
								Reverse ();
			
						} else {
								GetComponent<Rigidbody>().drag = fBaseDrag;
						}
						if (Input.GetKey (KeyCode.LeftArrow)) {
								TurnLeft ();
			
						}
						if (Input.GetKey (KeyCode.RightArrow)) {
								TurnRight ();
			
						}
				}
		}
//Outputs

	public void Accelerate()
	{
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * fAcceleration);
		//Debug.Log ("Accelerate:" +  rigidbody.velocity.magnitude.ToString());
	}
	public void Reverse()
	{
		GetComponent<Rigidbody>().AddRelativeForce(Vector3.back * fAcceleration);
		//Debug.Log ("Reverse");

	}
	public void TurnLeft()
	{
		transform.Rotate(Vector3.up, -fTurnSpeed);
		//Debug.Log ("TurnLeft");
		
	}
	public void TurnRight()
	{
		transform.Rotate(Vector3.up, +fTurnSpeed);
	//	Debug.Log ("TurnRight");
		
	}

	//ANN Driiven
	public void GetAutoInputs()
	{
		if(cBrain.cGenome != null)
		{
		cBrain.InputLayer.Neurons [0].fInput = cSensorControl.fLeftValue;
		cBrain.InputLayer.Neurons [1].fInput = cSensorControl.fCentralValue;
		cBrain.InputLayer.Neurons [2].fInput = cSensorControl.fRightValue;

		cBrain.FireSynapses ();
		ReadOutput ();
		}
	}
	public void ReadOutput()
	{
		
		
		for(int i = 0; i < cBrain.OutputLayer.Neurons.Count; i++)
		{
			if(i == 0)
			{
				iBestNeuronIndex = i;
				iBestNeuronOutput = cBrain.OutputLayer.Neurons[i].fOutput;
			}
			
			if(cBrain.OutputLayer.Neurons[i].fOutput > iBestNeuronOutput)
			{
				iBestNeuronIndex = i;
				iBestNeuronOutput = cBrain.OutputLayer.Neurons[i].fOutput;
			}
		}
		
		ExecuteOutput (iBestNeuronIndex);
	}
	
	public void ExecuteOutput(int iDominantOutputNeuronIndex)
	{
		if(iDominantOutputNeuronIndex == 0)
		{
			Accelerate();
		}
		if(iDominantOutputNeuronIndex == 1)
		{
			TurnLeft();
		}
		if(iDominantOutputNeuronIndex == 2)
		{
			TurnRight();
		}
		if(iDominantOutputNeuronIndex == 3)
		{
			Reverse();
		}
	//	Debug.Log (iDominantOutputNeuronIndex.ToString ());
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name == "Wall")
		{
			GameObject.Find("_ExperimentManager").GetComponent<CarExperimentControl>().TestNextGenome();
		}
	}
}
