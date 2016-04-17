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
