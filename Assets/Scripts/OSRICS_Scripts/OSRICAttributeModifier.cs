using System;
using System.Collections;
using System.ComponentModel;

//using OSRICAttributeModel;
//using OSRICEngine;
//using OSRICConstants;





public class OSRICAttributeModifier {

	public OSRIC_ATTRIBUTES attribute;//base attribute to modify
	public OSRIC_ATTRIBUTE_MODIFIER_TYPE type;
	public string name;//title or description - Racial Bonus, or potion
	public int value;//we use ints for everything, right?

	public OSRICAttributeModifier(){
		this.type = OSRIC_ATTRIBUTE_MODIFIER_TYPE.Unkown;
		this.name = "unkown";
		this.value = 0;
	}

	public OSRICAttributeModifier(int _value){
		this.type = OSRIC_ATTRIBUTE_MODIFIER_TYPE.Unkown;
		this.name = "unkown";
		this.value = _value;
	}

	public OSRICAttributeModifier(OSRIC_ATTRIBUTE_MODIFIER_TYPE _type,  int _value){
		this.type = _type;
		this.name = "unkown";
		this.value = _value;
	}

	public OSRICAttributeModifier(OSRIC_ATTRIBUTES _attribute, OSRIC_ATTRIBUTE_MODIFIER_TYPE _type,  int _value){
		this.attribute = _attribute;
		this.type = _type;
		this.name = "unkown";
		this.value = _value;
	}

}
