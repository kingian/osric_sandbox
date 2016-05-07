using UnityEngine;
using System.IO;
using System.Collections;

public class OSRICSaveLoadData
{
	OSRICEngine engine;
	MainController mainCon;
	string SavedCharactersFile = "Assets/Resources/saved_characters.json";
	JSONObject JSONCharacterList;


	public OSRICSaveLoadData(OSRICEngine _engine, MainController _mainCon)
	{
		engine = _engine;
		mainCon = _mainCon;
		VerifySaveFile();
	}


	private void VerifySaveFile()
	{
		if(File.Exists(SavedCharactersFile))
			return;
		StreamWriter sw = new StreamWriter(SavedCharactersFile);
		sw.Close();
	}


	public void SaveCharacter(RPGCharacterModel cm)
	{

	}

	public bool CompareJSONCharacterAttributes(JSONObject _member, JSONObject _addition)
	{

		if(!_member.HasField("characterName") || !_addition.HasField("characterName"))
			return false;

		if(_member["characterName"].str == _addition["characterName"].str &&
			_member["characterRace"].str == _addition["characterRace"].str &&
			_member["characterClass"].str == _addition["characterClass"].str)
			return true;

		return false;
	}


}
