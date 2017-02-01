using System.Collections.Generic;

public interface IGameScriptable
{
	IFactory GetFactory();
	List<GameScript> GetAllScripts();
}