﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSRICModifierCollection
{
	public List<OSRICCharacterModifier> ModifierList;


	public OSRICModifierCollection()
	{
		ModifierList = new List<OSRICCharacterModifier>();
	}


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

	public void Add(OSRICCharacterModifier candidate)
	{
		foreach(OSRICCharacterModifier ocm in ModifierList)
			if(ocm.Equals(candidate))
				return;
		
		ModifierList.Add(candidate);
	}

	public void RemoveAllRacialModifiers()
	{
		List<OSRICCharacterModifier> removalList = new List<OSRICCharacterModifier>();
		foreach(OSRICCharacterModifier ocm in ModifierList)
		{
			if(ocm.type == OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial)
				removalList.Add(ocm);
		}
		foreach(OSRICCharacterModifier ocm in removalList)
		{
			ModifierList.Remove(ocm);
		}
	}

}