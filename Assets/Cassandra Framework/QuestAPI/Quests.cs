using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using CassandraEvents;
using System;

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

		//Events
		[NonSerialized] public QuestEvent OnQuestStarted = new QuestEvent();
		[NonSerialized] public QuestEvent OnQuestFinished = new QuestEvent();
		[NonSerialized] public QuestStageEvent OnQuestStageStarted = new QuestStageEvent();
		
		/****************************************************************************************/
		/*										NATIVE METHODS									*/
		/****************************************************************************************/
		
		private void Start () 
		{
		
		}
		
		public void StartQuest (string questString) 
		{	
			if (QuestStarted(questString)) return;
			questFactory = ServiceLocator.GetService<QuestFactory>();
			Quest quest = questFactory.GetQuest(questString);
			quests[questString] = quest;
			currentQuest = quest;
			currentQuest.status = Quest.QuestStatus.Active;
			if (currentQuest.OnStageStarted == null) currentQuest.OnStageStarted = new QuestStageEvent();
			if (OnQuestStarted != null) OnQuestStarted.Invoke(currentQuest);
			currentQuest.OnStageStarted.AddListener((a, b) => {
				if (OnQuestStageStarted != null)
				{
					OnQuestStageStarted.Invoke(a, b);
				}
			});
			quest.StartStage(0);
		}

		public bool QuestStarted(string questString)
		{
			return quests.ContainsKey(questString);
		}

		public bool QuestActive(string questString)
		{
			if (!QuestStarted(questString)) return false;
			return quests[questString].status == Quest.QuestStatus.Active;
		}

		public void EndQuest (string quest) 
		{
			OnQuestFinished.Invoke(currentQuest);
			currentQuest = null;
			quests[quest].status = Quest.QuestStatus.Completed;
		}
	}
}