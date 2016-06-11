using UnityEngine;
using System.Collections;

public class testScript : MonoBehaviour {

	public verticalLayoutAdjuster adjuster;


	// Use this for initialization
	void Start () 
	{
	
		adjuster = GameObject.FindObjectOfType<verticalLayoutAdjuster>();
		for(int i = 0; i<5;i++)
		{
			GameObject go = Instantiate(Resources.Load("EquipmetItem")) as GameObject;
			go.transform.SetParent(adjuster.VerticalLayout.transform);
			adjuster.List.Add(go);
			adjuster.SetVerticalLayoutSize();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
