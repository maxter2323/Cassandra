using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldScene : MonoBehaviour 
{

	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//private List<SpawnPoint> spawnPoints = new List<SpawnPoint>();

	/****************************************************************************************/
	/*										METHODS									  		*/
	/****************************************************************************************/
		
	public void Init()
	{
		//GetSpawnPoints();
	}

	public void Start()
	{

	}

	public void Awake()
	{

	}

	public void Update()
	{
		//Debug.Log("Scene Update");
	}

	public Transform GetDynamicTransform()
	{
		return transform.FindChild("Dynamic");
	}

	public Transform GetStaticTransform()
	{
		return transform.FindChild("Static");
	}

	private void GetSpawnPoints()
	{
		GameObject spawnpointHolder = GameObject.Find("SpawnPoints");
		foreach (Transform child in spawnpointHolder.transform)
		{
			//SpawnPoint point = child.gameObject.GetComponent<SpawnPoint>();
			//spawnPoints.Add(point);
		}
	}

	//public SpawnPoint GetSpawnPoint(int index)
	//{
	//	return spawnPoints[index];
	//}

}
