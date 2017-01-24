using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CassandraFramework.Quests
{
	[Serializable]
	public class Quest 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		//Core
		public string key;
		public string name;
		public string description;
		public bool hidden;
		public List<QuestStage> stages = new List<QuestStage>();
		public int currentStageIndex = 0;
		public QuestStage currentStage;

		//Status
		public enum QuestStatus
		{
			Active,
			Completed,
			Failed,
			Inactive,
		}
		public QuestStatus status = QuestStatus.Inactive;

		/****************************************************************************************/
		/*											METHODS										*/
		/****************************************************************************************/
		
		public void Start () 
		{
			StartStage(0);
		}

		public void StartStage(int index)
		{
			stages[index].script.Run();
			currentStageIndex = index;
			currentStage = stages[index];
		}

		public void AddStage(QuestStage stage)
		{
			stages.Add(stage);
		}

		public List<GameScript> GetAllScripts()
		{
			List<GameScript> toreturn = new List<GameScript>();
			for (int i = 0; i < stages.Count; i++)
			{
				if(stages[i].script != null) toreturn.Add(stages[i].script);
			}
			return toreturn;
		}

		public void PrepareScripts()
		{
			List<GameScript> scripts = GetAllScripts();
			for (int i = 0; i < scripts.Count; i++)
			{
				scripts[i].Prepare();
			}
		}
	}
}