using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Slots 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private Dictionary<string, Slot> slots = new Dictionary<string, Slot>();
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void MakeSlot(string n, Type t)
	{
		Slot newSlot = new Slot(n, t);
		slots[n] = newSlot;
	}

	public void MakeSlot(string n, string ttag, Type t)
	{
		Slot newSlot = new Slot(n, ttag, t);
		slots[n] = newSlot;
	}

	public void SetSlot(string name, int val)
	{
		//Slot stat = GetStat(name);
		//if (stat != null) stat.Value = val;
	}

	public Slot GetSlot(string name)
	{
		//if (slots.ContainsKey(name)) return slots[name];
		return null;
	}

}
