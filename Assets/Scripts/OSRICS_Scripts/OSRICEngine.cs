﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

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

	public void init() {}

	// Use this for initialization
	void Start () 
	{
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
				if(_atm.GetAttribute(oa)<classMinimums.GetValue(oa.GetDesc(),classIndex))
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
//		string outstr = "";

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

//		Debug.Log(outstr);

		return retSet;
	}

	public void RandomizeCharactersAttributes(RPGCharacterModel charmod)
	{

		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			charmod.attributes.SetAttribute(oa,randomizeAttribute());
		}

		if(!AreAttributesViable(charmod.attributes))
			RandomizeCharactersAttributes(charmod);
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


	public static void RemoveRaceAdjustments(OSRICAttributeModel oam)
	{
		OSRIC_RACE or = oam.characterRace;
		switch(or)
		{
		case OSRIC_RACE.Dwarf:
			oam.Con -= 1;
			oam.Cha += 1;
			break;
		case OSRIC_RACE.Elf:
			oam.Dex -= 1;
			oam.Con += 1;
			break;
		case OSRIC_RACE.Gnome:
			break;
		case OSRIC_RACE.HalfElf:
			break;
		case OSRIC_RACE.Halfling:
			oam.Dex -= 1;
			oam.Str += 1;
			break;
		case OSRIC_RACE.HalfOrc:
			oam.Str -= 1;
			oam.Con -= 1;
			oam.Cha += 2;
			break;
		case OSRIC_RACE.Human:
			break;
		}
	}

	public static void AddRaceAdjustments(OSRICAttributeModel oam, OSRIC_RACE newOR)
	{
		int modVal;
		switch(newOR)
		{
		case OSRIC_RACE.Dwarf:
			oam.Con += 1;
			oam.Cha -= 1;
			break;
		case OSRIC_RACE.Elf:
			oam.Dex += 1;
			oam.Con -= 1;
			break;
		case OSRIC_RACE.Gnome:
			break;
		case OSRIC_RACE.HalfElf:
			break;
		case OSRIC_RACE.Halfling:
			oam.Dex += 1;
			oam.Str -= 1;
			break;
		case OSRIC_RACE.HalfOrc:
			oam.Str += 1;
			oam.Con += 1;
			oam.Cha -= 2;
			break;
		case OSRIC_RACE.Human:
			break;
		}
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
}
