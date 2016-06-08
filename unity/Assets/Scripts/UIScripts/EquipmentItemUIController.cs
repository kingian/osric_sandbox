using UnityEngine;
using UnityEngine.UI;
using System.Collections;



public class EquipmentItemUIController : MonoBehaviour {

	public Text ItemText;
	public OSRICItemModel ItemModel;


	public void BuildDisplayDescription()
	{
		if(ItemText!=null && ItemModel!=null)
		{
			string buildStr = ItemModel.Name + " ";
			buildStr += ItemModel.ItemType.GetDesc() + "\n";

			if(ItemModel.ItemType == OSRIC_ITEM_TYPE.meleeWeapon || 
				ItemModel.ItemType == OSRIC_ITEM_TYPE.missileWeapon)
			{
				buildStr += "Dmg. vs small / medium:" + ItemModel.SmallMediumDamage.ToString() + "\n";
				buildStr += "Dmg. vs large:" + ItemModel.LargeDamage.ToString() + "\n";
			}
			ItemText.text = buildStr;
		}
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
