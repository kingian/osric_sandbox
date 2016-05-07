﻿using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {

	[SerializeField] public List<RPGCharacterModel> CharacterList;
	[SerializeField] public RPGCharacterModel CurrentCharacter;
	public OSRICEngine engine;
	public CharacterCreatorUIController CreatorUI;
	public CharacterViewerUIController ViewerUI;
	public HomeDashboardUIController DashboardUI;
	public OSRICSaveLoadData DataIO;



	// Use this for initialization
	void Start () 
	{
		CharacterList = new List<RPGCharacterModel>();
		engine = gameObject.AddComponent<OSRICEngine>();
		CreatorUI.engine = engine;
		CreatorUI.mainCon = this;
		ViewerUI.engine = engine;
		SetToHomeMode();
		DataIO = new OSRICSaveLoadData(engine,this);
	}


	public void SetToCharacterCreationMode()
	{
		DashboardUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(false);
		CreatorUI.gameObject.SetActive(true);
		CreateCharacter();
		CreatorUI.attributeGroup.OrderAttributeElements();
	}

	public void SetToCharacterViewMode()
	{
		DashboardUI.gameObject.SetActive(false);
		CreatorUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(true);
		ViewerUI.LoadCharacterAttributes(CurrentCharacter);
		ViewerUI.attributeGroup.OrderAttributeElements();
	}


	public void SetToHomeMode()
	{
		CurrentCharacter = null;
//		Destroy(CurrentCharacter);
		CreatorUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(false);
		DashboardUI.gameObject.SetActive(true);
		DashboardUI.UpdateCharacterList();
	}


	// Update is called once per frame
	void Update () 
	{
		if(engine==null)
			engine = gameObject.AddComponent<OSRICEngine>();
	}

	public void SaveAndReturn()
	{
		SaveCharacter();
		CurrentCharacter = null;
		SetToHomeMode();
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
		CurrentCharacter = new RPGCharacterModel();
		CreatorUI.charModel = CurrentCharacter;
		engine.RandomizeCharactersAttributes(CurrentCharacter);
	}

	public void LoadCharacter(RPGCharacterModel cm)
	{
		CurrentCharacter = cm;
		SetToCharacterViewMode();
	}



}

