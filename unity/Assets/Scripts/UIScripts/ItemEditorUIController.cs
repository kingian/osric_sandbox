using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ItemEditorUIController : MonoBehaviour 
{
	public Dropdown ItemTypeDropdown;
	public InputField NameField;
	public RangeElementUIController SmlMedDmgCon;
	public RangeElementUIController LrgDmgCon;
	public InputField RangeField;
	public InputField EncumberanceField;
	public InputField CostField;
	public Button SaveAddButton;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	bool ValidateFormData()
	{
		return false;
	}

}
