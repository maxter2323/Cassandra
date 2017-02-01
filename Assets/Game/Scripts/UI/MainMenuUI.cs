using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/
	[SerializeField] private Button newGameButton;
	[SerializeField] private Button exitButton;

	/****************************************************************************************/
	/*										METHODS									  		*/
	/****************************************************************************************/

	public override void Init () 
	{
		newGameButton.onClick.AddListener(OnNewGameClick);
		exitButton.onClick.AddListener(OnExitClick);
	}

	private void OnNewGameClick()
	{
		NewGameRunner newGame = new NewGameRunner();
		newGame.Init();
	}

	private void OnExitClick()
	{
		Application.Quit();
	}

}