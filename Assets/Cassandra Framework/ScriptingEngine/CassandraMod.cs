using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using CassandraFramework.Quests;
using CassandraFramework.Dialogues;
using CassandraFramework.Perks;

[Serializable]
public class CassandraMod 
{
	public byte[] assembly;
	public static Assembly compiledCode;
	public List<Perk> perks = new List<Perk>();
	public List<Quest> quests = new List<Quest>();
	public List<Dialogue> dialogues = new List<Dialogue>();

	public void Load()
	{
		compiledCode = Assembly.Load(assembly);
		QuestFactory questFactory = ServiceLocator.GetService<QuestFactory>();
		for (int i = 0; i < quests.Count; i++)
		{
			Quest q = quests[i];
			q.PrepareScripts();
			questFactory.AddQuest(q);
		}
		DialogueFactory dialogueService = ServiceLocator.GetService<DialogueFactory>();
		for (int i = 0; i < dialogues.Count; i++)
		{
			Dialogue d = dialogues[i];
			d.PrepareScripts();
			dialogueService.AddDialogue(d);
		}
		PerkFactory perkService = ServiceLocator.GetService<PerkFactory>();
		for (int i = 0; i < perks.Count; i++)
		{
			Perk p = perks[i];
			p.PrepareScripts();
			perkService.AddPerk(p);
		}
	}	
}
