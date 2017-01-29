using SimpleJSON;
using System.Collections.Generic;

public class RequirementFactory : IService 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//CassandraRequirement
	private const string JSON_REQUIREMENT_TYPE = "Type";
	private const string JSON_REQUIREMENT_KEY = "Key";
	private const string JSON_REQUIREMENT_ARGUMENT = "Arg";
	private const string JSON_REQUIREMENT_VALUE = "Value";

	//CustomRequirement
	private const string JSON_REQUIREMENT_NAME = "Name";
	private const string JSON_REQUIREMENT_DESCRIPTION = "Description";
	private const string JSON_REQUIREMENT_SCRIPT = "Script";
	private const string JSON_REQUIREMENT_SCRIPT2 = "RequirementScript";

	//Category keys
	private const string JSON_REQUIREMENT_REQUIREMENTS = "Requirements";
	private const string JSON_REQUIREMENT_CASSANDRAREQUIREMENTS = "CassandraRequirements";
	private const string JSON_REQUIREMENT_CUSTOMREQUIREMENTS = "CustomRequirements";

	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/
	
	public void Init () 
	{
		//
	}

	public List<IRequirement> JSON_To_Requirements(JSONNode jsonData, string id)
	{
		List<IRequirement> toReturn = new List<IRequirement>();
		JSONNode requirements = jsonData[JSON_REQUIREMENT_REQUIREMENTS];
		if (requirements != null)
		{
			JSONNode cassRequirements = requirements[JSON_REQUIREMENT_CASSANDRAREQUIREMENTS];
			if (cassRequirements != null)
			{
				for (int i = 0; i < cassRequirements.Count; i++)
				{
					toReturn.Add(JSON_To_Requirement(cassRequirements[i]));
				}
			}
			JSONNode customRequirements = requirements[JSON_REQUIREMENT_CUSTOMREQUIREMENTS];
			if (customRequirements != null)
			{
				for (int i = 0; i < customRequirements.Count; i++)
				{
					toReturn.Add(JSON_To_CustomRequirement(customRequirements[i], id, i));
				}
			}
		}
		return toReturn;
	}

	public IRequirement JSON_To_Requirement(JSONNode json)
	{
		Requirement requirement = new Requirement();
		requirement.type = json[JSON_REQUIREMENT_TYPE];
		requirement.key = json[JSON_REQUIREMENT_KEY];
		requirement.argument = json[JSON_REQUIREMENT_ARGUMENT];
		requirement.value = json[JSON_REQUIREMENT_VALUE].AsInt;
		return requirement;
	}

	public IRequirement JSON_To_CustomRequirement(JSONNode json, string id, int index)
	{
		CustomRequirement requirement = new CustomRequirement();
		if (json[JSON_REQUIREMENT_NAME] != null) requirement.name = json[JSON_REQUIREMENT_NAME];
		if (json[JSON_REQUIREMENT_DESCRIPTION] != null) requirement.description = json[JSON_REQUIREMENT_DESCRIPTION];
		requirement.script = new GameScript(json[JSON_REQUIREMENT_SCRIPT]);
		requirement.script.scriptName = id + "_" + JSON_REQUIREMENT_SCRIPT2 + "_" + index.ToString();
		return requirement;
	}
}