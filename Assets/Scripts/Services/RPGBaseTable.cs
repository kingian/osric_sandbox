using UnityEngine;
using System.Text;
using System.Collections;
using System.Collections.Generic;


enum TableType { NUMERIC, TEXT }

public class RPGBaseTable<T>
{
	public string tableName;
	public ColumnWithLabel<T>[] rows;
	public ColumnWithLabel<int> xIndex;

	public RPGBaseTable(string tn)
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

	public void AddXIndex(ColumnWithLabel<int> cwl)
	{
		xIndex = cwl;
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

	public int NumberOfColumns()
	{
		return rows.Length;
	}

	private ColumnWithLabel<T> matchColumn(string labelName)
	{
		foreach(ColumnWithLabel<T> cwl in rows)
		{
			if(cwl.label == labelName)
				return cwl;
		}
		return null;
	}

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

	public int GetIndexValueOf(T val)
	{
		for(int i=0;i<column.Length;i++)
		{
			if(EqualityComparer<T>.Default.Equals(column[i],val))
				return i;
		}
		return -1;
	}

	public string ToString()
	{
		StringBuilder retStr = new StringBuilder(label);
		for(int i=0;i<column.Length;i++)
		{
			retStr.Append("index "+i.ToString()+": "+column[i].ToString()+", ");
		}
		return retStr.ToString();
	}
}



