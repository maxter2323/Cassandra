using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFactory
{
	List<IGameScriptable> MakeAll();
	void Add(object toAdd);
}