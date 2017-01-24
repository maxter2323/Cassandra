using UnityEngine;
using System.Collections;
using CassandraFramework.Quests;

public class PlayerController
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private UIManager uiManager;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public PlayerController()
	{
		uiManager = ServiceLocator.GetService<UIManager>();
	}

	public void Update () 
	{
		InventoryAccess();
		QuestAccess();
	}

	private void InventoryAccess()
	{
		if (Input.GetKeyUp(KeycodeService.InventoryAccess()))
		{
			if (uiManager.HasUI(N.UI.INVENTORY_UI))
			{
				uiManager.MakeUI(N.UI.Tiled.PLAYER_UI);
				uiManager.DeleteUI(N.UI.INVENTORY_UI);
				//Player.instance.updatePlayer = true;
			}
			else
			{
				uiManager.DeleteUI(N.UI.Tiled.PLAYER_UI);
				uiManager.MakeUI(N.UI.INVENTORY_UI);
				//Player.instance.updatePlayer = false;
			}
		}
	}

	private void QuestAccess()
	{
		if (Input.GetKeyUp(KeyCode.Q))
		{
			Quest q = Player.instance.quests.currentQuest;
			if (q == null)
			{
				Debug.Log("No current quest");
				return;
			}
			Debug.Log(q.name + ": " + q.currentStageIndex + ". " + q.currentStage.log);
		}
	}
}
