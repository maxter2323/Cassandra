using UnityEngine;
using System.Collections;

public class N 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//Scenes
	public class Scene
	{
		public const string INTRO_SCENE = "Intro";
		public const string MAIN_MENU_SCENE = "MainMenu";
		public const string PLAYGROUND_SCENE = "Playground";
		public const string TILE_SCENE = "TileScene";
	}

	//UI
	public class UI
	{
		public class Tiled
		{
			public const string PLAYER_UI = "TiledPlayerUI";
		}

		public const string INTRO_UI = "IntroUI";
		public const string MAIN_MENU_UI = "MainMenuUI";
		public const string LOADING_UI = "LoadingUI";
		public const string PLAYER_UI = "PlayerUI";
		public const string INVENTORY_UI = "InventoryUI";
		public const string DOUBLE_INVENTORY_UI = "DoubleInventoryUI";
		public const string DIALOGUE_UI = "DialogueUI";
		public const string CRAFT_UI = "CraftUI";
		public const string QUEST_UI = "QuestUI";
	}
}
