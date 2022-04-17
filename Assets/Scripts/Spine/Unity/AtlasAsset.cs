using System;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	public class AtlasAsset : ScriptableObject
	{
		public TextAsset atlasFile;

		public Material[] materials;

		private Atlas atlas;

		public void Reset()
		{
			atlas = null;
		}

		public Atlas GetAtlas()
		{
			if (atlasFile == null)
			{
				UnityEngine.Debug.LogError("Atlas file not set for atlas asset: " + base.name, this);
				Reset();
				return null;
			}
			if (materials == null || materials.Length == 0)
			{
				UnityEngine.Debug.LogError("Materials not set for atlas asset: " + base.name, this);
				Reset();
				return null;
			}
			if (atlas != null)
			{
				return atlas;
			}
			try
			{
				atlas = new Atlas(new StringReader(atlasFile.text), string.Empty, new MaterialsTextureLoader(this));
				atlas.FlipV();
				return atlas;
			}
			catch (Exception ex)
			{
				UnityEngine.Debug.LogError("Error reading atlas file for atlas asset: " + base.name + "\n" + ex.Message + "\n" + ex.StackTrace, this);
				return null;
			}
		}

		public Sprite GenerateSprite(string name, out Material material)
		{
			AtlasRegion atlasRegion = atlas.FindRegion(name);
			Sprite result = null;
			material = null;
			if (atlasRegion != null)
			{
			}
			return result;
		}

		public Mesh GenerateMesh(string name, Mesh mesh, out Material material, float scale = 0.01f)
		{
			AtlasRegion atlasRegion = atlas.FindRegion(name);
			material = null;
			if (atlasRegion != null)
			{
				if (mesh == null)
				{
					mesh = new Mesh();
					mesh.name = name;
				}
				Vector3[] array = new Vector3[4];
				Vector2[] array2 = new Vector2[4];
				Color[] colors = new Color[4]
				{
					Color.white,
					Color.white,
					Color.white,
					Color.white
				};
				int[] triangles = new int[6]
				{
					0,
					1,
					2,
					2,
					3,
					0
				};
				float num = (float)atlasRegion.width / -2f;
				float x = num * -1f;
				float num2 = (float)atlasRegion.height / 2f;
				float y = num2 * -1f;
				array[0] = new Vector3(num, y, 0f) * scale;
				array[1] = new Vector3(num, num2, 0f) * scale;
				array[2] = new Vector3(x, num2, 0f) * scale;
				array[3] = new Vector3(x, y, 0f) * scale;
				float u = atlasRegion.u;
				float v = atlasRegion.v;
				float u2 = atlasRegion.u2;
				float v2 = atlasRegion.v2;
				if (!atlasRegion.rotate)
				{
					array2[0] = new Vector2(u, v2);
					array2[1] = new Vector2(u, v);
					array2[2] = new Vector2(u2, v);
					array2[3] = new Vector2(u2, v2);
				}
				else
				{
					array2[0] = new Vector2(u2, v2);
					array2[1] = new Vector2(u, v2);
					array2[2] = new Vector2(u, v);
					array2[3] = new Vector2(u2, v);
				}
				mesh.triangles = new int[0];
				mesh.vertices = array;
				mesh.uv = array2;
				mesh.colors = colors;
				mesh.triangles = triangles;
				mesh.RecalculateNormals();
				mesh.RecalculateBounds();
				material = (Material)atlasRegion.page.rendererObject;
			}
			else
			{
				mesh = null;
			}
			return mesh;
		}
	}
}
