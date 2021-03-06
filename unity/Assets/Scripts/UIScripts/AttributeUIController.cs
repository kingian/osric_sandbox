﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class AttributeUIController : MonoBehaviour {


	public Text attributeLabel;
	public Text attributeValue;
	public Text attributeSubtext;
	public OSRIC_ATTRIBUTES attributeEnum;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if(attributeLabel.text!=attributeEnum.GetDesc())
			SetAttributeLabel();

	}

	void SetAttributeLabel()
	{
		attributeLabel.text = attributeEnum.GetDesc() + ":";
	}
	public void SetAttributeValue(int val)
	{
		attributeValue.text = val.ToString();
	}

	public void SetAttributeSubtext(string subt)
	{
		attributeSubtext.text = subt;
	}


}
