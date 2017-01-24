using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Intro : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private const float introSeconds = 2.0f;
	private UIManager uiManager;

	/****************************************************************************************/
	/*										METHODS									  	    */
	/****************************************************************************************/

	private void Start() 
	{
		uiManager = ServiceLocator.GetService<UIManager>();
		uiManager.MakeUI(N.UI.INTRO_UI);
		StartCoroutine(LoadMenu());
	}
	
	private IEnumerator LoadMenu()
	{
		yield return new WaitForSeconds(introSeconds);
		uiManager.DeleteUI(N.UI.INTRO_UI);
		SceneManager.LoadScene(N.Scene.MAIN_MENU_SCENE);
	}
}
