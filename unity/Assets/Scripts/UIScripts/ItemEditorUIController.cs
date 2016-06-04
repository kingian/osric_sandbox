using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemEditorUIController : MonoBehaviour 
{
	public MainController mainCon;
	public ItemDropdownUIController ItemTypeDropdown;
	public InputFieldElementUIController NameField;
	public RangeElementUIController SmlMedDmgCon;
	public RangeElementUIController LrgDmgCon;
	public InputFieldElementUIController RangeField;
	public InputFieldElementUIController EncumberanceField;
	public InputFieldElementUIController CostField;
	public Button SaveAddButton;


	bool ValidateFormData()
	{
		bool retBool = NameField.Validate();
		retBool = SmlMedDmgCon.Validate();
		retBool = LrgDmgCon.Validate();
		retBool = RangeField.Validate();
		retBool = EncumberanceField.Validate();
		retBool = CostField.Validate();
		return retBool;
	}


	public void AddItemToAllItems()
	{
//		if(!ValidateFormData())
//			return; // Add method to communicate the invalid to the user
		string test = ItemTypeDropdown.GetSelectedType().GetDesc() + " " +
			NameField.GetStr() + " " + SmlMedDmgCon.GetRange().min.ToString() + "-" +
			LrgDmgCon.GetRange().max.ToString();
		Debug.Log(test);
//		mainCon.engine.AllItems.Add(
//			new OSRICItemModel(
//			)
//		);

	}



}
