﻿using System;

[Serializable]
public class CustomRequirement : IRequirement
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public string name;
	public string key;
	public string description;
	public GameScript script;

	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public bool CheckRequirement()
	{
		return (bool)script.Run();
	}

	public string DescriptionString()
	{
		return description;
	}
}