using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;


public class CharacterSheetUIController : MonoBehaviour {
	

	public AttributeUIGroup attributeGroup;
	public RPGCharacterModel charModel;
	public OSRICEngine engine;
	public Dropdown raceDrop;
	public Dropdown genderDrop;
	public Dropdown classDrop;
	public Dropdown alignmentDrop;

	
	

	public void UpdateAttributeModelOptions()
	{

		string gen = genderDrop.options[genderDrop.value].text;
		string race = raceDrop.options[raceDrop.value].text;
		string clas = classDrop.options[classDrop.value].text;
		string alignment = alignmentDrop.options[alignmentDrop.value].text;
		Debug.Log("State Change: " + " " + gen + " " + race + " " + clas + " " + alignment);

		charModel.attributes.UpdateCharacterOptions(GetCharacterOptionsFromDrops());

	}

	public CharacterOptionCollection GetCharacterOptionsFromDrops()
	{
		CharacterOptionCollection retCol = new CharacterOptionCollection();
		retCol.charGender = OSRICConstants.GetEnum<OSRIC_GENDER>(genderDrop.options[genderDrop.value].text);
		retCol.charRace = OSRICConstants.GetEnum<OSRIC_RACE>(raceDrop.options[raceDrop.value].text);
		retCol.charClass = OSRICConstants.GetEnum<OSRIC_CLASS>(classDrop.options[classDrop.value].text);
		retCol.charAlignment = OSRICConstants.GetEnum<OSRIC_ALIGNMENT>(alignmentDrop.options[alignmentDrop.value].text);
		return retCol;
	}


	void OnEnable()
	{
		OSRICAttributeModel.attributeUpdateEvent += UpdateCharacterViewInformation;
		genderDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
		raceDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
		classDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
		alignmentDrop.onValueChanged.AddListener(delegate {UpdateAttributeModelOptions();});
	}

	void OnDisable()
	{
		OSRICAttributeModel.attributeUpdateEvent -= UpdateCharacterViewInformation;
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
		UpdateAttributeViewInformation(charModel.attributes);
		ClearAllDropdowns();
		SetRaceOptions();
		SetGenderOptions();
		SetAlignmentOptions();
		SetClassOptions();
	}


	void UpdateAttributeViewInformation(OSRICAttributeModel oam)
	{
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			Debug.Log( oa.GetDesc() + oam.GetAttribute(oa));
			attributeGroup.SetAttribute(oa,oam.GetAttribute(oa));
		}
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
		HashSet<OSRIC_CLASS> atts = engine.AvailableClassesByAttributes(charModel.attributes);
		HashSet<OSRIC_CLASS> race = engine.AvailableClassesByRace(charModel.attributes);
		
		race.IntersectWith(atts);
		foreach(OSRIC_CLASS oc in race)
		{
			classDrop.options.Add(new Dropdown.OptionData(oc.GetDesc()));
		}
		classDrop.value = GetOptionPosition(classDrop,charModel.attributes.characterClass.GetDesc());
	}

}
