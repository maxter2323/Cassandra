using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class WorldBuilder : IService
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/
	
	//Current Scene
	private WorldScene currentScene;
	private GameObject currentSceneObject;
	private string currentSceneName = "None";
	private const string sceneObjectHolder = "Scene";

	//Loading
	private DataLocator datalocator;
	private UIManager uiManager;
	private GameObject loadingUI;
	private float loadingProgress = 0.0f;

	//Events
	public UnityEvent LevelLoaded;
	public UnityEvent LevelStarted;

	//Player
	private GameObject playerObject;


	/****************************************************************************************/
	/*										SCENE METHODS									*/
	/****************************************************************************************/

	public void Init()
	{
		datalocator = ServiceLocator.GetService<DataLocator>();
		uiManager = ServiceLocator.GetService<UIManager>();
		LevelLoaded = new UnityEvent();
		LevelStarted = new UnityEvent();
	}

	public void LoadScene(string sceneName)
	{
		MakeLoadingUI();
		CoroutineService.RunCoroutine(LoadLevel(sceneName));
	}

	private IEnumerator LoadLevel(string sceneName)
	{
		currentSceneName = sceneName;
		AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
		while (!loading.isDone)
		{
			loadingProgress = loading.progress;
			yield return null;
		}
		loadingProgress = 1.0f;
		currentSceneObject = GameObject.Find(sceneObjectHolder);
		currentSceneObject.SetActive(false);
		currentScene = currentSceneObject.GetComponent<WorldScene>();
		LevelLoaded.Invoke();
	}

	private IEnumerator CompileScripts(string sceneName)
	{
		currentSceneName = sceneName;
		AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
		while (!loading.isDone)
		{
			loadingProgress = loading.progress;
			yield return null;
		}
		loadingProgress = 1.0f;
		currentSceneObject = GameObject.Find(sceneObjectHolder);
		currentSceneObject.SetActive(false);
		currentScene = currentSceneObject.GetComponent<WorldScene>();
		LevelLoaded.Invoke();
	}

	private void MakeLoadingUI()
	{
		Camera.main.gameObject.SetActive(false);
		uiManager.ClearStack();
		loadingUI = uiManager.MakeUI(N.UI.LOADING_UI).gameObject;
		GameObject.DontDestroyOnLoad(loadingUI);
		GameObject.DontDestroyOnLoad(uiManager.eventSystem);
	}

	public void StartLevel()
	{
		currentSceneObject.SetActive(true);
		currentScene.Init();
		LevelStarted.Invoke();
		//InstantiatePlayer();
		//PositionPlayer(0);
	}

	public void ClearScene()
	{
		if (currentScene != null)
		{
			GameObject.Destroy(currentSceneObject);
			currentScene = null;
		}
	}

	/****************************************************************************************/
	/*										PROPERTIES										*/
	/****************************************************************************************/

	public string CurrentSceneName
	{
	 	get { return currentSceneName;} 
	}

	public float LoadingProgress
	{
		get { return loadingProgress;} 
	}

	/****************************************************************************************/
	/*										PLAYER METHODS									*/
	/****************************************************************************************/

	/*
	public void InstantiatePlayer()
	{
		Player player = datalocator.Get<Player>();
		playerObject = GameObject.Instantiate(player.gameObject);
		playerObject.transform.parent = currentSceneObject.transform;
	}

	public void PositionPlayer(SpawnPoint point)
	{
		playerObject.transform.position = point.transform.position + SpawnPoint.spawnHeight;
	}

	public void PositionPlayer(int index)
	{
		SpawnPoint point = currentScene.GetSpawnPoint(index);
		PositionPlayer(point);
	}
	*/
	/****************************************************************************************/
	/*										TILES METHODS									*/
	/****************************************************************************************/

	private int[,] world = new int[,] { 
		{ 1, 1, 1, 1, 1, 1, 1, 1}, 
		{ 1, 1, 1, 1, 1, 1, 1, 1}, 
		{ 1, 1, 1, 1, 1, 1, 1, 1}, 
		{ 1, 1, 1, 1, 1, 1, 1, 1}, 
		{ 1, 1, 1, 1, 1, 1, 1, 1}, 
		{ 1, 1, 1, 1, 1, 1, 1, 1}, 
		{ 1, 2, 1, 1, 1, 1, 1, 1}, 
		{ 1, 1, 1, 1, 1, 1, 1, 1}
	};

	private List<List<Tile>> level = new List<List<Tile>>();
	private int tileWidth = 1;
	private int tileHeight = 1;

	public void MakeTiledLevel()
	{
		int x = 0;
		int z = 0;
		int bound0 = 8;
		int bound1 = 8;
		for (int i = 0; i < bound0; i++)
		{
			List<Tile> row = new List<Tile>();
			for (int j = 0; j < bound1; j++)
			{
				Vector3 pos = new Vector3(x, 0, z);
				Tile newTile = InstantiateTile(world[i, j], pos);
				newTile.x = x;
				newTile.z = z;
				//newTile.SetWorld(this);
				//newTile.SetCoordinates(i, j);
				row.Add(newTile);
				x+=tileWidth;
			}
			x = 0;
			z+=tileWidth;
			level.Add(row);
		}
	}

	private Tile InstantiateTile(int type, Vector3 pos)
	{
		GameObject prefab = null;
		prefab =  ServiceLocator.GetService<DataLocator>().LoadResource("Tile");
		GameObject newObj = (GameObject)GameObject.Instantiate(prefab, pos, Quaternion.identity);
		newObj.transform.SetParent(currentScene.GetStaticTransform());
		NPCFactory npcFactory = ServiceLocator.GetService<NPCFactory>();
		switch (type)
		{
			case 2:
				GameObject ethan = npcFactory.BuildAndInstantiateNPC("Ethan");
				ethan.transform.position = pos;
				ethan.transform.root.eulerAngles = new Vector3(0, -90, 0);
			break;
			case 3:
				npcFactory.BuildAndInstantiateNPC("Crawler").transform.position = pos;
			break;
		}
		return newObj.GetComponent<Tile>();
	}

	public GameObject MakeNPC(int x, int z, string NPCname)
	{
		GameObject newNPC = ServiceLocator.GetService<DataLocator>().InstantiateLoadedResource(NPCname);
		newNPC.transform.position = new Vector3(x, 0, z);
		//level[x][z].occupied = true;
		newNPC.transform.SetParent(currentSceneObject.transform);
		newNPC.gameObject.name = NPCname;
		return newNPC;
	}

}