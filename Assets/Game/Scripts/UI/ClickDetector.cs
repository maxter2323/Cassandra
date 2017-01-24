using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickDetector : MonoBehaviour, IPointerClickHandler
{
/****************************************************************************************/
/*										VARIABLES									  	*/
/****************************************************************************************/

    public delegate void ClickAction(GameObject button);
	public event ClickAction LeftClick;
	public event ClickAction RightClick;
	public event ClickAction MiddleClick;

/****************************************************************************************/
/*										NATIVE METHODS									*/
/****************************************************************************************/

	public void OnPointerClick(PointerEventData eventData)
	{
		switch(eventData.button)
		{
			case PointerEventData.InputButton.Left:
				if (LeftClick != null) LeftClick.Invoke(this.gameObject);
			break;
			case PointerEventData.InputButton.Right:
				if (RightClick != null)	RightClick.Invoke(this.gameObject);
			break;
			case PointerEventData.InputButton.Middle:
				if (MiddleClick != null) MiddleClick.Invoke(this.gameObject);
			break;
		}
	}
}
