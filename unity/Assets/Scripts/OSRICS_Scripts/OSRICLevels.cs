using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LevelArray
{
	int[] LevelArr;

	public LevelArray(int[] _levelArr)
	{
		LevelArr = _levelArr;
	}

	public int GetLevel(int xp)
	{
		for(int i=0; i < LevelArr.Length;i++)
			if(xp<LevelArr[i])
				return i+1;
		return 0;
	}

	public int GetNextLevelTarget(int _currentLevel)
	{
		return LevelArr[_currentLevel];
	}
}

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



	public Dictionary<OSRIC_CLASS,LevelArray> ClassLevels;

	public OSRICLevels()
	{
		ClassLevels = new Dictionary<OSRIC_CLASS, LevelArray>();
		ClassLevels.Add(OSRIC_CLASS.Assassin,new LevelArray(AssassinLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Cleric,new LevelArray(ClericLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Druid,new LevelArray(DruidLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Fighter,new LevelArray(FighterLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Illusionist,new LevelArray(IllusionistLevelArr));
		ClassLevels.Add(OSRIC_CLASS.MagicUser,new LevelArray(MagicUserLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Paladin,new LevelArray(PaladinLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Ranger,new LevelArray(RangerLevelArr));
		ClassLevels.Add(OSRIC_CLASS.Thief,new LevelArray(ThiefLevelArr));
	}

	public int[] GetClassLevel(RPGCharacterModel cm)
	{
		int[] retArr;

		string[] classArr = cm.attributes.characterClass.GetDesc().Split('/');

		if(classArr.Length<2)
		{
			return new int[]
			{ ClassLevels[cm.attributes.characterClass].GetLevel(cm.attributes.experiencePoints) };
		}

		int splitXP = cm.attributes.experiencePoints/classArr.Length;

		retArr = new int[classArr.Length];

		for(int i=0; i<retArr.Length;i++)
		{
			retArr[i] = ClassLevels[OSRICConstants.GetEnum<OSRIC_CLASS>(classArr[i])].GetLevel(splitXP);
		}

		return retArr;
	}
}
