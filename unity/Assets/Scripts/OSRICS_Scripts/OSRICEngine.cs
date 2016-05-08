using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

//using OSRICConstants;

public class OSRICEngine : MonoBehaviour {

	private RPGBaseTable<int> _attributeTable;
	public RPGBaseTable<int> attributeTable
	{ get 
		{
			if(_attributeTable==null)
				buildAttributeTable();
			return _attributeTable;
		}
	}

	private RPGBaseTable<int> _thacoTable;
	public RPGBaseTable<int> thacoTable
	{ get
		{
			if(_thacoTable==null)
				buildThacoTable();
			return _thacoTable;
		}
	}

	private RPGBaseTable<int> _classMinimums;
	public RPGBaseTable<int> classMinimums
	{ get
		{
			if(_classMinimums==null)
				buildClassMinsTable();
			return _classMinimums;
		}

	}

	private RPGBaseTable<bool> _raceClassMatrix;
	public RPGBaseTable<bool> raceClassMatrix
	{ get
		{
			if(_raceClassMatrix==null)
				buildRaceClassMatrixTable();
			return _raceClassMatrix;
		}

	}


	private RPGBaseTable<int> _raceMinMax;
	public RPGBaseTable<int> raceMinMax
	{ get
		{
			if(_raceMinMax==null)
				buildRaceMinMaxTable();
			return _raceMinMax;
		}
	}

	OSRICSaveTables SaveTables;
	OSRICLevels LevelTables;

	public void init() {}

	// Use this for initialization
	void Start () 
	{
		SaveTables = new OSRICSaveTables();
		LevelTables = new OSRICLevels(this);
		attributeTable.init();
		thacoTable.init();
		classMinimums.init ();
		raceClassMatrix.init ();
		raceMinMax.init ();
	}

	// Update is called once per frame
	void Update () {

	}


