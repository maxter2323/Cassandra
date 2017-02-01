using UnityEngine;
using System.Collections;

namespace CassandraFramework.Items
{
	public class Item 
	{

		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public enum ItemType
		{
			Quality,
			Quantity
		};

		public string name;
		public string key;
		public string description;
		public float weight;
		public ItemType type;
		public string itemTypeString;
		public string iconName;
		public Inventory currentContainer;

		public string GetName()
		{
			return name;
		}

		public float GetWeight()
		{
			return weight;
		}

		public virtual void Use()
		{

		}
	

	}
}