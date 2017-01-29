using System;
using System.Collections.Generic;
using CassandraEvents;

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
		public Requirements requirements = new Requirements();
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

		public List<GameScript> GetAllScripts()
		{
			List<GameScript> toreturn = new List<GameScript>();
			toreturn.AddRange(requirements.GetAllScripts());
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
			
		}

		public bool Ready()
		{
			return requirements.Ready();
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