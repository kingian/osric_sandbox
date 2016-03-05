using UnityEngine;
using System.Collections;

public static class Diagnostic 
{
	public const bool debugFlag = false;
	
	public static void debLog (string statement)
	{
		if (debugFlag)
			Debug.Log (statement);
	}

}
