using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class OSRICLevels  
{
	int[] AssassinLevelArr = new int[] {
		1600,
		3000,
		5750,
		12250,
		24750,
		50000,
		99000,
		200500,
		300000,
		400000,
		600000,
		750000,
		1000000,
		1500000};

	int[] ClericLevelArr = new int[] {
		1550,
		2900,
		6000,
		13250,
		27000,
		55000,
		110000,
		220000,
		450000,
		675000,
		900000,
		1125000,
		1350000,
		1575000,
		1800000,
		2025000,
		2250000,
		2475000,
		2700000,
		2925000,
		3150000,
		3375000,
		3600000};

	int[] DruidLevelArr = new int[] {
		2000,
		8000,
		12000,
		20000,
		35000,
		60000,
		90000,
		125000,
		200000,
		300000,
		750000,
		1500000};

	int[] FighterLevelArr = new int[] {
		1900,
		4250,
		7750,
		16000,
		35000,
		75000,
		125000,
		250000,
		500000,
		750000};

	int[] IllusionistLevelArr = new int[] {
		2500,
		4750,
		9000,
		18000,
		35000,
		60250,
		95000,
		144500,
		220000,
		440000,
		660000,
		880000,
		1100000,
		1320000,
		1540000,
		1760000,
		1980000,
		2200000,
		2420000,
		2640000,
		2860000,
		3080000,
		3300000};

	int[] MagicUserLevelArr = new int[] {
		2400,
		4800,
		10250,
		22000,
		40000,
		60000,
		80000,
		140000,
		250000,
		375000,
		750000,
		1125000,
		1500000,
		1875000,
		2250000,
		2625000,
		3000000,
		3375000,
		3750000,
		4125000,
		4500000,
		4875000,
		5250000};

	int[] PaladinLevelArr = new int[] {
		2550,
		5500,
		12500,
		25000,
		45000,
		95000,
		175000,
		325000,
		600000,
		1000000,
		1350000,
		1700000,
		2050000,
		2400000,
		2750000,
		3100000,
		3450000,
		3800000,
		4150000,
		4500000,
		4850000,
		5200000,
		5550000};

	int[] RangerLevelArr = new int[] {
		2250,
		4500,
		9500,
		20000,
		40000,
		90000,
		150000,
		225000,
		325000,
		650000,
		975000,
		1300000,
		1625000,
		1950000,
		2275000,
		2600000,
		2925000,
		3250000,
		3575000,
		3900000,
		4225000,
		4550000,
		4875000};

	int[] ThiefLevelArr = new int[] {
		1250,
		2500,
		5000,
		10000,
		20000,
		40000,
		70000,
		110000,
		160000,
		220000,
		440000
	};

	Dictionary<OSRIC_CLASS,int> UpperLevelTargets;

	Dictionary<OSRIC_CLASS,int[]> ClassLevels;

	OSRICEngine engine;

	public OSRICLevels(OSRICEngine _oe)
	{
		engine = _oe;
		ClassLevels = new Dictionary<OSRIC_CLASS, int[]>();
		ClassLevels.Add(OSRIC_CLASS.Assassin,AssassinLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Cleric, ClericLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Druid, DruidLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Fighter, FighterLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Illusionist, IllusionistLevelArr);
		ClassLevels.Add(OSRIC_CLASS.MagicUser, MagicUserLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Paladin, PaladinLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Ranger, RangerLevelArr);
		ClassLevels.Add(OSRIC_CLASS.Thief, ThiefLevelArr);
	}

	void LoadUpperLevelTargets()
	{
		UpperLevelTargets = new Dictionary<OSRIC_CLASS, int>();
		UpperLevelTargets.Add(OSRIC_CLASS.Cleric,225000);
		UpperLevelTargets.Add(OSRIC_CLASS.Fighter,250000);
		UpperLevelTargets.Add(OSRIC_CLASS.Illusionist,220000);
		UpperLevelTargets.Add(OSRIC_CLASS.MagicUser,375000);
		UpperLevelTargets.Add(OSRIC_CLASS.Paladin,350000);
		UpperLevelTargets.Add(OSRIC_CLASS.Ranger,325000);
		UpperLevelTargets.Add(OSRIC_CLASS.Thief,220000);
	}
		


	private int GetClassLevel(OSRIC_CLASS _oc, OSRIC_RACE _or, int _xp )
	{
		if(!ClassLevels.ContainsKey(_oc))
			return -1;
		
		int[] classArr = ClassLevels[_oc];
		int maxLev = engine.GetClassMaxByRace(_or,_oc);


		for(int i=0; i < classArr.Length;i++)
			if(_xp<classArr[i])
			{
				int nextLev = i+1;
				if(nextLev<maxLev)
					return nextLev;
				return maxLev;
			}
		
		if(!UpperLevelTargets.ContainsKey(_oc))
			return classArr.Length;

		int remainder = _xp - classArr[classArr.Length-1];

		int newLevel = remainder/UpperLevelTargets[_oc] + classArr.Length;

		if(newLevel>maxLev)
			return maxLev;

		return newLevel;
	}


	public int[] GetAllClassLevels(RPGCharacterModel cm)
	{
		int[] retArr;
		int xp = cm.attributes.experiencePoints;
		OSRIC_RACE curRace = cm.attributes.characterRace;

		string[] classArr = cm.attributes.characterClass.GetDesc().Split('/');
		if(classArr.Length<2)
		{
			Debug.Log("Class Arr: " + classArr[0]);
			return new int[] {GetClassLevel(OSRICConstants.GetEnum<OSRIC_CLASS>(classArr[0]), curRace, xp)};
		}

		int splitXP = xp/classArr.Length;
		Debug.Log("Split XP: "+splitXP);
		retArr = new int[classArr.Length];

		for(int i=0; i<retArr.Length;i++)
		{
			retArr[i] = GetClassLevel( OSRICConstants.GetEnum<OSRIC_CLASS>(classArr[i]),curRace,splitXP);
		}

		return retArr;
	}


	private int GetNextLevelTarget(OSRIC_CLASS _oc, int _level)
	{
		int[] clAr = ClassLevels[_oc];
		if((_level-1)<clAr.Length)
			return clAr[_level];

		if(_level<clAr.Length && (!UpperLevelTargets.ContainsKey(_oc)))
			return 0;

		int remainder = _level - clAr.Length;

		return clAr[clAr.Length-1] + (remainder*UpperLevelTargets[_oc]);
	}

	public int[] GetAllNextLevelTarget(RPGCharacterModel _cm)
	{
		int pos = 0;
		string[] classArr = _cm.attributes.characterClass.GetDesc().Split('/');
		int[] retArr = new int[classArr.Length];

		foreach(string s in classArr)
		{
			retArr[pos] = GetNextLevelTarget(OSRICConstants.GetEnum<OSRIC_CLASS>(s),_cm.attributes.level[pos]);
		}
		return retArr;
	}
		
}
