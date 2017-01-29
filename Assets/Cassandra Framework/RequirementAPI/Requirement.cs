using System;
using CassandraFramework.Stats;

[Serializable]
public class Requirement : IRequirement
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Core
	public string type;
	public string key;
	public string argument;
	public int value;
	public Character owner;

	//Strings
	private const string REQUIREMENT_STAT = "Stat";
	private const string REQUIREMENT_ITEM = "Item";
	private const string REQUIREMENT_STAT_EQUAL = "Equals";
	private const string REQUIREMENT_STAT_MORE = "More";
	private const string REQUIREMENT_STAT_LESS = "Less";

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public bool CheckRequirement()
	{
		bool answer = false;
		switch (type)
		{
			case REQUIREMENT_STAT:
				answer = CheckStatRequirement();
				break;
			case REQUIREMENT_ITEM:
				answer = CheckItemRequirement();
				break;
		}
		return answer;
	}

	private bool CheckStatRequirement()
	{
		bool answer = false;
		Stat s = owner.stats.GetStat(key);
		switch (argument)
		{
			case REQUIREMENT_STAT_EQUAL:
				answer = (value == s.Value);
				break;
			case REQUIREMENT_STAT_MORE:
				answer = (value < s.Value);
				break;
			case REQUIREMENT_STAT_LESS:
				answer = (value > s.Value);
				break;
		}
		return answer;
	}

	private bool CheckItemRequirement()
	{
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
		string toReturn = "None";
		switch (type)
		{
			case REQUIREMENT_STAT:
				toReturn = String.Format("{0} {1} {2}", owner.stats.GetStat(key).Name, argument, value);
				break;
			case REQUIREMENT_ITEM:
				toReturn = key + " (" + value + ")";
				break;
		}
		return toReturn;
	}
}