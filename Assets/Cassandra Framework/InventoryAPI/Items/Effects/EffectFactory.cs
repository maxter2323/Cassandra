using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class EffectFactory : IService 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private const string JSON_EFFECT_TYPE = "Type";
	private const string JSON_EFFECT_KEY = "Key";
	private const string JSON_EFFECT_ARGUMENT = "Arg";
	private const string JSON_EFFECT_VALUE = "Value";

	private const string JSON_EFFECT_NAME = "Name";
	private const string JSON_EFFECT_DESCRIPTION = "Description";
	private const string JSON_EFFECT_SCRIPT = "Script";

	private const string JSON_EFFECT_EFFECTS = "Effects";
	private const string JSON_EFFECT_CASSANDRAEFFECTS = "CassandraEffects";
	private const string JSON_EFFECT_CUSTOMEFFECTS = "CustomEffects";

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void Init () 
	{
		//
	}

	public List<IEffect> MakeEffectsFromJson(JSONNode jsonData, string id)
	{
		List<IEffect> toReturn = new List<IEffect>();
		if (jsonData[JSON_EFFECT_EFFECTS] != null &&
			jsonData[JSON_EFFECT_EFFECTS][JSON_EFFECT_CASSANDRAEFFECTS] != null)
		{
			JSONNode cassEffects = jsonData[JSON_EFFECT_EFFECTS][JSON_EFFECT_CASSANDRAEFFECTS];
			for (int i = 0; i < cassEffects.Count; i++)
			{
				toReturn.Add(MakeEffectFromJson(cassEffects[i]));
			}
		}
		if (jsonData[JSON_EFFECT_EFFECTS] != null &&
			jsonData[JSON_EFFECT_EFFECTS][JSON_EFFECT_CASSANDRAEFFECTS] != null)
		{
			JSONNode customEffects = jsonData[JSON_EFFECT_EFFECTS][JSON_EFFECT_CASSANDRAEFFECTS];
			for (int i = 0; i < customEffects.Count; i++)
			{
				toReturn.Add(MakeCustomEffectFromJson(customEffects[i], id, i));
			}
		}
		return toReturn;
	}

	public IEffect MakeEffectFromJson(JSONNode jsonEffects)
	{
		Effect effect = new Effect();
		effect.type = jsonEffects[JSON_EFFECT_TYPE];
		effect.key = jsonEffects[JSON_EFFECT_KEY];
		effect.argument = jsonEffects[JSON_EFFECT_ARGUMENT];
		effect.value = jsonEffects[JSON_EFFECT_VALUE].AsInt;
		return (IEffect)effect;
	}

	public IEffect MakeCustomEffectFromJson(JSONNode jsonRequirement, string id, int index)
	{
		CustomEffect requirement = new CustomEffect();
		requirement.name = jsonRequirement[JSON_EFFECT_NAME];
		requirement.description = jsonRequirement[JSON_EFFECT_DESCRIPTION];
		requirement.script = new GameScript(jsonRequirement[JSON_EFFECT_SCRIPT]);
		requirement.script.scriptName = id + "_" + JSON_EFFECT_SCRIPT + "_" + index.ToString();
		return (IEffect)requirement;
	}
}
