using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InputFieldElementUIController : MonoBehaviour {

	public Text Label;
	public InputField InputValue;


	public bool Validate()
	{
		if(InputValue.text != "")
			return true;
		return false;
 	}

	public T GetValue<T>()
	{
		
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
