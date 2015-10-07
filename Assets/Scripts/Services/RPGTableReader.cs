using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  

public class RPGTableReader  
{

	static string[] tableArray;

	void RGBTableReader()
	{
	}



	static public string[] Load(string fileName)
	{
		var io = Resources.Load(fileName);
		tableArray = io.ToString().Split('\n');
		return tableArray;
	}
}
