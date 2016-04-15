using UnityEngine;
using System.Collections;

public class CarExperimentControl : MonoBehaviour {

	public GeneticEvolutionModule cGeneticEvolutionModule;
	public int iGenomeToTest;


	void Start()
	{
		cGeneticEvolutionModule = gameObject.GetComponent<GeneticEvolutionModule> ();

		StartCoroutine (InitBuffer ());
	}
	public void TestNextGenome()
	{
		cGeneticEvolutionModule.Genomes [iGenomeToTest].fFitness = cGeneticEvolutionModule.cAgentBrain.gameObject.GetComponent<ANNCarFitnessModule>().fFitness;
		iGenomeToTest++;
		cGeneticEvolutionModule.cAgentBrain.gameObject.GetComponent<ANNCarFitnessModule> ().Reset ();

		if(iGenomeToTest == cGeneticEvolutionModule.Genomes.Count )
		{
			cGeneticEvolutionModule.Evolve();
			iGenomeToTest = 0;
			cGeneticEvolutionModule.cAgentBrain.cGenome = cGeneticEvolutionModule.Genomes[iGenomeToTest];
			cGeneticEvolutionModule.cAgentBrain.gameObject.transform.position = GameObject.Find("StartPos").transform.position;
			cGeneticEvolutionModule.cAgentBrain.gameObject.transform.rotation = GameObject.Find("StartPos").transform.rotation;


		}
		else
		{

			cGeneticEvolutionModule.cAgentBrain.gameObject.transform.position = GameObject.Find("StartPos").transform.position;
			cGeneticEvolutionModule.cAgentBrain.gameObject.transform.rotation = GameObject.Find("StartPos").transform.rotation;

			cGeneticEvolutionModule.cAgentBrain.cGenome = cGeneticEvolutionModule.Genomes[iGenomeToTest];
		}
	}

	public IEnumerator InitBuffer()
	{
		yield return new WaitForSeconds (1);
				cGeneticEvolutionModule.cAgentBrain.cGenome = cGeneticEvolutionModule.Genomes [0];

		}

}
