using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ItemScrollViewUIController : MonoBehaviour 
{
	public List<GameObject> ItemList;
	public GameObject ContentGO;
	public float spacing = 25;


	public void OrderList()
	{
		foreach(GameObject go in ItemList)
			go.transform.position = ContentGO.transform.position;

		foreach(GameObject go in ItemList)
		{
			Vector3 newpos = go.transform.position;
			newpos.y += (-1f * (float)ItemList.IndexOf(go)*spacing);
		}
	}

	public void AddItem(GameObject _go)
	{
		_go.transform.SetParent(ContentGO.transform);
		ItemList.Add(_go);
		OrderList();
	}

	public void RemoveItem(GameObject _go)
	{
		ItemList.Remove(_go);
		OrderList();
		// Need to destroy ALL associated resources here
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
