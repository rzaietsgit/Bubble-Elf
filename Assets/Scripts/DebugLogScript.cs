using TMPro;
using UnityEngine;

public class DebugLogScript : MonoBehaviour
{
	public static DebugLogScript action;

	public TextMeshProUGUI DebugMsg;

	public GameObject ShowDebugMsgBg;

	public static string Log = string.Empty;

	private void Start()
	{
		action = this;
		ShowDebugMsgBg.SetActive(value: false);
	}

	private void Update()
	{
	}

	public void WLog(string sLog)
	{
	}

	public void ClearMsg()
	{
		Log = string.Empty;
		DebugMsg.SetText(string.Empty);
	}

	public void ShowDebugPanel()
	{
		ShowDebugMsgBg.SetActive(value: true);
	}

	public void HideDebugPanel()
	{
		ShowDebugMsgBg.SetActive(value: false);
	}

	private void Awake()
	{
		if (!action)
		{
			action = this;
			Object.DontDestroyOnLoad(this);
		}
	}
}
