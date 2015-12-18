using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;  

public class RPGTableReader  
{

	static string[] tableArray;

	void RGBTableReader(){}

	
	static public string[] LoadResourceFile(string fileName)
	{
		var io = Resources.Load(fileName);
		tableArray = io.ToString().Split('\n');
		return tableArray;
	}

	static public RPGBaseTable<int> CreateIntBaseTable(string tableName, string[] content)
	{
		RPGBaseTable<int> tab;
		int height = content.Length;
		int width = content[0].Split(',').Length;
		string[,]strArr = new string[height, width];
		
		for(int i=0;i<content.Length;i++)
		{
			string[] tempArr = content[i].Split(',');
			for(int j=0; j<tempArr.Length; j++)
				strArr[i,j] = tempArr[j];
		}
		
		tab = new RPGBaseTable<int>(tableName,width);
		tab.InitCols(width-1);
		Debug.Log(tab.rows.Length);
		for(int i=1; i<width-1; i++)
		{

			tab.AddCol(i,new ColumnWithLabel<int>(strArr[0,i]));
			Debug.Log(tab.GetColName(i));
		}


		ColumnWithLabel<int> xAxis = new ColumnWithLabel<int>(strArr[0,0],height-1);

		for(int h=1;h<height;h++)
		{
			var t = strArr[h,0];
			int n;
			int.TryParse(t,out n);
			xAxis.AddValue(h-1,n);
		}
		tab.AddXIndex(xAxis);

		for(int i=1;i<width-1;i++)
		{
			int[] tempIntArr = new int[height-1];			
			for(int j=1;j<height-1;j++)
			{
				var y = strArr[j,i];
				int n;
				int.TryParse(y,out n);
				tempIntArr[j-1] = n;
			}
			tab.rows[i].AddColumn(tempIntArr);
			Debug.Log (tab.rows[i].ToString());
		}
//		tab.DebugLog();
		return tab;
	}


	static public RPGBaseTable<bool> CreateBoolBaseTable(string tableName, string[] content)
	{

		RPGBaseTable<bool> tab;
		int height = content.Length;
		int width = content[0].Split(',').Length;
		string[,]strArr = new string[height, width];
		
		for(int i=0;i<content.Length;i++)
		{
			string[] tempArr = content[i].Split(',');
			for(int j=0; j<tempArr.Length; j++)
				strArr[i,j] = tempArr[j];
		}
		
		tab = new RPGBaseTable<bool>(tableName,width);
		for(int i=0; i<width; i++)
		{
			tab.AddCol(i,new ColumnWithLabel<bool>(strArr[0,i]));
			Debug.Log(tab.GetColName(i));
		}

		ColumnWithLabel<int> xAxis = new ColumnWithLabel<int>(strArr[0,0],height-1);

		for(int h=1;h<height;h++)
		{
			var t = strArr[h,0];
			int n;
			int.TryParse(t,out n);
			xAxis.AddValue(h-1,n);
		}
		
		tab.AddXIndex(xAxis);

		for(int i=1;i<width;i++)
		{
			bool[] tempBoolArr = new bool[height-1];	
			for(int j=1;j<height;j++)
			{
				var y = strArr[j,i];
				bool m;
				Boolean.TryParse(y,out m);
				tempBoolArr[j-1] = m;
			}
			tab.rows[i].AddColumn(tempBoolArr);
		}
		return tab;
	}


}
