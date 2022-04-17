using System.Collections.Generic;
using UnityEngine;

public class TestScript : Singleton<TestScript>
{
	private static Dictionary<string, string> s_dictionary = new Dictionary<string, string>();

	private static Dictionary<string, int> i_dictionary = new Dictionary<string, int>();

	private static Dictionary<string, float> f_dictionary = new Dictionary<string, float>();

	private static List<string> save_list = new List<string>();

	public void InitData()
	{
	}

	public void Clear()
	{
	}

	public bool s_HasKey(string key)
	{
		return s_dictionary.ContainsKey(key);
	}

	public bool i_HasKey(string key)
	{
		return i_dictionary.ContainsKey(key);
	}

	public bool f_HasKey(string key)
	{
		return f_dictionary.ContainsKey(key);
	}

	public void Save()
	{
	}

	public string GetString(string key, string defaultValue = "")
	{
		return PlayerPrefs.GetString(key, defaultValue);
	}

	public int GetInt(string key, int defaultValue = 0)
	{
		return PlayerPrefs.GetInt(key, defaultValue);
	}

	public float GetFloat(string key, float defaultValue = 0f)
	{
		return PlayerPrefs.GetFloat(key, defaultValue);
	}

	public void SetString(string key, string value)
	{
		PlayerPrefs.SetString(key, value);
	}

	public void SetInt(string key, int value)
	{
		PlayerPrefs.SetInt(key, value);
	}

	public void SetFloat(string key, float value)
	{
		PlayerPrefs.SetFloat(key, value);
	}
}
