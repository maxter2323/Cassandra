using UnityEngine;
using System.Collections;
using CassandraFramework.Stats;
using System;
using System.Timers;
using CassandraEvents;

[Serializable]
public class Effect : IEffect
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Core
	public string type;
	public string key;
	public string argument;
	public int value;
	public int time;
	public Character owner;
	[NonSerialized] public Timer timer;

	//Events
	[NonSerialized] public EffectEvent OnStarted;
	[NonSerialized] public EffectEvent OnFinished;

	//Strings
	private const string EFFECTS_STAT = "Stat";
	private const string EFFECTS_ITEM = "Item";
	private const string EFFECTS_STAT_SET = "Set";
	private const string EFFECTST_STAT_INCREASE= "Increase";
	private const string EFFECTS_STAT_DECREASE = "Decrease";
	private const string EFFECTS_ITEM_ADD = "Add";
	private const string EFFECTS_ITEM_REMOVE = "Remove";

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
			case EFFECTS_STAT_SET:
				owner.stats.SetStat(key, value);
				break;
			case EFFECTST_STAT_INCREASE:
				owner.stats.IncreaseStat(key, value);
				break;
			case EFFECTS_STAT_DECREASE:
				owner.stats.DecreaseStat(key, value);
				break;
		}
	}

	public void DoItemEffect()
	{
		switch (argument)
		{
			case EFFECTS_ITEM_ADD:
				owner.inventory.AddItem(key);
				break;
			case EFFECTS_ITEM_REMOVE:
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
		if (OnStarted != null) OnStarted.Invoke(this);
		if (time != 0)
		{
			timer = new Timer(time);
			timer.Elapsed += new ElapsedEventHandler(OnElapse);
			timer.Enabled = true;
		}
		else 
		{ 
			if (OnFinished != null) OnFinished.Invoke(this);
		}
	}

	private void OnElapse(object sender, ElapsedEventArgs e)
	{
		Revert();
		if (OnFinished != null) OnFinished.Invoke(this);
	}

	public void Revert()
	{
		switch (type)
		{
			case "Stat":
				RevertStatEffect();
				break;
			case "Item":
				RevertItemEffect();
				break;
		}
	}

	public void RevertStatEffect()
	{
		switch (argument)
		{
			case EFFECTS_STAT_SET:
				owner.stats.SetStat(key, value);
				break;
			case EFFECTST_STAT_INCREASE:
				owner.stats.DecreaseStat(key, value);
				break;
			case EFFECTS_STAT_DECREASE:
				owner.stats.IncreaseStat(key, value);
				break;
		}
	}

	public void RevertItemEffect()
	{
		switch (argument)
		{
			case EFFECTS_ITEM_ADD:
				owner.inventory.RemoveItem(key);
				break;
			case EFFECTS_ITEM_REMOVE:
				owner.inventory.AddItem(key);
				break;
		}
	}

	public string DescriptionString()
	{
		string toReturn = "None";
		switch (type)
		{
			case EFFECTS_STAT:
				toReturn = String.Format("{0} {1} {2}", owner.stats.GetStat(key).Name, argument, value);
				break;
			case EFFECTS_ITEM:
				toReturn = argument + " " + key + " (" + value + ")";
				break;
		}
		return toReturn;
	}
}
