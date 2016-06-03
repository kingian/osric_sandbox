using System;
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

	public string GetStr()
	{
		return InputValue.text;
	}


	public int GetInt()
	{
		return Int32.Parse(InputValue.text);
	}
}
