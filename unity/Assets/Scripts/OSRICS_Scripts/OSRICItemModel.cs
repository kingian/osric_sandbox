using UnityEngine;
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
