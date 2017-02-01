using System.Collections.Generic;

public interface IFactory
{
	List<IGameScriptable> MakeAll();
	void Add(object toAdd);
}