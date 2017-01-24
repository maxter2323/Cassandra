using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class PrefabElement : MonoBehaviour
{

	[SerializeField] private GameObject prefab;
	private GameObject instance;

	protected void Awake() 
	{
		Refresh();
	}

	protected void Start()
	{
		if (Application.isEditor && Application.isPlaying) 
		{
			instance.transform.parent = this.transform.parent;
			Destroy(this.gameObject);
		}
	}
	
	protected void OnEnable() 
	{
		EditorRefresh();
	}
	
	protected void Update() 
	{
		EditorRefresh();
	}

	private void MakePrefab()
	{   
		if (prefab != null)
		{
			instance = Instantiate(prefab);
			instance.transform.parent = transform;
			instance.transform.localPosition = Vector3.zero;
			transform.gameObject.name = instance.name;
			transform.name = instance.name;

			//instance.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector;
		}
	}

	private void DisposePrefab()
	{
		if (instance != null)
		{
			CustomDestroy(instance);
		}
	}

	private void EditorRefresh()
	{
		if (Application.isEditor && !Application.isPlaying) 
		{
			Refresh();
		}
	}

	private void Refresh()
	{
		RemoveAllChildren();
		DisposePrefab();
		MakePrefab();
	}

	private void RemoveAllChildren()
	{   
		foreach(Transform child in transform) {
			CustomDestroy(child.gameObject);
		}
	}

	private void CustomDestroy(GameObject toDestroy)
	{
		if (Application.isEditor && !Application.isPlaying) 
		{
			DestroyImmediate(toDestroy);
		}
		else
		{
			Destroy(toDestroy);
		}
	}
}
