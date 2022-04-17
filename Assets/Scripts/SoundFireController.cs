using UnityEngine;

public class SoundFireController : MonoBehaviour
{
	public static SoundFireController action;

	public AudioClip MusicClips;

	public AudioSource audiosource;

	private void Start()
	{
		action = this;
	}

	private void Awake()
	{
		if (action == null)
		{
			Object.DontDestroyOnLoad(base.gameObject);
			action = this;
		}
		else if (action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	public void play()
	{
	}

	public void stop()
	{
	}
}
