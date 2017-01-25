using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CassandraEvents;
using System;
using System.Reflection;

namespace CassandraFramework.Perks
{
	[Serializable]
	public class Perk : IGameScriptable
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public string key;
		public string name;
		public string description;
		public string tag;
		public string tagName;

		public Character owner;
		public List<IRequirement> requirements = new List<IRequirement>();
		public List<IEffect> effects = new List<IEffect>();

		[NonSerialized] public PerkStringEvent OnTagChanged = new PerkStringEvent();

		/****************************************************************************************/
		/*										METHODS											*/
		/****************************************************************************************/
		public IFactory GetFactory()
		{
			return (IFactory)ServiceLocator.GetService<PerkFactory>();
		}
		
		public string Tag
		{
			get{return tag;}
			set
			{
				if(tag == value) return;
				string oldTag = tag;
				tag = value;
				if (OnTagChanged != null) OnTagChanged.Invoke(this, oldTag);
			}
		}

		public void PrepareScripts(Assembly assembly)
		{
			for (int i = 0; i < requirements.Count; i++)
			{
				if (requirements[i] is CustomRequirement)
				{
					((CustomRequirement)requirements[i]).script.Prepare(assembly);
				}
			}
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
			for (int i = 0; i < effects.Count; i++)
			{
				if (effects[i] is CustomEffect)
				{
					toreturn.Add(((CustomEffect)effects[i]).script);
				}
			}
			return toreturn;
		}

		public void SetOwners()
		{
			for (int i = 0; i < requirements.Count; i++)
			{
				if (requirements[i].CheckRequirement() == false)
				{
					//return false;
				} 
			}
			for (int i = 0; i < effects.Count; i++)
			{
				
			}
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

		public void ApplyEffects()
		{
			for (int i = 0; i < effects.Count; i++)
			{
				effects[i].Do();
			}
		}
	}
}