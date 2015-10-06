using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;  

public class RPGTableReader  
{

	List<string> tableList;

	void RGBTableReader()
	{
		tableList = new List<string>();
	}



	public bool Load(string fileName)
	{
		string line;

		try
		{
			StreamReader theReader = new StreamReader(fileName, Encoding.Default);


			using (theReader)
			{
				do
				{
					LineRenderer = theReader.ReadLine();
					if(line!=null)
					{
						tableList.Add(line);
					}
				}
				while (line != null);
				theReader.Close();
				return true;
			}
		}
		catch (IOException e)
		{
			Debug.Log("{0}",e.Message);
			return false;
		}
	}

}
