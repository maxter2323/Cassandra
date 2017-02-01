using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using CassandraFramework.Dialogues;

public class DialogueUI : UIElement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//UI
	[SerializeField] private GameObject answersPanel;
	[SerializeField] private GameObject replyText;
	private GameObject buttonPrefab;
	private List<GameObject> dialogueButtons;
	private Dictionary<GameObject, int> buttonsIndexes;

	//Core
	private Dialogue dialogue;
	private int currentNodeIndex = 0;


	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public override void Init() 
	{
		buttonPrefab = ServiceLocator.GetService<DataLocator>().LoadResource("DialogueButton");
		dialogueButtons = new List<GameObject>();
		buttonsIndexes = new Dictionary<GameObject, int>();
		OnBecameVisible.AddListener(ShowButtons);
		OnBecameInVisible.AddListener(Clear);
	}

	public void InitDialogueTiled(GameObject npc)
	{
		dialogue = ((NPC)npc.GetComponent<CharacterMono>().character).dialogue;
		SetReplyText(dialogue.greetings);
		ShowButtons();
	}

	public void ShowButtons()
	{
		Clear();
		List<DialogueOption> options = dialogue.GetOptionsReady();
		for (int i = 0; i < options.Count; i++)
		{	
			GameObject newButton = GameObject.Instantiate(buttonPrefab);
			ClickDetector detector = newButton.GetComponent<ClickDetector>();
			detector.LeftClick += SelectDialogueOption;
			dialogueButtons.Add(newButton);
			buttonsIndexes[newButton] = options[i].index;
			newButton.transform.SetParent(answersPanel.transform, false);
			Text textScript = newButton.GetComponentInChildren<Text>();
			textScript.text = (i+1).ToString() + ". " + options[i].text;
			//newButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
		}
	}
	
	private void SelectDialogueOption(GameObject button)
	{
		int index = buttonsIndexes[button];
		DialogueOption option = dialogue.nodes[dialogue.currentNode].options[index];
		int gotoIndex = option.gotoIndex;
		SetReplyText(option.reply);
		dialogue.SelectOption(option);
		if (gotoIndex == -1)
		{
			Player.instance.updatePlayer = true;
			UIManager uiService = ServiceLocator.GetService<UIManager>();
			uiService.MakeUI(N.UI.PLAYER_UI);
			uiService.DeleteUI(N.UI.DIALOGUE_UI);
			return;
		}
		ShowButtons();
	}

	public void SetReplyText(string reply)
	{
		replyText.GetComponent<Text>().text = reply;
	}

	private void Clear()
	{
		for (int i = 0; i < dialogueButtons.Count; i++)
		{
			GameObject.Destroy(dialogueButtons[i]);
		}
		dialogueButtons.Clear();
		buttonsIndexes.Clear();
	}
}
