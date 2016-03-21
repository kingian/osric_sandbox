using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {

	public List<RPGCharacterModel> CharacterList;
	public RPGCharacterModel CurrentCharacter;
	public OSRICEngine engine;
	public CharacterCreatorUIController CreatorUI;
	public CharacterViewerUIController ViewerUI;
	public HomeDashboardUIController DashboardUI;



	// Use this for initialization
	void Start () 
	{
		CharacterList = new List<RPGCharacterModel>();
		engine = gameObject.AddComponent<OSRICEngine>();
		CreatorUI.engine = engine;
		ViewerUI.engine = engine;
		ViewerUI.gameObject.SetActive(false);
		CreatorUI.gameObject.SetActive(false);
		DashboardUI.gameObject.SetActive(true);
	}


	public void SetToCharacterCreationMode()
	{
		DashboardUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(false);
		CreatorUI.gameObject.SetActive(true);
		CreatorUI.attributeGroup.OrderAttributeElements();
		CreateCharacter();
	}

	public void SetToCharacterViewMode()
	{
		DashboardUI.gameObject.SetActive(false);
		CreatorUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(true);
		ViewerUI.LoadCharacterAttributes(CurrentCharacter);
	}


	public void SetToHomeMode()
	{

		CreatorUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(false);
		DashboardUI.gameObject.SetActive(true);
	}


	// Update is called once per frame
	void Update () 
	{
		if(engine==null)
			engine = gameObject.AddComponent<OSRICEngine>();
	}


	public void SaveCharacter()
	{
		if(!CharacterList.Contains(CurrentCharacter))
			CharacterList.Add(CurrentCharacter);
	}

	public void DeleteCharacter(RPGCharacterModel rcm)
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

