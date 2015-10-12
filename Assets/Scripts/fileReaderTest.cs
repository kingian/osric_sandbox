using UnityEngine;
using System.Collections;
using System.IO;

public class fileReaderTest : MonoBehaviour {


	public string[] lines;
	public string[,] strArr;
	public RPGBaseTable<int> tab;
//	public File fl;
	// Use this for initialization
	void Start () 
	{


		lines = RPGTableReader.Load("OSRIC_attribute_table");

		int height = lines.Length;
		int width = lines[0].Split(',').Length;
		strArr = new string[height, width];

		for(int i=0;i<lines.Length;i++)
		{
			string[] tempArr = lines[i].Split(',');
			for(int j=0; j<tempArr.Length; j++)
				strArr[i,j] = tempArr[j];
		}

		Debug.Log(strArr.ToString());

		tab = new RPGBaseTable<int>("OSRIC",width);
//		foreach(string name in labels)
//		{
//			ColumnWithLabel<int> tempCWL = new ColumnWithLabel<int>(name);
//			int[] tempInts = new int[(lines.Length-1)];
//			for(int i=1;i<lines.Length;i++)
//			{
//
//			}
//		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
