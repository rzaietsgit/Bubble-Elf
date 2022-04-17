using UnityEngine;

public class bubblefps : MonoBehaviour
{
	public float fpsMeasuringDelta = 0.5f;

	private float timePassed;

	private int m_FrameCount;

	private float m_FPS;

	private void Start()
	{
		timePassed = 0f;
	}

	private void Update()
	{
		m_FrameCount++;
		timePassed += Time.deltaTime;
		if (timePassed > fpsMeasuringDelta)
		{
			m_FPS = (float)m_FrameCount / timePassed;
			timePassed = 0f;
			m_FrameCount = 0;
		}
	}

	private void OnGUI()
	{
		GUIStyle gUIStyle = new GUIStyle();
		gUIStyle.normal.background = null;
		gUIStyle.normal.textColor = new Color(1f, 0.5f, 0f);
		gUIStyle.fontSize = 40;
		GUI.Label(new Rect(Screen.width / 2 - 40, 0f, 200f, 200f), "FPS: " + m_FPS, gUIStyle);
	}
}
