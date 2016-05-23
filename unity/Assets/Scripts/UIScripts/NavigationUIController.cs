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
	public Button HomeButton;

	// Use this for initialization
	void Start () 
	{
//		AllButtons = new List<GameObject>();
//		DisplayedButtons = new List<GameObject>();
//		BuildButtons();
	}

	void BuildButtons()
	{
		GameObject go;
		NavButtonController butCon;
		string butName = "";
		Text text;
		foreach(NAV_STATE ns in Enum.GetValues(typeof(NAV_STATE)))
		{
			if(Contains(ns))
				continue;
			butName = ns.ToString() + "_button";
			go = Instantiate(Resources.Load("NavButton")) as GameObject;
			go.name = butName;
			go.transform.SetParent(allButtonsOrigin.transform);
			butCon = go.GetComponent<NavButtonController>();
			butCon.state = ns;
			butCon.main = main;
			butCon.navCon = this;
			text = butCon.gameObject.GetComponentInChildren<Text>();
			text.text = ns.GetDesc();
//			float textWidth = text.rectTransform.rect.width;
//			RectTransform buttonDimensions = butCon.button.GetComponent<RectTransform>();
//			Vector2 newSize = new Vector2(textWidth+20f,buttonDimensions.rect.height);
//			buttonDimensions.sizeDelta = newSize;
			AllButtons.Add(go);
			go.SetActive(false);
		}
	}

	void OnEnable()
	{
		HomeButton.onClick.AddListener(delegate 
			{main.SetNavigationMode(NAV_STATE.Home);});
		HomeButton.onClick.AddListener(delegate 
			{ SetNavigationMode(NAV_STATE.Home);});
	}

	void OnDisable()
	{
		HomeButton.onClick.RemoveAllListeners();
	}

	// Update is called once per frame
	void Update () 
	{

	}

	private GameObject GetButton(NAV_STATE _ns)
	{
		if(!Contains(_ns))
			BuildButtons();
		foreach(GameObject go in AllButtons)
		{
			NavButtonController temp = go.GetComponentInChildren<NavButtonController>();
//			Debug.Log("go: " +temp.state.GetDesc() + " comp: " + _ns.GetDesc());
			if(temp.state == _ns)
				return go;
		}
		return null;
	}

	private bool Contains(NAV_STATE _ns)
	{
		foreach(GameObject go in AllButtons)
		{
			NavButtonController temp = go.GetComponentInChildren<NavButtonController>();
			if(temp.state==_ns)
				return true;
		}
		return false;
	}

	void ClearDisplayedButtons()
	{
		foreach(GameObject go in DisplayedButtons)
		{
			go.transform.SetParent(allButtonsOrigin.transform);
			go.SetActive(false);
		}
		DisplayedButtons.Clear();
	}


	public void NavButtonClicked(GameObject _go)
	{
		NAV_STATE targetMode = _go.GetComponent<NavButtonController>().state;
		SetNavigationMode(targetMode);
	}

	public void SetNavigationMode(NAV_STATE _ns)
	{
		ClearDisplayedButtons();
		switch(_ns)
		{
		case NAV_STATE.Home:
			AddNavigationOption(NAV_STATE.CharacterCreator);
			AddNavigationOption(NAV_STATE.Settings);
			break;
		case NAV_STATE.CharacterCreator:
			break;
		case NAV_STATE.CharacterViewer:
			AddNavigationOption(NAV_STATE.Equip);
			AddNavigationOption(NAV_STATE.Spells);
			break;
		case NAV_STATE.Equip:
			AddNavigationOption(NAV_STATE.CharacterViewer);
			AddNavigationOption(NAV_STATE.Spells);
			break;
		case NAV_STATE.Spells:
			AddNavigationOption(NAV_STATE.CharacterViewer);
			AddNavigationOption(NAV_STATE.Equip);
			break;
		}
	}

	void AddNavigationOption(NAV_STATE _ns)
	{
		AddButtonToDisplayedList(this.GetButton(_ns));
	}

	void AddButtonToDisplayedList(GameObject go)
	{
		Vector3 curPos;
		go.transform.SetParent(NavButtonOrigin.transform);
		curPos = NavButtonOrigin.transform.position;
		DisplayedButtons.Add(go);
		int pos = DisplayedButtons.IndexOf(go);
		if(pos<1)
		{
			go.transform.position = curPos;
			go.SetActive(true);
			return;
		}
		float prevPos = DisplayedButtons[pos-1].transform.position.x;
		float prevWidth = 
			DisplayedButtons[pos-1].GetComponent<RectTransform>().rect.width;
		curPos.x = prevPos + prevWidth + 20;
		go.transform.position = curPos;
		go.SetActive(true);
	}



}
