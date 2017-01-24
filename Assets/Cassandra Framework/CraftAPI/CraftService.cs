using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class CraftService : IService 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private Dictionary<string, List<Recipe>> recipes = new Dictionary<string, List<Recipe>>();
	private JsonParser jsonParser;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void Init()
	{
		jsonParser = ServiceLocator.GetService<JsonParser>();
		GetRecipeListFromJson();
	}

	private void Start () 
	{
	
	}
	
	private void Update () 
	{
	
	}

	public void GetRecipeList()
	{

	}

	private void GetRecipeListFromJson()
	{
		JSONNode recipesNode = jsonParser.GetRecipesNode();
		for (int i = 0; i < recipesNode.Count; i++)
		{
			JSONNode jsonNode = recipesNode[i];
			Recipe newRecipe = new Recipe();
			Debug.Log(jsonNode["workbench"]);
			newRecipe.name = jsonNode["name"];
			newRecipe.description = jsonNode["description"];
			newRecipe.workbench = jsonNode["workbench"];
			for (int q = 0; q < jsonNode["requirements"].Count; q++)
			{
				Recipe.Requirement rec;
				rec.name = jsonNode["requirements"][q]["name"];
				rec.count = jsonNode["requirements"][q]["count"].AsInt;
				rec.use = jsonNode["requirements"][q]["use"];
				newRecipe.requirements.Add(rec);
			}
			for (int k = 0; k < jsonNode["results"].Count; k++)
			{
				Recipe.Result res;
				res.name = jsonNode["results"][k]["name"];
				res.count = jsonNode["results"][k]["count"].AsInt;
				newRecipe.results.Add(res);
			}
			if (!recipes.ContainsKey(newRecipe.workbench))
			{
				recipes[newRecipe.workbench] = new List<Recipe>();
			}
			recipes[newRecipe.workbench].Add(newRecipe);
		}
	}

	public List<Recipe> GetRecipesForWorkBench(string workbench)
	{
		Debug.Log(workbench);
		return recipes[workbench];
	}
}
