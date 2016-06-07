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
		
		if(!NameField.Validate())
		{
			return;
		}
		OSRICItemModel item = new OSRICItemModel(NameField.GetStr());
		item.ItemType = ItemTypeDropdown.GetSelectedType();
		if(SmlMedDmgCon.Validate())
			item.SmallMediumDamage = SmlMedDmgCon.GetRange();
		if(LrgDmgCon.Validate())
			item.LargeDamage = LrgDmgCon.GetRange();
		if(RangeField.Validate())
			item.WeaponRange = RangeField.GetInt();
		if(EncumberanceField.Validate())
			item.Encumberance = EncumberanceField.GetInt();
		if(CostField.Validate())
			item.Cost = CostField.GetInt();

		mainCon.engine.AllItems.Add(item);
	}


	public void LoadItemAttributes(OSRICItemModel _oim)
	{
		ItemTypeDropdown.SetDropSelection(_oim.ItemType);
		NameField.SetValue(_oim.Name);

	}


}
