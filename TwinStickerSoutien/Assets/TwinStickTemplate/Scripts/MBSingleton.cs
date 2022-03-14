using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	public static T _instance = null;

	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				T[] instances = FindObjectsOfType<T>();
				if (instances.Length == 0)
				{
					Debug.LogErrorFormat("No instance of singleton of type {0}.", typeof(T));
					return null;
				}

				if (instances.Length > 1)
				{
					Debug.LogError("More than  instance of singleton.");
					return null;
				}
				_instance = instances[0];
			}
			return _instance;
		}
	}
}
