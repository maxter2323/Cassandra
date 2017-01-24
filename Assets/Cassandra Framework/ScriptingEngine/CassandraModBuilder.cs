using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using SimpleJSON;
using CassandraFramework.Quests;
using CassandraFramework.Dialogues;
using CassandraFramework.Perks;

public class CassandraModBuilder : IService
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Strings
	public const string CASSANDRA_CORE_NAME = "Game";
	public const string CASSANDRA_FILE_FORMAT = ".cass";
	private const string USINGS_STRING = @"
		using System;
		using System.Collections;
		using System.Collections.Generic;
		using UnityEngine;
		using UnityEngine.Events;
		using CassandraFramework.Stats;
		using CassandraFramework.Items;
		using CassandraFramework.Quests;
		using CassandraFramework.Dialogues;
		using CassandraEvents;
	";

	//Services
	private JsonParser jsonParser;
	private QuestFactory questFactory;
	private DialogueFactory dialogueService;
	private PerkFactory perkService;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void Init()
	{
		jsonParser = ServiceLocator.GetService<JsonParser>();
		questFactory = ServiceLocator.GetService<QuestFactory>();
		dialogueService = ServiceLocator.GetService<DialogueFactory>();
		perkService = ServiceLocator.GetService<PerkFactory>();
	}

	public void LoadMod(string modName)
	{
		CassandraMod mod = (CassandraMod)Load(modName);
		mod.Load();
	}

	public void BuildAll()
	{	
		CassandraMod mod = new CassandraMod();
		JSONNode perks = jsonParser.GetPerksNode();
		JSONNode quests = jsonParser.GetQuestNode();
		JSONNode dialogues = jsonParser.GetDialogueNode();
		string finalAssemblySource = "";
		for (int i = 0; i < quests.Count; i++)
		{
			Quest q = questFactory.MakeQuestFromJson(quests[i]);
			AddScriptToFinalAssembly(ref finalAssemblySource, q.GetAllScripts());
			mod.quests.Add(q);
		}
		for (int i = 0; i < dialogues.Count; i++)
		{
			Dialogue d = dialogueService.MakeDialogueFromJson(dialogues[i]);
			AddScriptToFinalAssembly(ref finalAssemblySource, d.GetAllScripts());
			mod.dialogues.Add(d);
		}
		for (int i = 0; i < perks.Count; i++)
		{
			Perk p = perkService.MakePerkFromJson(perks[i]);
			AddScriptToFinalAssembly(ref finalAssemblySource, p.GetAllScripts());
			mod.perks.Add(p);
		}
		finalAssemblySource = USINGS_STRING + finalAssemblySource;
		mod.assembly = GameCompiler.CompileAsBytes(finalAssemblySource);
		Save(CASSANDRA_CORE_NAME + CASSANDRA_FILE_FORMAT, mod);
		Debug.Log("Everything is build");
	}

	private void AddScriptToFinalAssembly(ref string assemblySource, List<GameScript> scripts)
	{
		for (int i = 0; i < scripts.Count; i++)
		{
			assemblySource += scripts[i].GetFullSource();
		}
	}

	/****************************************************************************************/
	/*										SAVE/LOAD METHODS								*/
	/****************************************************************************************/

	public void Save(string fileName, object objToSave)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.streamingAssetsPath + "/" + fileName);
		bf.Serialize(file, objToSave);
		file.Close();
	}

	public object Load(string fileName)
	{
		string fullName = Application.streamingAssetsPath + "/" + fileName;
		if (!File.Exists(fullName)) return null;
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(fullName, FileMode.Open);
		object obj = bf.Deserialize(file);
		return obj;
	}
}
