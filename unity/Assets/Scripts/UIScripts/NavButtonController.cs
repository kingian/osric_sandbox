using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NavButtonController : MonoBehaviour
{

	public MainController main;
	NAV_STATE state;
	public Button button;


	void OnStart()
	{
		button = gameObject.GetComponent<Button>();
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
		main.SetNavigationState(state);
	}

}
