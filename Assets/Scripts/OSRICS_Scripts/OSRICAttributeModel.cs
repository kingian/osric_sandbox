using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using OSRICAttributeModifier;

[ExecuteInEditMode]//you only need execute in edit mod if you need to continously call the update/lateupdate type events. it'll run if called regardless
public class OSRICAttributeModel : RPGAttributeModel 
{	
	//for when a reroll, or manual attribute change happens
	public delegate void baseAttributeDidChange();
	public static event baseAttributeDidChange BaseAttributeDidChange;
	//for when someone chooses a new race, or maybe magic changes it for them
	public delegate void racialModifierDidChange();
	public static event racialModifierDidChange RacialModifierDidChange;
	//your global hook for any attribute change, use sparingly
	public delegate void attributeModelDidChange();
	public static event attributeModelDidChange AttributeModelDidChange;
	public RPGCharacterModel cm;


	public string characterName;

	//Attributes
	public int Str;
	public int Dex;
	public int Con;
	public int Int;
	public int Wis;
	public int Cha;


	public int hitPoints;
	public int armorClass;
	public int[] level;
	public int experiencePoints;
	public int vision;
	public int move;
	public OSRIC_GENDER characterGender = OSRIC_GENDER.Male;
	public OSRIC_RACE characterRace = OSRIC_RACE.Human;
	public OSRIC_CLASS characterClass = OSRIC_CLASS.None;
	public OSRIC_ALIGNMENT characterAlignment = OSRIC_ALIGNMENT.Neutral;
	public OSRIC_CHARACTER_STATE characterState;

	public List<OSRICCharacterModifier> SomaticModifiers;  //Body related modifiers, e.g. race modifiers, spell effects
	public List<OSRICCharacterModifier> EquipedModifiers;

	void BroadcastBaseAttributeDidChange()
	{
		if(BaseAttributeDidChange!=null)
			BaseAttributeDidChange();
	}
	void BroadcastRacialAttributeDidChange()
	{
		if(RacialModifierDidChange!=null)
			RacialModifierDidChange();
	}
	//in the past, as a cleanup step, i might call the global event from the specific events, but that can cause spaghett so i usually wait
	void BroadcastAttributeModelDidChange()
	{
		if(AttributeModelDidChange!=null)
			AttributeModelDidChange();
	}
	
	void Awake ()
	{
		SomaticModifiers = new List<OSRICCharacterModifier> ();
		cm = gameObject.GetComponentInParent<RPGCharacterModel>();
	}
	

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}


	//we should be able to take this boilerplate and slim it down,but it's whatevs
	public int StrTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in SomaticModifiers) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Strength)
				racial_bonus += mod.value;
		}
//		Debug.Log("Str Mod: "+racial_bonus);
		return this.Str + racial_bonus;
	}

	public int DexTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in SomaticModifiers) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Dexterity)
				racial_bonus += mod.value;
		}
//		Debug.Log("Dex Mod: "+racial_bonus);
		return this.Dex + racial_bonus;
	}

	public int ConTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in SomaticModifiers) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Constitution)
				racial_bonus += mod.value;
		}
//		Debug.Log("Con Mod: "+racial_bonus);
		return this.Con + racial_bonus;
	}

	public int IntTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in SomaticModifiers) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Intellegence)
				racial_bonus += mod.value;
		}
		return this.Int + racial_bonus;
	}

	public int WisTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in SomaticModifiers) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Wisdom)
				racial_bonus += mod.value;
		}
//		Debug.Log("Wis Mod: "+racial_bonus);
		return this.Wis + racial_bonus;
	}

	public int ChaTotal(){
		//racial bonuses
		int racial_bonus = 0;
		foreach (OSRICCharacterModifier mod in SomaticModifiers) {
			if (mod.attribute == OSRIC_ATTRIBUTES.Charisma)
				racial_bonus += mod.value;
		}
//		Debug.Log("Cha Mod: "+racial_bonus);
		return this.Cha + racial_bonus;
	}


	public void UpdateCharacterOptions(CharacterOptionCollection coc)
	{
		//OSRICEngine.RemoveRaceAdjustments(this);//if we really need to call function on the engine from here we're probably in trouble and need to rethink a few things
		this.ClearRacialModifiers();//this works now without engine call like above
		characterRace = coc.charRace;
		OSRICEngine.AddRaceAdjustments(this,characterRace);
		characterAlignment = coc.charAlignment;
		characterGender = coc.charGender;
		characterClass = coc.charClass;

		string strout = "Current Modifiers: ";
		foreach(OSRICCharacterModifier ocm in SomaticModifiers)
		{
			strout += ocm.attribute.GetDesc() + " | ";
		}
		Debug.Log(strout);

		BroadcastRacialAttributeDidChange();
		BroadcastAttributeModelDidChange ();
	}

	public int GetAttributeTotal(OSRIC_ATTRIBUTES oa)
	{
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
			return this.StrTotal();
		case OSRIC_ATTRIBUTES.Dexterity:
			return this.DexTotal();
		case OSRIC_ATTRIBUTES.Constitution:
			return this.ConTotal();
		case OSRIC_ATTRIBUTES.Intellegence:
			return this.IntTotal();
		case OSRIC_ATTRIBUTES.Wisdom:
			return this.WisTotal();
		case OSRIC_ATTRIBUTES.Charisma:
			return this.ChaTotal();
		default:
			return -1;
		}
	}

	public int GetBaseAttribute(OSRIC_ATTRIBUTES oa)
	{
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
			return Str;
		case OSRIC_ATTRIBUTES.Dexterity:
			return Dex;
		case OSRIC_ATTRIBUTES.Constitution:
			return Con;
		case OSRIC_ATTRIBUTES.Intellegence:
			return Int;
		case OSRIC_ATTRIBUTES.Wisdom:
			return Wis;
		case OSRIC_ATTRIBUTES.Charisma:
			return Cha;
		default:
			return -1;
		}
	}

	public void SetBaseAttribute(OSRIC_ATTRIBUTES oa, int val)
	{
		switch(oa)
		{
		case OSRIC_ATTRIBUTES.Strength:
		{
			Str = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Dexterity:
		{
			Dex = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Constitution:
		{
			Con = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Intellegence:
		{
			Int = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Wisdom:
		{
			Wis = val;
			break;
		}
		case OSRIC_ATTRIBUTES.Charisma:
		{
			Cha = val;
			break;
		}
		default:
			return;
		}

		BroadcastBaseAttributeDidChange ();
		BroadcastAttributeModelDidChange ();
	}

	//we could make this more generic and call it AddBaseAttributeModifier and store it all one list
	public void AddRacialModifier(OSRICCharacterModifier modifier)
	{
		if(!SomaticModifiers.Contains(modifier))
			SomaticModifiers.Add (modifier);

		BroadcastRacialAttributeDidChange ();
		BroadcastAttributeModelDidChange ();
	}
	//in this particular use case the only time we need to remove a racial modifier we can just remove all of them cause someone changed their race
	//we dont want direct access to anything in the model, even its redundant, because enevitably we'll want to broadast events, or do cleanup, etc.
	public void ClearRacialModifiers(){
		SomaticModifiers.Clear ();
		BroadcastRacialAttributeDidChange();
		BroadcastAttributeModelDidChange ();
	}
}
