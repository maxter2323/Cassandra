using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CassandraFramework.Items;


public class CraftUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//UI
	[SerializeField] private GameObject buttonPanel;
	[SerializeField] private Text recipeText;
	[SerializeField] private Button craftButton;

	//Buttons
	private GameObject buttonWithImagePrefab;
	private List<GameObject> craftButtons;
	private Dictionary<GameObject, int> buttonsIndexes;
	public string workbenchtype;
	private List<Recipe> recipes = new List<Recipe>();
	private int craftIndex = 0;
	
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public override void Init () 
	{
		buttonWithImagePrefab = ServiceLocator.GetService<DataLocator>().LoadResource("DialogueButton");
		craftButtons = new List<GameObject>();
		buttonsIndexes = new Dictionary<GameObject, int>();
		OnBecameInVisible.AddListener(Clear);
	}
	
	private void Update () 
	{
		Quit();
	}

	public void GetInfoTiled(GameObject bench)
	{
		workbenchtype = bench.GetComponent<WorkBench>().name;
	}

	public void ShowButtons()
	{
	    Clear();
		recipes = ServiceLocator.GetService<RecipeFactory>().GetRecipesForWorkBench(workbenchtype);
		for (int i = 0; i < recipes.Count; i++)
		{
			GameObject newButton = GameObject.Instantiate(buttonWithImagePrefab);
			newButton.transform.SetParent(buttonPanel.transform, false);
			craftButtons.Add(newButton);
			buttonsIndexes[newButton] = i;
			//Text textScript = newButton.GetComponentInChildren<Text>();
			//textScript.text = itemList[i].itemList[0].name;
			//if (itemList[i].Count() > 1) textScript.text += "(" + itemList[i].Count() + ")";
			//Image itemImage = newButton.transform.FindChild("Image").GetComponent<Image>();
			//itemImage.sprite = dataLocator.LoadSprite(itemList[i].itemList[0].iconName);
			newButton.transform.SetParent(buttonPanel.transform);
			ClickDetector detector = newButton.GetComponent<ClickDetector>();
			detector.LeftClick += SelectCraftRecipe;
			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = recipes[i].name;
		}
		if (craftButtons.Count != 0) SelectCraftRecipe(craftButtons[0]);
	}

	private void SelectCraftRecipe(GameObject button)
	{
		craftIndex = buttonsIndexes[button];
		Recipe rec = recipes[craftIndex];
		recipeText.text = MakeRecipeString(rec);
	}

	private void Quit()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Player.instance.updatePlayer = true;
			ServiceLocator.GetService<UIManager>().MakeUI(N.UI.PLAYER_UI);
			ServiceLocator.GetService<UIManager>().DeleteUI(N.UI.CRAFT_UI);
			return;
		}
	}

	private string MakeRecipeString(Recipe rec)
	{
		bool ableToCraft = true;
		string a = rec.name + "\n";
		string b = rec.description + "\n";
		string c = "Requirements: \n";
		for (int i = 0; i < rec.requirements.Count; i++)
		{
			string itemName = rec.requirements[i].name;
			int requiredCount = rec.requirements[i].count;
			int currentCount = Player.instance.inventory.GetItemCount(itemName);
			if (currentCount < requiredCount) ableToCraft = false;
			c+=itemName + " " + currentCount + "/" + requiredCount + "\n";
		}
		string d = "Result: \n";
		for (int j = 0; j < rec.results.Count; j++)
		{
			string itemName = rec.results[j].name;
			int requiredCount = rec.results[j].count;
			d+=itemName + "(" + requiredCount + ")" + "\n";
		}

		if(ableToCraft) craftButton.gameObject.GetComponent<Image>().color = Color.green;
		if(ableToCraft) craftButton.gameObject.GetComponent<ClickDetector>().LeftClick += CraftItem;
		if(!ableToCraft) craftButton.gameObject.GetComponent<Image>().color = Color.red;
		return a + b + c + d;
	}

	private void CraftItem(GameObject button)
	{
		Recipe rec = recipes[craftIndex];
		ItemFactory itemFactory = ServiceLocator.GetService<ItemFactory>();
		for (int i = 0; i < rec.requirements.Count; i++)
		{
			string itemName = rec.requirements[i].name;
			int requiredCount = rec.requirements[i].count;
			if (rec.requirements[i].use == "use")
			{
				for (int k = 0; k < requiredCount; k++)
				{
					Player.instance.inventory.RemoveItem(itemName);
				}
			}
		}
		for (int j = 0; j < rec.results.Count; j++)
		{
			string itemName = rec.results[j].name;
			int requiredCount = rec.results[j].count;
			for (int k = 0; k < requiredCount; k++)
			{
				Player.instance.inventory.AddItem(itemFactory.BuildItemScript(itemName));
			}
		}
		ShowButtons();
	}

	private void Clear()
	{
		for (int i = 0; i < craftButtons.Count; i++)
		{
			GameObject.Destroy(craftButtons[i]);
		}
		craftButtons.Clear();
		buttonsIndexes.Clear();
		recipeText.text = "";
	}
}
