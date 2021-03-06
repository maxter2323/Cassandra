﻿using System;
using System.Collections.Generic;

namespace CassandraFramework.Dialogues
{
	[Serializable]
	public class DialogueNode 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		public List<DialogueOption> options = new List<DialogueOption>();
		public int index;
		
		/****************************************************************************************/
		/*										 METHODS										*/
		/****************************************************************************************/

		public void AddOption(DialogueOption n)
		{
			options.Add(n);
		}

		public void RemoveOption(int indexOption)
		{
			options.RemoveAt(indexOption);
			// 0 1 3 4 5
			for (int i = indexOption; i < options.Count; i++) 
			{
				options[i].index = options[i].index - 1;
			}
		}

		public List<DialogueOption> GetOptions()
		{
			return options;
		}

	}
}