using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
//using OSRICAttributeModifier;

[System.Serializable]
public class OSRICAttributeModel : RPGAttributeModel 
{	
	//for when a reroll, or manual attribute change happens
	public delegate void baseAttributeDidChange();
	public static event baseAttributeDidChange BaseAttributeDidChange;
	//for when someone chooses a new race, or maybe magic changes it for them
	public delegate void racialModifierDidChange();
	public static event racialModifierDidChange RacialModifierDidChange;
	//your global hook for any attribute change, use sparingly
	public delegate void attributeModelDidChange();
	public static event attributeModelDidChange AttributeModelDidChange;
	public RPGCharacterModel cm;

	private string uniqueId;
	public string characterName;
	public DateTime creationDate;
	public DateTime lastEditDate;

	//Attributes
	public int Str;
	public int Dex;
	public int Con;
	public int Int;
	public int Wis;
	public int Cha;


	public int hitPoints;
	public int armorClass;
	public int[] level;
	public int experiencePoints;
	public int vision;
	public int move;
	public OSRIC_GENDER characterGender = OSRIC_GENDER.Male;
	public OSRIC_RACE characterRace = OSRIC_RACE.Human;
	public OSRIC_CLASS characterClass = OSRIC_CLASS.None;
	public OSRIC_ALIGNMENT characterAlignment = OSRIC_ALIGNMENT.Neutral;
	public OSRIC_CHARACTER_STATE characterState;

	public OSRICModifierCollection CharacterModifiers;  //Body related modifiers, e.g. race modifiers, spell effects
//	public OSRICModifierCollection EquipedModifiers;

	public JSONObject Serialize()
	{
		JSONObject retObj = new JSONObject(JSONObject.Type.OBJECT);
		retObj.AddField("characterName",characterName);
		retObj.AddField("Str",Str);
		retObj.AddField("Dex",Dex);
		retObj.AddField("Con",Con);
		retObj.AddField("Int",Int);
		retObj.AddField("Wis",Wis);
		retObj.AddField("Cha",Cha);
		retObj.AddField("hitPoints",hitPoints);
		string levelStr = level[0].ToString();
		for(int i=1;i<level.Length;i++)
			levelStr +=  "/" + level[i].ToString();
		retObj.AddField("level",levelStr);

		retObj.AddField("experiencePoints",experiencePoints);
		retObj.AddField("vision",vision);
		retObj.AddField("move",move);
		retObj.AddField("characterGender",characterGender.GetDesc());
		retObj.AddField("characterRace",characterRace.GetDesc());
		retObj.AddField("characterClass",characterClass.GetDesc());
		retObj.AddField("characterAlignment",characterAlignment.GetDesc());
		retObj.AddField("characterState",characterState.GetDesc());
		retObj.AddField("creationDate",creationDate.ToString());
		retObj.AddField("lastEditDate",lastEditDate.ToString());

		Debug.Log("# of mods: " + CharacterModifiers.ModifierList.Count.ToString());
		retObj.AddField("CharacterModifiers",CharacterModifiers.Serialize());

		return retObj;
	}



	void BroadcastBaseAttributeDidChange()
	{
		if(BaseAttributeDidChange!=null)
			BaseAttributeDidChange();
	}
	void BroadcastRacialAttributeDidChange()
	{
		if(RacialModifierDidChange!=null)
			RacialModifierDidChange();
	}
	//in the past, as a cleanup step, i might call the global event from the specific events, but that can cause spaghett so i usually wait
	void BroadcastAttributeModelDidChange()
	{
		if(AttributeModelDidChange!=null)
			AttributeModelDidChange();
	}

	public OSRICAttributeModel(RPGCharacterModel _cm)
	{
		cm = _cm;
		CharacterModifiers = new OSRICModifierCollection();
	}
		
	public OSRICAttributeModel(RPGCharacterModel _cm, JSONObject _jo)
	{
		cm = _cm;
		CharacterModifiers = new OSRICModifierCollection();
		characterName = _jo["characterName"].str;
		Str = (int)_jo["Str"].n;
		Dex = (int)_jo["Dex"].n;
		Con = (int)_jo["Con"].n;
		Int = (int)_jo["Int"].n;
		Wis = (int)_jo["Wis"].n;
		Cha = (int)_jo["Cha"].n;
		hitPoints = (int)_jo["hitPoints"].n;

		string[] levelStr = _jo["level"].str.Split('/');
		level = new int[levelStr.Length];

		for(int i=0; i<levelStr.Length; i++)
			level[i] = Int32.Parse(levelStr[i]);
		
		experiencePoints = (int)_jo["experiencePoints"].n;
		vision = (int)_jo["vision"].n;
		move = (int)_jo["move"].n;
		characterGender = OSRICConstants.GetEnum<OSRIC_GENDER>(_jo["characterGender"].str);
		characterRace = OSRICConstants.GetEnum<OSRIC_RACE>(_jo["characterRace"].str);
		characterClass = OSRICConstants.GetEnum<OSRIC_CLASS>(_jo["characterClass"].str);
		characterAlignment = OSRICConstants.GetEnum<OSRIC_ALIGNMENT>(_jo["characterAlignment"].str);
		characterState = OSRICConstants.GetEnum<OSRIC_CHARACTER_STATE>(_jo["characterState"].str);
		foreach(JSONObject obj in _jo["CharacterModifiers"].list)
			CharacterModifiers.Add(new OSRICCharacterModifier(obj));

	}

