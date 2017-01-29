using System.Collections.Generic;

namespace CassandraFramework.Stats
{
	public class Stats 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		private Dictionary<string, Stat> stats = new Dictionary<string, Stat>();
		private Dictionary<string, Dictionary<string, Stat>> statGroups = new Dictionary<string, Dictionary<string, Stat>>();
		
		/****************************************************************************************/
		/*										STATS MANAGEMENT								*/
		/****************************************************************************************/

		public int Count()
		{
			return stats.Count;
		}

		public Stat GetStat(string key)
		{
			if (stats.ContainsKey(key)) return stats[key];
			return null;
		}

		public void AddStat(Stat s)
		{
			stats[s.key] = s;
			AddStatToGroup(s);
			s.OnTagChanged.AddListener(ChangeStatGroup);
		}

		public void RemoveStat(Stat s)
		{
			if (s == null) return;
			RemoveStatFromGroup(s);
			stats.Remove(s.key);
			s = null;
		}

		public void RemoveStat(string key)
		{
			RemoveStat(GetStat(key));
		}

		/****************************************************************************************/
		/*										STATS VALUES									*/
		/****************************************************************************************/

		public void SetStat(string key, int val)
		{
			Stat stat = GetStat(key);
			if (stat != null) stat.Value = val;
		}

		public void IncreaseStat(string key, int val)
		{
			Stat stat = GetStat(key);
			if (stat != null) stat.Increase(val);
		}

		public void DecreaseStat(string key, int val)
		{
			Stat stat = GetStat(key);
			if (stat != null) stat.Decrease(val);
		}

		/****************************************************************************************/
		/*										RETRIEVE GROUPS									*/
		/****************************************************************************************/

		public Dictionary<string, Stat> GetStatGroupDict(string tag)
		{
			return statGroups[tag];
		}

		public List<Stat> GetStatGroupList(string tag)
		{
			List<Stat> toreturn = new List<Stat>();
			Dictionary<string, Stat> statGroup = GetStatGroupDict(tag);
			if (statGroup == null) return null;
			foreach(Stat item in statGroup.Values)
			{
				toreturn.Add(item);
			}
			return toreturn;
		}

		public List<Stat> GetAllStatsList()
		{
			List<Stat> toreturn = new List<Stat>();
			foreach(Stat s in stats.Values)
			{
				toreturn.Add(s);
			}
			return toreturn;
		}

		/****************************************************************************************/
		/*										GROUP MANAGEMENT								*/
		/****************************************************************************************/

		private void AddStatToGroup(Stat s)
		{
			if (!statGroups.ContainsKey(s.Tag))
			{
				statGroups.Add(s.Tag, new Dictionary<string, Stat>());
			}
			statGroups[s.Tag].Add(s.key, s);
		}

		private void RemoveStatFromGroup(Stat s)
		{
			RemoveStatFromGroup(s, s.Tag);
		}

		private void RemoveStatFromGroup(Stat s, string tag)
		{
			if (statGroups.ContainsKey(tag))
			{
				Dictionary<string, Stat> statGroup =  statGroups[tag];
				statGroup.Remove(s.key);
				if (statGroup.Count == 0)
				{
					statGroups.Remove(tag);
				}
			}
		}

		private void ChangeStatGroup(Stat s, string oldTag)
		{
			RemoveStatFromGroup(s, oldTag);
			AddStatToGroup(s);
		}

	}
}