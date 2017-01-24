using UnityEngine;
using System.Collections;

public class ButterflyAnim : MonoBehaviour 
{
	private void Start () 
	{
		foreach (AnimationState state in GetComponent<Animation>())
		{
			state.speed = 0.5f;
		}
	}
}
