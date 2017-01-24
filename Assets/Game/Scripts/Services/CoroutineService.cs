using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineService : MonoBehaviour
{
	static private List<IEnumerator> routineStorage = new List<IEnumerator>();
	static private GameObject instance;

	static private void InstantiateSelf()
	{
		GameObject emptyObj = new GameObject("Coroutine Service");
		instance = GameObject.Instantiate(emptyObj);
		instance.AddComponent<CoroutineService>();
		DontDestroyOnLoad(instance);
	}

	static public void RunCoroutine(IEnumerator routine)
	{
		if (instance == null)
		{
			InstantiateSelf();
		}
		routineStorage.Add(routine);
		instance.GetComponent<CoroutineService>().StartCoroutine(routine);
	}

}
