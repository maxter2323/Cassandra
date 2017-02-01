using UnityEngine;
using CassandraFramework.Items;

public class NewGameRunner
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private WorldBuilder worldBuilder;
	private DataLocator datalocator;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public void Init()
	{
		datalocator = ServiceLocator.GetService<DataLocator>();
		worldBuilder = ServiceLocator.GetService<WorldBuilder>();
		worldBuilder.LoadScene(N.Scene.TILE_SCENE);
		worldBuilder.LevelStarted.AddListener(NewGameStart);
	}

	public void NewGameStart()
	{	
		//CreateGame instance
		GameMono gameHolder = datalocator.InstantiateLoadedResource("GameHolder").GetComponent<GameMono>();
		gameHolder.game = new Game();

		//Instantiate Player
		GameObject playerObject = worldBuilder.MakeNPC(7, 0, "TiledPlayer");
		gameHolder.game.player.SetView(playerObject);

		//Load other objects;
		worldBuilder.MakeTiledLevel();
		FakeLevelEditorLoading();

		//Make UI for the player
		UIManager uiManager = ServiceLocator.GetService<UIManager>();
		uiManager.MakeUI(N.UI.PLAYER_UI);

		//Start updatring the game
		gameHolder.game.updateGame = true;
		gameHolder.game.player.updatePlayer = true;
		worldBuilder.LevelStarted.RemoveListener(NewGameStart);
	}

	public void FakeLevelEditorLoading()
	{
		GameObject campfire = datalocator.InstantiateLoadedResource("Campfire");
		campfire.transform.position = new Vector3(4, 0, 6);
		campfire.transform.name = "Campfire";

		GameObject drawer = datalocator.InstantiateLoadedResource("Drawer");
		drawer.transform.position = new Vector3(7, 0, 6);

		ItemFactory itemFactory = ServiceLocator.GetService<ItemFactory>();
		drawer.GetComponent<Container>().inventory.AddItem(itemFactory.BuildItemScript("Jerrycan"), 1);

		Player.instance.inventory.AddItem(itemFactory.BuildItemScript("Water"), 1);
	}

}