using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class RPGCharacterModel : MonoBehaviour {


	public RPGAttributeModel attributes;


	void Awake (){
		Debug.Log("Blah");
		attributes = gameObject.AddComponent<OSRICAttributeModel>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
