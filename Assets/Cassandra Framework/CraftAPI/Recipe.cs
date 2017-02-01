using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Recipe : IGameScriptable 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public struct Requirement
	{
		public string name;
		public int count;
		public string use;
	}

	public struct Result
	{
		public string name;
		public int count;
	}

	public string name;
	public string description;
	public string workbench;
	public List<Requirement> requirements = new List<Requirement>();
	public List<Result> results = new List<Result>();

	public IFactory GetFactory()
	{
		return ServiceLocator.GetService<RecipeFactory>();
	}

	public List<GameScript> GetAllScripts()
	{
		throw new NotImplementedException();
	}
}
