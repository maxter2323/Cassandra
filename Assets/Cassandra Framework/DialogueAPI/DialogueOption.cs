using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using System;

namespace CassandraFramework.Dialogues
{
	[Serializable]
	public class DialogueOption 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public int index;
		public string text;
		public string reply;
		public int gotoIndex;
		public Requirements requirements = new Requirements();
		public GameScript script;

		/****************************************************************************************/
		/*										METHODS									  		*/
		/****************************************************************************************/

		public void SetRequirements(List<IRequirement> r)
		{
			requirements.requirements.Clear();
			requirements.requirements = r;
		}

		public bool Ready()
		{
			return requirements.Ready();
		}

		public List<GameScript> GetAllScripts()
		{
			List<GameScript> toreturn = new List<GameScript>();
			toreturn.AddRange(requirements.GetAllScripts());
			if (script != null) toreturn.Add(script);
			return toreturn;
		}

		public bool NoRequirements()
		{
			return requirements.Count() == 0;
		}

	}
}