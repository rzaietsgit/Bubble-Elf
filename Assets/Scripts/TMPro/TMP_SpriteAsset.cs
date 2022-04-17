using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	public class TMP_SpriteAsset : TMP_Asset
	{
		public Texture spriteSheet;

		public Material material;

		public List<TMP_Sprite> spriteInfoList;

		private List<Sprite> m_sprites;

		private void OnEnable()
		{
		}

		public void AddSprites(string path)
		{
		}

		private void OnValidate()
		{
			TMPro_EventManager.ON_SPRITE_ASSET_PROPERTY_CHANGED(isChanged: true, this);
		}
	}
}
