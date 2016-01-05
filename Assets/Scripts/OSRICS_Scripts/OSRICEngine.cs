using UnityEngine;
using System.Collections;

public class OSRICEngine : MonoBehaviour {

	private RPGBaseTable<int> _attributeTable;
	public RPGBaseTable<int> attributeTable
	{ get 
		{
			if(_attributeTable==null)
				buildAttributeTable();
			return _attributeTable;
		}
	}

	private RPGBaseTable<int> _thacoTable;
	public RPGBaseTable<int> thacoTable
	{ get
		{
			if(_thacoTable==null)
				buildThacoTable();
			return _thacoTable;
		}
	}

	private RPGBaseTable<int> _classMinimums;
	public RPGBaseTable<int> classMinimums
	{ get
		{
			if(_classMinimums==null)
				buildClassMinsTable();
			return _classMinimums;
		}

	}

	private RPGBaseTable<bool> _raceClassMatrix;
	public RPGBaseTable<bool> raceClassMatrix
	{ get
		{
			if(_raceClassMatrix==null)
				buildRaceClassMatrixTable();
			return _raceClassMatrix;
		}

	}


	private RPGBaseTable<int> _raceMinMax;
	public RPGBaseTable<int> raceMinMax
	{ get
		{
			if(_raceMinMax==null)
				buildRaceMinMaxTable();
			return _raceMinMax;
		}
	}


	// Use this for initialization
	void Start () 
	{
		attributeTable.DebugLog();
		thacoTable.DebugLog();
		classMinimums.DebugLog();
		raceClassMatrix.DebugLog();
		raceMinMax.DebugLog();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	private void buildAttributeTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_attribute_table");
		_attributeTable = RPGTableReader.CreateIntBaseTable("OSRIC Attribute Table",lines, YINDEX_TYPE.IntIndex);
	}

	private void buildThacoTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_thac0");
		_thacoTable = RPGTableReader.CreateIntBaseTable("OSRIC Attribute Table",lines, YINDEX_TYPE.StringIndex);
	}

	private void buildClassMinsTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_class_mins");
		_classMinimums = RPGTableReader.CreateIntBaseTable("OSRIC Class Minimum Attributes",lines, YINDEX_TYPE.StringIndex);
	}

	private void buildRaceClassMatrixTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_race_class_matrix");
		_raceClassMatrix = RPGTableReader.CreateBoolBaseTable("OSRIC Race Class Matrix",lines, YINDEX_TYPE.StringIndex);
	}


	private void buildRaceMinMaxTable()
	{
		string[] lines = RPGTableReader.LoadResourceFile("OSRIC_race_mins_maxs");
		_raceMinMax = RPGTableReader.CreateIntBaseTable("OSRIC Race Min Max",lines, YINDEX_TYPE.StringIndex);
	}

}
