using UnityEngine;
using System;
using System.Collections;
using System.Reflection;
using System.IO;

[Serializable]
public class GameScript
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public string sourceCode;
	public string scriptName;
	
	[NonSerialized] public object instance;
	[NonSerialized] public MethodInfo method;
	
	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/

	public GameScript(string source)
	{
		sourceCode = source;
	}

	public string GetFullSource()
	{
		bool containsReturn = sourceCode.Contains("return");
		string returnString = "return null;";
		if (containsReturn) returnString = "";
		string fullSource = String.Format(@"
		[Serializable]
		public class {0}
		{{
			public object Run()
			{{
				{1}
				{2}
			}}
		}}
		", scriptName, sourceCode, returnString);
		return fullSource;
	}

	public void Prepare()
	{
		Type instanceType = CassandraMod.compiledCode.GetType(scriptName);
		instance = Activator.CreateInstance(instanceType);
		method = instanceType.GetMethod("Run");
	}

	public object Run()
	{
		object[] mparams = new object[0];
		return method.Invoke(instance, mparams);
	}
}
