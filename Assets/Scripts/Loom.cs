using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class Loom : MonoBehaviour
{
	public struct DelayedQueueItem
	{
		public float time;

		public Action action;
	}

	public static int maxThreads = 8;

	private static int numThreads;

	private static Loom _current;

	private static bool initialized;

	private List<Action> _actions = new List<Action>();

	private List<DelayedQueueItem> _delayed = new List<DelayedQueueItem>();

	private List<DelayedQueueItem> _currentDelayed = new List<DelayedQueueItem>();

	private List<Action> _currentActions = new List<Action>();

	[CompilerGenerated]
	private static WaitCallback _003C_003Ef__mg_0024cache0;

	public static Loom Current
	{
		get
		{
			Initialize();
			return _current;
		}
	}

	public static void Initialize()
	{
		if (!initialized && Application.isPlaying)
		{
			initialized = true;
			GameObject gameObject = new GameObject("Loom");
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
			_current = gameObject.AddComponent<Loom>();
		}
	}

	public static void QueueOnMainThread(Action action)
	{
		QueueOnMainThread(action, 0f);
	}

	public static void QueueOnMainThread(Action action, float time)
	{
		if (time != 0f)
		{
			if (Current != null)
			{
				lock (Current._delayed)
				{
					Current._delayed.Add(new DelayedQueueItem
					{
						time = Time.time + time,
						action = action
					});
				}
			}
		}
		else if (Current != null)
		{
			lock (Current._actions)
			{
				Current._actions.Add(action);
			}
		}
	}

	public static Thread RunAsync(Action a)
	{
		Initialize();
		while (numThreads >= maxThreads)
		{
			Thread.Sleep(1);
		}
		Interlocked.Increment(ref numThreads);
		ThreadPool.QueueUserWorkItem(RunAction, a);
		return null;
	}

	private static void RunAction(object action)
	{
		try
		{
			((Action)action)();
		}
		catch
		{
		}
		finally
		{
			Interlocked.Decrement(ref numThreads);
		}
	}

	private void OnDisable()
	{
		if (_current == this)
		{
			_current = null;
		}
	}

	private void Start()
	{
	}

	private void Update()
	{
		lock (_actions)
		{
			_currentActions.Clear();
			_currentActions.AddRange(_actions);
			_actions.Clear();
		}
		foreach (Action currentAction in _currentActions)
		{
			currentAction();
		}
		lock (_delayed)
		{
			_currentDelayed.Clear();
			_currentDelayed.AddRange(from d in _delayed
				where d.time <= Time.time
				select d);
			foreach (DelayedQueueItem item in _currentDelayed)
			{
				_delayed.Remove(item);
			}
		}
		foreach (DelayedQueueItem item2 in _currentDelayed)
		{
			DelayedQueueItem current3 = item2;
			current3.action();
		}
	}
}
