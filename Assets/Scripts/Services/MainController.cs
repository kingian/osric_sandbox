using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {

	public static List<RPGCharacterModel> CharacterList;
	public RPGCharacterModel CurrentCharacter;
	public OSRICEngine engine;
	public CharacterCreatorUIController CreatorUI;



	// Use this for initialization
	void Start () 
	{
		CharacterList = new List<RPGCharacterModel>();
		engine = gameObject.AddComponent<OSRICEngine>();
		CreatorUI.engine = engine;
		CreateCharacter();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(engine==null)
			engine = gameObject.AddComponent<OSRICEngine>();
	}


	public static void SaveCharacter(RPGCharacterModel rcm)
	{
		CharacterList.Add(rcm);
	}

	public static void DeleteCharacter(RPGCharacterModel rcm)
	{
		CharacterList.Remove(rcm);
	}

	public void CreateCharacter()
	{
		CurrentCharacter = gameObject.AddComponent<RPGCharacterModel>();
		CreatorUI.charModel = CurrentCharacter;
		engine.RandomizeCharactersAttributes(CurrentCharacter);
	}

}
