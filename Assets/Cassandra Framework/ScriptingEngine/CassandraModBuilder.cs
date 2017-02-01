using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using CassandraFramework.Quests;
using CassandraFramework.Dialogues;
using CassandraFramework.Perks;

public class CassandraModBuilder : IService
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Strings
	public const string CASSANDRA_CORE_FOLDER = "CassandraData";
	public const string CASSANDRA_CORE_NAME = "Game";
	public const string CASSANDRA_FILE_FORMAT = ".cass";
	private string finalPath;

	//Usings
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
	private List<IFactory> factories = new List<IFactory>();
	
	/****************************************************************************************/
	/*										 METHODS										*/
	/****************************************************************************************/
	
	public void Init()
	{
		finalPath = Application.streamingAssetsPath + "/" + CASSANDRA_CORE_FOLDER + "/";
		factories.Add(ServiceLocator.GetService<DialogueFactory>());
		factories.Add(ServiceLocator.GetService<QuestFactory>());
		factories.Add(ServiceLocator.GetService<PerkFactory>());
	}

	public void LoadMod(string modName)
	{
		CassandraMod mod = (CassandraMod)Load(modName);
		mod.Load();
	}

	public void BuildAll()
	{	
		CassandraMod mod = new CassandraMod();
		string finalAssemblySource = "";
		for (int i = 0; i < factories.Count; i++)
		{
			List<IGameScriptable> cassObjects = factories[i].MakeAll();
			for (int j = 0; j < cassObjects.Count; j++)
			{
				IGameScriptable cassObject = cassObjects[j];
				AddScriptToFinalAssembly(ref finalAssemblySource, cassObject.GetAllScripts());
				mod.allObjects.Add(cassObject);
			}
		}
		mod.assembly = GameCompiler.CompileAsBytes(USINGS_STRING + finalAssemblySource);
		Save(CASSANDRA_CORE_NAME + CASSANDRA_FILE_FORMAT, mod);
		Debug.Log("Cassandra: everything is build to " + CASSANDRA_CORE_NAME + CASSANDRA_FILE_FORMAT);
	}

	private void AddScriptToFinalAssembly(ref string assemblySource, List<GameScript> scripts)
	{
		for (int i = 0; i < scripts.Count; i++)
		{
			assemblySource += scripts[i].GetFullSource();
		}
	}

	public List<string> GetAllModFiles()
	{
		List<string> toReturn = new List<string>();
		DirectoryInfo dir = new DirectoryInfo(finalPath);
		FileInfo[] fileInfo = dir.GetFiles("*.*");
		foreach (FileInfo file in fileInfo) 
		{
			if (file.Extension == CASSANDRA_FILE_FORMAT)
			{
				toReturn.Add(file.Name);
			}
		}
		return toReturn;
	}

	/****************************************************************************************/
	/*										SAVE/LOAD METHODS								*/
	/****************************************************************************************/

	public void Save(string fileName, object objToSave)
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(finalPath + "/" + fileName);
		bf.Serialize(file, objToSave);
		file.Close();
	}

	public object Load(string fileName)
	{
		string fullPath = finalPath + "/" + fileName;
		if (!File.Exists(fullPath)) return null;
		FileStream file = File.Open(fullPath, FileMode.Open);
		BinaryFormatter bf = new BinaryFormatter();
		return bf.Deserialize(file);
	}
}
