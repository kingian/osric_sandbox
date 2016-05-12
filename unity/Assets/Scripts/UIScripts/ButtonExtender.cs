using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonExtender : MonoBehaviour {


	public Button targetButton;
	public Text targetText;
	public RawImage thing;
	Color startColor;
	public GameObject go;
	public TextMesh tm;

	void OnEnable()
	{
	}

	// Use this for initialization
	void Start () {
		tm = targetText.GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		startColor = targetText.color;
		targetText.color = Color.red;
	}

	void OnPointerEnter()
	{
		startColor = targetText.color;
		targetText.color = Color.red;
	}


	void OnPointerExit()
	{
		targetText.color = startColor;
	}

}
