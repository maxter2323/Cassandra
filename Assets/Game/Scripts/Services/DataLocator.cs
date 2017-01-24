using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DataLocator : IService
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private static Dictionary<Type, GameObject> dataList;

	/****************************************************************************************/
	/*										METHODS									  		*/
	/****************************************************************************************/

	public void Init()
	{
		dataList = new Dictionary<Type,GameObject>();
	}
	
	public T Get<T>(string path = null)
	{
		Type objectType = typeof(T);
		if (!dataList.ContainsKey(objectType))
		{
			MakeData(objectType, path); 
		}
		return dataList[objectType].GetComponent<T>();;
	}

	private void MakeData(Type objectType, string path = null)
	{
		string filename = objectType.ToString();
		if (path != null){
			filename = path + "/" + filename;
		}
		GameObject theObject = LoadResource(filename);
		dataList.Add(objectType, theObject);
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