using UnityEngine;
using System;
using System.Collections;
using SimpleJSON;
using CassandraFramework.Items;

namespace CassandraFramework.Items
{
	public class ItemFactory : IService 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		private JsonParser jsonParser;
		private DataLocator dataLocator;
		
		/****************************************************************************************/
		/*										NATIVE METHODS									*/
		/****************************************************************************************/
		
		public void Init () 
		{
			jsonParser = ServiceLocator.GetService<JsonParser>();
			dataLocator = ServiceLocator.GetService<DataLocator>(); 
		}

		public Item BuildItemScript(string itemName)
		{
			JSONNode jsonItem = jsonParser.itemsNode[itemName];
			string t = jsonItem["Category"];
			Type itemType = Type.GetType("CassandraFramework.Items." + t); //TODO: Avoid "CassandraFramework.Items."
			var objectWithoutType = Activator.CreateInstance(itemType);
			Item item = (Item)objectWithoutType;
			item.key = jsonItem["Key"];
			item.name = jsonItem["Name"];
			item.iconName = jsonItem["Icon"];
			item.description = jsonItem["Description"];
			item.weight = jsonItem["Weight"].AsFloat;
			item.itemTypeString = jsonItem["ItemType"];
			BuildPersonalProperties(item, jsonItem);
			return item;
		}

		private void BuildPersonalProperties(Item item, JSONNode jsonItem)
		{
			switch(jsonItem["category"])
			{
				case "WeaponItem":
					BuildWeapon((WeaponItem)item, jsonItem);
					break;
				case "ConsumableItem":
					BuildConsumable((ConsumableItem)item, jsonItem);
					break;
			}
		}

		private void BuildWeapon(WeaponItem item, JSONNode jsonItem)
		{

		}

		private void BuildConsumable(ConsumableItem item, JSONNode jsonItem)
		{
			EffectFactory effectFactory = ServiceLocator.GetService<EffectFactory>();
			item.effects = effectFactory.MakeEffectsFromJson(jsonItem, item.key);
		}

		public GameObject BuildItemObject(string itemName) 
		{
			Item script = BuildItemScript(itemName);
			return BuildItemObject(script);
		}

		public GameObject BuildItemObject(Item itemScript) 
		{
			GameObject instance = dataLocator.InstantiateLoadedResource(itemScript.name);
			instance.GetComponent<ItemMono>().item = itemScript;
			return instance;
		}
	}
}