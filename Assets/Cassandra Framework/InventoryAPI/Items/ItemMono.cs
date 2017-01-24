using UnityEngine;
using System.Collections;
using CassandraFramework.Items;

public class ItemMono : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Item item;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	private void Start () 
	{
		if (item == null)
		{
			ItemFactory factory = ServiceLocator.GetService<ItemFactory>();
			item = factory.BuildItemScript(this.name);
		}
	}

}
