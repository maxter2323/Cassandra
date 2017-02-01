using UnityEngine;
using System.Collections;
using CassandraFramework.Stats;
using CassandraFramework.Items;
using System;
using CassandraFramework.Quests;
using CassandraFramework.Perks;

[Serializable]
public class Character 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Stats stats = new Stats();
	public Inventory inventory = new Inventory();
	public Slots slots = new Slots();
	public Quests quests = new Quests();
	public Perks perks = new Perks();
	public Recipes recipes = new Recipes();
	
	/****************************************************************************************/
	/*										 METHODS										*/
	/****************************************************************************************/
	
	public Character()
	{
		inventory.owner = this;
	}
}
