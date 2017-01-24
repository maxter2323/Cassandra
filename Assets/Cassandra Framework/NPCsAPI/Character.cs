using UnityEngine;
using System.Collections;
using CassandraFramework.Stats;
using CassandraFramework.Items;
using System;

[Serializable]
public class Character 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Stats stats = new Stats();
	public Inventory inventory = new Inventory();
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	private void Start () 
	{
	
	}
	
	private void Update () 
	{
	
	}
}
