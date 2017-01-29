using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public interface IGameScriptable
{
	IFactory GetFactory();
	List<GameScript> GetAllScripts();
}
