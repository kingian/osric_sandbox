using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class CharacterSheetUIController : MonoBehaviour {

	public AttributeUIGroup attributeGroup;
	public RPGCharacterModel charModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCharacterViewInformation();
	}

	void UpdateCharacterViewInformation()
	{
		UpdateAttributeViewInformation(charModel.attributes);
	}


	void UpdateAttributeViewInformation(OSRICAttributeModel oam)
	{
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			attributeGroup.SetAttribute(oa,oam.GetAttribute(oa));
		}
	}
}
