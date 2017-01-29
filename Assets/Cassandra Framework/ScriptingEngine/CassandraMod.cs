using System;
using System.Collections.Generic;
using System.Reflection;

[Serializable]
public class CassandraMod 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public byte[] assembly;
	public Assembly compiledCode;
	public List<IGameScriptable> allObjects = new List<IGameScriptable>();

	/****************************************************************************************/
	/*										 METHODS										*/
	/****************************************************************************************/

	public void Load()
	{
		compiledCode = Assembly.Load(assembly);
		for (int i = 0; i < allObjects.Count; i++)
		{
			IGameScriptable s = allObjects[i];
			List<GameScript> scripts = s.GetAllScripts();
			for (int j = 0; j < scripts.Count; j++)
			{
				scripts[j].Prepare(compiledCode);
			}
			IFactory f = s.GetFactory();
			f.Add(s);
		}
	}
}