using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Effects
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public List<IEffect> effects = new List<IEffect>();

	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/

	public void Set(List<IEffect> e)
	{
		effects.Clear();
		effects = e;
	}

	public int Count()
	{
		return effects.Count;
	}

	public void Do()
	{
		for (int i = 0; i < effects.Count; i++)
		{
			effects[i].Do();
		}
	}

	public void Revert()
	{
		for (int i = 0; i < effects.Count; i++)
		{
			if (effects[i] is Effect)
			{
				((Effect)effects[i]).Revert();
			}
		}
	}

	public void SetOwners(Character owner)
	{
		for (int i = 0; i < effects.Count; i++)
		{
			if (effects[i] is Effect)
			{
				((Effect)effects[i]).owner = owner;
			}
		}
	}

	public List<GameScript> GetAllScripts()
	{
		List<GameScript> toreturn = new List<GameScript>();
		for (int i = 0; i < effects.Count; i++)
		{
			if (effects[i] is CustomEffect)
			{
				toreturn.Add(((CustomEffect)effects[i]).script);
			}
		}
		return toreturn;
	}
}