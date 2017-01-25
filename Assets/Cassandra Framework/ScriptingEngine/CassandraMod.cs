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
			s.PrepareScripts(compiledCode);
			IFactory f = s.GetFactory();
			f.Add(s);
		}
	}
}