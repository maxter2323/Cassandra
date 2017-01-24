using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Movement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private bool isMoving = false;
	private int moveSpeed = 4;
	private Vector3 dest;
	public float distanceToObject = 0.1f;

	public UnityEvent TargetReached = new UnityEvent();
	public GameObject view;

	/****************************************************************************************/
	/*										NATIVE METHODS									*/
	/****************************************************************************************/
	
	public void MoveTo(int x, int y)
	{
		TargetReached = new UnityEvent();
		Animator anim = view.GetComponentInChildren<Animator>();
		anim.Play("Run");
		isMoving = true;
		dest = new Vector3(x, 0, y);
	}
	
	public void Update () 
	{
		if (isMoving)
		{
			Vector3 movementVec = dest - view.transform.position;
			if (movementVec.magnitude < distanceToObject)
			{
				isMoving = false;
				Animator anim = view.GetComponentInChildren<Animator>();
				anim.Play("Idle");
				TargetReached.Invoke();
				return;
			}
			view.transform.LookAt(dest);
			view.transform.Translate(movementVec.normalized * moveSpeed * Time.deltaTime, Space.World);
		}
	}


}
