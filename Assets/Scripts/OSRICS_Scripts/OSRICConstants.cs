using UnityEngine;
using System;
using System.Collections;
using System.ComponentModel;


public enum OSRIC_ATTRIBUTES
{
	[Description("str")]
	Strength,
	[Description("dex")]
	Dexterity,
	[Description("con")]
	Constitution,
	[Description("int")]
	Intellegence,
	[Description("wis")]
	Wisdom,
	[Description("cha")]
	Charisma
}
;



public enum OSRIC_RACE
{
	[Description("Human")]
	Human,
	[Description("Elf")]
	Elf,
	[Description("Dwarf")]
	Dwarf,
	[Description("Half-Elf")]
	HalfElf,
	[Description("Halfling")]
	Halfling,
	[Description("Gnome")]
	Gnome,
	[Description("Half-Orc")]
	HalfOrc
	}
;

public enum OSRIC_CLASS
{
	[Description("Thief")]
	Thief,
	[Description("Assassin")]
	Assassin,
	[Description("Cleric")]
	Cleric,
	[Description("Druid")]
	Druid,
	[Description("Fighter")]
	Fighter,
	[Description("Paladin")]
	Paladin,
	[Description("Ranger")]
	Ranger,
	[Description("Magic-User")]
	MagicUser,
	[Description("Illusionist")]
	Illusionist,
	[Description("Fighter/Thief")]
	Fighter_Thief,
	[Description("Fighter/Magic-User")]
	Fighter_MagicUser,
	[Description("Magic-User/Thief")]
	MagicUser_Thief,
	[Description("Fighter/Magic-User/Thief")]
	Fighter_MagicUser_Thief,
	[Description("Fighter/Illusionist")]
	Fighter_Illusionist,
	[Description("Illusionist/Thief")]
	Illusionist_Thief,
	[Description("Cleric/Fighter")]
	Cleric_Fighter,
	[Description("Cleric/Ranger")]
	Cleric_Ranger,
	[Description("Cleric/Magic-User")]
	Cleric_MagicUser,
	[Description("Cleric/Fighter/Magic-User")]
	Cleric_Fighter_MagicUser,
	[Description("Cleric/Thief")]
	Cleric_Thief,
	[Description("Cleric/Assassin")]
	Cleric_Assassin,
	[Description("Fighter/Assassin")]
	Fighter_Assassin
}


public enum OSRIC_ALIGNMENT
{
	[Description("Lawful Good")]
	LawfulGood,
	[Description("Chaotic Good")]
	ChaoticGood,
	[Description("Neutral Good")]
	NeutralGood,
	[Description("Chaotic Neutral")]
	ChaoticNeutral,
	[Description("Neutral")]
	Neutral,
	[Description("Lawful Neutral")]
	LawfulNeutral,
	[Description("Neutral Evil")]
	NeutralEvil,
	[Description("Chaotic Evil")]
	ChaoticEvil,
	[Description("Lawful Evil")]
	LawfulEvil
}

public enum OSRIC_GENDER
{
	[Description("Male")]
	Male,
	[Description("Female")]
	Female,
}


public struct CharacterOptionCollection
{
	public OSRIC_GENDER charGender;
	public OSRIC_RACE charRace;
	public OSRIC_CLASS charClass;
	public OSRIC_ALIGNMENT charAlignment;
	public string charName;
}

public struct AttributeAdjustment
{
	public string title;
	public int adjustment;

	public AttributeAdjustment(string _title, int _adjustment)
	{
		title = _title;
		adjustment = _adjustment;
	}
}

public struct EnumAttributePair
{
	public OSRIC_ATTRIBUTES stat;
	public int value;
	public EnumAttributePair(OSRIC_ATTRIBUTES _stat, int _value)
	{
		stat = _stat;
		value = _value;
	}
}

public static class OSRICConstants
{

	public static T GetAttributeOfType<T>(this Enum enumVal) where T:System.Attribute
	{
		var type = enumVal.GetType();
		var memInfo = type.GetMember(enumVal.ToString());
		var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
		return (attributes.Length > 0) ? (T)attributes[0] : null;
	}


	public static string GetDesc(this Enum enumValue)
	{
		var attribute = enumValue.GetAttributeOfType<DescriptionAttribute>();
		
		return attribute == null ? String.Empty : attribute.Description;
	} 

}
