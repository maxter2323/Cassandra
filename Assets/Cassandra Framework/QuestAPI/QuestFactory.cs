using UnityEngine;
using SimpleJSON;
using System.Collections;
using System.Collections.Generic;

namespace CassandraFramework.Quests
{
	public class QuestFactory : IService, IFactory
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		//Core
		private JsonParser jsonParser;
		private Dictionary<string, Quest> quests = new Dictionary<string, Quest>();

		//Strings
		private const string JSON_QUEST_KEY = "Key";
		private const string JSON_QUEST_NAME = "Name";
		private const string JSON_QUEST_DESCRIPTION = "Description";
		private const string JSON_QUEST_HIDDEN = "Hidden";
		private const string JSON_QUEST_LOG = "Log";
		private const string JSON_QUEST_SCRIPT = "Script";
		private const string JSON_QUEST_STAGES = "Stages";
		private const string JSON_QUEST_GOTO = "Goto";

		/****************************************************************************************/
		/*										 METHODS										*/
		/****************************************************************************************/

		public void Init()
		{
			jsonParser = ServiceLocator.GetService<JsonParser>();
		}

		public void Add(object toAdd)
		{
			AddQuest(toAdd as Quest);
		}

		public void AddQuest(Quest q)
		{
			quests.Add(q.key, q);
		}

		public Quest GetQuest(string questKey)
		{
			if (!quests.ContainsKey(questKey)) return null;
			return quests[questKey];
		}

		public Quest MakeQuestFromJson(string questKey)
		{
			JSONNode jsonQuest = jsonParser.questsNode[questKey];
			return MakeQuestFromJson(jsonQuest);
		}

		public List<IGameScriptable> MakeAll()
		{
			List<IGameScriptable> toreturn = new List<IGameScriptable>();
			for (int i = 0; i < jsonParser.questsNode.Count; i++)
			{
				JSONNode jsonQuest = jsonParser.questsNode[i];
				IGameScriptable s = MakeQuestFromJson(jsonQuest);
				toreturn.Add(s);
			}
			return toreturn;
		}

		public Quest MakeQuestFromJson(JSONNode jsonQuest)
		{
			Quest quest = new Quest();
			quest.key = jsonQuest[JSON_QUEST_KEY];
			quest.name = jsonQuest[JSON_QUEST_NAME];
			quest.description = jsonQuest[JSON_QUEST_DESCRIPTION];
			quest.hidden = jsonQuest[JSON_QUEST_HIDDEN].AsBool;
			JSONNode stages = jsonQuest[JSON_QUEST_STAGES];
			for (int i = 0; i < stages.Count; i++)
			{
				QuestStage stage = new QuestStage();
				JSONNode jStage = stages[i];
				stage.index = i;
				stage.gotoIndex = jStage[JSON_QUEST_GOTO].AsInt;
				stage.log = jStage[JSON_QUEST_LOG];
				stage.script = new GameScript(jStage[JSON_QUEST_SCRIPT]);
				stage.script.scriptName = quest.key + "_Stage_" + i.ToString();
				quest.AddStage(stage);
			}
			return quest;
		}
	}
}