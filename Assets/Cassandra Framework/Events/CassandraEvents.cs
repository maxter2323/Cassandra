using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using CassandraFramework.Items;
using CassandraFramework.Stats;
using CassandraFramework.Quests;
using CassandraFramework.Dialogues;
using CassandraFramework.Perks;

namespace CassandraEvents
{
	//Stats
	public class StatEvent : UnityEvent<Stat> {}
	public class StatStringEvent : UnityEvent<Stat, string> {}

	//Items
	public class ItemEvent : UnityEvent<Item> {}

	//Quests
	public class QuestEvent : UnityEvent<Quest> {}

	//Dialogues
	public class DialgoueEvent : UnityEvent<Dialogue> {}

	//Perk
	public class PerkEvent : UnityEvent<Perk> {}
	public class PerkStringEvent : UnityEvent<Perk, string> {}

	//Effects
	public class EffectEvent : UnityEvent<Effect> {};
}