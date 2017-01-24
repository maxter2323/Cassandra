using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UIElement : MonoBehaviour 
{

	[SerializeField] private string uiName;
	private const string canvasName = "Canvas";
	public bool isDisplayed;
	public UnityEvent OnBecameVisible = new UnityEvent();
	public UnityEvent OnBecameInVisible = new UnityEvent();

	protected void Start () 
	{
		
	}
	
	protected void Update () 
	{
		
	}

	public virtual void Init()
	{

	}

	public void SetVisible(bool isVisible)
	{	
		GetCanvas().SetActive(isVisible);
		isDisplayed = isVisible;
		if (isVisible) OnBecameVisible.Invoke();
		if (!isVisible) OnBecameInVisible.Invoke();
	}

	public GameObject GetCanvas()
	{
		return this.transform.FindChild(canvasName).gameObject;
	}

	public string GetName()
	{	
		if (string.IsNullOrEmpty(name))
		{
			Debug.LogError("Scene Name is null");
			return null;
		}
		return uiName;
	}
}
