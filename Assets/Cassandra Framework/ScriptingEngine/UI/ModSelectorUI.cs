using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ModSelectorUI : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Core
	private CassandraModBuilder modBuilder;
	private const string SCENE_TO_LOAD = "MainMenu";
	[SerializeField] bool rebuildOnStart;

	//Buttons
	[SerializeField] private Button playButton;
	[SerializeField] private GameObject verticalLayout;
	[SerializeField] private GameObject togglePrefab;
	private List<Toggle> toggles = new List<Toggle>();

	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/
	
	private void Start () 
	{
		modBuilder = ServiceLocator.GetService<CassandraModBuilder>();
		playButton.onClick.AddListener(Play);
		if (rebuildOnStart) modBuilder.BuildAll();
		GetMods();
	}

	private void GetMods()
	{
		List<string> fileNames = modBuilder.GetAllModFiles();
		for (int i = 0; i < fileNames.Count; i++)
		{
			GameObject toogleObject = Instantiate(togglePrefab);
			toogleObject.transform.SetParent(verticalLayout.transform, false);
			Toggle toggle = toogleObject.GetComponent<Toggle>();
			toggle.GetComponentInChildren<Text>().text = fileNames[i];
			toggles.Add(toggle);
		}
	}
	
	private void Play() 
	{
		for (int i = 0; i < toggles.Count; i++)
		{
			if (toggles[i].isOn)
			{
				string fileName = toggles[i].GetComponentInChildren<Text>().text;
				modBuilder.LoadMod(fileName);
			}
		}
		SceneManager.LoadScene(SCENE_TO_LOAD);
	}
}