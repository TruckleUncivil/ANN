using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GeneticEvolutionModule : MonoBehaviour {
	public int iPopulation;
	public List<Genome> Genomes = new List<Genome>();
	public GameObject goAgent;
	public Brain cAgentBrain;

	public int iGeneration;
	public int iElitism;
	public int iElitismCutOf;
    public float iNeuronMutationRate;
    public float iSynapseMutationRate;

	void Start()
		{
		StartCoroutine (Init ());
	     }


	public IEnumerator Init()
	{
		yield return new  WaitForSeconds (.5f);
		CreateRandomGenomes (iPopulation, cAgentBrain.AllNeurons.Count, cAgentBrain.AllSynapses.Count);

	}
	public void CreateRandomGenomes(int iPopulation, int iThresholdMatrixSize, int iWeightMatrixSize)
	{
		for(int i = 0; i < iPopulation; i++)
		{
		Genome genome = gameObject.AddComponent<Genome> ();

		for(int n = 0; n < iThresholdMatrixSize; n++)
		{
				if(n < cAgentBrain.iNoOfInputs)
				{
					genome.ThresholdMatrix.Add(0f);

				}
				else
				{
			genome.ThresholdMatrix.Add(Random.Range(0f,1f));
				}
		}
		
		for(int x = 0; x < iWeightMatrixSize; x++)
		{
			genome.WeightMatrix.Add(Random.Range(-1f,1f));
		}
		Genomes.Add(genome);
	}
	}


	public void Evolve()
	{
				Debug.Log ("Evolved");
				//start here
				Genomes.Sort (delegate(Genome x, Genome y) {
						return y.fFitness.CompareTo (x.fFitness);
				});

				Debug.Log (Genomes [0].fFitness.ToString ());
				iGeneration++;

				float a = (float)iPopulation / 100;
				float b = (float)a * iElitism;
				iElitismCutOf = (int)b;
	
				int n = 0;
				foreach (Genome genome in Genomes) {
		    
						if (n < iElitismCutOf) {
								//Elite Gene
						} else {
								ReplaceBrainWithChild (genome);
						}
	
						n++;
				}

		}
	
		public void ReplaceBrainWithChild(Genome goChild)
		{
	

	
				Genome gParentA = Genomes [Random.Range (0, iElitismCutOf)];
				List<float> ParentANeuronMap = gParentA.ThresholdMatrix;
				List<float> ParentASynapseMap = gParentA.WeightMatrix;
		
				Genome gParentB = Genomes [Random.Range (0, iElitismCutOf)];
				List<float> ParentBNeuronMap = gParentB.ThresholdMatrix;
				List<float> ParentBSynapseMap = gParentB.WeightMatrix;



		
		if (Random.Range (0, 2)== 0) 
		{
							//Evolve by averages
	
							for (int i = 0; i < ParentANeuronMap.Count; i++)
			{
				goChild.ThresholdMatrix[i] = (ParentANeuronMap[i]  + ParentBNeuronMap[i]) / 2;

				if((Random.Range(0,100) < iNeuronMutationRate) && i > cAgentBrain.iNoOfInputs)
				{
					goChild.ThresholdMatrix[i] = Random.Range(0f,1f);
				}
			}
			for (int n = 0; n < ParentASynapseMap.Count; n++)
			{
				goChild.WeightMatrix[n] = (ParentASynapseMap[n]  + ParentBSynapseMap[n]) / 2;

				if((Random.Range(0,100) < iSynapseMutationRate))
				{
					goChild.WeightMatrix[n] = Random.Range(-1f,1f);
				}
			
			}
		}
	


			else {

				////////////
				int a = Random.Range(0, ParentANeuronMap.Count) ;
				for (int i = 0; i < ParentANeuronMap.Count; i++) {
					if(i < a){
						goChild.ThresholdMatrix[i] = ParentANeuronMap [i];
						if(Random.Range(0,100) < iNeuronMutationRate && i > cAgentBrain.iNoOfInputs)
						{
							goChild.ThresholdMatrix [i] = Random.Range(0f,1f);
						}
				}
					else
					{
						goChild.ThresholdMatrix [i] = ParentBNeuronMap [i];
					if(Random.Range(0,100) < iNeuronMutationRate && i > cAgentBrain.iNoOfInputs)
						{
							goChild.ThresholdMatrix [i] = Random.Range(0f,1f);
						}
				}
				}
				int b = Random.Range(0, ParentASynapseMap.Count);
	
				for (int n = 0; n < ParentASynapseMap.Count; n++) {
					if(n < b){
						goChild.WeightMatrix [n] = ParentASynapseMap [n] ;
						if(Random.Range(0,100) < iSynapseMutationRate)
						{
							goChild.WeightMatrix [n] = Random.Range(-1f,1f);
						}

				}
					else
					{
						goChild.WeightMatrix [n] = ParentBSynapseMap [n] ;
						if(Random.Range(0,100) < iSynapseMutationRate)
						{
							goChild.WeightMatrix [n] = Random.Range(-1f,1f);
						}
	
					}
				}
								
		}
	}

}




