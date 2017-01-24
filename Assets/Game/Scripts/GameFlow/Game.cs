using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Player player;
	public WorldScene scene;
	public List<NPC> NPCs = new List<NPC>();
	public bool updateGame = false;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public Game() 
	{
		player = new Player();
	}
	
	public void Update () 
	{
		if (updateGame)
		{
			player.Update();
		}
	}

	private void FixedUpdate () 
	{
	
	}
}
