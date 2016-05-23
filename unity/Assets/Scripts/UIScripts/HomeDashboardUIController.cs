using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class HomeDashboardUIController : MonoBehaviour {


	public GameObject characterListOrigin;
	public List<GameObject> characterButtonList;
	public MainController mainCon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}


	public void UpdateCharacterList()
	{
		ClearButtonList();
		string curButName = "";
		float yOffset = 0;
		Vector3 currentPos;
		GameObject newGO;
		CharButController newCon;
		foreach(RPGCharacterModel cm in mainCon.CharacterList)
		{
			curButName = cm.GetHashCode() + "_button";
			if(InButtonList(curButName))
				continue;

			newGO = Instantiate(Resources.Load("CharacterButton")) as GameObject;
			newGO.transform.SetParent(characterListOrigin.transform);
			newGO.transform.position = characterListOrigin.transform.position;
			newCon = newGO.GetComponentInChildren<CharButController>();
			newCon.name = curButName;
			newCon.characterNameText.text = cm.attributes.characterName;
			string accumulator = cm.attributes.characterRace.GetDesc();
			accumulator += "\n" + cm.attributes.characterClass.GetDesc();

			newCon.characterDetailText.text = accumulator;
			newCon.cm = cm;
			newCon.mainCon = mainCon;
			characterButtonList.Add(newGO);
			currentPos = newGO.transform.position;
			currentPos.y += yOffset;
			newGO.transform.position = currentPos;
			yOffset -= 100f;
		}
	}

	bool InButtonList(string buttonName)
	{
		foreach(GameObject b in characterButtonList)
		{
			if(b.name==buttonName)
				return true;
		}
		return false;
	}

	void ClearButtonList()
	{
		foreach(GameObject b in characterButtonList)
			Destroy(b);
		characterButtonList.Clear();
	}


}
