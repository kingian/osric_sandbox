using UnityEngine;
using System.Collections;
using System.ComponentModel;

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
	[Description("Multi-Class")]
	MultiClass
}

public enum OSRIC_MULTICLASS
{
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
	Fighter_Assassin,
	[Description("Single Class")]
	SingleClass

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

public static class OSRICConstants
{


}
