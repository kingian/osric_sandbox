using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;  



public enum YINDEX_TYPE
{
	IntIndex,
	StringIndex
}


public class RPGTableReader  
{

	static string[] tableArray;

	void RGBTableReader(){}

	
	static public string[] LoadResourceFile(string fileName)
	{
		var io = Resources.Load(fileName);
		tableArray = io.ToString().Split('\n');
		for(int i=0;i<tableArray.Length; i++)
		{
			string s = tableArray[i];
			tableArray[i] = Regex.Replace(s, @"\t|\n|\r", "");
		}
		return tableArray;
	}

	static public RPGBaseTable<int> CreateIntBaseTable(string tableName, string[] content, YINDEX_TYPE yIndexType)
	{
		RPGBaseTable<int> tab;
		int height = content.Length;
		int width = content[0].Split(',').Length;
		string[,]strArr = new string[height, width];

		Diagnostic.debLog("Array Dims: " + strArr.GetLength(0).ToString() + ", " + strArr.GetLength(1).ToString());

		
		for(int i=0;i<content.Length;i++)
		{
			string[] tempArr = content[i].Split(',');
			if(tempArr.Length<1)
				continue;
			for(int j=0; j<tempArr.Length; j++)
				strArr[i,j] = tempArr[j];
		}
		
		tab = new RPGBaseTable<int>(tableName,width-1);
		tab.InitCols(width-1);
		for(int i=1; i<width; i++)
		{

			tab.AddCol(i-1,new ColumnWithLabel<int>(strArr[0,i]));
//			Diagnostic.debLog(tab.GetColName(i));
		}

		if(yIndexType==YINDEX_TYPE.IntIndex)
		{
			ColumnWithLabel<int> yIndexInt = new ColumnWithLabel<int>(strArr[0,0],height-1);
			for(int h=1;h<height;h++)
			{
				var t = strArr[h,0];
				int n;
				int.TryParse(t,out n);
				yIndexInt.AddValue(h-1,n);
			}
			tab.AddIntYIndex(yIndexInt);
		}
		else
		{
			ColumnWithLabel<string> yIndexStr = new ColumnWithLabel<string>(strArr[0,0],height-1);
			for(int h=1;h<height;h++)
			{
				string t = strArr[h,0];
				yIndexStr.AddValue(h-1,t);
			}
			tab.AddStrYIndex(yIndexStr);
		}


		for(int i=1;i<width;i++)
		{
			int[] tempIntArr = new int[height-1];			
			for(int j=1;j<height;j++)
			{
				var y = strArr[j,i];
				int n;
				int.TryParse(y,out n);
				tempIntArr[j-1] = n;
			}
			tab.rows[i-1].AddColumn(tempIntArr);
			Diagnostic.debLog (tab.rows[i-1].ToString());
		}
//		tab.DebugLog();
		return tab;
	}

	static public RPGBaseTable<bool> CreateBoolBaseTable(string tableName, string[] content, YINDEX_TYPE yIndexType)
	{
		RPGBaseTable<bool> tab;
		int height = content.Length;
		int width = content[0].Split(',').Length;
		string[,]strArr = new string[height, width];

		Diagnostic.debLog("Array Dims:" + strArr.GetLength(0).ToString() + ", " + strArr.GetLength(1).ToString());
		
		for(int i=0;i<content.Length;i++)
		{
			string[] tempArr = content[i].Split(',');
			if(tempArr.Length<1)
				continue;
			for(int j=0; j<tempArr.Length; j++)
				strArr[i,j] = tempArr[j];
		}
		
		tab = new RPGBaseTable<bool>(tableName,width-1);
		tab.InitCols(width-1);
		Diagnostic.debLog(tableName +  " Table Length: " + tab.rows.Length);
		for(int i=1; i<width; i++)
		{
			
			tab.AddCol(i-1,new ColumnWithLabel<bool>(strArr[0,i]));
			//			Diagnostic.debLog(tab.GetColName(i));
		}
		
		if(yIndexType==YINDEX_TYPE.IntIndex)
		{
			ColumnWithLabel<int> yIndexInt = new ColumnWithLabel<int>(strArr[0,0],height-1);
			for(int h=1;h<height;h++)
			{
				var t = strArr[h,0];
				int n;
				int.TryParse(t,out n);
				yIndexInt.AddValue(h-1,n);
			}
			tab.AddIntYIndex(yIndexInt);
		}
		else
		{
			ColumnWithLabel<string> yIndexStr = new ColumnWithLabel<string>(strArr[0,0],height-1);
			for(int h=1;h<height;h++)
			{
				string t = strArr[h,0];
				yIndexStr.AddValue(h-1,t);
			}
			tab.AddStrYIndex(yIndexStr);
		}
		
		
		for(int i=1;i<width;i++)
		{
			bool[] tempBoolArr = new bool[height-1];			
			for(int j=1;j<strArr.GetLength(0);j++)
			{
				var y = strArr[j,i];
				bool m;
				Boolean.TryParse(y,out m);
				tempBoolArr[j-1] = m;
			}
			tab.rows[i-1].AddColumn(tempBoolArr);
			Diagnostic.debLog (tab.rows[i-1].ToString());
		}
		//		tab.DebugLog();
		return tab;
	}
}
