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
		"OSRIC_ranger_save"};
	
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
			if(curSaveTable.IntYIndex.ValueAtIndex(i)<=level)
				levelIndex = i;

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
			return 0;
		}
	}

	public SaveCollection GetClassSaves(RPGCharacterModel rcm)
	{
		SaveCollection retCol = new SaveCollection();


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
