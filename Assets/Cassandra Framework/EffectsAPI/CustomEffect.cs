using System;

[Serializable]
public class CustomEffect : IEffect 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public string name;
	public string key;
	public string description;
	public int time;
	public GameScript script;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void Do()
	{	
		script.Run();
	}

	public string DescriptionString()
	{
		return description;
	}
}
