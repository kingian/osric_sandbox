using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OSRICItemCollection
{

	public List<OSRICItemModel> ItemList;

	public OSRICItemCollection()
	{
		ItemList = new List<OSRICItemModel>();
	}


	public bool Add(OSRICItemModel _oim)
	{
		_oim.GenerateGUID();
		if(ItemList.Contains(_oim))
			return false;
		ItemList.Add(_oim);
		return true;
	}

	public bool Remove(OSRICItemModel _oim)
	{
		if(_oim.UID.Length<1)
			_oim.GenerateGUID();
		if(ItemList.Contains(_oim))
		{
			ItemList.Remove(_oim);
			return true;
		}
		return false;
	}

	public List<OSRICItemModel> GetAllItemsOfType(OSRIC_ITEM_TYPE _oit)
	{
		List<OSRICItemModel> retList = new List<OSRICItemModel>();
		foreach(OSRICItemModel item in ItemList)
			if(item.ItemType == _oit)
				retList.Add(item);
		return retList;
	}
		

}
