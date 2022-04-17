using System;
using UnityEngine;

namespace TMPro
{
	[Serializable]
	[ExecuteInEditMode]
	public class TMP_Settings : ScriptableObject
	{
		public static TMP_Settings s_Instance;

		public bool enableWordWrapping;

		public bool enableKerning;

		public bool enableExtraPadding;

		public bool warningsDisabled;

		public TMP_FontAsset fontAsset;

		public TMP_SpriteAsset spriteAsset;

		public TMP_StyleSheet styleSheet;

		public static TMP_Settings instance
		{
			get
			{
				if (s_Instance == null)
				{
					s_Instance = (Resources.Load("TMP Settings") as TMP_Settings);
				}
				return s_Instance;
			}
		}

		public static TMP_Settings LoadDefaultSettings()
		{
			if (s_Instance == null)
			{
				TMP_Settings x = Resources.Load("TMP Settings") as TMP_Settings;
				if (x != null)
				{
					s_Instance = x;
				}
			}
			return s_Instance;
		}

		public static TMP_Settings GetSettings()
		{
			if (instance == null)
			{
				return null;
			}
			return instance;
		}

		public static TMP_FontAsset GetFontAsset()
		{
			if (instance == null)
			{
				return null;
			}
			return instance.fontAsset;
		}

		public static TMP_SpriteAsset GetSpriteAsset()
		{
			if (instance == null)
			{
				return null;
			}
			return instance.spriteAsset;
		}

		public static TMP_StyleSheet GetStyleSheet()
		{
			if (instance == null)
			{
				return null;
			}
			return instance.styleSheet;
		}
	}
}
