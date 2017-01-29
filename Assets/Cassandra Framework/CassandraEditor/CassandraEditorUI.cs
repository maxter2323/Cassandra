using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CassandraEditorUI : MonoBehaviour 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private JsonParser jsonparser;

	/****************************************************************************************/
	/*										METHODS											*/
	/****************************************************************************************/

	private void Start () 
	{
		jsonparser = ServiceLocator.GetService<JsonParser>();
	}

	private void Update () 
	{
		
	}
}
