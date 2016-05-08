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
//		JSONCharacterList = new JSONObject(JSONObject.Type.ARRAY);
		LoadCharactersFromSaveFile();
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
		JSONObject addition = cm.attributes.Serialize();
		foreach(JSONObject jo in JSONCharacterList.list)
			if(CompareJSONCharacterAttributes(jo,addition))
				return;
		JSONCharacterList.Add(addition);

		StreamWriter sw = new StreamWriter(SavedCharactersFile,false);
		sw.Write(JSONCharacterList.ToString());
		sw.Close();
	}

	private bool CompareJSONCharacterAttributes(JSONObject _member, JSONObject _addition)
	{
		Debug.Log("MEMBER: " + _member.ToString());
		Debug.Log("ADD: " + _addition.ToString());
		if(!_member.HasField("characterName") || !_addition.HasField("characterName"))
		{
			Debug.Log("One of these items isn't a JSON character representation.");
			return false;
		}

		if(_member["characterName"].str == _addition["characterName"].str &&
			_member["characterRace"].str == _addition["characterRace"].str &&
			_member["characterClass"].str == _addition["characterClass"].str)
			return true;

		return false;
	}

	public void LoadCharactersFromSaveFile()
	{
		StreamReader sr = new StreamReader(SavedCharactersFile);
		JSONCharacterList = new JSONObject(sr.ReadToEnd());
		if(JSONCharacterList.IsNull)
		{
			JSONCharacterList = new JSONObject(JSONObject.Type.ARRAY);
			return;
		}
		foreach(JSONObject jo in JSONCharacterList.list)
		{
			RPGCharacterModel cm = new RPGCharacterModel();
			cm.attributes = new OSRICAttributeModel(cm,jo);
			Debug.Log(cm.attributes.characterName);
			if(!mainCon.CharacterList.Contains(cm))
				mainCon.CharacterList.Add(cm);
		}
		Debug.Log(mainCon.CharacterList.Count);
	}


}
