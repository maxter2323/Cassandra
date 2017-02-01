using UnityEngine;
using System;
using System.Collections;
using CassandraFramework.Items;

public class Slot 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public string name = "None";
	public string tag = "None";
	public Type itemType;
	public Item currentItem;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public Slot()
	{

	}

	public void SetItem(Item newItem)
	{
		currentItem = newItem;
	}

	public Item GetItem()
	{
		return currentItem;
	}

	public Slot(string nname, Type t)
	{
		name = nname;
		itemType = t;
	}

	public Slot(string nname, string ttag, Type t)
	{
		name = nname;
		tag = ttag;
		itemType = t;
	}

}
