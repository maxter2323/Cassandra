using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	[SerializeField] private Text progressBar;
	[SerializeField] private Button playButton;
	private WorldBuilder worldBuilder;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public override void Init () 
	{
		worldBuilder = ServiceLocator.GetService<WorldBuilder>();
		worldBuilder.LevelLoaded.AddListener(OnLevelLoaded);
		playButton.gameObject.SetActive(false);
	}
	
	private void Update () 
	{
		progressBar.text = worldBuilder.CurrentSceneName + ": " + (worldBuilder.LoadingProgress * 100).ToString() + "%";
	}

	private void OnLevelLoaded()
	{
		worldBuilder.LevelLoaded.RemoveListener(OnLevelLoaded);
		playButton.gameObject.SetActive(true);
		playButton.onClick.AddListener(DestroyAndStart);
	}

	private void DestroyAndStart()
	{
		ServiceLocator.GetService<UIManager>().RemoveEventSystem();
		Destroy(this.gameObject);
		worldBuilder.StartLevel();
	}
}
