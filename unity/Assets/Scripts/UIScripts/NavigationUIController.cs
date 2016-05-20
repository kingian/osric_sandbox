using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class NavigationUIController : MonoBehaviour {

	public MainController main;
	public OSRICEngine engine;
	public GameObject NavButtonOrigin;
	public GameObject allButtonsOrigin;
	public List<GameObject> AllButtons;
	public List<GameObject> DisplayedButtons;

	// Use this for initialization
	void Start () 
	{
		GameObject go;
		NavButtonController butCon;
		string butName = "";
		Text text;
		foreach(NAV_STATE ns in Enum.GetValues(typeof(NAV_STATE)))
		{
			butName = ns.ToString() + "_button";
			go = Instantiate(Resources.Load("NavButton")) as GameObject;
			go.name = butName;
			go.transform.SetParent(allButtonsOrigin.transform);
			butCon = go.GetComponent<NavButtonController>();
			butCon.state = ns;
			butCon.main = main;
			text = butCon.gameObject.GetComponentInChildren<Text>();
			text.text = ns.GetDesc();
			AllButtons.Add(go);
			go.SetActive(false);
		}


	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
