using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using CassandraFramework.Stats;
using CassandraFramework.Items;
using CassandraFramework.Quests;

public class Player 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public static Player instance;

	public Stats stats = new Stats();
	public Inventory inventory = new Inventory();
	public Slots slots = new Slots();
	public Quests quests = new Quests();

	public bool updatePlayer = false;
	public GameObject view;
	public PlayerController controller= new PlayerController();
	public Movement movement = new Movement();

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/

	public Player() 
	{
		instance = this;
		MakeStats();
		MakeSlots();
	}

	public void SetView(GameObject newView)
	{
		view = newView;
		movement.view = newView;
	}


	private void MakeStats()
	{
		stats.AddStat(new Stat("Health", 100, 0, 100, "Stat"));
		stats.AddStat(new Stat("Stamina", 100, 0, 100, "Stat"));

		stats.AddStat(new Stat("Strength", 5, 1, 10, "Attribute"));
		stats.AddStat(new Stat("Charisma", 5, 1, 10, "Attribute"));

		stats.AddStat(new Stat("Guns", 20, 0, 100, "Skill"));
		stats.AddStat(new Stat("Repair", 20, 0, 100, "Skill"));
		stats.AddStat(new Stat("Medicine", 20, 0, 100, "Skill"));

		stats.AddStat(new Stat("Level", 1, 0, 100, "Level"));
		stats.AddStat(new Stat("XP", 0, 0, 100, "Level"));

		stats.AddStat(new Stat("Thirst", 1, 0, 1000, "Need"));
		stats.AddStat(new Stat("Hunger", 1, 0, 1000, "Need"));
		stats.AddStat(new Stat("Deprivation", 1, 0, 1000, "Need"));

		//Localisation Example in Russian
		stats.AddStat(new Stat("Thirst2", "Жажда", 1, 0, 1000, "Need", "Потребность"));
	}

	/*
	public Stats stats = new Stats();
	private void MakeStats()
	{
		//Set Key, Curent & Min & Max values, and Tag in Constructor
		stats.AddStat(new Stat("Health", 100, 0, 100, "Stat"));
		stats.AddStat(new Stat("Stamina", 100, 0, 100, "Stat"));

		stats.AddStat(new Stat("Strength", 5, 1, 10, "Attribute"));
		stats.AddStat(new Stat("Charisma", 5, 1, 10, "Attribute"));

		stats.AddStat(new Stat("Thirst", 1, 0, 1000, "Need"));
		stats.AddStat(new Stat("Hunger", 1, 0, 1000, "Need"));
		stats.AddStat(new Stat("Deprivation", 1, 0, 1000, "Need"));

		//Localisation Example in Russian
		stats.AddStat(new Stat("Thirst2", "Жажда", 1, 0, 1000, "Need2", "Потребность"));

		//Retrieve
		stats.GetStat("Health").Value; //100

		//Remove
		stats.RemoveStat("Deprivation");

		stats.GetStatGroupList("Need"); // Thirst & Hunger, Deprivation was removed
	}
	*/

	private void MakeSlots()
	{
		slots.MakeSlot("Head", typeof(ApparelItem));
		slots.MakeSlot("Body", typeof(ApparelItem));
		slots.MakeSlot("Weapon", typeof(WeaponItem));
	}
	
	public void Update () 
	{	
		if(!updatePlayer) return;
		controller.Update();
		movement.Update();
	}

}
