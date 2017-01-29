using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public class Requirements
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public List<IRequirement> requirements = new List<IRequirement>();

	/****************************************************************************************/
	/*										Perks MANAGEMENT								*/
	/****************************************************************************************/

	public void Set(List<IRequirement> r)
	{
		requirements.Clear();
		requirements = r;
	}

	public int Count()
	{
		return requirements.Count;
	}

	public bool Ready()
	{
		for (int i = 0; i < requirements.Count; i++)
		{
			if (requirements[i].CheckRequirement() == false)
			{
				return false;
			}
		}
		return true;
	}

	public List<GameScript> GetAllScripts()
	{
		List<GameScript> toreturn = new List<GameScript>();
		for (int i = 0; i < requirements.Count; i++)
		{
			if (requirements[i] is CustomRequirement)
			{
				toreturn.Add(((CustomRequirement)requirements[i]).script);
			}
		}
		return toreturn;
	}

}