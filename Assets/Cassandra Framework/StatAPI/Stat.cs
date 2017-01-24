using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using CassandraEvents;

namespace CassandraFramework.Stats
{
	public class Stat 
	{
		/****************************************************************************************/
		/*										VARIABLES									  	*/
		/****************************************************************************************/

		//Strings
		public string name = "None";
		public string key = "None";
		private string tag = "None";
		public string tagName = "None";

		//Values
		private int curValue = 0;
		public int maxValue = 100;
		public int minValue = 0;

		//Events
		public StatEvent OnValueChanged = new StatEvent();
		public StatEvent OnValueReachedMax = new StatEvent();
		public StatEvent OnValueReachedMin = new StatEvent();
		public StatStringEvent OnTagChanged = new StatStringEvent();
		
		/****************************************************************************************/
		/*										CONSTRUCTORS									*/
		/****************************************************************************************/

		public Stat()
		{

		}

		public Stat(string k, int cV, int minV, int maxV)
		{
			key = k;
			name = k;
			minValue = cV;
			maxValue = minV;
			Value = maxV;
		}

		public Stat(string k, int cV, int minV, int maxV, string t):this(k, cV, minV, maxV)
		{
			tag = t;
		}

		public Stat(string k, string n, int cV, int minV, int maxV)
		{
			key = k;
			name = n;
			minValue = cV;
			maxValue = minV;
			Value = maxV;
		}

		public Stat(string k, string n, int cV, int minV, int maxV, string t):this( n, k, cV, minV, maxV)
		{
			tag = t;
		}

		public Stat(string k, string n, int cV, int minV, int maxV, string t, string tL):this( n, k, cV, minV, maxV, t)
		{
			tagName = tL;
		}

		/****************************************************************************************/
		/*										METHODS											*/
		/****************************************************************************************/

		public string Name
		{
			get{return name;}
			set{name = value;}
		}

		public int Value
		{
			get{return curValue;}
			set
			{
				if(curValue == value) return;
				curValue = value;
				if (OnValueChanged != null) OnValueChanged.Invoke(this);
				if (curValue >= maxValue)
				{
					curValue = maxValue;
					if (OnValueReachedMax != null) OnValueReachedMax.Invoke(this);
				} 
				if (curValue <= minValue) 
				{
					curValue = minValue;
					if (OnValueReachedMin != null) OnValueReachedMin.Invoke(this);
				}
			}
		}

		public string Tag
		{
			get{return tag;}
			set
			{
				if(tag == value) return;
				string oldTag = tag;
				tag = value;
				if (OnTagChanged != null) OnTagChanged.Invoke(this, oldTag);
			}
		}

		public void SetToMax()
		{
			Value = maxValue;
		}

		public void SetToMin()
		{
			Value = maxValue;
		}
	}
}