using UnityEngine;
using System.Collections;
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
		int val = engine.attributeTable.GetYIndexOf(testChar.attributes.Str);
		for(int i=0; i<5;i++)
		{

			AttributeAdjustment temp = engine.GetStrengthAdjustments(val)[i];
			Debug.Log(temp.title + ":" + temp.adjustment);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void RandomizeCharactersAttributes(RPGCharacterModel charmod)
	{
		charmod.attributes.Str = randomizeAttribute();
		charmod.attributes.Int = randomizeAttribute();
		charmod.attributes.Dex = randomizeAttribute();
		charmod.attributes.Con = randomizeAttribute();
		charmod.attributes.Wis = randomizeAttribute();
		charmod.attributes.Cha = randomizeAttribute();
	}

	int randomizeAttribute()
	{
		return Random.Range(3,18);
	}


}
