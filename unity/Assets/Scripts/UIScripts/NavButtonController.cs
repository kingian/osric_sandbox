﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavButtonController : MonoBehaviour
{

	public MainController main;
	public NAV_STATE state;
	public Button button;
	public NavigationUIController navCon;


	void Awake()
	{
		button = gameObject.GetComponent<Button>();
//		main = gameObject.gameObject.GetComponent<MainController>();
	}

	void OnEnable()
	{
		button.onClick.AddListener(delegate { SendNavChange();});
	}

	void OnDisable()
	{
		button.onClick.RemoveAllListeners();
	}


	void SendNavChange()
	{
		main.SetNavigationMode(state);
		navCon.SetNavigationMode(state);
	}

}
