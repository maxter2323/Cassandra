using UnityEngine;

public class KeycodeService : IService
{

	public void Init()
	{
		//	
	}

	static public KeyCode InventoryAccess()
	{
		return KeyCode.I;
	}

	static public KeyCode StatsAccess()
	{
		return KeyCode.U;
	}
}