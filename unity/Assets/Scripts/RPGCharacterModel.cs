using UnityEngine;
using System.Collections;

public class RPGCharacterModel {


	public OSRICAttributeModel attributes;


	public RPGCharacterModel()
	{
		attributes = new OSRICAttributeModel(this);
	}

}
