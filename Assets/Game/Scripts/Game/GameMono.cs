﻿using UnityEngine;

public class GameMono : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Game game;
	
	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/
	
	private void Start () 
	{
		DontDestroyOnLoad(this.gameObject);
	}

	private void Update()
	{
		game.Update();
	}

}