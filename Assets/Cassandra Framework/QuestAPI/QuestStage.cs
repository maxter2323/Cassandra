using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace CassandraFramework.Quests
{
	[Serializable]
	public class QuestStage 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/
		public int index;
		public int gotoIndex;
		public string log;
		public GameScript script;
	}
}