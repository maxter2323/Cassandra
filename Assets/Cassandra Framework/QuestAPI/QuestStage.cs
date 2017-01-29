using System;

namespace CassandraFramework.Quests
{
	[Serializable]
	public class QuestStage
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public Quest parent;
		public int index;
		public int gotoIndex;
		public string log;
		public GameScript script;

		/****************************************************************************************/
		/*										METHODS									  		*/
		/****************************************************************************************/

		public void Start()
		{
			script.Run();
		}
	}
}