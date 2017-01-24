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
	private JSONNode perksNode;
	private JSONNode itemsNode;
	private JSONNode recipesNode;
	private JSONNode questsNode;
	private JSONNode dialoguesNode;
	private JSONNode NPCsNode;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void Init () 
	{
		LoadFromDisk(PERKS_FILE, "Perks", ref perksNode);
		LoadFromDisk(ITEMS_FILE, "Items", ref itemsNode);
		LoadFromDisk(RECIPES_FILE, "Recipes", ref recipesNode);
		LoadFromDisk(QUESTS_FILE, "Quests", ref questsNode);
		LoadFromDisk(DIALOGUES_FILE, "Dialogues", ref dialoguesNode);
		LoadFromDisk(NPCS_FILE, "NPCs", ref NPCsNode);
	}

	public void LoadFromDisk(string fileName, string nodeName, ref JSONNode node)
	{
		using (StreamReader reader = new StreamReader(GetPath(fileName)))
		{
			node = JSON.Parse(reader.ReadToEnd())[nodeName];
		}
	}

	public JSONNode GetItemData(string itemKey)
	{
		return itemsNode[itemKey];
	}

	public JSONNode GetNPCData(string itemKey)
	{
		return NPCsNode[itemKey];
	}

	public JSONNode GetDialogueData(string itemKey)
	{
		return dialoguesNode[itemKey];
	}

	public JSONNode GetQuestData(string itemKey)
	{
		return questsNode[itemKey];
	}

	public JSONNode GetRecipeData(string itemKey)
	{
		return recipesNode[itemKey];
	}

	public JSONNode GetPerksData(string itemKey)
	{
		return perksNode[itemKey];
	}

	public JSONNode GetDialogueNode()
	{
		return dialoguesNode;
	}

	public JSONNode GetPerksNode()
	{
		return questsNode;
	}

	public JSONNode GetQuestNode()
	{
		return questsNode;
	}

	public JSONNode GetRecipesNode()
	{
		return recipesNode;
	}

	private string GetPath(string filename)
	{
		return Application.streamingAssetsPath + "/" + filename;
	}
	
}
