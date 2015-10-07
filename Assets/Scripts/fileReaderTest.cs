using UnityEngine;
using System.Collections;
using System.IO;

public class fileReaderTest : MonoBehaviour {


	public string[] lines;
//	public File fl;
	// Use this for initialization
	void Start () 
	{


		lines = RPGTableReader.Load("OSRIC_attribute_table");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
