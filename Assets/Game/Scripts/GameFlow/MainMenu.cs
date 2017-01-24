using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	private void Start () 
	{
		UIManager uiManager = ServiceLocator.GetService<UIManager>();
		uiManager.MakeUI(N.UI.MAIN_MENU_UI);
	}

}
