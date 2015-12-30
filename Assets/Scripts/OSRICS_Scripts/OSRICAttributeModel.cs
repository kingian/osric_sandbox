using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OSRICAttributeModel : RPGAttributeModel {

	public RPGCharacterModel cm;

	//Attributes
	public int Str;
	public int Dex;
	public int Con;
	public int Int;
	public int Wis;
	public int Cha;

	public int hitPoints;
	public int armorClass;
	public int level;
	public int experiencePoints;




	void Awake ()
	{

		cm = gameObject.GetComponentInParent<RPGCharacterModel>();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
