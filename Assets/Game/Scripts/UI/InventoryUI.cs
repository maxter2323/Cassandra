using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CassandraFramework.Items;

public class InventoryUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//UI
	[SerializeField] private GameObject buttonPanel;
	[SerializeField] private GameObject itemViewPanel;
	[SerializeField] private Text itemText;
	[SerializeField] private Text descriptionText;
	[SerializeField] private Image itemIcon;

	//Prefabs
	private DataLocator dataLocator;
	private GameObject buttonWithImagePrefab;
	private GameObject basicButtonPrefab;
	private List<GameObject> inventoryButtons = new List<GameObject>();
	private Dictionary<GameObject, int> buttonsIndexes = new Dictionary<GameObject, int>();

	//Core
	public Inventory inventory;
	private List<ItemGroup> itemList = new List<ItemGroup>();

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public override void Init () 
	{
		inventory = Player.instance.inventory;
		dataLocator = ServiceLocator.GetService<DataLocator>();
		basicButtonPrefab = dataLocator.LoadResource("BasicButton");
		buttonWithImagePrefab = dataLocator.LoadResource("ButtonWithImage");
		OnBecameVisible.AddListener(ShowInventory);
		OnBecameInVisible.AddListener(Clear);
		ShowInventory();
		//SetVisible(false);
	}

	public void Start()
	{	
		//Used for debug;
		/*
		inventory = new Inventory();
		ItemFactory itemFactory = ServiceLocator.GetService<ItemFactory>();
		inventory.AddItem(itemFactory.BuildItemScript("Water"), 5);
		inventory.AddItem(itemFactory.BuildItemScript("Jerry"), 1);
		inventory.AddItem(itemFactory.BuildItemScript("Pistol"), 1);
		inventory.AddItem(itemFactory.BuildItemScript("Medkit"), 1);
		Init();
		ShowInventory();
		*/
	}

	private void Update() 
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			//SetVisible(false);
			//Player.instance.updatePlayer = true;
			return;
		}
	}

	/****************************************************************************************/
	/*										CORE METHODS									*/
	/****************************************************************************************/

	private void ShowInventory()
	{
		Debug.Log("Show");
		Clear();
		inventory = Player.instance.inventory;
		itemList = inventory.GetItemList();
		for (int i = 0; i < itemList.Count; i++)
		{
			GameObject newButton = GameObject.Instantiate(buttonWithImagePrefab);
			newButton.transform.SetParent(buttonPanel.transform, false);
			//newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
			inventoryButtons.Add(newButton.GetComponentInChildren<Button>().gameObject);
			buttonsIndexes[newButton.GetComponentInChildren<Button>().gameObject] = i;
			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = itemList[i].itemList[0].name;
			if (itemList[i].Count() > 1) textScript.text +="(" + itemList[i].Count() + ")";
			Image itemImage = newButton.transform.FindChild("Image").GetComponent<Image>();
			itemImage.sprite = dataLocator.LoadSprite(itemList[i].itemList[0].iconName);
			ClickDetector detector = newButton.GetComponentInChildren<ClickDetector>();
			detector.LeftClick += ShowContextMenu;
		}
	}

	private void ShowContextMenu(GameObject button)
	{	
		foreach (Transform child in itemViewPanel.transform) 
		{
			Destroy(child.gameObject);
		}
		int index = buttonsIndexes[button];
		ItemGroup itemGroup = itemList[index];
		Item item = itemGroup.GetAt(0);
		itemText.text = item.name;
		descriptionText.text = item.description;
		itemIcon.sprite = button.transform.parent.transform.FindChild("Image").GetComponent<Image>().sprite;
		MakeContextAccordingToType(item, index);
	}

	public void GetItemToMe(GameObject button)
	{
		int index = buttonsIndexes[button];
		ItemGroup itemGroup = itemList[index];
		Item item = itemGroup.GetAt(0);
		Player.instance.inventory.AddItem(item, itemGroup.Count());
		inventory.RemoveItem(item);
		ShowInventory();
	}

	private void MakeContextAccordingToType(Item script, int index)
	{
		if (script is WeaponItem)
		{
			GameObject newButton = GameObject.Instantiate(basicButtonPrefab);
			newButton.transform.SetParent(itemViewPanel.transform);
			buttonsIndexes[newButton] = index;

			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = "Equip";
		}
		if (script is ConsumableItem)
		{
			GameObject newButton = GameObject.Instantiate(basicButtonPrefab);
			newButton.transform.SetParent(itemViewPanel.transform);
			buttonsIndexes[newButton] = index;

			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = "Use";
			ClickDetector detector = newButton.GetComponent<ClickDetector>();
			detector.LeftClick += UseItemFromUI;
		}
	}

	private void UseItemFromUI(GameObject button)
	{
		int index = buttonsIndexes[button];
		Item item = itemList[index].itemList[0];
		item.Use();
		inventory.RemoveItem(item);
		ShowInventory();
	}

	private void Clear()
	{
		for (int i = 0; i < inventoryButtons.Count; i++)
		{
			GameObject.Destroy(inventoryButtons[i]);
		}
		inventoryButtons.Clear();
		itemList.Clear();
		buttonsIndexes.Clear();
	}

}
