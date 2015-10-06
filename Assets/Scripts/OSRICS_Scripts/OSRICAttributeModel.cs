using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OSRICAttributeModel : RPGAttributeModel {

	public RPGCharacterModel cm;

	//Attributes
	public OSRICstrength Str;
	public OSRICdexterity Dex;
	public OSRICconstitution Con;
	public OSRICintelligence Int;
	public OSRICwisdom Wis;
	public OSRICcharisma Cha;

	public int hitPoints;
	public int armorClass;
	public int level;
	public int experiencePoints;
	public OSRICCoreRace race;



	void Awake ()
	{
		Str = new OSRICstrength(0);
		Dex = new OSRICdexterity(0);
		Con = new OSRICconstitution(0);
		Int = new OSRICintelligence(0);
		Wis = new OSRICwisdom(0);
		Cha = new OSRICcharisma(0);
		cm = gameObject.GetComponentInParent<RPGCharacterModel>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
