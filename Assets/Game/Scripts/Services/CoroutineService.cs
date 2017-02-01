using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CoroutineService : MonoBehaviour
{

	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	static private List<IEnumerator> routineStorage = new List<IEnumerator>();
	static private GameObject instance;

	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/

	static private void InstantiateSelf()
	{
		GameObject emptyObj = new GameObject("Coroutine Service");
		instance = Instantiate(emptyObj);
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
