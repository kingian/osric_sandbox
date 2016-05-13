using System;
using System.Collections;
using System.ComponentModel;

public class OSRICCharacterModifier : System.Object
{

	public OSRIC_ATTRIBUTES attribute;//base attribute to modify
	public OSRIC_ATTRIBUTE_MODIFIER_TYPE type;
	public OSRIC_CHARACTER_VARIABLES characterVariable;
	public OSRIC_SAVING_THROWS savingThrow;
	public string name;//title or description - Racial Bonus, or potion
	public string description;
	public int value;//we use ints for everything, right?
	public int duration;



	public OSRICCharacterModifier(){
		this.type = OSRIC_ATTRIBUTE_MODIFIER_TYPE.Unkown;
		this.name = "unkown";
		this.value = 0;
	}

	public OSRICCharacterModifier(int _value){
		this.type = OSRIC_ATTRIBUTE_MODIFIER_TYPE.Unkown;
		this.name = "unkown";
		this.value = _value;
	}

	public OSRICCharacterModifier(OSRIC_ATTRIBUTE_MODIFIER_TYPE _type,  int _value){
		this.type = _type;
		this.name = "unkown";
		this.value = _value;
	}

	public OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES _charVariable, OSRIC_ATTRIBUTES _attribute,
								OSRIC_ATTRIBUTE_MODIFIER_TYPE _type,  int _value)
	{
		this.characterVariable = _charVariable;
		this.attribute = _attribute;
		this.type = _type;
		this.name = "unkown";
		this.value = _value;
	}

	public OSRICCharacterModifier(OSRIC_CHARACTER_VARIABLES _charVariable, 
								OSRIC_SAVING_THROWS _savingThrow, OSRIC_ATTRIBUTES _attribute, 
								OSRIC_ATTRIBUTE_MODIFIER_TYPE _type,  int _value)
	{
		this.characterVariable = _charVariable;
		this.savingThrow = _savingThrow;
		this.attribute = _attribute;
		this.type = _type;
		this.name = "unkown";
		this.value = _value;
	}

	public OSRICCharacterModifier(JSONObject obj)
	{
		attribute = OSRICConstants.GetEnum<OSRIC_ATTRIBUTES>(obj["attribute"].str);
		type = OSRICConstants.GetEnum<OSRIC_ATTRIBUTE_MODIFIER_TYPE>(obj["type"].str);
		characterVariable = OSRICConstants.GetEnum<OSRIC_CHARACTER_VARIABLES>
			(obj["characterVariable"].str);
		savingThrow = OSRICConstants.GetEnum<OSRIC_SAVING_THROWS>(obj["savingThrow"].str);
		name = obj["name"].str;
		description = obj["description"].str);
		value = (int)obj["value"].n;
		duration = (int)obj["duration"].n;
	}


	public override bool Equals(System.Object obj)
	{
		// If parameter is null return false.
		if (obj == null)
		{
			return false;
		}

		// If parameter cannot be cast to Point return false.
		OSRICCharacterModifier p = obj as OSRICCharacterModifier;
		if ((System.Object)p == null)
		{
			return false;
		}

		if(this.characterVariable == p.characterVariable &&
			this.attribute == p.attribute &&
			this.type == p.type &&
			this.savingThrow == p.savingThrow)
			return true;
		return false;
	}

	public bool Equals(OSRICCharacterModifier ocm)
	{
		if(this.characterVariable == ocm.characterVariable &&
				this.attribute == ocm.attribute &&
				this.type == ocm.type &&
				this.savingThrow == ocm.savingThrow)
			return true;
		return false;
	}
	
	public override int GetHashCode()
	{
		return base.GetHashCode();
	}

	public JSONObject Serialize()
	{
		JSONObject retObj = new JSONObject(JSONObject.Type.OBJECT);
		retObj.AddField("attribute",attribute.GetDesc());
		retObj.AddField("type", type.GetDesc());
		retObj.AddField("characterVariable", characterVariable.GetDesc());
		retObj.AddField("savingThrow", savingThrow.GetDesc());
		retObj.AddField("name", name);
		retObj.AddField("description", description);
		retObj.AddField("value", value);
		retObj.AddField("duration", value);
		return retObj;
	}
}
