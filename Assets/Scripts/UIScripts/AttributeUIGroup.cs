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
// 		OrderAttributeElements();

	}

	// Use this for initialization
	void Start () {
//		attributeContollerArray = gameObject.GetComponentsInChildren<AttributeUIController>();
		OrderAttributeElements();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OrderAttributeElements()
	{
		Vector3 groupPos = new Vector3(-250f,145f);
		gameObject.transform.localPosition = groupPos;
		float yOffset = 0f;
		Vector3 currentPos;
		GameObject currentGO;
		foreach(OSRIC_ATTRIBUTES oa in Enum.GetValues(typeof(OSRIC_ATTRIBUTES)))
		{
			currentGO = GetController(oa);
			currentPos =  currentGO.transform.position;
			currentPos.y += yOffset;
			currentGO.transform.position = currentPos;
			yOffset -= 50f;
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

	public void SetAttribute(OSRIC_ATTRIBUTES oa, int value, string subtext)
	{
		if(attributeContollerArray.Length<1)
		{
			return;
		}
		foreach(AttributeUIController ac in attributeContollerArray)
		{
			if(ac.attributeEnum==oa)
			{
				ac.SetAttributeValue(value);
				ac.SetAttributeSubtext(subtext);
			}
		}
	}
		
}
