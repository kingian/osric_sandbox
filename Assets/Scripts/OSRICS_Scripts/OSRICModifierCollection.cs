using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSRICModifierCollection
{
	public List<OSRICCharacterModifier> ModifierList;


	public List<OSRICCharacterModifier> GetModifierByAttribute(OSRIC_ATTRIBUTES oa)
	{
		List<OSRICCharacterModifier> retList = new List<OSRICCharacterModifier>();

		foreach(OSRICCharacterModifier ocm in ModifierList)
		{
			if(ocm.characterVariable == OSRIC_CHARACTER_VARIABLES.attribute
				&& ocm.attribute == oa)
				retList.Add(ocm);	
		}
		return retList;
	}


	public List<OSRICCharacterModifier> GetModifierByCharacterVariable(OSRIC_CHARACTER_VARIABLES ocv)
	{
		List<OSRICCharacterModifier> retList = new List<OSRICCharacterModifier>();

		foreach(OSRICCharacterModifier ocm in ModifierList)
		{
			if(ocm.characterVariable == OSRIC_CHARACTER_VARIABLES.attribute)
				retList.Add(ocm);	
		}
		return retList;
	}

}
