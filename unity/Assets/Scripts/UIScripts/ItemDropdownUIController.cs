using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemDropdownUIController : MonoBehaviour 
{
	public Dropdown drop;

	void Start()
	{
		drop.options.Clear();
		foreach(OSRIC_ITEM_TYPE itype in Enum.GetValues(typeof(OSRIC_ITEM_TYPE)))
			drop.options.Add(new Dropdown.OptionData(itype.GetDesc()));
		
		drop.value = 0;
	}

	public OSRIC_ITEM_TYPE GetSelectedType()
	{
		string selectedStr = drop.options[drop.value].text;
		return OSRICConstants.GetEnum<OSRIC_ITEM_TYPE>(selectedStr);
	}



}
