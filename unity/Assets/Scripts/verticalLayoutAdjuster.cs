using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class verticalLayoutAdjuster : MonoBehaviour {

	public List<GameObject> List;
	public GameObject VerticalLayout;
	public GameObject Content;


	public void SetVerticalLayoutSize()
	{
		Vector2 newSize = new Vector2(200f,50f);
		float newHeight = (float)List.Count*50f+20f;
		newSize.y = newHeight;
		Debug.Log("New Height:" + newHeight.ToString());
		RectTransform rect = VerticalLayout.GetComponent<RectTransform>();
		rect.sizeDelta = newSize;
//		Content.GetComponent<RectTransform>().sizeDelta = newSize;
		float top = rect.localPosition.y;
		Debug.Log("Pos Y: " + top.ToString());
		Vector3 newTop = new Vector3(
			VerticalLayout.transform.localPosition.x,
			(-1f*newHeight/2),
//			(VerticalLayout.transform.localPosition.y),
			0);
		//MOVE THE LOCALPOSITION.Y DOWN BY .5 THE HEIGHT
		VerticalLayout.transform.localPosition = newTop;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
