using UnityEngine;

public abstract class DDOLSingleton<T> : MonoBehaviour where T : DDOLSingleton<T>
{
	protected static T _Instance;

	public static T Instance
	{
		get
		{
			if ((Object)null == (Object)_Instance)
			{
				GameObject gameObject = GameObject.Find("InitGame");
				if (null == gameObject)
				{
					gameObject = new GameObject("InitGame");
					Object.DontDestroyOnLoad(gameObject);
				}
				_Instance = gameObject.AddComponent<T>();
			}
			return _Instance;
		}
	}

	private void OnApplicationQuit()
	{
		_Instance = (T)null;
	}
}
