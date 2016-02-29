using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class CharacterSheetUIController : MonoBehaviour {
	

	public AttributeUIGroup attributeGroup;
	public RPGCharacterModel charModel;
	public OSRICEngine engine;
	public ClassDropUIController classDropController;
	public Dropdown raceDrop;
	public Dropdown genderDrop;
	public Dropdown classDrop;
	public Dropdown alignmentDrop;

	
	

	public void UpdateAttributeModelOptions()
	{
//		Debug.Log("UPDATE ATTRIBUTE MODEL OPTIONS CALLED");
		CharacterOptionCollection attCol = GetCharacterOptionsFromDrops();
		if(!engine.VerifyOptionCollection(attCol))
			attCol.charClass = OSRIC_CLASS.None;
		charModel.attributes.UpdateCharacterOptions(attCol);

	}

	public CharacterOptionCollection GetCharacterOptionsFromDrops()
	{
		CharacterOptionCollection retCol = new CharacterOptionCollection();
		retCol.charGender = OSRICConstants.GetEnum<OSRIC_GENDER>(genderDrop.options[genderDrop.value].text);
//		Debug.Log("options from drop: " + raceDrop.value);
		try
		{
			retCol.charRace = OSRICConstants.GetEnum<OSRIC_RACE>(raceDrop.options[raceDrop.value].text);
		}
		catch(Exception e)
		{
			Debug.Log(e.ToString());
		}
		Debug.Log("option from class drop: " + classDrop.value);
		try
		{
		if(classDrop.options.Contains(classDrop.options[classDrop.value])) // THIS CALL IS THROWING AN EXCEPTION
			retCol.charClass = OSRICConstants.GetEnum<OSRIC_CLASS>(classDrop.options[classDrop.value].text);
		else
			retCol.charClass = OSRIC_CLASS.None;
		}
		catch(Exception e)
		{
			Debug.Log(e.ToString());
		}
		retCol.charAlignment = OSRICConstants.GetEnum<OSRIC_ALIGNMENT>(alignmentDrop.options[alignmentDrop.value].text);
		return retCol;
	}


	void OnEnable()
	{
		OSRICAttributeModel.BaseAttributeDidChange += UpdateCharacterViewInformation;
		OSRICAttributeModel.RacialModifierDidChange += UpdateCharacterViewInformation;
		genderDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
		raceDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
		classDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
		alignmentDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
	}

	void OnDisable()
	{
		OSRICAttributeModel.BaseAttributeDidChange -= UpdateCharacterViewInformation;
		OSRICAttributeModel.RacialModifierDidChange -= UpdateCharacterViewInformation;
		genderDrop.onValueChanged.RemoveAllListeners();
		raceDrop.onValueChanged.RemoveAllListeners();
		classDrop.onValueChanged.RemoveAllListeners();
		alignmentDrop.onValueChanged.RemoveAllListeners();
	}


	void Awake()
	{
		raceDrop = GameObject.Find("raceDropdown").GetComponent<Dropdown>();
		genderDrop = GameObject.Find("genderDropdown").GetComponent<Dropdown>();
		classDrop = GameObject.Find ("classDropdown").GetComponent<Dropdown>();
		alignmentDrop = GameObject.Find ("alignmentDropdown").GetComponent<Dropdown>();
		engine = GameObject.FindObjectOfType<OSRICEngine>();
		classDropController = gameObject.GetComponentInChildren<ClassDropUIController>();
	}



	// Use this for initialization
	void Start () 
	{
		UpdateCharacterViewInformation();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void UpdateCharacterViewInformation()
	{
		UpdateAttributeViewInformation();
		ClearAllDropdowns();
		SetGenderOptions();
		SetAlignmentOptions();
		SetRaceOptions();
		SetClassOptions();
	}


	void UpdateAttributeViewInformation()
	{
		OSRICAttributeModel oam = charModel.attributes;
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
//			Debug.Log( oa.GetDesc() + oam.GetAttribute(oa));
			int currentAtt = oam.GetAttributeTotal(oa);
			string subtext = GetAttributeAdjustments(oa,currentAtt);
			attributeGroup.SetAttribute(oa,currentAtt,subtext);
		}
	}



	string GetAttributeAdjustments(OSRIC_ATTRIBUTES oa, int val)
	{
		string retStr="";
		AttributeAdjustment[] attCollector;
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
			attCollector = engine.GetStrengthAdjustments(val);
			break;
		case OSRIC_ATTRIBUTES.Dexterity:
			attCollector = engine.GetDexterityAdjustments(val);
			break;
		case OSRIC_ATTRIBUTES.Constitution:
			attCollector = engine.GetConstitutionAdjustments(val);
			break;
		case OSRIC_ATTRIBUTES.Intellegence:
			attCollector = engine.GetIntelligenceAdjustments(val);
			break;
		case OSRIC_ATTRIBUTES.Wisdom:
			attCollector = engine.GetWisdomAdjustments(val);
			break;
		case OSRIC_ATTRIBUTES.Charisma:
			attCollector = engine.GetCharismaAdjustments(val);
			break;
		default:
			attCollector = new AttributeAdjustment[0];
			break;
		}
		if(attCollector.Length>0)
		{
			retStr = attCollector[0].title + ": " + attCollector[0].adjustment;
			for(int i=1;i<attCollector.Length;i++)
				retStr += " " + attCollector[i].title + ": " + attCollector[i].adjustment;

		}
		return retStr;
	}

	// DROP DOWN SECTION

	void ClearAllDropdowns()
	{
		raceDrop.options.Clear();
		genderDrop.options.Clear();
		classDrop.options.Clear();
		alignmentDrop.options.Clear();
	}

	int GetOptionPosition(Dropdown drop, string searchTerm)
	{
		foreach(Dropdown.OptionData opt in drop.options)
		{
			if(opt.text==searchTerm)
				return drop.options.IndexOf(opt);
		}
		return -1;
	}

	void SetRaceOptions()
	{
		foreach(OSRIC_RACE or in engine.AvailableRaces(charModel.attributes))
		{
			raceDrop.options.Add(new Dropdown.OptionData(or.GetDesc()));
		}
		raceDrop.value = GetOptionPosition(raceDrop,charModel.attributes.characterRace.GetDesc());
	}

	void SetGenderOptions()
	{
		foreach(OSRIC_GENDER og in Enum.GetValues(typeof(OSRIC_GENDER)))
		{
			genderDrop.options.Add(new Dropdown.OptionData(og.GetDesc()));
		}
		genderDrop.value = GetOptionPosition(genderDrop,charModel.attributes.characterGender.GetDesc());
	}

	void SetAlignmentOptions()
	{
		foreach(OSRIC_ALIGNMENT oa in Enum.GetValues(typeof(OSRIC_ALIGNMENT)))
		{
			alignmentDrop.options.Add(new Dropdown.OptionData(oa.GetDesc()));
		}
		alignmentDrop.value = GetOptionPosition(alignmentDrop,charModel.attributes.characterAlignment.GetDesc());
	}

	void SetClassOptions()
	{
//			Debug.Log("SET CLASS OPTION CALLED");
		HashSet<OSRIC_CLASS> atts = engine.AvailableClassesByAttributes(charModel.attributes);
		HashSet<OSRIC_CLASS> race = engine.AvailableClassesByRace(charModel.attributes);
		
		race.IntersectWith(atts);
//		foreach(OSRIC_CLASS oc in race)
//		{
//			Dropdown.OptionData tempOpt = new Dropdown.OptionData(oc.GetDesc()); 
//			if(!classDrop.options.Contains(tempOpt))
//				classDrop.options.Add(tempOpt);
//		}
	classDropController.SetOptionsAndSelected(race,charModel.attributes.characterClass);

		if(!race.Contains(charModel.attributes.characterClass))
			charModel.attributes.characterClass = OSRIC_CLASS.None;

		classDrop.value = GetOptionPosition(classDrop,charModel.attributes.characterClass.GetDesc());
	}

}
