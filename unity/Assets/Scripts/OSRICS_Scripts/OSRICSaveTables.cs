using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class OSRICSaveTables 
{


	List<string> SaveTableNames = new List<string> {
		"OSRIC_assassin_save",
		"OSRIC_cleric_save",
		"OSRIC_druid_save",
		"OSRIC_fighter_save",
		"OSRIC_illusionist_save",
		"OSRIC_magic-user_save",
		"OSRIC_paladin_save",
		"OSRIC_ranger_save",
		"OSRIC_thief_save"};
	
	Dictionary<OSRIC_CLASS,RPGBaseTable<int>> SaveDict;


	public OSRICSaveTables()
	{
		SaveDict = new Dictionary<OSRIC_CLASS, RPGBaseTable<int>>();
		LoadSaveTables();

	}

	private void LoadSaveTables()
	{
		foreach (OSRIC_CLASS cls in Enum.GetValues(typeof(OSRIC_CLASS)))
		{
			string tabName = "";
			foreach(string s in SaveTableNames)
			{
				if(s.Contains(cls.GetDesc().ToLower()))
					tabName = s;
			}

			if(tabName=="")
				continue;

			string[] lines = RPGTableReader.LoadResourceFile(tabName);
			RPGBaseTable<int> tempTable = RPGTableReader.CreateIntBaseTable(tabName,lines, YINDEX_TYPE.IntIndex);

			SaveDict.Add(cls,tempTable);
		}
	}



	public int GetSaveValue(OSRIC_CLASS oc, OSRIC_SAVING_THROWS ost, int level)
	{
		RPGBaseTable<int> curSaveTable = SaveDict[oc];

		int levelIndex=0;

		for(int i = 0; i<curSaveTable.IntYIndex.Length();i++)
			if(level<=curSaveTable.IntYIndex.ValueAtIndex(i))
			{
				levelIndex = i;
				break;
			}

		switch(ost)
		{
		case OSRIC_SAVING_THROWS.saveRoSaWa:
			return curSaveTable.GetValue("aimed_magic_items",levelIndex);
			break;
		case OSRIC_SAVING_THROWS.saveBreath:
			return curSaveTable.GetValue("breath_weapons",levelIndex);
			break;
		case OSRIC_SAVING_THROWS.saveDeath: 
			return curSaveTable.GetValue("death_paralysis_poison",levelIndex);
			break;
		case OSRIC_SAVING_THROWS.savePetPoly: 
			return curSaveTable.GetValue("petrifaction_polymorph",levelIndex);
			break;
		case OSRIC_SAVING_THROWS.saveSpell: 
			return curSaveTable.GetValue("spells",levelIndex);
			break;
		default:
			return 10;
		}
	}

	public SaveCollection GetClassSaves(RPGCharacterModel rcm)
	{
		SaveCollection retCol = new SaveCollection();

		string[] characterClasses = rcm.attributes.characterClass.GetDesc().Split('/');

		for(int i=0; i<characterClasses.Length;i++)
		{
			OSRIC_CLASS curClass = OSRICConstants.GetEnum<OSRIC_CLASS>(characterClasses[i]);
			foreach(OSRIC_SAVING_THROWS ost in Enum.GetValues(typeof(OSRIC_SAVING_THROWS)))
			{
				EnumSavePair sav = new EnumSavePair(ost, GetSaveValue(curClass,ost,rcm.attributes.level[i]));
				retCol.UpdateBestSave(sav);
			}
		}

		List<OSRICCharacterModifier> racialBonuses = 
			rcm.attributes.CharacterModifiers.GetModifierByCharacterVariable(OSRIC_CHARACTER_VARIABLES.savingthrow);

		Debug.Log("Racial Bonuses: " + racialBonuses.Count.ToString());

		foreach(OSRICCharacterModifier ocm in racialBonuses)
		{
			Debug.Log(ocm.savingThrow.GetDesc() + " " + ocm.value.ToString());
			retCol.saveArr[(int)ocm.savingThrow].val += ocm.value;
		}
			
		return retCol;
	}



	public void DebugTables()
	{
		foreach (OSRIC_CLASS cls in Enum.GetValues(typeof(OSRIC_CLASS)))
		{
			try
			{
				RPGBaseTable<int> rbt = SaveDict[cls];
				rbt.DebugLog();
			}
			catch
			{
				continue;
			}
		}
	}
}
