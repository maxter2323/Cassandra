using UnityEngine;

public class MainMenu : MonoBehaviour 
{
	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/
	
	private void Start () 
	{
		UIManager uiManager = ServiceLocator.GetService<UIManager>();
		uiManager.MakeUI(N.UI.MAIN_MENU_UI);
	}

}