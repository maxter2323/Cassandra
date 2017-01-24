using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;

public class RequirementFactory : IService 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private const string JSON_REQUIREMENT_TYPE = "Type";
	private const string JSON_REQUIREMENT_KEY = "Key";
	private const string JSON_REQUIREMENT_ARGUMENT = "Arg";
	private const string JSON_REQUIREMENT_VALUE = "Value";

	private const string JSON_REQUIREMENT_NAME = "Name";
	private const string JSON_REQUIREMENT_DESCRIPTION = "Description";
	private const string JSON_REQUIREMENT_SCRIPT = "Script";

	private const string JSON_REQUIREMENT_REQUIREMENTS = "Requirements";
	private const string JSON_REQUIREMENT_CASSANDRAREQUIREMENTS = "CassandraRequirements";
	private const string JSON_REQUIREMENT_CUSTOMREQUIREMENTS = "CustomRequirements";

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void Init () 
	{
		//
	}

	public List<IRequirement> MakeRequirementsFromJson(JSONNode jsonData, string id)
	{
		List<IRequirement> toReturn = new List<IRequirement>();
		if (jsonData[JSON_REQUIREMENT_REQUIREMENTS] != null &&
			jsonData[JSON_REQUIREMENT_REQUIREMENTS][JSON_REQUIREMENT_CASSANDRAREQUIREMENTS] != null
		)
		{
			JSONNode cassRequirements = jsonData[JSON_REQUIREMENT_REQUIREMENTS][JSON_REQUIREMENT_CASSANDRAREQUIREMENTS];
			for (int i = 0; i < cassRequirements.Count; i++)
			{
				toReturn.Add(MakeRequirementFromJson(cassRequirements[i]));
			}
		}
		if (jsonData[JSON_REQUIREMENT_REQUIREMENTS] != null &&
			jsonData[JSON_REQUIREMENT_REQUIREMENTS][JSON_REQUIREMENT_CUSTOMREQUIREMENTS] != null
		)
		{
			JSONNode customRequirements = jsonData[JSON_REQUIREMENT_REQUIREMENTS][JSON_REQUIREMENT_CUSTOMREQUIREMENTS];
			for (int i = 0; i < customRequirements.Count; i++)
			{
				toReturn.Add(MakeCustomRequirementFromJson(customRequirements[i], id, i));
			}
		}
		return toReturn;
	}

	public IRequirement MakeRequirementFromJson(JSONNode jsonRequirement)
	{
		Requirement requirement = new Requirement();
		requirement.type = jsonRequirement[JSON_REQUIREMENT_TYPE];
		requirement.key = jsonRequirement[JSON_REQUIREMENT_KEY];
		requirement.argument = jsonRequirement[JSON_REQUIREMENT_ARGUMENT];
		requirement.value = jsonRequirement[JSON_REQUIREMENT_VALUE].AsInt;
		return (IRequirement)requirement;
	}

	public IRequirement MakeCustomRequirementFromJson(JSONNode jsonRequirement, string id, int index)
	{
		CustomRequirement requirement = new CustomRequirement();
		requirement.name = jsonRequirement[JSON_REQUIREMENT_NAME];
		requirement.description = jsonRequirement[JSON_REQUIREMENT_DESCRIPTION];
		requirement.script = new GameScript(jsonRequirement[JSON_REQUIREMENT_SCRIPT]);
		requirement.script.scriptName = id + "_" + JSON_REQUIREMENT_SCRIPT + "_" + index.ToString();
		return (IRequirement)requirement;
	}
}
