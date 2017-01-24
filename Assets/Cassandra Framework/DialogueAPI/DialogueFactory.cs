using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace CassandraFramework.Dialogues
{

	public class DialogueFactory : IService 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		//Core
		private JsonParser jsonParser;
		private Dictionary<string, Dialogue> dialogues = new Dictionary<string, Dialogue>();

		//Strings
		private const string JSON_DIALOGUE_KEY = "Key";
		private const string JSON_DIALOGUE_NAME = "Name";
		private const string JSON_DIALOGUE_GREETINGS = "Greetings";
		private const string JSON_DIALOGUE_NODES = "Nodes";
		private const string JSON_DIALOGUE_TEXT = "Text";
		private const string JSON_DIALOGUE_GOTO = "Goto";
		private const string JSON_DIALOGUE_REPLY = "Reply";
		private const string JSON_DIALOGUE_SCRIPT = "Script";
		private const string JSON_DIALOGUE_CONDITION = "Condition";
		
		/****************************************************************************************/
		/*										METHODS											*/
		/****************************************************************************************/

		public void Init () 
		{
			jsonParser = ServiceLocator.GetService<JsonParser>();
		}

		public void AddDialogue(Dialogue d)
		{
			dialogues.Add(d.key, d);
		}

		public Dialogue GetDialogue(string dialogueKey)
		{
			if (!dialogues.ContainsKey(dialogueKey)) return null;
			return dialogues[dialogueKey];
		}

		public Dialogue MakeDialogueFromJson(string dialogueName)
		{
			JSONNode jsonDialogue = jsonParser.GetDialogueData(dialogueName);
			return MakeDialogueFromJson(jsonDialogue);
		}

		public Dialogue MakeDialogueFromJson(JSONNode jsonDialogue)
		{
			Dialogue dialogue = new Dialogue();
			dialogue.key = jsonDialogue[JSON_DIALOGUE_KEY];
			dialogue.name = jsonDialogue[JSON_DIALOGUE_NAME];
			dialogue.greetings = jsonDialogue[JSON_DIALOGUE_GREETINGS];
			JSONNode jsonDialogueNodes = jsonDialogue[JSON_DIALOGUE_NODES];
			for (int i = 0; i < jsonDialogueNodes.Count; i++)
			{	
				dialogue.AddNode(MakeNewNode(dialogue, jsonDialogueNodes[i]));
			}
			return dialogue;
		}

		public DialogueNode MakeNewNode(Dialogue dialogue, JSONNode jsonNode)
		{
			DialogueNode newNode = new DialogueNode();
			for (int i = 0; i < jsonNode.Count; i++)
			{	
				newNode.AddOption(MakeNewOption(dialogue, jsonNode[i], i));
			}
			return newNode;
		}

		public DialogueOption MakeNewOption(Dialogue dialogue, JSONNode jsonNode, int index)
		{
			DialogueOption newOption = new DialogueOption();
			newOption.index = index;
			newOption.text = jsonNode[JSON_DIALOGUE_TEXT];
			newOption.reply = jsonNode[JSON_DIALOGUE_REPLY]; 
			newOption.gotoIndex = jsonNode[JSON_DIALOGUE_GOTO].AsInt;
			if (jsonNode[JSON_DIALOGUE_SCRIPT] != null)
			{
				newOption.script =  new GameScript(jsonNode[JSON_DIALOGUE_SCRIPT]);
				newOption.script.scriptName = dialogue.key + "_" + JSON_DIALOGUE_SCRIPT + "_" + index.ToString();
			}
			if (jsonNode[JSON_DIALOGUE_CONDITION] != null)
			{
				newOption.condition = new GameScript(jsonNode[JSON_DIALOGUE_CONDITION]);
				newOption.condition.scriptName = dialogue.key + "_" + JSON_DIALOGUE_CONDITION + "_" +  index.ToString();
			} 
			return newOption;
		}
	}
}