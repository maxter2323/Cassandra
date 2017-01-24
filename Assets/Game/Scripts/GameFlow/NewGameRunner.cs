using UnityEngine;
using System.Collections;
using CassandraFramework.Items;

public class NewGameRunner
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private const string START_QUEST_NAME = "Tutorial";
	private WorldBuilder worldBuilder;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void Init()
	{
		worldBuilder = ServiceLocator.GetService<WorldBuilder>();
		worldBuilder.LoadScene(N.Scene.TILE_SCENE);
		worldBuilder.LevelStarted.AddListener(NewGameStart);
	}

	public void NewGameStart()
	{	
		DataLocator datalocator = ServiceLocator.GetService<DataLocator>();
		GameMono gameHolder = datalocator.InstantiateLoadedResource("GameHolder").GetComponent<GameMono>();
		gameHolder.game = new Game();
		Player.instance.SetView(worldBuilder.MakeNPC(7, 0, "TiledPlayer"));

		worldBuilder.MakeTiledLevel();

		GameObject drawer = ServiceLocator.GetService<DataLocator>().InstantiateLoadedResource("Drawer");
		drawer.transform.position = new Vector3(7, 0, 6);

		ItemFactory itemFactory = ServiceLocator.GetService<ItemFactory>();
		drawer.GetComponent<Container>().inventory.AddItem(itemFactory.BuildItemScript("Jerry"), 1); 

		GameObject campfire = ServiceLocator.GetService<DataLocator>().InstantiateLoadedResource("Campfire");
		campfire.transform.position = new Vector3(2, 0, 6);
		campfire.transform.name = "Campfire";

		UIManager uiManager = ServiceLocator.GetService<UIManager>();
		uiManager.MakeUI(N.UI.Tiled.PLAYER_UI);

		gameHolder.game.updateGame = true;
		Player.instance.updatePlayer = true;
		worldBuilder.LevelStarted.RemoveListener(NewGameStart);

	}

}
