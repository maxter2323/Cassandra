using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class SceneSelector : EditorWindow
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Files
    private List<string> fileList = new List<string>();
	private const string FILE_FORMAT = "*.unity";
	private bool started = false;
	//GUI
	private Vector2 scrollPos;
	private int widthAmount = 5;
	private int nativeOffset = 2;
	private int tileHeight = 50;

	/****************************************************************************************/
	/*										 METHODS										*/
	/****************************************************************************************/

    [MenuItem("Window/SceneSelector")]
    public static void ShowWindow()
    {
		EditorWindow.GetWindow(typeof(SceneSelector));
    }

    private void Start()
    {
		if (!started)
		{
			fileList.Clear();
			RecursiveFileSearch(Application.dataPath);
			started = true;
		}
    }

	private void RecursiveFileSearch(string startDir) 
	{
		foreach (string dir in Directory.GetDirectories(startDir)) 
		{
			foreach (string file in Directory.GetFiles(dir, FILE_FORMAT)) 
			{
				fileList.Add(file);
			}
			RecursiveFileSearch(dir);
		}
	}

	private void OpenScene(string sceneName)
	{
		bool go = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
		if (go) EditorSceneManager.OpenScene(sceneName);
	}

    private void OnGUI()
    {
		Start();
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Width (1000), GUILayout.Height (1000));
		int totalWidth = 0;
		int unitWidth = (int)(position.width/widthAmount) - nativeOffset;
		EditorGUILayout.BeginVertical();
		EditorGUILayout.BeginHorizontal();
		for (int i = 0; i < fileList.Count; i++)
		{
			string displayString = Path.GetFileNameWithoutExtension(fileList[i]);
			if(GUILayout.Button(displayString, GUILayout.Width (unitWidth), GUILayout.Height (tileHeight)))
			{
				OpenScene(fileList[i]);
			}
			totalWidth += (unitWidth + nativeOffset);
			if (totalWidth >= position.width)
			{
				totalWidth = 0;
				EditorGUILayout.EndHorizontal();
				EditorGUILayout.BeginHorizontal();
			}
		}
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
    }
}