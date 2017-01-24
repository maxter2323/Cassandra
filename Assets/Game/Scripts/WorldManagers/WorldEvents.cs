using UnityEngine;
using System.Collections;

public class WorldEvents 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	public delegate void VoidAction();
	public static event VoidAction LevelStarted;

	public delegate void NPCAction(NPC n);
	public static event NPCAction NpcTalk;
	public static event NPCAction NpcDied;

}
