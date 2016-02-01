using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class AttributeUIGroup : MonoBehaviour {
	
	public AttributeUIController[] attributeContollerArray;

	void Awake()
	{
		attributeContollerArray = gameObject.GetComponentsInChildren<AttributeUIController>();
		OrderAttributeElements();

	}

	// Use this for initialization
	void Start () {
//		attributeContollerArray = gameObject.GetComponentsInChildren<AttributeUIController>();
//		OrderAttributeElements();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OrderAttributeElements()
	{
		float yOffset = 0f;
		Vector3 currentPos;
		GameObject currentGO;
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			currentGO = GetController(oa);
			currentPos =  currentGO.transform.position;
			currentPos.y += yOffset;
			currentGO.transform.position = currentPos;
			yOffset -= 25f;
		}
	}

	GameObject GetController(OSRIC_ATTRIBUTES oa)
	{
		if(attributeContollerArray.Length<1)
			return null;
		foreach(AttributeUIController ac in attributeContollerArray)
		{
			if(ac.attributeEnum==oa)
				return ac.gameObject;
		}
		return null;
	}

	public void SetAttribute(OSRIC_ATTRIBUTES oa, int value)
	{
		if(attributeContollerArray.Length<1)
		{
			Debug.Log("GAH!!!");
			return;
		}
		foreach(AttributeUIController ac in attributeContollerArray)
		{
			if(ac.attributeEnum==oa)
				ac.SetAttributeValue(value);
		}
	}
}
