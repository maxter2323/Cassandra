using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using CassandraEvents;

namespace CassandraFramework.Items
{

	public class Inventory
	{

		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		private int capacity = 100;
		private float weight = 0.0f;
		private Dictionary<string, ItemGroup> inventory = new Dictionary<string, ItemGroup>();

		public ItemEvent OnItemAdded = new ItemEvent();
		public ItemEvent OnItemRemoved = new ItemEvent();

		/****************************************************************************************/
		/*										METHODS									  		*/
		/****************************************************************************************/

		public void Init () 
		{
			inventory = new Dictionary<string, ItemGroup>();
		}

		public void TakeItem(GameObject newItem)
		{
			Item itemScript = newItem.GetComponent<ItemMono>().item;
			AddItem(itemScript);
			GameObject.Destroy(newItem);
		}

		public void AddItem(string newItem)
		{
			ItemFactory factory = ServiceLocator.GetService<ItemFactory>();
			Item item = factory.BuildItemScript(newItem);
			AddItem(item, 1);
		}

		public void AddItem(Item newItem)
		{
			AddItem(newItem, 1);
		}

		public bool HasItem(string itemName)
		{
			return inventory.ContainsKey(itemName);
		}

		public void AddItem(Item newItem, int amount)
		{
			string itemKey = newItem.key;
			weight += newItem.GetWeight() * amount;
			if (inventory.ContainsKey(itemKey))
			{
				ItemGroup itemGroup = inventory[itemKey];
				for (int i = 0; i < amount; i++)
				{
					itemGroup.AddItem(newItem);
				}
			}
			else
			{
				ItemGroup newGroup = new ItemGroup();
				for (int i = 0; i < amount; i++)
				{
					newGroup.AddItem(newItem);
				}
				inventory.Add(itemKey, newGroup);
			}
			if (OnItemAdded != null) OnItemAdded.Invoke(newItem);
		}

		public void RemoveItem(Item itemToRemove)
		{
			string itemName = itemToRemove.key;
			if (inventory.ContainsKey(itemName))
			{
				ItemGroup itemGroup = inventory[itemName];
				itemGroup.RemoveItem(itemToRemove);
				if (itemGroup.Count() == 0)
				{
					inventory.Remove(itemName);
					if (OnItemRemoved != null) OnItemRemoved.Invoke(itemToRemove);
				}
			}
		}

		public void RemoveItem(Item itemToRemove, int count)
		{
			for (int i = 0; i < count; i++)
			{
				RemoveItem(itemToRemove);
			}
		}

		public void RemoveItem(string itemName)
		{
			if (inventory.ContainsKey(itemName))
			{
				ItemGroup itemGroup = inventory[itemName];
				itemGroup.RemoveItem();
				if (itemGroup.Count() == 0)
				{
					inventory.Remove(itemName);
				}
			}
		}

		public void DropItem(Item itemToRemove)
		{
			//GameObject obj = ServiceLocator.GetService<DataLocator>().InstantiateLoadedResource(itemToRemove.name);
			//obj.GetComponent<ItemMono>().item = itemToRemove;
			//obj.transform.position = Player.instance.viewObj.transform.position + (Player.instance.viewObj.transform.forward * 2);
			//RemoveItem(itemToRemove);
		}

		public List<ItemGroup> GetItemList()
		{
			List<ItemGroup> toReturn = new List<ItemGroup>();
			foreach(ItemGroup group in inventory.Values)
			{
				toReturn.Add(group);
			}
			return toReturn;
		}

		public int GetItemCount(string itemName)
		{
			if (inventory.ContainsKey(itemName))
			{
				return inventory[itemName].Count();
			}
			return 0;
		}

	}
}