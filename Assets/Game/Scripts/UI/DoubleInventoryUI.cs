using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CassandraFramework.Items;

public class DoubleInventoryUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//UI
	[SerializeField] private GameObject itemLeftPanel;
	[SerializeField] private GameObject itemRightPanel;

	//Prefabs
	private DataLocator dataLocator;
	private GameObject buttonWithImagePrefab;
	private List<GameObject> inventoryButtonsLeft = new List<GameObject>();
	private Dictionary<GameObject, int> buttonsIndexesLeft = new Dictionary<GameObject, int>();
	private List<GameObject> inventoryButtonsRight = new List<GameObject>();
	private Dictionary<GameObject, int> buttonsIndexesRight = new Dictionary<GameObject, int>();

	//Core
	public Inventory inventoryLeft;
	public Inventory inventoryRight;
	private List<ItemGroup> itemListLeft = new List<ItemGroup>();
	private List<ItemGroup> itemListRight = new List<ItemGroup>();

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public override void Init () 
	{
		inventoryLeft = Player.instance.inventory;
		dataLocator = ServiceLocator.GetService<DataLocator>();
		buttonWithImagePrefab = dataLocator.LoadResource("ButtonWithImage");
		OnBecameVisible.AddListener(ShowInventory);
		OnBecameInVisible.AddListener(Clear);
		//ShowInventory();
	}

	private void Update() 
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Player.instance.updatePlayer = true;
			ServiceLocator.GetService<UIManager>().MakeUI(N.UI.PLAYER_UI);
			ServiceLocator.GetService<UIManager>().DeleteUI(N.UI.DOUBLE_INVENTORY_UI);
			return;
		}
	}

	/****************************************************************************************/
	/*										CORE METHODS									*/
	/****************************************************************************************/

	public void ShowInventory()
	{
		Clear();
		ShowItemsOnPanel(inventoryLeft.GetItemList(), itemLeftPanel, inventoryButtonsLeft, buttonsIndexesLeft);
		ShowItemsOnPanel(inventoryRight.GetItemList(), itemRightPanel, inventoryButtonsRight, buttonsIndexesRight);
	}

	public void ShowItemsOnPanel(List<ItemGroup> itemList, GameObject parent, List<GameObject> buttons, Dictionary<GameObject, int> indexes )
	{
		if (parent == itemLeftPanel)
		{
			itemListLeft = itemList;
		}
		else
		{
			itemListRight = itemList;
		}	
		for (int i = 0; i < itemList.Count; i++)
		{
			GameObject newButton = GameObject.Instantiate(buttonWithImagePrefab);
			newButton.transform.SetParent(parent.transform, false);
			buttons.Add(newButton.GetComponentInChildren<Button>().gameObject.transform.parent.gameObject);
			indexes[newButton.GetComponentInChildren<Button>().gameObject] = i;
			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = itemList[i].itemList[0].name;
			if (itemList[i].Count() > 1) textScript.text +="(" + itemList[i].Count() + ")";
			Image itemImage = newButton.transform.FindChild("Image").GetComponent<Image>();
			itemImage.sprite = dataLocator.LoadSprite(itemList[i].itemList[0].iconName);
			ClickDetector detector = newButton.GetComponentInChildren<ClickDetector>();
			detector.LeftClick += MoveItem;
		}
	}

	private void MoveItem(GameObject button)
	{	
		bool left = false;
		int index;
		if (buttonsIndexesLeft.ContainsKey(button))
		{
			left = true;
		}
		if (left)
		{
			index = buttonsIndexesLeft[button];
			ItemGroup itemGroup = itemListLeft[index];
			Item item = itemGroup.GetAt(0);
			inventoryRight.AddItem(item, itemGroup.Count());
			inventoryLeft.RemoveItem(item);
		}
		else
		{
			index = buttonsIndexesRight[button];
			ItemGroup itemGroup = itemListRight[index];
			Item item = itemGroup.GetAt(0);
			inventoryLeft.AddItem(item, itemGroup.Count());
			inventoryRight.RemoveItem(item);
		}	
		ShowInventory();
	}

	private void Clear()
	{
		for (int i = 0; i < inventoryButtonsLeft.Count; i++)
		{
			GameObject.Destroy(inventoryButtonsLeft[i]);
		}
		for (int i = 0; i < inventoryButtonsRight.Count; i++)
		{
			GameObject.Destroy(inventoryButtonsRight[i]);
		}
		inventoryButtonsLeft.Clear();
		inventoryButtonsRight.Clear();
		itemListLeft.Clear();
		itemListRight.Clear();
		buttonsIndexesLeft.Clear();
		buttonsIndexesRight.Clear();
	}

}
