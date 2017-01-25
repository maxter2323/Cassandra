using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

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
		/*										NATIVE METHODS									*/
		/****************************************************************************************/
		public IFactory GetFactory()
		{
			return (IFactory)ServiceLocator.GetService<DialogueFactory>();
		}

		public List<DialogueNode> GetNodes()
		{
			return nodes;
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

		public void RemoveNode(int index)
		{
			nodes.RemoveAt(index);
		}

		public void RemoveOptionFromNode(int indexNode, int indexOption)
		{
			nodes[indexNode].RemoveOption(indexOption);
		}

		public void AddNode(DialogueNode n)
		{
			nodes.Add(n);
		}

		public List<GameScript> GetAllScripts()
		{
			List<GameScript> toreturn = new List<GameScript>();
			List<DialogueOption> options = GetAllOptions();
			for (int i = 0; i < options.Count; i++)
			{
				if(options[i].script != null) toreturn.Add(options[i].script);
				if(options[i].condition != null) toreturn.Add(options[i].condition);
			}
			return toreturn;
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

		public List<DialogueOption> GetOptionsReady()
		{
			List<DialogueOption> options = GetOptionsForNode(currentNode);
			List<DialogueOption> toreturn = new List<DialogueOption>();
			for (int i = 0; i < options.Count; i++)
			{
				if(options[i].condition == null)
				{	
					toreturn.Add(options[i]);
				}
				else
				{
					if ((bool)(options[i].condition.Run())) toreturn.Add(options[i]);
				}
			}
			return toreturn;
		}

		public void PrepareScripts(Assembly assembly)
		{
			List<GameScript> scripts = GetAllScripts();
			for (int i = 0; i < scripts.Count; i++)
			{
				scripts[i].Prepare(assembly);
			}
		}
	}
}