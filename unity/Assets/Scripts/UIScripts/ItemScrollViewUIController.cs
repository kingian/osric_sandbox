using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ItemScrollViewUIController : MonoBehaviour 
{
	public List<GameObject> ItemList;
	public GameObject ContentGO;
	public GameObject VerticalLayout;
	public float spacing = 25;


//	public void OrderList()
//	{
//
//		foreach(GameObject go in ItemList)
//		{
//			Vector3 pos = new Vector3(0,0,0);
//		}
//		foreach(GameObject go in ItemList)
//		{
//			Vector3 newpos = go.transform.position;
//			newpos.y += (-1f * (float)ItemList.IndexOf(go)*spacing);
//			go.transform.localPosition = newpos;
//		}
//	}

	public void AddItem(GameObject _go)
	{
		_go.transform.SetParent(VerticalLayout.transform);
		ItemList.Add(_go);
		_go.transform.localPosition = VerticalLayout.transform.localPosition;
		RectTransform rectTran = VerticalLayout.GetComponent<RectTransform>();
		Vector2 newSize = new Vector2(200, (ItemList.Count*50+10));
		rectTran.sizeDelta = newSize;
		Vector3 newPos = new Vector3(
			VerticalLayout.transform.localPosition.x,
			(-1*newSize.y/2),
			0);
		VerticalLayout.transform.localPosition = newPos;

//		OrderList();
	}

	public void RemoveItem(GameObject _go)
	{
		ItemList.Remove(_go);
//		OrderList();
//		 Need to destroy ALL associated resources here
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
