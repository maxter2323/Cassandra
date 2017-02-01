using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using CassandraFramework.Items;
using CassandraFramework.Quests;

public class NotificationUI : UIElement
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	[SerializeField] private GameObject buttonPanel;
	private GameObject notificationPrefab;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public override void Init()
	{
		DataLocator dataLocator = ServiceLocator.GetService<DataLocator>();
		notificationPrefab = dataLocator.LoadResource("Notification");
		SubscribeToNotify();
	}

	/****************************************************************************************/
	/*										CORE METHODS									*/
	/****************************************************************************************/

	private void SubscribeToNotify()
	{
		Player.instance.quests.OnQuestStarted.AddListener(QuestNotification);
		Player.instance.quests.OnQuestFinished.AddListener(QuestEndNotification);
		Player.instance.quests.OnQuestStageStarted.AddListener(QuestStageNotification);
	}

	private void QuestEndNotification(Quest q)
	{
		CreateNotification("Quest " + q.name + " finished", 5.0f);
	}

	private void QuestNotification(Quest q)
	{
		CreateNotification("Quest " + q.name + " started", 5.0f);
	}

	private void QuestStageNotification(Quest q, QuestStage s)
	{
		CreateNotification("(" + q.name + ")" + s.index + " " + s.log, 5.0f);
	}

	private void CreateNotification(string text, float destroyTime)
	{
		GameObject newButton = Instantiate(notificationPrefab);
		newButton.transform.SetParent(buttonPanel.transform, false);
		newButton.GetComponentInChildren<Text>().text = text;
		Destroy(newButton, destroyTime);
	}
}
