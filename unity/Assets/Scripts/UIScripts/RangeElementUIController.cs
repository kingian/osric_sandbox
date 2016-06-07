﻿using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RangeElementUIController : MonoBehaviour 
{

	public InputField LowField;
	public InputField HightField;



	public bool Validate()
	{
		if(LowField.text != "" && HightField.text != "")
		{
			if( Int32.Parse(LowField.text) < Int32.Parse(HightField.text))
				return true;
		}
		return false;
	}


	public Range GetRange()
	{
		return new Range(Int32.Parse(LowField.text), Int32.Parse(HightField.text));
	}

}