	private void buildAttributeTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_attribute_table");
		_attributeTable = RPGTableReader.CreateIntBaseTable("OSRIC Attribute Table",lines, YINDEX_TYPE.IntIndex);
	}

	private void buildThacoTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_thac0");
		_thacoTable = RPGTableReader.CreateIntBaseTable("OSRIC Attribute Table",lines, YINDEX_TYPE.StringIndex);
	}

	private void buildClassMinsTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_class_mins");
		_classMinimums = RPGTableReader.CreateIntBaseTable("OSRIC Class Minimum Attributes",lines, YINDEX_TYPE.StringIndex);
	}

	private void buildRaceClassMatrixTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_race_class_matrix");
		_raceClassMatrix = RPGTableReader.CreateBoolBaseTable("OSRIC Race Class Matrix",lines, YINDEX_TYPE.StringIndex);
	}


	private void buildRaceMinMaxTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_race_mins_maxs");
		_raceMinMax = RPGTableReader.CreateIntBaseTable("OSRIC Race Min Max",lines, YINDEX_TYPE.StringIndex);
	}

	private void buildSaveTables()
	{

	}

	public AttributeAdjustment[] GetStrengthAdjustments(int _str)
	{
		AttributeAdjustment[] retArr = new AttributeAdjustment[5];

		_str = attributeTable.GetYIndexOf(_str);

		retArr[0] = new AttributeAdjustment("Bonus to Hit",attributeTable.GetValue("str_bonus_to_hit",_str));
		retArr[1] = new AttributeAdjustment("Bonus to Damage",attributeTable.GetValue("str_bonus_to_damage",_str));
		retArr[2] = new AttributeAdjustment("Encumberance Adjustment",attributeTable.GetValue("str_encumberance_adjustment",_str));
		retArr[3] = new AttributeAdjustment("Minor Tests",attributeTable.GetValue("str_minor_tests",_str));
		retArr[4] = new AttributeAdjustment("Major Tests",attributeTable.GetValue("str_major_test",_str));

		return retArr;
	}

	public AttributeAdjustment[] GetDexterityAdjustments(int _dex)
	{
		AttributeAdjustment[] retArr = new AttributeAdjustment[3];

		_dex = attributeTable.GetYIndexOf(_dex);

		retArr[0] = new AttributeAdjustment("Surprise Bonus",attributeTable.GetValue("dex_surprise_bonus",_dex));
		retArr[1] = new AttributeAdjustment("Missile Bonus",attributeTable.GetValue("dex_missile_bonus",_dex));
		retArr[2] = new AttributeAdjustment("AC Adjustment",attributeTable.GetValue("dex_AC_adjustment",_dex));

		return retArr;
	}

	public AttributeAdjustment[] GetIntelligenceAdjustments(int _int)
	{
		AttributeAdjustment[] retArr = new AttributeAdjustment[1];

		_int = attributeTable.GetYIndexOf(_int);

		retArr[0] = new AttributeAdjustment("Max Additional Languages",attributeTable.GetValue("int_max_additional_languages",_int));

		return retArr;
	}

	public AttributeAdjustment[] GetWisdomAdjustments(int _wis)
	{
		AttributeAdjustment[] retArr = new AttributeAdjustment[1];

		_wis = attributeTable.GetYIndexOf(_wis);

		retArr[0] = new AttributeAdjustment("Mental Saving Throw Bonus",attributeTable.GetValue("wis_mental_saving_throw_bonus",_wis));

		return retArr;
	}

	public AttributeAdjustment[] GetConstitutionAdjustments(int _con)
	{
		AttributeAdjustment[] retArr = new AttributeAdjustment[3];

		_con = attributeTable.GetYIndexOf(_con);

		retArr[0] = new AttributeAdjustment("HP per die",attributeTable.GetValue("con_HP_per_die",_con));
		retArr[1] = new AttributeAdjustment("Survive Resurrection & Raise Dead",attributeTable.GetValue("con_survive_resurrection_raise_dead",_con));
		retArr[2] = new AttributeAdjustment("Survive System Shock",attributeTable.GetValue("con_survive_system_shock",_con));

		return retArr;
	}

	public AttributeAdjustment[] GetCharismaAdjustments(int _cha)
	{
		AttributeAdjustment[] retArr = new AttributeAdjustment[3];

		_cha = attributeTable.GetYIndexOf(_cha);

		retArr[0] = new AttributeAdjustment("Max Henchman",attributeTable.GetValue("cha_max_henchmen",_cha));
		retArr[1] = new AttributeAdjustment("Loyalty Bonus",attributeTable.GetValue("cha_loyalty_bonus",_cha));
		retArr[2] = new AttributeAdjustment("Reaction Bonus",attributeTable.GetValue("cha_reaction_bonus",_cha));

		return retArr;
	}

	public HashSet<OSRIC_RACE> AvailableRaces(OSRICAttributeModel _atm)
	{
		HashSet<OSRIC_RACE> retSet = new HashSet<OSRIC_RACE>();
		//		retSet.Add (OSRIC_RACE.Human);
		//		retSet.Add (OSRIC_RACE.HalfElf);
		//		retSet.Add (OSRIC_RACE.Dwarf);
		//		retSet.Add (OSRIC_RACE.Elf);
		//		retSet.Add (OSRIC_RACE.Gnome);
		//		retSet.Add (OSRIC_RACE.Halfling);
		//		retSet.Add (OSRIC_RACE.HalfOrc);

		int val;

		foreach (OSRIC_RACE race in Enum.GetValues(typeof(OSRIC_RACE)))
		{
			if(race==OSRIC_RACE.Human)
			{
				retSet.Add(race);
				continue;
			}
			val = raceMinMax.GetYIndexOf(race.GetDesc());
			if (_atm.Str < raceMinMax.GetValue("str_min", val))
				continue;
			if(_atm.Dex < raceMinMax.GetValue("dex_min", val))
				continue;
			if(_atm.Int < raceMinMax.GetValue("int_min", val))
				continue;
			if(_atm.Wis < raceMinMax.GetValue("wis_min", val))
				continue;
			if(_atm.Con < raceMinMax.GetValue("con_min", val))
				continue;
			if(_atm.Cha < raceMinMax.GetValue("cha_min", val))
				continue;
			retSet.Add(race);
		}
		return retSet;
	}

	public  HashSet<OSRIC_CLASS> AvailableClassesByAttributes(OSRICAttributeModel _atm)
	{
		HashSet<OSRIC_CLASS> retSet = new HashSet<OSRIC_CLASS>();
		bool addClass;

		foreach(OSRIC_CLASS oc in Enum.GetValues(typeof(OSRIC_CLASS)))
		{
			if(oc == OSRIC_CLASS.None)
			{
				retSet.Add(oc);
				continue;
			}
			int classIndex = classMinimums.GetYIndexOf(oc.GetDesc());
			addClass = true;
			foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
			{
				if(_atm.GetBaseAttribute(oa)<classMinimums.GetValue(oa.GetDesc(),classIndex))
					addClass = false;
			}
			if(addClass)
				retSet.Add(oc);
		}
		return retSet;
	}

	public  HashSet<OSRIC_CLASS> AvailableClassesByRace(OSRICAttributeModel _atm)
	{
		HashSet<OSRIC_CLASS> retSet = new HashSet<OSRIC_CLASS>();
		bool available;

		foreach(OSRIC_CLASS oc in Enum.GetValues(typeof(OSRIC_CLASS)))
		{
			if(oc==OSRIC_CLASS.None)
			{
				retSet.Add(oc);
				continue;
			}
			available = raceClassMatrix.GetValue(_atm.characterRace.GetDesc(),raceClassMatrix.GetYIndexOf(oc.GetDesc()));
			if(available)
				retSet.Add(oc);
		}

		return retSet;
	}

	public void RandomizeCharactersAttributes(RPGCharacterModel charmod)
	{
		//		if(!init)
		ResetChracterOptions(charmod.attributes);

//		charmod.attributes.characterRace = OSRIC_RACE.Human;

		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			charmod.attributes.SetBaseAttribute(oa,randomizeAttribute());
		}

		if(!AreAttributesViable(charmod.attributes))
			RandomizeCharactersAttributes(charmod);
	}


	public void ResetChracterOptions(OSRICAttributeModel oam)
	{
 
		oam.characterClass = OSRIC_CLASS.None;
		oam.characterRace = OSRIC_RACE.Human;
		oam.characterAlignment = OSRIC_ALIGNMENT.Neutral;
		oam.characterGender = OSRIC_GENDER.Male;
	}

	public bool AreAttributesViable(OSRICAttributeModel oam)
	{
		if(AvailableClassesByAttributes(oam).Count>1)
			return true;	
		return false;
	}

	public int randomizeAttribute()
	{
		return UnityEngine.Random.Range(3,18);
	}




	public static void AddRaceAdjustments(OSRICAttributeModel oam, OSRIC_RACE newOR)
	{	
		List<OSRICCharacterModifier> accumulator = new List<OSRICCharacterModifier>();
		OSRICCharacterModifier tempMod;

		switch(newOR)
		{
		case OSRIC_RACE.Dwarf:
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Constitution, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, 1);
			accumulator.Add(tempMod);
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Charisma, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, -1);
			accumulator.Add(tempMod);
			break;
		case OSRIC_RACE.Elf:
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Dexterity, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, 1);
			accumulator.Add(tempMod);
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Constitution, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, -1);
			accumulator.Add(tempMod);
			break;
		case OSRIC_RACE.Gnome:
			break;
		case OSRIC_RACE.HalfElf:
			break;
		case OSRIC_RACE.Halfling:
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Dexterity, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, 1);
			accumulator.Add(tempMod);

			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Strength, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, -1);
			accumulator.Add(tempMod);

			// Broken here
			int conAdjust = -1*(int)((float)oam.Con/3.5);

			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.savingthrow,
				OSRIC_SAVING_THROWS.saveRoSaWa, OSRIC_ATTRIBUTES.Constitution,
				OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, conAdjust);
			accumulator.Add(tempMod);

			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.savingthrow,
				OSRIC_SAVING_THROWS.saveSpell, OSRIC_ATTRIBUTES.Constitution,
				OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, conAdjust);
			accumulator.Add(tempMod);

			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.savingthrow,
				OSRIC_SAVING_THROWS.saveDeath, OSRIC_ATTRIBUTES.Constitution,
				OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, conAdjust);
			accumulator.Add(tempMod);

			break;
		case OSRIC_RACE.HalfOrc:
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Strength, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, 1);
			accumulator.Add(tempMod);
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Constitution, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, 1);
			accumulator.Add(tempMod);
			tempMod = new OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES.attribute,
				OSRIC_ATTRIBUTES.Charisma, OSRIC_ATTRIBUTE_MODIFIER_TYPE.Racial, -2);
			accumulator.Add(tempMod);
			break;
		case OSRIC_RACE.Human:
			break;
		}

		foreach(OSRICCharacterModifier ocm in accumulator)
		{
			oam.AddRacialModifier(ocm);
		}
