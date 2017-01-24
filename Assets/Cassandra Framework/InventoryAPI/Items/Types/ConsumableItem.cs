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

		public List<IEffect> effects = new List<IEffect>();

		/****************************************************************************************/
		/*										NATIVE METHODS									*/
		/****************************************************************************************/

		public override void Use()
		{
			for (int i = 0; i < effects.Count; i++)
			{
				effects[i].Do();
			}
		}

	}
}