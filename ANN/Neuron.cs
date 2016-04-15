using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Neuron : MonoBehaviour {

	public int iId;
	public float fInput;
	public float fOutput;

	public bool bIsInput;
	public bool bIsOutput;

	public List<Synapse> InputSynapses = new List<Synapse>();
	public List<Synapse> OutputSynapses = new List<Synapse>();


	public float fThreshold()
	{
		if(gameObject.GetComponent<Brain>().cGenome == null)
		{
			return 0;
		}
		else
		return gameObject.GetComponent<Brain>().cGenome.ThresholdMatrix[iId];
	}
}
