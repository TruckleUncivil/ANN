using UnityEngine;
using System.Collections;

public class Synapse : MonoBehaviour {

	public int iId;

	public Neuron FromNeuron;
	public Neuron ToNeuron;

	public float fWeight()
	{
		if(gameObject.GetComponent<Brain>().cGenome == null)
		{
			return 0;
		}
		else
		return gameObject.GetComponent<Brain>().cGenome.WeightMatrix[iId];

	}

}