//		Debug.Log("Modifier List: " + oam.CharacterModifiers.ModifierList.Count.ToString());
	}

	public bool VerifyOptionCollection (CharacterOptionCollection coc)
	{
		HashSet<OSRIC_CLASS> retSet = new HashSet<OSRIC_CLASS> ();
		bool available;
		foreach (OSRIC_CLASS oc in Enum.GetValues(typeof(OSRIC_CLASS))) {
			if (oc == OSRIC_CLASS.None) 
			{
				retSet.Add (oc);
				continue;
			}
			available = raceClassMatrix.GetValue (coc.charRace.GetDesc(), raceClassMatrix.GetYIndexOf (oc.GetDesc ()));
			if (available)
				retSet.Add (oc);
		}

		if (retSet.Contains (coc.charClass))
			return true;
		return false;
	}

	public void CompleteCharacterCreation(RPGCharacterModel cm)
	{
		cm.attributes.level = LevelTables.GetAllClassLevels(cm);
		// HP computation
		int hpAccumulator, con, bonus, summedLevels;
		con = cm.attributes.GetAttributeTotal(OSRIC_ATTRIBUTES.Constitution);
		con = attributeTable.GetYIndexOf(con);
		bonus = attributeTable.GetValue("con_HP_per_die",con);
		// Get all levels and roll for each
		summedLevels = 0;
		foreach(int lev in cm.attributes.level)
			summedLevels += lev;

		hpAccumulator = 0;
		for(int i=0;i<summedLevels;i++)
			hpAccumulator += RollHitPoints(cm.attributes.characterClass, bonus,2);
		
		cm.attributes.hitPoints = hpAccumulator;
		cm.attributes.creationDate = DateTime.Now;
		cm.attributes.lastEditDate = DateTime.Now;
		Debug.Log(cm.attributes.Serialize().Print());


//		numClass = cm.attributes.characterClass.GetDesc().Split('/').Length;
//		cm.attributes.level = new int[numClass];
//		for(int i=0;i<numClass;i++)
//			cm.attributes.level[i]=1;
		
	}

	public int RollHitPoints(OSRIC_CLASS oc,int bonus,int _minHPthreshhold)
	{
		int accumulator,die,temp;
		accumulator = die = temp = 0;
		string[] classString = oc.GetDesc().Split('/');
		for(int i = 0;i<classString.Length;i++)
		{
			die = OSRICConstants.ClassHitDie[classString[i]];
			temp = UnityEngine.Random.Range(1,die) + bonus;
			accumulator += (int)(temp/classString.Length);
		}
		if(accumulator >= _minHPthreshhold)
			return accumulator;
		else
			return RollHitPoints(oc,bonus,_minHPthreshhold);
	}

	public int ComputeArmorClass(RPGCharacterModel cm)
	{
		int ac = 10;
		int dex = cm.attributes.GetAttributeTotal(OSRIC_ATTRIBUTES.Dexterity);
		dex = attributeTable.GetYIndexOf(dex);
		ac += attributeTable.GetValue("dex_AC_adjustment",dex);
		return ac;
	}



	public SaveCollection GetSaveCollection(RPGCharacterModel cm)
	{
		return SaveTables.GetClassSaves(cm);
	}

	public int GetClassMaxByRace(OSRIC_RACE _or, OSRIC_CLASS _oc)
	{
		int yindex = raceMinMax.GetYIndexOf(_or.GetDesc());

		if(yindex<0)
			return 0;
		try
		{
			return raceMinMax.GetValue(_oc.GetDesc(),yindex);
		}
		catch
		{
			return 0;
		}
	}

}