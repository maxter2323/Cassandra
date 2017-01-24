using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

namespace CassandraFramework.Quests
{
	public class Quests 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		private QuestFactory questFactory;
		private Dictionary<string, Quest> quests = new Dictionary<string, Quest>();
		public Quest currentQuest;
		
		/****************************************************************************************/
		/*										NATIVE METHODS									*/
		/****************************************************************************************/
		
		private void Start () 
		{
		
		}
		
		public void StartQuest (string questString) 
		{	
			Debug.Log("Starting quest " + questString);
			if (QuestStarted(questString)) return;
			questFactory = ServiceLocator.GetService<QuestFactory>();
			Quest quest = questFactory.GetQuest(questString);
			quest.StartStage(0);
			currentQuest = quest;
			quests[questString] = quest;
		}

		public bool QuestStarted(string questString)
		{
			if (quests.ContainsKey(questString)) return true;
			return false;
		}

		public void EndQuest (string quest) 
		{
			Debug.Log("Ending quest" + quest);
			currentQuest = null;
		}
	}
}