using UnityEngine;
using System.Collections;

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

	public string Key()
	{
		return key;
	}

	public bool CheckRequirement()
	{
		return (bool)script.Run();
	}

	public string DescriptionString()
	{
		return description;
	}
}
