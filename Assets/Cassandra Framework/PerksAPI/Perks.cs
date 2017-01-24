using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CassandraFramework.Perks
{
	public class Perks 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/
		private Dictionary<string, Perk> perks = new Dictionary<string, Perk>();
		private Dictionary<string, Dictionary<string, Perk>> perksGroups = new Dictionary<string, Dictionary<string, Perk>>();
		
		/****************************************************************************************/
		/*										Perks MANAGEMENT								*/
		/****************************************************************************************/

		public int Count()
		{
			return perks.Count;
		}

		public Perk GetPerk(string key)
		{
			if (perks.ContainsKey(key)) return perks[key];
			return null;
		}

		public void AddPerk(Perk p)
		{
			perks[p.key] = p;
			AddStatToGroup(p);
			p.OnTagChanged.AddListener(ChangeStatGroup);
		}

		public void RemovePerk(Perk s)
		{
			if (s == null) return;
			RemoveStatFromGroup(s);
			perks.Remove(s.key);
			s = null;
		}

		public void RemovePerk(string key)
		{
			RemovePerk(GetPerk(key));
		}

		/****************************************************************************************/
		/*										RETRIEVE GROUPS									*/
		/****************************************************************************************/

		public Dictionary<string, Perk> GetStatGroupDict(string tag)
		{
			return perksGroups[tag];
		}

		public List<Perk> GetStatGroupList(string tag)
		{
			List<Perk> toreturn = new List<Perk>();
			Dictionary<string, Perk> statGroup = GetStatGroupDict(tag);
			if (statGroup == null) return null;
			foreach(Perk item in statGroup.Values)
			{
				toreturn.Add(item);
			}
			return toreturn;
		}

		public List<Perk> GetAllStatsList()
		{
			List<Perk> toreturn = new List<Perk>();
			foreach(Perk s in perks.Values)
			{
				toreturn.Add(s);
			}
			return toreturn;
		}

		/****************************************************************************************/
		/*										GROUP MANAGEMENT								*/
		/****************************************************************************************/

		private void AddStatToGroup(Perk s)
		{
			if (!perksGroups.ContainsKey(s.Tag))
			{
				perksGroups.Add(s.Tag, new Dictionary<string, Perk>());
			}
			perksGroups[s.Tag].Add(s.key, s);
		}

		private void RemoveStatFromGroup(Perk s)
		{
			RemoveStatFromGroup(s, s.Tag);
		}

		private void RemoveStatFromGroup(Perk s, string tag)
		{
			if (perksGroups.ContainsKey(tag))
			{
				Dictionary<string, Perk> statGroup =  perksGroups[tag];
				statGroup.Remove(s.key);
				if (statGroup.Count == 0)
				{
					perksGroups.Remove(tag);
				}
			}
		}

		private void ChangeStatGroup(Perk s, string oldTag)
		{
			RemoveStatFromGroup(s, oldTag);
			AddStatToGroup(s);
		}

	}
}