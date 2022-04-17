using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
	public static MusicController action;

	public AudioClip[] MusicClips;

	public AudioSource audiosource;

	public Dictionary<string, AudioClip> dClips;

	public bool InitOver;

	public static bool MusicSwitch = true;

	public bool bCombo;

	public bool bPlay;

	public static int LevelNowMusicIndex;

	private void Start()
	{
		action = this;
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_MusicSwitch", 1) == 0)
		{
			MusicSwitch = false;
		}
		LoadAudio();
		Init_Music();
		BG_menu();
		InitOver = true;
	}

	public void UpdateErrorLog(string str)
	{
		UnityEngine.Debug.Log("jy error= " + str);
		if (!Singleton<DataManager>.Instance.bChinaIos)
		{
			StartCoroutine(IEUpdateErrorLog(str));
		}
	}

	private IEnumerator IEUpdateErrorLog(string str)
	{
		yield return new WWW(DataManager.errorhttp + str);
	}

	public void LoadAudio()
	{
		dClips = new Dictionary<string, AudioClip>();
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

	public void MusicON()
	{
		Singleton<DataManager>.Instance.SaveUserDate("DB_MusicSwitch", 1);
		MusicSwitch = true;
		audiosource.mute = false;
	}

	public void MusicOFF()
	{
		Singleton<DataManager>.Instance.SaveUserDate("DB_MusicSwitch", 0);
		MusicSwitch = false;
		audiosource.mute = true;
	}

	public void PlayAdCloseMp3()
	{
		if (MusicSwitch)
		{
			audiosource.mute = true;
		}
	}

	public void AdReturnOpenMp3()
	{
		if (MusicSwitch)
		{
			audiosource.mute = false;
		}
	}

	public void LoadingOFF()
	{
		audiosource.mute = true;
	}

	public void Init_Music()
	{
		if (!MusicSwitch)
		{
			MusicOFF();
		}
		else
		{
			MusicON();
		}
	}

	public void BG_menu()
	{
		if (MusicClips != null)
		{
			if (MusicSwitch)
			{
				audiosource.mute = false;
			}
			audiosource.clip = MusicClips[0];
			bPlay = false;
			audiosource.Play();
			audiosource.loop = true;
			audiosource.volume = 0.3f;
		}
	}

	public void ReturnLevelMusic()
	{
		if (MusicClips != null)
		{
			audiosource.clip = MusicClips[LevelNowMusicIndex];
			audiosource.Play();
			audiosource.loop = true;
		}
	}

	public void BG_play()
	{
		if (bPlay)
		{
			return;
		}
		bCombo = false;
		bPlay = true;
		if (MusicClips != null)
		{
			if (MusicSwitch)
			{
				audiosource.mute = false;
			}
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex >= 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
			{
				audiosource.clip = MusicClips[3];
			}
			else
			{
				audiosource.clip = MusicClips[1];
			}
			audiosource.Play();
			audiosource.loop = true;
			audiosource.volume = 0.19f;
		}
	}

	public void BG_Combo()
	{
		if (bCombo)
		{
			return;
		}
		bPlay = false;
		bCombo = true;
		if (MusicClips != null)
		{
			if (MusicSwitch)
			{
				audiosource.mute = false;
			}
			if (Singleton<LevelManager>.Instance.iNowSelectLevelIndex > 10000 && Singleton<LevelManager>.Instance.iNowSelectLevelIndex < 20000)
			{
				audiosource.clip = MusicClips[4];
			}
			else
			{
				audiosource.clip = MusicClips[2];
			}
			audiosource.Play();
			audiosource.loop = true;
			audiosource.volume = 0.2f;
		}
	}

	public void BG_Over()
	{
		bPlay = false;
		bCombo = true;
		if (MusicClips != null)
		{
			if (MusicSwitch)
			{
				audiosource.mute = false;
			}
			audiosource.volume = 0.15f;
		}
	}

	public void BG_Win_play()
	{
	}

	public void BG_Boss_for_wolf()
	{
	}

	public void BG_Lose()
	{
	}

	public void BG_Stop()
	{
	}

	public IEnumerator LoadAudioByMp3(string key, string mp3Name)
	{
		string _mp3Name = mp3Name.Replace(".mp3", string.Empty);
		AudioClip clip = Resources.Load<AudioClip>("Audio/" + _mp3Name);
		yield return new WaitForEndOfFrame();
		if (clip != null)
		{
			dClips[key] = clip;
			Object.Instantiate(clip);
		}
	}
}
