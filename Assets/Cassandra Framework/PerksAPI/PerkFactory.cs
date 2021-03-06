﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

namespace CassandraFramework.Perks
{
	public class PerkFactory : IService, IFactory 
	{
			/****************************************************************************************/
			/*										VARIABLES									  	*/
			/****************************************************************************************/

			//Core
			private JsonParser jsonParser;
			private RequirementFactory requirementFactory;
			private EffectFactory effectFactory;
			private Dictionary<string, Perk> perks = new Dictionary<string, Perk>();

			//Strings
			private const string JSON_PERK_KEY = "Key";
			private const string JSON_PERK_NAME = "Name";
			private const string JSON_PERK_TAG = "Tag";
			private const string JSON_PERK_TAGNAME = "TagName";
			private const string JSON_PERK_DESCRIPTION = "Description";
			private const string JSON_PERK_REQUIREMENTS = "Requirements";
			private const string JSON_PERK_EFFECTS = "Effects";

			/****************************************************************************************/
			/*										METHODS											*/
			/****************************************************************************************/

			public void Init () 
			{
				jsonParser = ServiceLocator.GetService<JsonParser>();
				requirementFactory = ServiceLocator.GetService<RequirementFactory>();
				effectFactory = ServiceLocator.GetService<EffectFactory>();
			}
			
			public List<IGameScriptable> MakeAll()
			{
				List<IGameScriptable> toreturn = new List<IGameScriptable>();
				for (int i = 0; i < jsonParser.perksNode.Count; i++)
				{
					JSONNode jsonPerk = jsonParser.perksNode[i];
					IGameScriptable s = MakePerkFromJson(jsonPerk);
					toreturn.Add(s);
				}
				return toreturn;
			}

			public void Add(object toAdd)
			{
				AddPerk(toAdd as Perk);
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
				JSONNode jsonPerk = jsonParser.perksNode[perkKey];
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
				perk.requirements.Set(requirementFactory.JSON_To_Requirements(jsonPerk, perk.key));
				perk.effects.Set(effectFactory.JSON_To_Effects(jsonPerk, perk.key));
				return perk;
			}

	}
}