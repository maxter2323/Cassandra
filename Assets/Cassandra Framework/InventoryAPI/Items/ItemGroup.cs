using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CassandraFramework.Items
{
	public class ItemGroup 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public List<Item> itemList = new List<Item>();

		/****************************************************************************************/
		/*										METHODS									  	    */
		/****************************************************************************************/

		public void AddItem(Item itemToAdd)
		{
			itemList.Add(itemToAdd);
		}

		public void RemoveItem(Item itemToRemove)
		{
			itemList.Remove(itemToRemove);
		}

		public void RemoveItem()
		{
			itemList.RemoveAt(itemList.Count - 1);
		}

		public int Count()
		{
			return itemList.Count;
		}

		public Item GetAt(int index)
		{
			return itemList[index];
		}
	}
}