using UnityEngine;
using CassandraFramework.Quests;

public class PlayerController
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private UIManager uiManager;
	
	/****************************************************************************************/
	/*										CONTROLLER METHODS								*/
	/****************************************************************************************/

	public PlayerController()
	{
		uiManager = ServiceLocator.GetService<UIManager>();
	}

	public void Update () 
	{
		InventoryAccess();
		QuestAccess();
		StatsAccess();
	}

	private void InventoryAccess()
	{
		if (Input.GetKeyUp(KeycodeService.InventoryAccess()))
		{
			if (uiManager.HasUI(N.UI.INVENTORY_UI))
			{
				ShowPlayerHideCurrent(N.UI.INVENTORY_UI);
			}
			else
			{
				HidePlayerShowCurrent(N.UI.INVENTORY_UI);
			}
		}
	}

	private void StatsAccess()
	{
		if (Input.GetKeyUp(KeycodeService.StatsAccess()))
		{
			if (uiManager.HasUI(N.UI.STATS_UI))
			{
				ShowPlayerHideCurrent(N.UI.STATS_UI);
			}
			else
			{
				HidePlayerShowCurrent(N.UI.STATS_UI);
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
				return;
			}
			Debug.Log(q.name + ": " + q.currentStageIndex + ". " + q.currentStage.log);
		}
	}

	/****************************************************************************************/
	/*										GENERAL METHODS									*/
	/****************************************************************************************/

	private void ShowPlayerHideCurrent(string uiToHide)
	{
		uiManager.DeleteUI(uiToHide);
		uiManager.MakeUI(N.UI.PLAYER_UI);
		//Player.instance.updatePlayer = true;
	}

	private void HidePlayerShowCurrent(string uiToShow)
	{
		uiManager.DeleteUI(N.UI.PLAYER_UI);
		uiManager.MakeUI(uiToShow);
		//Player.instance.updatePlayer = false;
	}
}
