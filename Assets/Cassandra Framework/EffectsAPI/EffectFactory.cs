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
	private const string JSON_EFFECT_TIME = "Time";

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

	public List<IEffect> JSON_To_Effects(JSONNode jsonData, string id)
	{
		List<IEffect> toReturn = new List<IEffect>();
		JSONNode effects = jsonData[JSON_EFFECT_EFFECTS];
		if (effects != null)
		{
			JSONNode cassEffects = effects[JSON_EFFECT_CASSANDRAEFFECTS];
			if (cassEffects != null)
			{
				for (int i = 0; i < cassEffects.Count; i++)
				{
					toReturn.Add(JSON_To_Effect(cassEffects[i]));
				}
			}
			JSONNode customEffects = effects[JSON_EFFECT_CUSTOMEFFECTS];
			if (customEffects != null)
			{
				for (int i = 0; i < customEffects.Count; i++)
				{
					toReturn.Add(JSON_To_CustomEffect(customEffects[i], id, i));
				}
			}
		}
		return toReturn;
	}

	public IEffect JSON_To_Effect(JSONNode jsonEffect)
	{
		Effect effect = new Effect();
		effect.type = jsonEffect[JSON_EFFECT_TYPE];
		effect.key = jsonEffect[JSON_EFFECT_KEY];
		effect.time = jsonEffect[JSON_EFFECT_TIME].AsInt;
		effect.argument = jsonEffect[JSON_EFFECT_ARGUMENT];
		effect.value = jsonEffect[JSON_EFFECT_VALUE].AsInt;
		return effect;
	}

	public IEffect JSON_To_CustomEffect(JSONNode jsonEffect, string id, int index)
	{
		CustomEffect effect = new CustomEffect();
		effect.name = jsonEffect[JSON_EFFECT_NAME];
		effect.description = jsonEffect[JSON_EFFECT_DESCRIPTION];
		effect.time = jsonEffect[JSON_EFFECT_TIME].AsInt;
		effect.script = new GameScript(jsonEffect[JSON_EFFECT_SCRIPT]);
		effect.script.scriptName = id + "_" + JSON_EFFECT_SCRIPT + "_" + index.ToString();
		return effect;
	}
}
