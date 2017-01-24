using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class ServiceLocator
{
	/****************************************************************************************/
	/*										VARIABLES									  	*/
	/****************************************************************************************/

	private static Dictionary<Type,IService> serviceList = new Dictionary<Type,IService>();

	/****************************************************************************************/
	/*										METHODS									  		*/
	/****************************************************************************************/

	public static void Init()
	{
		serviceList = new Dictionary<Type,IService>();
	}

	public static T GetService<T>()
	{
		Type serviceType = typeof(T);
		if (!serviceList.ContainsKey(serviceType))
		{
			MakeService(serviceType); 
		}
		return (T)serviceList[serviceType];
	}

	private static void MakeService(Type serviceType)
	{
		var objectWithoutType = Activator.CreateInstance(serviceType);
		IService instance = (IService)objectWithoutType;
	    serviceList.Add(serviceType, instance);
		instance.Init();
	}

}