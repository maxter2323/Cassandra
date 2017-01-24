using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class ModSelectorUI : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Core
	private CassandraModBuilder modBuilder;
	private const string SCENE_TO_LOAD = "MainMenu";
	[SerializeField] bool RebuildOnStart;
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
		if (RebuildOnStart) modBuilder.BuildAll();
		GetMods();
	}

	private void GetMods()
	{
		toggles.Clear();
		DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
		FileInfo[] fileInfo = dir.GetFiles("*.*");
		foreach (FileInfo file in fileInfo) 
		{
			if (!(file.Extension == CassandraModBuilder.CASSANDRA_FILE_FORMAT)) continue;
			GameObject newToggle = Instantiate(togglePrefab);
			newToggle.transform.SetParent(verticalLayout.transform, false);
			Toggle toggle = newToggle.GetComponent<Toggle>();
			toggle.GetComponentInChildren<Text>().text = file.Name;
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
