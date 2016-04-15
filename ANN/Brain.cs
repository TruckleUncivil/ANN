using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Brain : MonoBehaviour {

	public int iNoOfInputs;
	public int iNoOfLayers;
	public int iNoOfNeuronsPerLayer;
	public int iNoOfOutputs;

	public Layer InputLayer;
	public List<Layer> Layers = new List<Layer>();
	public Layer OutputLayer;


	public List<Neuron> AllNeurons = new List<Neuron>();
	public List<Synapse> AllSynapses = new List<Synapse>();
	public Genome cGenome;

	
	void Start()
	{
		InitializeNeuralMap ();
		cGenome = gameObject.GetComponent<Genome> ();
	}



	public void InitializeNeuralMap()
	{
		SetupLayersAndNeurons ();
		SetupSynapses ();
		WriteGenomeIds ();

	}

	public void SetupLayersAndNeurons()
	{

	InputLayer = gameObject.AddComponent<Layer>();
	for(int InputsCount = 0 ; InputsCount < iNoOfInputs ; InputsCount++)
	{
	    Neuron neuron = gameObject.AddComponent<Neuron>();
		InputLayer.Neurons.Add(neuron);
		neuron.bIsInput = true;
			AllNeurons.Add(neuron);
		
		
	}
		for(int LayerCount = 0 ; LayerCount < iNoOfLayers ; LayerCount++)
		{
			Layer CurrentLayer = gameObject.AddComponent<Layer>();
			Layers.Add(CurrentLayer);
			for(int NeuronCount = 0; NeuronCount < iNoOfNeuronsPerLayer; NeuronCount++)
			{
				Neuron neuron = gameObject.AddComponent<Neuron>();
				CurrentLayer.Neurons.Add(neuron);
				AllNeurons.Add(neuron);

			}
		}
	
	OutputLayer = gameObject.AddComponent<Layer>();
	for(int OutputsCount = 0 ; OutputsCount < iNoOfOutputs ; OutputsCount++)
	{
			Neuron neuron = gameObject.AddComponent<Neuron>();
			OutputLayer.Neurons.Add(neuron);
			neuron.bIsOutput = true;
			AllNeurons.Add(neuron);
		

			
		}
	

}

	public void SetupSynapses()
	{

		foreach (Neuron neuron in InputLayer.Neurons )
		{
			foreach(Neuron ForwardNeuron in Layers[0].Neurons)
			{
				Synapse synapse = gameObject.AddComponent<Synapse>();
				neuron.OutputSynapses.Add(synapse);
				synapse.FromNeuron = neuron;
				ForwardNeuron.InputSynapses.Add(synapse);
				synapse.ToNeuron = ForwardNeuron;

				AllSynapses.Add(synapse);
			
			}
		}
		for(int cnt = 0 ; cnt < iNoOfLayers ; cnt++)
		{
			if(cnt == iNoOfLayers - 1)
			{
				foreach (Neuron neuron in Layers[cnt].Neurons)
				{
					foreach(Neuron ForwardNeuron in OutputLayer.Neurons)
					{
						Synapse synapse = gameObject.AddComponent<Synapse>();
						neuron.OutputSynapses.Add(synapse);
						synapse.FromNeuron = neuron;
						ForwardNeuron.InputSynapses.Add(synapse);
						synapse.ToNeuron = ForwardNeuron;
						
						AllSynapses.Add(synapse);

					}
				}
			}
			else
			{
		foreach (Neuron neuron in Layers[cnt].Neurons)
		{
			foreach(Neuron ForwardNeuron in Layers[cnt + 1].Neurons)
			{
				Synapse synapse = gameObject.AddComponent<Synapse>();
				neuron.OutputSynapses.Add(synapse);
				synapse.FromNeuron = neuron;
				ForwardNeuron.InputSynapses.Add(synapse);
				synapse.ToNeuron = ForwardNeuron;
				
				AllSynapses.Add(synapse);

					}
		}
			}
		}
	}

	public void WriteGenomeIds ()
	{
		int iNeuronCount = 0;
		foreach(Neuron neuron in AllNeurons)
		{
			neuron.iId = iNeuronCount;
			iNeuronCount++;
		}
		int iSynapseCount = 0;
		foreach(Synapse synapse in AllSynapses)
		{
			synapse.iId = iSynapseCount;
			iSynapseCount++;
		}
	}



	public void FireSynapses()
	{
		foreach(Neuron neuron in OutputLayer.Neurons)
		{
			neuron.fOutput = 0;
			
		}
		foreach(Neuron neuron in InputLayer.Neurons)
		{
			EvaluateFiringSynapse(neuron);
		}
		foreach(Layer layer in Layers)
		{
		foreach(Neuron neuron in layer.Neurons)
		{
			EvaluateFiringSynapse(neuron);
		}
		}



	}
	public void EvaluateFiringSynapse(Neuron neuron)
	{
		float fAdjustedOutput;
		neuron.fOutput = neuron.fOutput + neuron.fInput;

		fAdjustedOutput = neuron.fOutput;
		if (neuron.bIsInput) {
						fAdjustedOutput = SigmoidOutput (neuron.fOutput);
				
				}
		if (neuron.bIsInput == false && neuron.bIsOutput == false) {
			fAdjustedOutput = TanAdjustedOutput (neuron.fOutput);
			
		}

			
		if(fAdjustedOutput > neuron.fThreshold())
		{
			foreach(Synapse synapse in neuron.OutputSynapses)
			{
				synapse.ToNeuron.fOutput = synapse.ToNeuron.fOutput + (neuron.fOutput * synapse.fWeight());
			}
		}
		neuron.fOutput = 0;
	}

	public float SigmoidOutput(float x)
	{
	
			if (x < -45.0f) return 0.0f;
			else if (x > 45.0f) return 1.0f;
			else return 1.0f / (1.0f + Mathf.Exp(-x));
		
	}

	public float TanAdjustedOutput(float x)
	{
		if (x < -10.0f) return -1.0f;
		else if (x > 10.0f) return 1.0f;
		else return (float)System.Math.Tanh(x);
	
	}


}
