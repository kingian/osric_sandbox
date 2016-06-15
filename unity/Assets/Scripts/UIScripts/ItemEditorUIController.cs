using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemEditorUIController : MonoBehaviour 
{
	public MainController mainCon;
	public ItemScrollViewUIController listView;
	public ItemDropdownUIController ItemTypeDropdown;
	public InputFieldElementUIController NameField;
	public RangeElementUIController SmlMedDmgCon;
	public RangeElementUIController LrgDmgCon;
	public InputFieldElementUIController RangeField;
	public InputFieldElementUIController EncumberanceField;
	public InputFieldElementUIController CostField;
	public Button SaveAddButton;

	void Start()
	{
		for(int i=0;i<5;i++)
		{
			GameObject go = Instantiate(Resources.Load("EquipmetItem")) as GameObject;
			EquipmentItemUIController item = go.GetComponent<EquipmentItemUIController>();
			item.ItemModel.Name = "Long Sword";
			item.ItemModel.ItemType = OSRIC_ITEM_TYPE.meleeWeapon;
			item.ItemModel.SmallMediumDamage = new Range(1,8);
			item.ItemModel.LargeDamage = new Range(1,12);
			item.ItemModel.Cost = 80;
			listView.AddItem(go);
		}
	}


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
		if(_oim.Name != "")
			NameField.SetValue(_oim.Name);
		if(!_oim.SmallMediumDamage.Empty())
			SmlMedDmgCon.SetValue(_oim.SmallMediumDamage);
		if(!_oim.LargeDamage.Empty())
			LrgDmgCon.SetValue(_oim.LargeDamage);
		if(_oim.WeaponRange!=0)
			RangeField.SetValue(_oim.WeaponRange);
		if(_oim.Encumberance!=0)
			EncumberanceField.SetValue(_oim.Encumberance);
		if(_oim.Cost!=0)
			CostField.SetValue(_oim.Cost);
	}


}
