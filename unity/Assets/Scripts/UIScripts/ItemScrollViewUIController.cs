using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class ItemScrollViewUIController : MonoBehaviour 
{
	public List<GameObject> ItemList;
	public GameObject ContentGO;
	public GameObject View;
	public float spacing = 25;


	public void OrderList()
	{
		Debug.Log("X: " + View.transform.position.x.ToString() 
			+ " Y: " + View.transform.position.y.ToString());
		foreach(GameObject go in ItemList)
		{
			Vector3 pos = new Vector3(0,0,0);
			go.transform.position = View.transform.position;
		}
		foreach(GameObject go in ItemList)
		{
			Vector3 newpos = go.transform.position;
			newpos.y += (-1f * (float)ItemList.IndexOf(go)*spacing);
			go.transform.localPosition = newpos;
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
