using UnityEngine;
using UnityEngine.Events;

public class Movement 
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private bool isMoving = false;
	private Vector3 destination;
	public float distanceToObject = 0.1f;
	public UnityEvent OnTargetReached = new UnityEvent();
	public GameObject view;

	/****************************************************************************************/
	/*										 METHODS										*/
	/****************************************************************************************/
	
	public void MoveTo(int x, int y)
	{
		destination = new Vector3(x, 0, y);
		OnTargetReached = new UnityEvent();
		view.GetComponentInChildren<Animator>().Play("Run");
		view.transform.LookAt(destination);
		isMoving = true;
	}
	
	public void Update () 
	{
		if (isMoving)
		{
			Move();
		}
	}

	private void Move()
	{
		Vector3 movementVec = destination - view.transform.position;
		if (movementVec.magnitude < distanceToObject)
		{
			StopMoving();
			return;
		}
		int speed = Player.instance.stats.GetStat("Speed").Value;
		view.transform.Translate(movementVec.normalized * speed * Time.deltaTime, Space.World);
	}

	private void StopMoving()
	{
		isMoving = false;
		view.GetComponentInChildren<Animator>().Play("Idle");
		OnTargetReached.Invoke();
	}
}
