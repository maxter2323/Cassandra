using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenuUI : UIElement 
{

	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/
	[SerializeField] private Button newGameButton;
	[SerializeField] private Button loadGameButton;
	[SerializeField] private Button optionsButton;
	[SerializeField] private Button exitButton;

	/****************************************************************************************/
	/*										METHODS									  		*/
	/****************************************************************************************/

	public override void Init () 
	{
		newGameButton.onClick.AddListener(OnNewGameClick);
		loadGameButton.onClick.AddListener(OnLoadGameClick);
		optionsButton.onClick.AddListener(OnOptionsGameClick);
		exitButton.onClick.AddListener(OnExitClick);
	}

	private void OnNewGameClick()
	{
		NewGameRunner newGame = new NewGameRunner();
		newGame.Init();
	}

	private void OnLoadGameClick()
	{

	}

	private void OnOptionsGameClick()
	{

	}

	private void OnExitClick()
	{
		Application.Quit();
	}

}
