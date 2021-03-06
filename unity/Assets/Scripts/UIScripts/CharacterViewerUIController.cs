﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterViewerUIController : MonoBehaviour {


	public AttributeUIGroup attributeGroup;
	public RPGCharacterModel charModel;
	public OSRICEngine engine;
	public GameObject SaveUIOrigin;
	public LabelValueUIController[] characterDetailsArr;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{
		characterDetailsArr = 
			gameObject.GetComponentsInChildren<LabelValueUIController>();
		attributeGroup = gameObject.GetComponentInChildren<AttributeUIGroup>();
	}

	public void LoadCharacterAttributes(RPGCharacterModel cm)
	{
		if(gameObject.activeInHierarchy)
		{
			charModel = cm;
			UpdateAttributeViewInformation();
			SetCharacterDetails(cm);
			SetCharacterSavingThrows(cm);
		}
	}

	void SetCharacterDetails(RPGCharacterModel cm)
	{
		foreach(LabelValueUIController lvuc in characterDetailsArr)
		{
			if(lvuc.labelText.text.ToLower().Contains("name"))
				lvuc.SetValueString(cm.attributes.characterName);
			else if(lvuc.labelText.text.ToLower().Contains("race"))
				lvuc.SetValueString(cm.attributes.characterRace.GetDesc());
			else if(lvuc.labelText.text.ToLower().Contains("class"))
				lvuc.SetValueString(cm.attributes.characterClass.GetDesc());
			else if(lvuc.labelText.text.ToLower().Contains("alignment"))
				lvuc.SetValueString(cm.attributes.characterAlignment.GetDesc());
			else if(lvuc.labelText.text.ToLower().Contains("level"))
			{
				string levelString = cm.attributes.level[0].ToString();
				for(int i=1;i<cm.attributes.level.Length;i++)
					levelString += " / " + cm.attributes.level[i].ToString();
				lvuc.SetValueString(levelString);
			}
			else if(lvuc.labelText.text.ToLower().Contains("hp"))
				lvuc.SetValueString(cm.attributes.hitPoints.ToString());
			else if(lvuc.labelText.text.ToLower().Contains("ac"))
				lvuc.SetValueString(engine.ComputeArmorClass(charModel).ToString());
			else
				continue;
		}
	}


	void SetCharacterSavingThrows(RPGCharacterModel cm)
	{
		List<LabelValueUIController> saveUIList = new List<LabelValueUIController>();

		foreach(LabelValueUIController lvuc in characterDetailsArr)
		{
			if(lvuc.gameObject.name.ToLower().Contains("save"))
			{
				int insertAt = Int32.Parse(lvuc.name.Split('_')[1]);
				saveUIList.Insert((insertAt-1),lvuc);
				//saveList.Add(lvuc);
			}
		}
		SaveCollection saves = engine.GetSaveCollection(cm);
		int i = 0;
		foreach(EnumSavePair esp in saves.saveArr)
		{
			LabelValueUIController cur = saveUIList[i];
			cur.SetLableString(esp.save.GetDesc());
			cur.SetValueString(esp.val.ToString());
			i++;
		}
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
		if(val<1)
			return retStr;
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

}
