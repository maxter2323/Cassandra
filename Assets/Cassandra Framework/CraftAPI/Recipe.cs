using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recipe : MonoBehaviour 
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

}
