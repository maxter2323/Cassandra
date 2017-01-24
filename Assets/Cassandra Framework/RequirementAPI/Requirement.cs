using System;
using System.Collections;
using UnityEngine;
using CassandraFramework.Stats;
using CassandraFramework.Items;

[Serializable]
public class Requirement : IRequirement
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

	public string Key()
	{
		return key;
	}

	public bool CheckRequirement()
	{
		switch (type)
		{
			case "Stat":
				return CheckStatRequirement();
				break;
			case "Item":
				return CheckItemRequirement();
				break;
		}
		return false;
	}

	private bool CheckStatRequirement()
	{
		bool answer = false;
		Stat s = owner.stats.GetStat(key);
		switch (argument)
		{
			case "Equal":
				answer = (value == s.Value);
				break;
			case "More":
				answer = (value < s.Value);
				break;
			case "Less":
				answer = (value > s.Value);
				break;
		}
		return answer;
	}

	private bool CheckItemRequirement()
	{
		bool answer;
		if (owner.inventory.HasItem(key))
		{
			if (owner.inventory.GetItemCount(key) == value)
			{
				return true;
			}
		}
		return false;
	}

	public string DescriptionString()
	{
		switch (type)
		{
			case "Stat":
				return String.Format("{0} {1} {2}", owner.stats.GetStat(key).Name, argument, value);
				break;
			case "Item":
				return key + " (" + value + ")";
				break;
		}
		return "None";
	}
}