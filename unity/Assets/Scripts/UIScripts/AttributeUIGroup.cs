using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class AttributeUIGroup : MonoBehaviour {
	
	public AttributeUIController[] attributeContollerArray;
	public bool layoutTriggered;

	IEnumerator pauseAfterLoad()
	{
		yield return new WaitForSeconds(.5f);
		OrderAttributeElements();
	}

	void Awake()
	{
		attributeContollerArray = gameObject.GetComponentsInChildren<AttributeUIController>();
		StartCoroutine(pauseAfterLoad());
	}



	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	public void OrderAttributeElements()
	{
		if(layoutTriggered)
			return;
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
		layoutTriggered = true;
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
