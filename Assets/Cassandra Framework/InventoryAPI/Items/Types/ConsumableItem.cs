using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CassandraFramework.Items
{
	public class ConsumableItem : Item 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public Effects effects = new Effects();

		/****************************************************************************************/
		/*										NATIVE METHODS									*/
		/****************************************************************************************/

		public override void Use()
		{
			effects.SetOwners(currentContainer.owner);
			effects.Do();
			currentContainer.owner.inventory.RemoveItem(this);
		}

	}
}