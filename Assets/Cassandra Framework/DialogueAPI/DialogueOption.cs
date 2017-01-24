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
		public GameScript condition;
		public GameScript script;
	}
}