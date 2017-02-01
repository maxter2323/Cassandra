using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using System;

public class RecipeFactory : IService, IFactory 
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

	private void GetRecipeListFromJson()
	{
		JSONNode recipesNode = jsonParser.GetRecipesNode();
		for (int i = 0; i < recipesNode.Count; i++)
		{
			JSONNode jsonNode = recipesNode[i];
			Recipe newRecipe = new Recipe();
			newRecipe.name = jsonNode["Name"];
			newRecipe.description = jsonNode["Description"];
			newRecipe.workbench = jsonNode["Workbench"];
			for (int q = 0; q < jsonNode["Requirements"].Count; q++)
			{
				Recipe.Requirement rec;
				rec.name = jsonNode["Requirements"][q]["name"];
				rec.count = jsonNode["Requirements"][q]["count"].AsInt;
				rec.use = jsonNode["Requirements"][q]["use"];
				newRecipe.requirements.Add(rec);
			}
			for (int k = 0; k < jsonNode["Results"].Count; k++)
			{
				Recipe.Result res;
				res.name = jsonNode["Results"][k]["name"];
				res.count = jsonNode["Results"][k]["count"].AsInt;
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
		return recipes[workbench];
	}

	public List<IGameScriptable> MakeAll()
	{
		return new List<IGameScriptable>();
	}

	public void Add(object toAdd)
	{
		//
	}
}
