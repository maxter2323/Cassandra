using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CassandraFramework.Stats;

public class StatsUI : UIElement
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//UI
	[SerializeField] private GameObject uiPanel;
	private GameObject buttonPrefab;
	private List<GameObject> statsButtons = new List<GameObject>();
	private Dictionary<GameObject, int> buttonsIndexes = new Dictionary<GameObject, int>();
	                                                                                       
	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public override void Init()
	{
		buttonPrefab = ServiceLocator.GetService<DataLocator>().LoadResource("DialogueButton");
		OnBecameVisible.AddListener(ShowButtons);
		OnBecameInVisible.AddListener(Clear);
		ShowButtons();
	}

	private void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Player.instance.updatePlayer = true;
			ServiceLocator.GetService<UIManager>().MakeUI(N.UI.PLAYER_UI);
			ServiceLocator.GetService<UIManager>().DeleteUI(N.UI.STATS_UI);
			return;
		}
	}

	public void ShowButtons()
	{
		Clear();
		List<Stat> stats = Player.instance.stats.GetAllStatsList();
		for (int i = 0; i < stats.Count; i++)
		{
			GameObject newButton = Instantiate(buttonPrefab);
			newButton.transform.SetParent(uiPanel.transform, false);
			statsButtons.Add(newButton);
			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = stats[i].Name + " (" + stats[i].Tag + ") " + stats[i].Value;
		}
	}

	private void Clear()
	{
		for (int i = 0; i < statsButtons.Count; i++)
		{
			Destroy(statsButtons[i]);
		}
		statsButtons.Clear();
	}
}