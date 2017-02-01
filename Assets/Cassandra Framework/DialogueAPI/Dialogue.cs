using System;
using System.Collections.Generic;

namespace CassandraFramework.Dialogues
{
	[Serializable]
	public class Dialogue : IGameScriptable
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public string key;
		public string name;
		public string greetings;
		public int currentNode = 0;
		public List<DialogueNode> nodes = new List<DialogueNode>();
		
		/****************************************************************************************/
		/*										GENERAL METHODS									*/
		/****************************************************************************************/

		public IFactory GetFactory()
		{
			return (IFactory)ServiceLocator.GetService<DialogueFactory>();
		}

		public List<GameScript> GetAllScripts()
		{
			List<GameScript> toreturn = new List<GameScript>();
			List<DialogueOption> options = GetAllOptions();
			for (int i = 0; i < options.Count; i++)
			{
				toreturn.AddRange(options[i].GetAllScripts());
			}
			return toreturn;
		}

		/****************************************************************************************/
		/*										NODES METHODS									*/
		/****************************************************************************************/

		public List<DialogueNode> GetNodes()
		{
			return nodes;
		}

		public void AddNode(DialogueNode n)
		{
			nodes.Add(n);
		}

		public void RemoveNode(int index)
		{
			nodes.RemoveAt(index);
		}

		/****************************************************************************************/
		/*										OPTIONS METHODS									*/
		/****************************************************************************************/

		public void RemoveOptionFromNode(int nodeIndex, int optionIndex)
		{
			nodes[nodeIndex].RemoveOption(optionIndex);
		}

		public void SelectOption(DialogueOption option)
		{
			if (option.script != null)
			{
				option.script.Run();
			}
			currentNode = option.gotoIndex;
			if (currentNode == -1) currentNode = 0;
		}

		public List<DialogueOption> GetOptionsForNode(int index)
		{
			return nodes[index].GetOptions();
		}

		public List<DialogueOption> GetAllOptions()
		{
			List<DialogueOption> l = new List<DialogueOption>();
			for (int i = 0; i < nodes.Count; i++)
			{
				l.AddRange(GetOptionsForNode(i));
			}
			return l;
		}

		public List<DialogueOption> GetOptionsReady()
		{
			List<DialogueOption> options = GetOptionsForNode(currentNode);
			List<DialogueOption> toreturn = new List<DialogueOption>();
			for (int i = 0; i < options.Count; i++)
			{
				if(options[i].NoRequirements())
				{
					toreturn.Add(options[i]);
				}
				else
				{
					if (options[i].Ready()) toreturn.Add(options[i]);
				}
			}
			return toreturn;
		}
	}
}