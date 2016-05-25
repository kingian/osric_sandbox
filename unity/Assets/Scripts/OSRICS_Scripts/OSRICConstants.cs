using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;


public enum NAV_STATE
{
	[Description("Home")]
	Home,
	[Description("Character Viewer")]
	CharacterViewer,
	[Description("Settings")]
	Settings,
	[Description("Character Creator")]
	CharacterCreator,
	[Description("Equip")]
	Equip,
	[Description("Spells")]
	Spells,
	[Description("Invitations")]
	Invitations
};


public enum OSRIC_CHARACTER_STATE
{
	[Description("Editing")]
	Editing,
	[Description("Completed")]
	Completed,
	[Description("In Play")]
	InPlay
}
;


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
	[Description("None")]
	None,
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

public enum OSRIC_ATTRIBUTE_MODIFIER_TYPE
{
	[Description("Unkown")]
	Unkown,
	[Description("Racial")]
	Racial,
	[Description("Item")]
	Item,
	[Description("Magical")]
	Magical,
	[Description("Divine")]
	Divine,
	[Description("Misc")]
	Miscellenous
}

public enum OSRIC_CHARACTER_VARIABLES
{
	[Description("None")]
	none,
	[Description("Attribute")]
	attribute,
	[Description("Hit Points")]
	hitpoints,
	[Description("Armor Class")]
	armorclass,
	[Description("Melee THAC0")]
	meleethac0,
	[Description("Missile THAC0")]
	missilethac0,
	[Description("Saving Throw")]
	savingthrow,
	[Description("Vision")]
	vision,
	[Description("Movement")]
	movement
}

public enum OSRIC_SAVING_THROWS
{
	[Description("Save vs. Rod, Staff, Wand")]
	saveRoSaWa,
	[Description("Save vs. Breath Weapon")]
	saveBreath,
	[Description("Save vs. Death,Paralysis and Poison")]
	saveDeath,
	[Description("Save vs. Petrification and Polymorph")]
	savePetPoly,
	[Description("Save vs. Spell")]
	saveSpell,
}


public enum OSRIC_ITEM_TYPE
{
	[Description("Melee Weapon")]
	meleeWeapon,
	[Description("Missile Weapon")]
	missileWeapon,
	[Description("Armor, Shield")]
	armorShield,
	[Description("Rod, Staff, Wand")]
	rodStaffWand,
	[Description("Amulet, Necklace")]
	amuletNecklace,
	[Description("Ring")]
	ring,
	[Description("Potion")]
	potion,
	[Description("Scroll")]
	scroll,
	[Description("miscMagical")]
	miscMagical,
	[Description("miscMundane")]
	miscMundane
}

public enum OSRIC_TIME_UNIT
{
	[Description("Turn")]
	turn,
	[Description("Round")]
	round,
	[Description("Segment")]
	segment
}

public struct EnumSavePair
{
	public OSRIC_SAVING_THROWS save;
	public int val;
	public EnumSavePair(OSRIC_SAVING_THROWS _save, int _val)
	{
		save = _save;
		val = _val;
	}
}

public class SaveCollection
{
	public EnumSavePair[] saveArr;

	public SaveCollection()
	{
		saveArr = new EnumSavePair[5];
		int ip = 0;
		foreach(OSRIC_SAVING_THROWS ost in Enum.GetValues(typeof(OSRIC_SAVING_THROWS)))
		{
			saveArr[ip] = new EnumSavePair(ost,20);
			ip++;
		}
	}

	public void UpdateBestSave(EnumSavePair esp)
	{
		for(int i=0;i<saveArr.Length;i++)
			if((esp.save==saveArr[i].save) && (esp.val < saveArr[i].val))
			{
				saveArr[i] = esp;
				return;
			}
	}

	public EnumSavePair GetEnumSavePair(OSRIC_SAVING_THROWS ost)
	{
		return saveArr[(int)ost];
	}

	public void DebugSaveCollection()
	{
		foreach(EnumSavePair esp in saveArr)
		{
			Debug.Log(esp.save.GetDesc() + ": " + esp.val.ToString());
		}
	}
}


public struct Range
{
	public int min;
	public int max;
	public Range(int _min, int _max)
	{
		min = _min;
		max = _max;
	}
	
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
	public static Dictionary<string,int> ClassHitDie =
		new Dictionary<string, int>()
	{
		{"Thief",6},
		{"Assassin",6},
		{"Cleric",8},
		{"Druid",8},
		{"Fighter",10},
		{"Paladin",10},
		{"Ranger",8},
		{"Magic-User",4},
		{"Illusionist",4}
	};


	public static int SumCharacters(string _str)
	{
		char[] chArr = _str.ToCharArray();
		int retInt = 0;
		foreach(char c in chArr)
			retInt += (int)c;
		return retInt*chArr.Length;
	}

	public static string HashVariables(List<string> _list)
	{
		int accumulator = 0;
		foreach(string s in _list)
			accumulator += SumCharacters(s);
		return accumulator.ToString("x8");
	}


	
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


	public static T GetEnum<T>(string description)
	{
		var type = typeof(T);
		if(!type.IsEnum) throw new InvalidOperationException();
		foreach(var field in type.GetFields())
		{
			var attribute = Attribute.GetCustomAttribute(field,
			                                             typeof(DescriptionAttribute)) as DescriptionAttribute;
			if(attribute != null)
			{
				if(attribute.Description == description)
					return (T)field.GetValue(null);
			}
			else
			{
				if(field.Name == description)
					return (T)field.GetValue(null);
			}
		}
		throw new ArgumentException("Not found.", "description");
		// or return default(T);
	}

}
