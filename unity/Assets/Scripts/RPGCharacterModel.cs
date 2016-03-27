using UnityEngine;
using System.Collections;

[System.Serializable]
public class RPGCharacterModel : MonoBehaviour {


	public OSRICAttributeModel attributes;


	void Awake (){
//			Debug.Log("Blah");
		attributes = gameObject.GetComponent<OSRICAttributeModel> ();
		if(attributes == null)
			attributes = gameObject.AddComponent<OSRICAttributeModel>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
		
	void OnDestroy()
	{
		Destroy(attributes);
	}

}
