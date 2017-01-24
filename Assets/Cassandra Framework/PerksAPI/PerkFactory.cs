using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace CassandraFramework.Perks
{
	public class PerkFactory : IService 
	{
			/****************************************************************************************/
			/*										VARIABLES									  	*/
			/****************************************************************************************/

			//Core
			private JsonParser jsonParser;
			private RequirementFactory requirementFactory;
			private Dictionary<string, Perk> perks = new Dictionary<string, Perk>();

			//Strings
			private const string JSON_PERK_KEY = "Key";
			private const string JSON_PERK_NAME = "Name";
			private const string JSON_PERK_TAG = "Tag";
			private const string JSON_PERK_TAGNAME = "TagName";
			private const string JSON_PERK_DESCRIPTION = "Description";
			private const string JSON_PERK_REQUIREMENTS = "Requirements";

			/****************************************************************************************/
			/*										METHODS											*/
			/****************************************************************************************/

			public void Init () 
			{
				jsonParser = ServiceLocator.GetService<JsonParser>();
				requirementFactory = ServiceLocator.GetService<RequirementFactory>();
			}

			public void AddPerk(Perk p)
			{
				perks.Add(p.key, p);
			}

			public Perk GetPerk(string perkKey)
			{
				if (!perks.ContainsKey(perkKey)) return null;
				return perks[perkKey];
			}

			public Perk MakePerkFromJson(string perkKey)
			{
				JSONNode jsonPerk = jsonParser.GetPerksData(perkKey);
				return MakePerkFromJson(jsonPerk);
			}

			public Perk MakePerkFromJson(JSONNode jsonPerk)
			{
				Perk perk = new Perk();
				perk.key = jsonPerk[JSON_PERK_KEY];
				perk.name = jsonPerk[JSON_PERK_NAME];
				perk.tag = jsonPerk[JSON_PERK_TAG];
				perk.tagName = jsonPerk[JSON_PERK_TAGNAME];
				perk.description = jsonPerk[JSON_PERK_DESCRIPTION];
				perk.requirements = requirementFactory.MakeRequirementsFromJson(jsonPerk, perk.key);
				return perk;
			}

	}
}