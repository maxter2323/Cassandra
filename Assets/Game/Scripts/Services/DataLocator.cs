using UnityEngine;

public class DataLocator : IService
{

	/****************************************************************************************/
	/*										METHODS									  		*/
	/****************************************************************************************/

	public void Init()
	{
		//
	}

	public GameObject LoadResource(string resourceName)
	{
		return Resources.Load(resourceName) as GameObject;
	}

	public Sprite LoadSprite(string resourceName)
	{
		string formattedName = resourceName.Split('.')[0];
		return Resources.Load<Sprite>(formattedName);
	}

	public GameObject InstantiateLoadedResource(string resourceName)
	{
		return GameObject.Instantiate(LoadResource(resourceName));
	}

}