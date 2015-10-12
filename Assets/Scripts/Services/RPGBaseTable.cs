using UnityEngine;
using System.Collections;
using System.Collections.Generic;


enum TableType { NUMERIC, TEXT }

public class RPGBaseTable<T>
{
	public string tableName;
	ColumnWithLabel<T>[] rows;

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

	public void AddColumn(T[] inarr)
	{
		column = inarr;
	}

	public T ValueAtIndex(int index)
	{
		return column[index];
	}
}



