using UnityEngine;
using System.Collections;

public class OSRICSaveLoadCharacter
{
	OSRICEngine engine;
	MainController mainCon;


	OSRICSaveLoadCharacter(OSRICEngine _engine, MainController _mainCon)
	{
		engine = _engine;
		mainCon = _mainCon;
	}


	public JSONObject SerializeOsricAttributeModel(RPGCharacterModel cm)
	{
		JSONObject retObject = new JSONObject();



		return retObject;
	}


}
