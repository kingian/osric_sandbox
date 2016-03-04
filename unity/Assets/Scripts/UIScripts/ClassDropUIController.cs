using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ClassDropUIController : MonoBehaviour {

	public Dropdown drop;
	public HashSet<OSRIC_CLASS> classSet;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	HashSet<OSRIC_CLASS> GetDropdownOptionsAsEnum()
	{
		HashSet<OSRIC_CLASS> retSet = new HashSet<OSRIC_CLASS>();
		foreach(Dropdown.OptionData dopt in drop.options)
			classSet.Add(OSRICConstants.GetEnum<OSRIC_CLASS>(dopt.text));
		return retSet;
	}

	int GetOptionPosition(string searchTerm)
	{
		foreach(Dropdown.OptionData opt in drop.options)
		{
			if(opt.text==searchTerm)
				return drop.options.IndexOf(opt);
		}
		return -1;
	}

	public void SetSelectedValue(OSRIC_CLASS oc)
	{
		int selected = GetOptionPosition(oc.GetDesc());
		if(selected>-1)
			drop.value = selected;
	}

	void SetOptions(HashSet<OSRIC_CLASS> inSet)
	{
		drop.options.Clear();
		foreach(OSRIC_CLASS oc in inSet)
		{
			drop.options.Add(new Dropdown.OptionData(oc.GetDesc()));
		}
	}

	public void SetOptionsAndSelected(HashSet<OSRIC_CLASS> inSet, OSRIC_CLASS selected)
	{
		this.SetOptions(inSet);
		this.SetSelectedValue(selected);
	}

}
