using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharButController : MonoBehaviour {

	public Text characterNameText;
	public Text characterDetailText;
	public Button charButton;
	public RPGCharacterModel cm;
	public MainController mainCon;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnEnable()
	{
		charButton.onClick.AddListener(delegate{LoadCharacter();});
	}

	void OnDisable()
	{
		charButton.onClick.RemoveAllListeners();
	}

	void LoadCharacter()
	{
		mainCon.LoadCharacter(cm);
	}

}
