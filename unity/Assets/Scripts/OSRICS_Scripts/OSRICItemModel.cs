using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class OSRICItemModel
{
	public OSRIC_ITEM_TYPE ItemType;
	public string Name;
	public string Description;
	public Range SmallMediumDamage;
	public Range LargeDamage;
	public int Encumberance;
	public int Cost;
	public int WeaponRange;
	public int Charges;
	public OSRICModifierCollection ModifierList;

	public OSRICItemModel(string _name)
	{
		Name = _name;
	}

	// Melee weapon constructor
	public OSRICItemModel(string _name, Range _smallDam, Range _largeDam, int _weight, int _cost)
	{
		Name = _name;
		SmallMediumDamage = _smallDam;
		LargeDamage = _largeDam;
		Encumberance = _weight;
		Cost = _cost;
	}

	//Ranged weapon constructor
	public OSRICItemModel(string _name, Range _smallDam, Range _largeDam, int _range, int _weight, int _cost)
	{
		Name = _name;
		SmallMediumDamage = _smallDam;
		LargeDamage = _largeDam;
		Encumberance = _weight;
		Cost = _cost;
		WeaponRange = _range;
	}

	public OSRICItemModel(JSONObject _jo)
	{
		ItemType = OSRICConstants.GetEnum<OSRIC_ITEM_TYPE>(_jo["ItemType"].str);
		Name = _jo["Name"].str;
		Description = _jo["Description"].str;
		string[] smd = _jo[""].str.Split('-');
		if(smd.Length==2)
		{
			SmallMediumDamage = new Range();
			SmallMediumDamage.min = Int32.Parse(smd[0]);
			SmallMediumDamage.max = Int32.Parse(smd[1]);
		}
		string[] ld = _jo["LargeDamage"].str.Split('-');
		if(ld.Length==2)
		{
			LargeDamage = new Range();
			LargeDamage.min = Int32.Parse(ld[0]);
			LargeDamage.max = Int32.Parse(ld[1]);
		}
		Encumberance = (int)_jo["Encumberance"].n;
		Cost = (int)_jo["Cost"].n;
		WeaponRange = (int)_jo["WeaponRange"].n;
		Charges = (int)_jo["Charges"].n;
		ModifierList =  new OSRICModifierCollection(_jo["ModifierList"]);
	}


	private void initializeModifierList()
	{
		ModifierList = new OSRICModifierCollection();
	}

	public void AddModifier(OSRICCharacterModifier _ocm)
	{
		if(ModifierList==null)
			initializeModifierList();
		ModifierList.Add(_ocm);
	}


	public JSONObject Serialize()
	{
		JSONObject retObj = new JSONObject(JSONObject.Type.OBJECT);
		retObj.AddField("ItemType",ItemType.GetDesc());
		retObj.AddField("Name",Name);
		retObj.AddField("Description",Description);
		retObj.AddField("SmallMediumDamage",SmallMediumDamage.min.ToString()+"-"+SmallMediumDamage.max.ToString());
		retObj.AddField("LargeDamage",LargeDamage.min.ToString()+"-"+LargeDamage.max.ToString());
		retObj.AddField("Encumberance",Encumberance);
		retObj.AddField("Cost",Cost);
		retObj.AddField("WeaponRange",WeaponRange);
		retObj.AddField("Charges",Charges);
		retObj.AddField("ModifierList",ModifierList.Serialize());
		return retObj;
	}

}
