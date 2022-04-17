using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	public static SoundController action;

	private Dictionary<string, AudioClip> SoundClips;

	public static int iSonudLv;

	public AudioSource audiosource;

	public Dictionary<string, AudioClip> dClips;

	public Dictionary<string, int> dPlayeTime;

	public bool InitOver;

	public static bool SoundSwitch = true;

	public int bdoubleHit;

	private bool bLocalSwitchData;

	private bool brole_girl_happy = true;

	private bool brole_girl_wait = true;

	private bool brole_girl_cry = true;

	private bool brole_girl_worry = true;

	private bool brole_girl_anger = true;

	private bool brole_girl_win = true;

	private void Awake()
	{
		if (action == null)
		{
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			action = this;
		}
		else if (action != this)
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
		dPlayeTime = new Dictionary<string, int>();
	}

	private void Start()
	{
		action = this;
		Singleton<DataManager>.Instance.LocalStaticLoadDataAudio();
		LoadAudio();
		InitAudioConfig();
		if (Singleton<TestScript>.Instance.GetInt(DataManager.SDBNO + "DB_SoundSwitch", 1) == 0)
		{
			SoundSwitch = false;
		}
		Init_Sound();
		InitOver = true;
	}

	public void InitAudioConfig()
	{
	}

	public void SoundON()
	{
		Singleton<DataManager>.Instance.SaveUserDate("DB_SoundSwitch", 1);
		audiosource.mute = false;
		SoundSwitch = true;
	}

	public void SoundOFF()
	{
		Singleton<DataManager>.Instance.SaveUserDate("DB_SoundSwitch", 0);
		audiosource.mute = true;
		SoundSwitch = false;
	}

	public void PlayAdCloseMp3()
	{
		if (SoundSwitch)
		{
			audiosource.mute = true;
		}
	}

	public void AdReturnOpenMp3()
	{
		if (SoundSwitch)
		{
			audiosource.mute = false;
		}
	}

	public void LoadingOFF()
	{
		audiosource.mute = true;
	}

	public void Init_Sound()
	{
		if (!SoundSwitch)
		{
			SoundOFF();
		}
		else
		{
			SoundON();
		}
	}

	private IEnumerator _DelayedPlay(string key, float DelayedTime = 0.3f)
	{
		yield return new WaitForSeconds(DelayedTime);
		play(key);
	}

	public void play(string key, int idPlayeTime = 300)
	{
		if (!dPlayeTime.ContainsKey(key))
		{
			dPlayeTime[key] = GetAudioNowTime();
		}
		else
		{
			if (GetAudioNowTime() - dPlayeTime[key] <= idPlayeTime)
			{
				return;
			}
			dPlayeTime[key] = GetAudioNowTime();
		}
		try
		{
			audiosource.PlayOneShot(dClips[key]);
		}
		catch (Exception ex)
		{
			UnityEngine.Debug.Log("play mp3 Error （key:" + key + "） = " + ex);
		}
	}

	public void ClearPlay()
	{
		brole_girl_happy = true;
		brole_girl_wait = true;
		brole_girl_cry = true;
		brole_girl_worry = true;
		brole_girl_anger = true;
		brole_girl_win = true;
	}

	public bool CheckPlayOne(string key)
	{
		if (key == "role_girl_win")
		{
			if (!brole_girl_win)
			{
				return true;
			}
			brole_girl_win = false;
		}
		else if (key == "role_girl_anger")
		{
			if (!brole_girl_anger)
			{
				return true;
			}
			brole_girl_anger = false;
		}
		else if (key == "role_girl_worry")
		{
			if (!brole_girl_worry)
			{
				return true;
			}
			brole_girl_worry = false;
		}
		else if (key == "role_girl_cry")
		{
			if (!brole_girl_cry)
			{
				return true;
			}
			brole_girl_cry = false;
		}
		else if (key == "role_girl_happy")
		{
			if (!brole_girl_happy)
			{
				return true;
			}
			brole_girl_happy = false;
		}
		else if (key == "role_girl_wait")
		{
			if (!brole_girl_wait)
			{
				return true;
			}
			brole_girl_wait = false;
		}
		return false;
	}

	public void playNow(string key, bool NowPlay = false, int idPlayeTime = 300)
	{
		if (CheckPlayOne(key))
		{
			return;
		}
		if (NowPlay)
		{
			UnityEngine.Debug.Log("playNow key=" + key);
			audiosource.PlayOneShot(dClips[key]);
			return;
		}
		if (key == "b_boom")
		{
		}
		if (key.LastIndexOf("role_girl") >= 0)
		{
			audiosource.PlayOneShot(dClips[key]);
			return;
		}
		if (!dPlayeTime.ContainsKey(key))
		{
			dPlayeTime[key] = GetAudioNowTime();
		}
		else
		{
			if (GetAudioNowTime() - dPlayeTime[key] <= idPlayeTime)
			{
				return;
			}
			dPlayeTime[key] = GetAudioNowTime();
		}
		try
		{
			audiosource.PlayOneShot(dClips[key]);
		}
		catch (Exception)
		{
		}
	}

	public IEnumerator LoadAudioByMp3(string key, string mp3Name)
	{
		if (bLocalSwitchData)
		{
			WWW w = null;
			try
			{
				mp3Name = mp3Name.Replace(".mp3", string.Empty);
				string url = DataManager.Net_Address + "/Data/Audio/" + mp3Name + ".OGG";
				w = new WWW(url);
			}
			catch (Exception arg)
			{
				UnityEngine.Debug.Log("LoadAudio error = " + arg);
			}
			yield return new WaitForEndOfFrame();
			try
			{
				AudioClip audioClip = w.GetAudioClip();
				mp3Name = mp3Name.Replace(".mp3", string.Empty);
				dClips[key] = audioClip;
				UnityEngine.Object.Instantiate(audioClip);
			}
			catch (Exception arg2)
			{
				UnityEngine.Debug.Log("LoadAudio error = " + arg2);
			}
			yield break;
		}
		string _mp3Name = mp3Name.Replace(".mp3", string.Empty);
		AudioClip clip = Resources.Load<AudioClip>("Audio/" + _mp3Name);
		yield return new WaitForEndOfFrame();
		if (clip != null)
		{
			dClips[key] = clip;
			UnityEngine.Object.Instantiate(clip);
		}
	}

	public void LoadAudio()
	{
		dClips = new Dictionary<string, AudioClip>();
		for (int i = 0; i < Singleton<DataManager>.Instance.dDataAudio.Keys.Count; i++)
		{
			string text = Singleton<DataManager>.Instance.dDataAudio.Keys.ToArray()[i];
			if (text.Length > 1)
			{
				string mp3Name = Singleton<DataManager>.Instance.dDataAudio[text]["name"];
				StartCoroutine(LoadAudioByMp3(text, mp3Name));
			}
		}
	}

	public static int GetAudioNowTime()
	{
		string s = DateTime.Now.ToString("hhmmssfff");
		return int.Parse(s);
	}
}
