using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainController : MonoBehaviour {

	[SerializeField] public List<RPGCharacterModel> CharacterList;
	[SerializeField] public RPGCharacterModel CurrentCharacter;
	public OSRICEngine engine;
	public CharacterCreatorUIController CreatorUI;
	public CharacterViewerUIController ViewerUI;
	public HomeDashboardUIController DashboardUI;
	public SettingsEditorUIController SettingsUI;
	public EquipEditorUIController EquipUI; 
	public NavigationUIController NavUI;
	public OSRICSaveLoadData DataIO;
	public NAV_STATE ApplicationState;




	// Use this for initialization
	void Start () 
	{
		CharacterList = new List<RPGCharacterModel>();
		engine = gameObject.AddComponent<OSRICEngine>();
		DataIO = new OSRICSaveLoadData(engine,this);
		CreatorUI.engine = engine;
		CreatorUI.mainCon = this;
		ViewerUI.engine = engine;
		NavUI.SetNavigationMode(NAV_STATE.Home);
		SetNavigationMode(NAV_STATE.Home);


	}


	public void SetToCharacterCreationMode()
	{
		CreatorUI.gameObject.SetActive(true);
		CreateCharacter();
		CreatorUI.attributeGroup.OrderAttributeElements();
	}

	public void SetToCharacterViewMode()
	{
		ViewerUI.gameObject.SetActive(true);
		ViewerUI.LoadCharacterAttributes(CurrentCharacter);
		ViewerUI.attributeGroup.OrderAttributeElements();
	}


	public void SetToHomeMode()
	{
		DashboardUI.gameObject.SetActive(true);
		DashboardUI.UpdateCharacterList();
	}

	public void SetToSettingsMode()
	{
		SettingsUI.gameObject.SetActive(true);
	}

	public void SetToEquipMode()
	{
		EquipUI.gameObject.SetActive(true);
	}


	public void CloseAllUI()
	{
		CurrentCharacter = null;
		CreatorUI.gameObject.SetActive(false);
		ViewerUI.gameObject.SetActive(false);
		DashboardUI.gameObject.SetActive(false);
		SettingsUI.gameObject.SetActive(false);
		EquipUI.gameObject.SetActive(false);
	}


	public void SetNavigationMode(NAV_STATE _ns)
	{
		CloseAllUI();
		ApplicationState = _ns;

		switch(ApplicationState)
		{
		case NAV_STATE.Home:
			SetToHomeMode();
			break;
		case NAV_STATE.CharacterCreator:
			SetToCharacterCreationMode();
			break;
		case NAV_STATE.CharacterViewer:
			SetToCharacterViewMode();
			break;
		case NAV_STATE.Equip:
			SetToEquipMode();
			break;
		case NAV_STATE.Settings:
			SetToSettingsMode();
			break;
		case NAV_STATE.Spells:
			SetToEquipMode();
			break;
		case NAV_STATE.Invitations:
			SetToHomeMode();
			break;
		default:
			SetToHomeMode();
			break;
		}

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
		DataIO.SaveCharacter(CurrentCharacter);
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