	//we should be able to take this boilerplate and slim it down,but it's whatevs
	public int StrTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in CharacterModifiers.ModifierList) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Strength)
				racial_bonus += mod.value;
		}
//		Debug.Log("Str Mod: "+racial_bonus);
		return this.Str + racial_bonus;
	}

	public int DexTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in CharacterModifiers.ModifierList) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Dexterity)
				racial_bonus += mod.value;
		}
//		Debug.Log("Dex Mod: "+racial_bonus);
		return this.Dex + racial_bonus;
	}

	public int ConTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in CharacterModifiers.ModifierList) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Constitution)
				racial_bonus += mod.value;
		}
//		Debug.Log("Con Mod: "+racial_bonus);
		return this.Con + racial_bonus;
	}

	public int IntTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in CharacterModifiers.ModifierList) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Intellegence)
				racial_bonus += mod.value;
		}
		return this.Int + racial_bonus;
	}

	public int WisTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in CharacterModifiers.ModifierList) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Wisdom)
				racial_bonus += mod.value;
		}
//		Debug.Log("Wis Mod: "+racial_bonus);
		return this.Wis + racial_bonus;
	}

	public int ChaTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in CharacterModifiers.ModifierList) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Charisma)
				racial_bonus += mod.value;
		}
//		Debug.Log("Cha Mod: "+racial_bonus);
		return this.Cha + racial_bonus;
	}


	public void UpdateCharacterOptions(CharacterOptionCollection coc)
	{
		//OSRICEngine.RemoveRaceAdjustments(this);//if we really need to call function on the engine from here we're probably in trouble and need to rethink a few things
		this.ClearRacialModifiers();//this works now without engine call like above
		characterName = coc.charName;
		characterRace = coc.charRace;
		OSRICEngine.AddRaceAdjustments(this,characterRace);
		characterAlignment = coc.charAlignment;
		characterGender = coc.charGender;
		characterClass = coc.charClass;

		string strout = "Current Modifiers: ";
		foreach(OSRICCharacterModifier ocm in CharacterModifiers.ModifierList)
		{
			strout += ocm.attribute.GetDesc() + " | ";
		}
//		Debug.Log(strout);

		BroadcastRacialAttributeDidChange();
		BroadcastAttributeModelDidChange ();
	}

	public int GetAttributeTotal(OSRIC_ATTRIBUTES oa)
	{
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
			return this.StrTotal();
		case OSRIC_ATTRIBUTES.Dexterity:
			return this.DexTotal();
		case OSRIC_ATTRIBUTES.Constitution:
			return this.ConTotal();
		case OSRIC_ATTRIBUTES.Intellegence:
			return this.IntTotal();
		case OSRIC_ATTRIBUTES.Wisdom:
			return this.WisTotal();
		case OSRIC_ATTRIBUTES.Charisma:
			return this.ChaTotal();
		default:
			return -1;
		}
	}

	public int GetBaseAttribute(OSRIC_ATTRIBUTES oa)
	{
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
			return Str;
		case OSRIC_ATTRIBUTES.Dexterity:
			return Dex;
		case OSRIC_ATTRIBUTES.Constitution:
			return Con;
		case OSRIC_ATTRIBUTES.Intellegence:
			return Int;
		case OSRIC_ATTRIBUTES.Wisdom:
			return Wis;
		case OSRIC_ATTRIBUTES.Charisma:
			return Cha;
		default:
			return -1;
		}
	}

	public void SetBaseAttribute(OSRIC_ATTRIBUTES oa, int val)
	{
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
		{
			Str = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Dexterity:
		{
			Dex = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Constitution:
		{
			Con = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Intellegence:
		{
			Int = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Wisdom:
		{
			Wis = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Charisma:
		{
			Cha = val;
			break;
		}
		default:
			return;
		}

		BroadcastBaseAttributeDidChange ();
		BroadcastAttributeModelDidChange ();
	}

	//we could make this more generic and call it AddBaseAttributeModifier and store it all one list
	public void AddRacialModifier(OSRICCharacterModifier modifier)
	{
		CharacterModifiers.Add (modifier);

		BroadcastRacialAttributeDidChange ();
		BroadcastAttributeModelDidChange ();
	}
	//in this particular use case the only time we need to remove a racial modifier we can just remove all of them cause someone changed their race
	//we dont want direct access to anything in the model, even its redundant, because enevitably we'll want to broadast events, or do cleanup, etc.
	public void ClearRacialModifiers(){
		CharacterModifiers.RemoveAllRacialModifiers();
		BroadcastRacialAttributeDidChange();
		BroadcastAttributeModelDidChange ();
	}
}
