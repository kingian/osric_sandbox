using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;

[ExecuteInEditMode]
public class generalTests : MonoBehaviour {
	

	OSRICEngine _engine;
	public OSRICEngine engine
	{get
		{
			if(_engine==null)
				_engine = gameObject.GetComponentInChildren<OSRICEngine>();
				if(_engine==null)
					_engine = gameObject.AddComponent<OSRICEngine>();
			return _engine;
		}
	}

	public RPGCharacterModel testChar;
	

		

	void Start () 
	{
		engine.init();
		testChar = FindObjectOfType<RPGCharacterModel>();
		RandomizeCharactersAttributes(testChar);
		int i;
		AttributeAdjustment temp;
		string outStr = "Str:" + testChar.attributes.Str.ToString();
		for(i=0; i<5;i++)
		{
			temp = engine.GetStrengthAdjustments(testChar.attributes.Str)[i];
			outStr += " " + temp.title + ":" + temp.adjustment.ToString();
		}
		Debug.Log(outStr);
		outStr = "";

		temp = engine.GetIntelligenceAdjustments(testChar.attributes.Int)[0];
		Debug.Log( "Int: " + testChar.attributes.Int + " " + temp.title + ":" + temp.adjustment);


		outStr = "Dex:" + testChar.attributes.Dex.ToString();
		for(i=0; i<3;i++)
		{
			temp = engine.GetDexterityAdjustments(testChar.attributes.Dex)[i];
			outStr += " " + temp.title + ":" + temp.adjustment.ToString();
		}
		Debug.Log(outStr);
		outStr = "";

		outStr = "Wis:" + testChar.attributes.Wis.ToString();
		for(i=0; i<1;i++)
		{
			temp = engine.GetWisdomAdjustments(testChar.attributes.Wis)[i];
			outStr += " " + temp.title + ":" + temp.adjustment.ToString();
		}
		Debug.Log(outStr);
		outStr = "";

		outStr = "Con:" + testChar.attributes.Con.ToString();
		for(i=0; i<3;i++)
		{
			temp = engine.GetConstitutionAdjustments(testChar.attributes.Con)[i];
			outStr += " " + temp.title + ":" + temp.adjustment.ToString();
		}
		Debug.Log(outStr);
		outStr = "";

		outStr = "Cha:" + testChar.attributes.Cha.ToString();
		for(i=0; i<3;i++)
		{
			temp = engine.GetCharismaAdjustments(testChar.attributes.Cha)[i];
			outStr += " " + temp.title + ":" + temp.adjustment.ToString();
		}
		Debug.Log(outStr);
		outStr = "";

		foreach(OSRIC_RACE or in engine.AvailableRaces(testChar.attributes))
			outStr += or.GetDesc() + " : ";

		Debug.Log(outStr);
		outStr = "by attributes: ";

		foreach(OSRIC_CLASS oc in engine.AvailableClassesByAttributes(testChar.attributes))
			outStr += oc.GetDesc() + " : ";

		Debug.Log(outStr);
		outStr = "by race: ";

		foreach(OSRIC_CLASS oc in engine.AvailableClassesByRace(testChar.attributes))
			outStr += oc.GetDesc() + " : ";

		Debug.Log(outStr);
		outStr = "intersection: ";

		HashSet<OSRIC_CLASS> atts = engine.AvailableClassesByAttributes(testChar.attributes);
		HashSet<OSRIC_CLASS> race = engine.AvailableClassesByRace(testChar.attributes);

		race.IntersectWith(atts);

		foreach(OSRIC_CLASS oc in race)
			outStr += oc.GetDesc() + " : ";
		
		Debug.Log(outStr);
		outStr = "";

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RandomizeCharactersAttributes(RPGCharacterModel charmod)
	{
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			charmod.attributes.SetAttribute(oa,randomizeAttribute());
		}
	}

	int randomizeAttribute()
	{
		List<int> rolls = new List<int>();
		rolls.Add(UnityEngine.Random.Range(1,6));
		rolls.Add(UnityEngine.Random.Range(1,6));
		rolls.Add(UnityEngine.Random.Range(1,6));
		rolls.Add(UnityEngine.Random.Range(1,6));
		rolls.Add(UnityEngine.Random.Range(1,6));
		rolls.Sort();
		rolls.RemoveRange(0,2);

		int valout = 0;

		foreach(int i in rolls)
			valout += i;

		

//		return valout;
		return UnityEngine.Random.Range(3,18);
	}



	string parseMulti(string mclass)
	{
		attributes mins = new attributes(mclass);
		string [] classes;
		classes = mclass.Split('/');
		foreach(string s in classes)
		{
			int classIndex = engine.classMinimums.GetYIndexOf(s);
			foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
			{
				int tempStat = engine.classMinimums.GetValue(oa.GetDesc(),classIndex);
				mins.compareMin(new EnumAttributePair(oa,tempStat));
			}
		}
		return mins.ouputString();
	}


}

public class attributes
{
	public string mclassNmae;
	public List<EnumAttributePair> stats;

	public attributes(string _className)
	{
		mclassNmae = _className;
		stats = new List<EnumAttributePair>();
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			stats.Add(new EnumAttributePair(oa,0));
		}
	}

	public int FindStat(OSRIC_ATTRIBUTES oa)
	{
		foreach(EnumAttributePair eap in stats)
		{
			if(eap.stat==oa)
				return stats.IndexOf(eap);
		}
		return -1;
	}

	public string ouputString ()
	{
		string retStr = mclassNmae + "\t\t\t,";
		foreach(EnumAttributePair eap in stats)
		{
			retStr += eap.value.ToString() + ", ";
		}
		retStr += "\n";
		Debug.Log(retStr);
		return retStr;
	}

	public void compareMin(EnumAttributePair eap)
	{
		int currentIndex = FindStat(eap.stat);
		if(eap.value > stats[currentIndex].value)
		{
			stats.RemoveAt(currentIndex);
			stats.Insert(currentIndex,eap);
		}
	}
}
