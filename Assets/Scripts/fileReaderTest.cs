using UnityEngine;
using System.Collections;
using System.IO;

public class fileReaderTest : MonoBehaviour {


	public string[] lines;
	public string[,] strArr;
	public RPGBaseTable<int> tab;

	void Start () 
	{
		RPGBaseTable<int> blah;

		lines = RPGTableReader.LoadResourceFile("OSRIC_attribute_table");


		blah = RPGTableReader.CreateIntBaseTable("New Table",lines);

		Debug.Log (blah);

		Debug.Log(blah.tableName);

		Debug.Log(blah.xIndex.label);

//		for(int n=0; n < blah.xIndex.Length();n++)
//		{
//			Debug.Log("xindex val: " + blah.xIndex.ValueAtIndex(n));
//		}
//
		Debug.Log(blah.GetColName(0));
//
//		blah.DebugLog();

//		int height = lines.Length;
//		int width = lines[0].Split(',').Length;
//		strArr = new string[height, width];
//
//		for(int i=0;i<lines.Length;i++)
//		{
//			string[] tempArr = lines[i].Split(',');
//			for(int j=0; j<tempArr.Length; j++)
//				strArr[i,j] = tempArr[j];
//		}
//
//		Debug.Log("Array Length: " + strArr.Length);
//
//		tab = new RPGBaseTable<int>("OSRIC",width);
//		for(int i=0; i<width; i++)
//		{
//			tab.AddCol(i,new ColumnWithLabel<int>(strArr[0,i]));
//			Debug.Log(tab.GetColName(i));
//		}
//
//		for(int i=0;i<width;i++)
//		{
//			int[] tempArr = new int[height];
//			for(int j=1;j<height;j++)
//			{
//				int n;
//				int.TryParse(strArr[j,i],out n);
//				tempArr[j] = n;
//				Debug.Log(i + "," + j + ":" + n.ToString());
//			}
//			tab.rows[i].AddColumn(tempArr);
//		}


	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
