using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : IService
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	//UI core
	private Dictionary<string, UIElement> uiStack;
	private DataLocator dataLocator;

	//Event System
	private const string eventSystemName = "EventSystem";
	public GameObject eventSystem;

	/****************************************************************************************/
	/*										ADD METHODS										*/
	/****************************************************************************************/

	public void Init () 
	{
		dataLocator = ServiceLocator.GetService<DataLocator>(); 
		uiStack = new Dictionary<string, UIElement>();
	}

	public UIElement MakeUI(string uiName)
	{
		GameObject uiObject = dataLocator.InstantiateLoadedResource(uiName);
		uiObject.name = uiName;
		UIElement uiScript = uiObject.GetComponent<UIElement>();
		uiScript.Init();
		if (eventSystem == null)
		{
			eventSystem = dataLocator.InstantiateLoadedResource(eventSystemName);
		}
		uiStack.Add(uiName, uiScript);
		return uiScript;
	}

	public UIElement GetUI(string uiName)
	{
		if (HasUI(uiName))
		{
			return uiStack[uiName]; 
		} 
		return null;
	}

	public bool HasUI(string uiName)
	{
		if (!uiStack.ContainsKey(uiName))
		{
			return false;
		}
		return true; 
	}

	/****************************************************************************************/
	/*										REMOVE METHODS									*/
	/****************************************************************************************/

	public void DeleteUI(string uiName)
	{
		if (uiStack.ContainsKey(uiName))
		{
			GameObject.Destroy(uiStack[uiName].gameObject);
			uiStack.Remove(uiName);
			if (uiStack.Count == 0 && eventSystem != null)
			{
				RemoveEventSystem();
			}
		}
	}

	public void ClearStack()
	{
		string[] keyArray = new string[uiStack.Count];
		int counter = 0;
		foreach(var key in uiStack.Keys)
		{
			keyArray[counter] = (string)key;
		}
		foreach(string keyToRemove in keyArray)
		{
			DeleteUI(keyToRemove);
		}
	}

	public void RemoveEventSystem()
	{
		GameObject.Destroy(eventSystem);
		eventSystem = null;
	}

}
