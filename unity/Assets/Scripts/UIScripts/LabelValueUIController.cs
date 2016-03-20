using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class LabelValueUIController : MonoBehaviour {

	public Text labelText;
	public Text valueText;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetValueString(string val)
	{
		valueText.text = val;
	}

	public void SetLableString(string lable)
	{
		labelText.text = lable;
	}
}
