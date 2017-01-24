using UnityEngine;
using System.Collections;

public class KeycodeService : IService
{

	public void Init()
	{
		//LoadKeys	
	}

	static public KeyCode MoveForward()
	{
		return KeyCode.W;
	}

	static public KeyCode MoveBack()
	{
		return KeyCode.S;
	}

	static public KeyCode MoveRight()
	{
		return KeyCode.D;
	}

	static public KeyCode MoveLeft()
	{
		return KeyCode.A;
	}

	static public KeyCode Jump()
	{
		return KeyCode.Space;
	}

	static public KeyCode Sprint()
	{
		return KeyCode.LeftShift;
	}

	static public KeyCode Interact()
	{
		return KeyCode.E;
	}

	static public KeyCode InventoryAccess()
	{
		return KeyCode.I;
	}


}