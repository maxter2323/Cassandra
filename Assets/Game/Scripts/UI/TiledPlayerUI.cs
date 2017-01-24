using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TiledPlayerUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public Text infoText;
	private GameObject selectedTile;
	private GameObject lastNPC;
	private GameObject lastBench;
	private GameObject lastContainer;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void Init () 
	{
		
	}
	
	private void Update () 
	{
		Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		RaycastHit hit;
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
	
		if (Physics.Raycast(ray, out hit, 100)) 
		{
			if (hit.transform.gameObject.tag == "Tile")
			{
				if (selectedTile != hit.transform.gameObject)
				{
					if (selectedTile != null) selectedTile.GetComponent<Renderer>().material.color = Color.white;
					selectedTile = hit.transform.gameObject;
					selectedTile.GetComponent<Renderer>().material.color = Color.red;

					Tile tile = selectedTile.GetComponent<Tile>();
					infoText.text = "X: " + tile.x + " Y: " + tile.z;
				}

				if(Input.GetMouseButtonUp(0))
				{
					Tile tile = selectedTile.GetComponent<Tile>();
					Player.instance.movement.distanceToObject = 0.1f;
					Player.instance.movement.MoveTo(tile.x, tile.z);
				}
			}
			else
			{
				infoText.text = "";
				if (selectedTile != null) selectedTile.GetComponent<Renderer>().material.color = Color.white;
			}

			if (hit.transform.gameObject.tag == "NPC")
			{
				infoText.text = "Talk to ";
				if(Input.GetMouseButtonUp(0))
				{
					lastNPC = hit.transform.gameObject;
					Player.instance.movement.distanceToObject = 1.0f;
					Player.instance.movement.MoveTo((int)hit.transform.position.x, (int)hit.transform.position.z);
					Player.instance.movement.TargetReached.AddListener(InteractWithCharacter);
				}
			}

			if (hit.transform.gameObject.tag == "Container")
			{
				infoText.text = "Open";
				if(Input.GetMouseButtonUp(0))
				{
					lastContainer = hit.transform.gameObject;
					Player.instance.movement.distanceToObject = 1.0f;
					Player.instance.movement.MoveTo((int)hit.transform.position.x, (int)hit.transform.position.z);
					Player.instance.movement.TargetReached.AddListener(InteractWithContainer);
				}
			}

			if (hit.transform.gameObject.tag == "WorkBench")
			{
				infoText.text = "Craft";
				if(Input.GetMouseButtonUp(0))
				{
					lastBench = hit.transform.gameObject;
					Player.instance.movement.distanceToObject = 1.0f;
					Player.instance.movement.MoveTo((int)hit.transform.position.x, (int)hit.transform.position.z);
					Player.instance.movement.TargetReached.AddListener(InteractWithWorkbench);
				}
			}

		}
			
		infoText.rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
	}

	private void InteractWithCharacter()
	{
		UIManager uiService = ServiceLocator.GetService<UIManager>();
		DialogueUI dUI = (DialogueUI)uiService.MakeUI(N.UI.DIALOGUE_UI);
		uiService.DeleteUI(N.UI.Tiled.PLAYER_UI);
		dUI.InitDialogueTiled(lastNPC.transform.parent.gameObject);
		Player.instance.updatePlayer = false;
	}

	private void InteractWithWorkbench()
	{
		UIManager uiService = ServiceLocator.GetService<UIManager>();
		uiService.MakeUI(N.UI.CRAFT_UI).SetVisible(true);
		CraftUI cUI = (CraftUI)uiService.MakeUI(N.UI.CRAFT_UI);
		cUI.GetInfoTiled(lastBench.transform.parent.gameObject);
		cUI.ShowButtons();
	}

	private void InteractWithContainer()
	{
		UIManager uiService = ServiceLocator.GetService<UIManager>();
		DoubleInventoryUI iUI = (DoubleInventoryUI)uiService.MakeUI(N.UI.DOUBLE_INVENTORY_UI);
		iUI.inventoryLeft = Player.instance.inventory;
		iUI.inventoryRight = lastContainer.GetComponent<Container>().inventory;
		iUI.ShowInventory();
		uiService.DeleteUI(N.UI.Tiled.PLAYER_UI);
		Player.instance.updatePlayer = false;
	}

	public void OnDestroy()
	{
		infoText.text = "";
		if (selectedTile != null) selectedTile.GetComponent<Renderer>().material.color = Color.white;
	}

				
}
