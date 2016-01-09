using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;


enum TableType { NUMERIC, TEXT }

public class RPGBaseTable<T>
{
	public string tableName;
	public YINDEX_TYPE yIndexType;
	public ColumnWithLabel<T>[] rows;
	public ColumnWithLabel<int> IntYIndex;
	public ColumnWithLabel<string> StrYIndex;

	public RPGBaseTable(string tn, YINDEX_TYPE indexType)
	{
		tableName = tn;
	}

	public RPGBaseTable(string tn, int columns)
	{
		tableName = tn;
		rows = new ColumnWithLabel<T>[columns];
	}

	public void InitCols(int columns)
	{
		rows = new ColumnWithLabel<T>[columns];
	}

	public string GetColName(int colIndex)
	{
		return rows[colIndex].label;
	}

	public void AddIntYIndex(ColumnWithLabel<int> cwl)
	{
		IntYIndex = cwl;
		yIndexType = YINDEX_TYPE.IntIndex;
	}

	public void AddStrYIndex(ColumnWithLabel<string> cwl)
	{
		StrYIndex = cwl;
		yIndexType = YINDEX_TYPE.StringIndex;
	}


	public void AddCol(int index, ColumnWithLabel<T> cwl)
	{
		rows[index] = cwl;
	}

	public T GetValue(string columnName, int index)
	{
		T ret;
		ret = matchColumn(columnName).ValueAtIndex(index);
		return ret;
	}

	public int GetIndexOf(string labelName, T val)
	{
		return matchColumn(labelName).GetIndexValueOf(val);
	}

	public int GetYIndexOf(int val)
	{
		return IntYIndex.GetIndexValueOf(val);
	}

	public int GetYIndexOf(string val)
	{
		return StrYIndex.GetIndexValueOf(val);
	}


	public int NumberOfColumns()
	{
		return rows.Length;
	}

	public ColumnWithLabel<T> matchColumn(string labelName)
	{
		foreach(ColumnWithLabel<T> cwl in rows)
		{
			if(cwl.label == labelName)
				return cwl;
		}
		return null;
	}

	public void DebugLog()
	{
		Debug.Log("==== TABLE NAME: " + tableName + "====");

		string tmpStr = "";
		int i;



		if(yIndexType==YINDEX_TYPE.IntIndex)
		{
			tmpStr += IntYIndex.label + " len(" + IntYIndex.Length() + ")";
			for(i=0;i<IntYIndex.Length();i++)
				tmpStr += IntYIndex.ValueAtIndex(i).ToString() + " ";
		}
		else
		{
			tmpStr += StrYIndex.label+ " len(" + StrYIndex.Length() + ")";;
			for(i=0;i<StrYIndex.Length();i++)
				tmpStr += StrYIndex.ValueAtIndex(i) + " ";
		}

		Debug.Log(tmpStr);

		for(i=0; i<rows.Length; i++)
		{
			tmpStr = "";
			tmpStr += i.ToString();
			tmpStr += " " + rows[i].label + " : ";

			for(int j=0; j<rows[i].Length();j++)
			{
				tmpStr += " " + j.ToString() + ":" + rows[i].ValueAtIndex(j);
			}
			Debug.Log(tmpStr);
		}
	}

	public void init(){}
}

public class ColumnWithLabel<T>
{
	public string label;
	TableType tabtyp;
	T[] column;

	public ColumnWithLabel(string name)
	{
		label = name;
	}

	public ColumnWithLabel(string name, int size)
	{
		label = name;
		column = new T[size];
	}

	public void AddColumn(T[] inarr)
	{
		column = inarr;
	}

	public void AddValue(int index, T val)
	{
		column[index] = val;
	}

	public T ValueAtIndex(int index)
	{
 		return column[index];
	}

	public int Length()
	{
		return column.Length;
	}

	public int GetIndexValueOf(T val)
	{
		for(int i=0;i<column.Length;i++)
		{
			if(EqualityComparer<T>.Default.Equals(column[i],val))
				return i;
		}
		return -1;
	}

	public List<int> GetAllIndexVaulesOf(T val)
	{
		List<int> retList = new List<int>();
		for(int i=0;i<column.Length;i++)
		{
			if(EqualityComparer<T>.Default.Equals(column[i],val))
				retList.Add(i);
		}
		return retList;
	}


	public override string ToString()
	{
		StringBuilder retStr = new StringBuilder(label);
		for(int i=0;i<column.Length;i++)
		{
			retStr.Append("index "+i.ToString()+": "+column[i].ToString()+", ");
		}
		return retStr.ToString();
	}
}



