using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Custom/RapidBlurEffect")]
public class RapidBlurEffect : MonoBehaviour
{
	private string ShaderName = "Custom/RapidBlurEffect";

	public Shader CurShader;

	private Material CurMaterial;

	public static int ChangeValue;

	public static float ChangeValue2;

	public static int ChangeValue3;

	[Range(0f, 6f)]
	[Tooltip("[降采样次数]向下采样的次数。此值越大,则采样间隔越大,需要处理的像素点越少,运行速度越快。")]
	public int DownSampleNum = 2;

	[Range(0f, 20f)]
	[Tooltip("[模糊扩散度]进行高斯模糊时，相邻像素点的间隔。此值越大相邻像素间隔越远，图像越模糊。但过大的值会导致失真。")]
	public float BlurSpreadSize = 3f;

	[Range(0f, 8f)]
	[Tooltip("[迭代次数]此值越大,则模糊操作的迭代次数越多，模糊效果越好，但消耗越大。")]
	public int BlurIterations = 3;

	private Material material
	{
		get
		{
			if (CurMaterial == null)
			{
				CurMaterial = new Material(CurShader);
				CurMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return CurMaterial;
		}
	}

	private void Start()
	{
		ChangeValue = DownSampleNum;
		ChangeValue2 = BlurSpreadSize;
		ChangeValue3 = BlurIterations;
		CurShader = Shader.Find(ShaderName);
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
		}
	}

	private void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
	{
		if (CurShader != null)
		{
			float num = 1f / (1f * (float)(1 << DownSampleNum));
			material.SetFloat("_DownSampleValue", BlurSpreadSize * num);
			sourceTexture.filterMode = FilterMode.Bilinear;
			int width = sourceTexture.width >> DownSampleNum;
			int height = sourceTexture.height >> DownSampleNum;
			RenderTexture renderTexture = RenderTexture.GetTemporary(width, height, 0, sourceTexture.format);
			renderTexture.filterMode = FilterMode.Bilinear;
			Graphics.Blit(sourceTexture, renderTexture, material, 0);
			for (int i = 0; i < BlurIterations; i++)
			{
				float num2 = (float)i * 1f;
				material.SetFloat("_DownSampleValue", BlurSpreadSize * num + num2);
				RenderTexture temporary = RenderTexture.GetTemporary(width, height, 0, sourceTexture.format);
				Graphics.Blit(renderTexture, temporary, material, 1);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
				temporary = RenderTexture.GetTemporary(width, height, 0, sourceTexture.format);
				Graphics.Blit(renderTexture, temporary, CurMaterial, 2);
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = temporary;
			}
			Graphics.Blit(renderTexture, destTexture);
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		else
		{
			Graphics.Blit(sourceTexture, destTexture);
		}
	}

	private void OnValidate()
	{
		ChangeValue = DownSampleNum;
		ChangeValue2 = BlurSpreadSize;
		ChangeValue3 = BlurIterations;
	}

	private void Update()
	{
		if (Application.isPlaying)
		{
			DownSampleNum = ChangeValue;
			BlurSpreadSize = ChangeValue2;
			BlurIterations = ChangeValue3;
		}
	}

	private void OnDisable()
	{
		if ((bool)CurMaterial)
		{
			UnityEngine.Object.DestroyImmediate(CurMaterial);
		}
	}
}
