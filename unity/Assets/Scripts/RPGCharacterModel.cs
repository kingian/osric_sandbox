using UnityEngine;
using System.Collections;

public class RPGCharacterModel {


	public OSRICAttributeModel attributes;


	public RPGCharacterModel()
	{
		attributes = new OSRICAttributeModel(this);
	}


	void Awake (){
//			Debug.Log("Blah");
//		attributes = gameObject.GetComponent<OSRICAttributeModel> ();
//		if(attributes == null)
//			attributes = gameObject.AddComponent<OSRICAttributeModel>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
//	void OnDestroy()
//	{
//		Destroy(attributes);
//	}

}
