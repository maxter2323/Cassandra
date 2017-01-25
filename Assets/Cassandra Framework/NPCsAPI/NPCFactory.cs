using UnityEngine;
using System.Collections;
using SimpleJSON;
using CassandraFramework.Items;
using CassandraFramework.Dialogues;

public class NPCFactory : IService 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private JsonParser jsonParser;
	private DataLocator dataLocator;
	private DialogueFactory dialogueService;
	private ItemFactory itemFactory;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void Init () 
	{
		jsonParser = ServiceLocator.GetService<JsonParser>();
		dialogueService = ServiceLocator.GetService<DialogueFactory>(); 
		itemFactory = ServiceLocator.GetService<ItemFactory>(); 
		dataLocator = ServiceLocator.GetService<DataLocator>();
	}

	public NPC BuildNPCScript(string NPCname)
	{
		JSONNode jsonItem = jsonParser.NPCsNode[NPCname];
		NPC npc = new NPC();
		npc.name = jsonItem["Name"];
		npc.viewName = jsonItem["View"];
		npc.unique = jsonItem["Unique"].AsBool;
		if (jsonItem["Dialogue"] != null) npc.dialogue = dialogueService.GetDialogue(jsonItem["Dialogue"]);
		JSONNode statsNode = jsonItem["Stats"];
		for (int i = 0; i < statsNode.Count; i++)
		{
			string statName = statsNode[i]["name"];
			int value = statsNode[i]["value"].AsInt;
			npc.stats.SetStat(statName, value);
		}
		JSONNode inventoryNode = jsonItem["Inventory"];
		for (int i = 0; i < inventoryNode.Count; i++)
		{
			string itemName = inventoryNode[i]["name"];
			int amount = inventoryNode[i]["amount"].AsInt;
			Item item = itemFactory.BuildItemScript(itemName);
			npc.inventory.AddItem(item, amount);
		}
		return npc;
	}

	public GameObject BuildAndInstantiateNPC(string NPCname)
	{
		NPC npc = BuildNPCScript(NPCname);
		GameObject instance = dataLocator.InstantiateLoadedResource(npc.viewName);
		instance.GetComponent<CharacterMono>().character = npc;
		return instance;
	}
}
