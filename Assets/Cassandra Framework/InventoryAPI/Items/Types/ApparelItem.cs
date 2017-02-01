using UnityEngine;
using System.Collections;
using CassandraEvents;

namespace CassandraFramework.Items
{
	public class ApparelItem : Item 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		//Core
		public string slotKey;
		public Effects effects;

		//Events
		public ItemEvent OnEquiped = new ItemEvent();
		public ItemEvent OnUnequiped = new ItemEvent();

		/****************************************************************************************/
		/*										NATIVE METHODS									*/
		/****************************************************************************************/

		public void Equip()
		{
			currentContainer.owner.slots.GetSlot(slotKey).SetItem(this);
			effects.SetOwners(currentContainer.owner);
			effects.Do();
			if (OnEquiped != null) OnEquiped.Invoke(this);
		}

		public void Unequip()
		{
			currentContainer.owner.slots.GetSlot(slotKey).SetItem(null);
			effects.Revert();
			if (OnUnequiped != null) OnUnequiped.Invoke(this);
		}
	}
}