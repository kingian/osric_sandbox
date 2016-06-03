using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemEditorUIController : MonoBehaviour 
{
	public Dropdown ItemTypeDropdown;
	public InputFieldElementUIController NameField;
	public RangeElementUIController SmlMedDmgCon;
	public RangeElementUIController LrgDmgCon;
	public InputFieldElementUIController RangeField;
	public InputFieldElementUIController EncumberanceField;
	public InputFieldElementUIController CostField;
	public Button SaveAddButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

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





}
