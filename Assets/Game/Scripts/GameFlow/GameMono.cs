using UnityEngine;
using System.Collections;

public class GameMono : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Game game;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
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
