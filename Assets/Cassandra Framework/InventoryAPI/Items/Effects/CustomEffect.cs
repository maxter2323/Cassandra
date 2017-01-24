using UnityEngine;
using System.Collections;

public class CustomEffect : IEffect 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public string name;
	public string key;
	public string description;
	public GameScript script;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void Do()
	{	
		script.Run();
	}
}
