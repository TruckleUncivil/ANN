using UnityEngine;
using System.Collections;

public class SensorControl : MonoBehaviour {


	public GameObject goCentralPanel;
	public GameObject goLeftPanel;
	public GameObject goRightPanel;

	public SensorPanel cCentralPanel;
	public SensorPanel cLeftPanel;
    public SensorPanel cRightPanel;

	public float fCentralValue;
	public float fLeftValue;
	public float fRightValue;



	void Start()
	{
		cCentralPanel = goCentralPanel.GetComponent<SensorPanel> ();
		cLeftPanel = goLeftPanel.GetComponent<SensorPanel> ();
		cRightPanel = goRightPanel.GetComponent<SensorPanel> ();

	}

	void Update()
	{
		fCentralValue = cCentralPanel.fValue;
		fLeftValue = cLeftPanel.fValue;
		fRightValue = cRightPanel.fValue;

		//Debug.Log (fLeftValue.ToString()+ "," +fCentralValue.ToString()+ "," +fRightValue.ToString());

	}

}
