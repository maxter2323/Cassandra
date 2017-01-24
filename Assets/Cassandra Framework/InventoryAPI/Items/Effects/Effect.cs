using UnityEngine;
using System.Collections;
using CassandraFramework.Stats;
using System;

[Serializable]
public class Effect : IEffect
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public string type;
	public string key;
	public string argument;
	public int value;
	public Character owner;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public Effect()
	{

	}

	public void DoStatEffect()
	{
		switch (argument)
		{
			case "Set":
				owner.stats.SetStat(key, value);
				break;
			case "Increase":
				owner.stats.IncreaseStat(key, value);
				break;
			case "Decrease":
				owner.stats.DecreaseStat(key, value);
				break;
		}
	}

	public void DoItemEffect()
	{
		switch (argument)
		{
			case "Add":
				owner.inventory.AddItem(key);
				break;
			case "Remove":
				owner.inventory.RemoveItem(key);
				break;
		}
	}

	public void Do()
	{	
		switch (type)
		{
			case "Stat":
				DoStatEffect();
				break;
			case "Item":
				DoItemEffect();
				break;
		}
	}
}
