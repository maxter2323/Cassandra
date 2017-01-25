using UnityEngine;
using System;
using System.Reflection;
using System.Collections;
using System.IO;
using SimpleJSON;

public class JsonParser : IService
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//FileNames
	private const string PERKS_FILE = "perks.json";
	private const string ITEMS_FILE = "items.json";
	private const string RECIPES_FILE = "recipes.json";
	private const string QUESTS_FILE = "quests.json";
	private const string DIALOGUES_FILE = "dialogues.json";
	private const string NPCS_FILE = "npcs.json";

	//JsonNodes
	public JSONNode perksNode;
	public JSONNode itemsNode;
	public JSONNode recipesNode;
	public JSONNode questsNode;
	public JSONNode dialoguesNode;
	public JSONNode NPCsNode;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void Init () 
	{
		LoadFromDisk(PERKS_FILE,  ref perksNode);
		LoadFromDisk(ITEMS_FILE, ref itemsNode);
		LoadFromDisk(RECIPES_FILE, ref recipesNode);
		LoadFromDisk(QUESTS_FILE, ref questsNode);
		LoadFromDisk(DIALOGUES_FILE, ref dialoguesNode);
		LoadFromDisk(NPCS_FILE, ref NPCsNode);
	}

	private void LoadFromDisk(string fileName, ref JSONNode node)
	{
		using (StreamReader reader = new StreamReader(GetPath(fileName)))
		{
			node = JSON.Parse(reader.ReadToEnd());
		}
	}

	public JSONNode GetRecipesNode()
	{
		return recipesNode;
	}

	private string GetPath(string filename)
	{
		return Application.streamingAssetsPath + "/" + CassandraModBuilder.CASSANDRA_CORE_FOLDER + "/" + filename;
	}
	
}