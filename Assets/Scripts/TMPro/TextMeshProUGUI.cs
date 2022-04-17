using System;
using UnityEngine;
using UnityEngine.UI;

namespace TMPro
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	[RequireComponent(typeof(CanvasRenderer))]
	[AddComponentMenu("UI/TextMeshPro Text", 12)]
	public class TextMeshProUGUI : TMP_Text, ILayoutElement
	{
		[SerializeField]
		private Vector2 m_uvOffset = Vector2.zero;

		[SerializeField]
		private float m_uvLineOffset;

		[SerializeField]
		private bool m_hasFontAssetChanged;

		private Vector3 m_previousLossyScale;

		private Vector3[] m_RectTransformCorners = new Vector3[4];

		private CanvasRenderer m_uiRenderer;

		private Canvas m_canvas;

		private bool m_isFirstAllocation;

		private int m_max_characters = 8;

		private WordWrapState m_SavedWordWrapState = default(WordWrapState);

		private WordWrapState m_SavedLineState = default(WordWrapState);

		private bool m_isMaskingEnabled;

		private bool m_isScrollRegionSet;

		private int m_stencilID;

		[SerializeField]
		private Vector4 m_maskOffset;

		private Matrix4x4 m_EnvMapMatrix = default(Matrix4x4);

		private bool m_isAwake;

		[NonSerialized]
		private bool m_isRegisteredForEvents;

		private int m_recursiveCount;

		private int m_recursiveCountA;

		private int loopCountA;

		private bool m_isRebuildingLayout;

		public override Material fontSharedMaterial
		{
			get
			{
				return m_uiRenderer.GetMaterial();
			}
			set
			{
				if (canvasRenderer.GetMaterial() != value)
				{
					m_isNewBaseMaterial = true;
					SetFontSharedMaterial(value);
					m_havePropertiesChanged = true;
					SetVerticesDirty();
				}
			}
		}

		public override Mesh mesh => m_mesh;

		public new CanvasRenderer canvasRenderer => m_uiRenderer;

		public InlineGraphicManager inlineGraphicManager => m_inlineGraphics;

		public Bounds bounds
		{
			get
			{
				if (m_mesh != null)
				{
					return m_mesh.bounds;
				}
				return default(Bounds);
			}
		}

		public Vector4 maskOffset
		{
			get
			{
				return m_maskOffset;
			}
			set
			{
				m_maskOffset = value;
				UpdateMask();
				m_havePropertiesChanged = true;
			}
		}

		protected override void Awake()
		{
			m_isAwake = true;
			m_canvas = base.canvas;
			m_isOrthographic = true;
			m_rectTransform = base.gameObject.GetComponent<RectTransform>();
			if (m_rectTransform == null)
			{
				m_rectTransform = base.gameObject.AddComponent<RectTransform>();
			}
			m_uiRenderer = GetComponent<CanvasRenderer>();
			if (m_uiRenderer == null)
			{
				m_uiRenderer = base.gameObject.AddComponent<CanvasRenderer>();
			}
			if (m_mesh == null)
			{
				m_mesh = new Mesh();
				m_mesh.hideFlags = HideFlags.HideAndDontSave;
			}
			if (m_settings == null)
			{
				m_settings = TMP_Settings.LoadDefaultSettings();
			}
			if (m_settings != null)
			{
				if (m_isNewTextObject)
				{
					m_enableWordWrapping = m_settings.enableWordWrapping;
					m_enableKerning = m_settings.enableKerning;
					m_enableExtraPadding = m_settings.enableExtraPadding;
					m_isNewTextObject = false;
				}
				m_warningsDisabled = m_settings.warningsDisabled;
			}
			LoadFontAsset();
			TMP_StyleSheet.LoadDefaultStyleSheet();
			m_char_buffer = new int[m_max_characters];
			m_cached_TextElement = new TMP_Glyph();
			m_isFirstAllocation = true;
			m_textInfo = new TMP_TextInfo(this);
			if (m_fontAsset == null)
			{
				UnityEngine.Debug.LogWarning("Please assign a Font Asset to this " + base.transform.name + " gameobject.", this);
				return;
			}
			if (m_fontSizeMin == 0f)
			{
				m_fontSizeMin = m_fontSize / 2f;
			}
			if (m_fontSizeMax == 0f)
			{
				m_fontSizeMax = m_fontSize * 2f;
			}
			m_isInputParsingRequired = true;
			m_havePropertiesChanged = true;
			m_isCalculateSizeRequired = true;
		}

		protected override void OnEnable()
		{
			if (!m_isRegisteredForEvents)
			{
				m_isRegisteredForEvents = true;
			}
			GraphicRegistry.RegisterGraphicForCanvas(base.canvas, this);
			if (m_canvas == null)
			{
				m_canvas = base.canvas;
			}
			if (m_uiRenderer.GetMaterial() == null)
			{
				if (m_sharedMaterial != null)
				{
					m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
				}
				else
				{
					m_isNewBaseMaterial = true;
					fontSharedMaterial = m_baseMaterial;
					RecalculateMasking();
				}
				m_havePropertiesChanged = true;
			}
			ComputeMarginSize();
			m_verticesAlreadyDirty = false;
			SetVerticesDirty();
			m_layoutAlreadyDirty = false;
			SetLayoutDirty();
			RecalculateClipping();
		}

		protected override void OnDisable()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(base.canvas, this);
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			m_uiRenderer.Clear();
			LayoutRebuilder.MarkLayoutForRebuild(m_rectTransform);
			RecalculateClipping();
		}

		protected override void OnDestroy()
		{
			GraphicRegistry.UnregisterGraphicForCanvas(base.canvas, this);
			if (m_maskingMaterial != null)
			{
				MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
				m_maskingMaterial = null;
			}
			if (m_fontMaterial != null)
			{
				UnityEngine.Object.DestroyImmediate(m_fontMaterial);
			}
			m_isRegisteredForEvents = false;
		}

		protected override void LoadFontAsset()
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (m_settings == null)
			{
				m_settings = TMP_Settings.LoadDefaultSettings();
			}
			if (m_fontAsset == null)
			{
				if (m_settings != null && m_settings.fontAsset != null)
				{
					m_fontAsset = m_settings.fontAsset;
				}
				else
				{
					m_fontAsset = (Resources.Load("Fonts & Materials/ARIAL SDF", typeof(TMP_FontAsset)) as TMP_FontAsset);
				}
				if (m_fontAsset == null)
				{
					UnityEngine.Debug.LogWarning("The ARIAL SDF Font Asset was not found. There is no Font Asset assigned to " + base.gameObject.name + ".", this);
					return;
				}
				if (m_fontAsset.characterDictionary == null)
				{
					UnityEngine.Debug.Log("Dictionary is Null!");
				}
				m_baseMaterial = m_fontAsset.material;
				m_sharedMaterial = m_baseMaterial;
				m_isNewBaseMaterial = true;
			}
			else
			{
				if (m_fontAsset.characterDictionary == null)
				{
					m_fontAsset.ReadFontDefinition();
				}
				m_sharedMaterial = m_baseMaterial;
				m_isNewBaseMaterial = true;
				if (m_sharedMaterial == null || m_sharedMaterial.mainTexture == null || m_fontAsset.atlas.GetInstanceID() != m_sharedMaterial.mainTexture.GetInstanceID())
				{
					m_sharedMaterial = m_fontAsset.material;
					m_baseMaterial = m_sharedMaterial;
					m_isNewBaseMaterial = true;
				}
			}
			if (!m_fontAsset.characterDictionary.TryGetValue(95, out m_cached_Underline_GlyphInfo) && m_settings == null && !m_settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("Underscore character wasn't found in the current Font Asset. No characters assigned for Underline.", this);
			}
			m_stencilID = MaterialManager.GetStencilID(base.gameObject);
			if (m_stencilID == 0)
			{
				if (m_maskingMaterial != null)
				{
					MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
					m_maskingMaterial = null;
				}
				m_sharedMaterial = m_baseMaterial;
			}
			else
			{
				if (m_maskingMaterial == null)
				{
					m_maskingMaterial = MaterialManager.GetStencilMaterial(m_baseMaterial, m_stencilID);
				}
				else if (m_maskingMaterial.GetInt(ShaderUtilities.ID_StencilID) != m_stencilID || m_isNewBaseMaterial)
				{
					MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
					m_maskingMaterial = MaterialManager.GetStencilMaterial(m_baseMaterial, m_stencilID);
				}
				m_sharedMaterial = m_maskingMaterial;
			}
			m_isNewBaseMaterial = false;
			SetShaderDepth();
			if (m_uiRenderer == null)
			{
				m_uiRenderer = GetComponent<CanvasRenderer>();
			}
			m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			m_padding = GetPaddingForMaterial();
		}

		private void UpdateEnvMapMatrix()
		{
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_EnvMap) && !(m_sharedMaterial.GetTexture(ShaderUtilities.ID_EnvMap) == null))
			{
				Vector3 euler = m_sharedMaterial.GetVector(ShaderUtilities.ID_EnvMatrixRotation);
				m_EnvMapMatrix = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(euler), Vector3.one);
				m_sharedMaterial.SetMatrix(ShaderUtilities.ID_EnvMatrix, m_EnvMapMatrix);
			}
		}

		private void EnableMasking()
		{
			if (m_fontMaterial == null)
			{
				m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
				m_uiRenderer.SetMaterial(m_fontMaterial, m_sharedMaterial.mainTexture);
			}
			m_sharedMaterial = m_fontMaterial;
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				UpdateMask();
			}
			m_isMaskingEnabled = true;
		}

		private void DisableMasking()
		{
			if (m_fontMaterial != null)
			{
				if (m_stencilID > 0)
				{
					m_sharedMaterial = m_maskingMaterial;
				}
				else
				{
					m_sharedMaterial = m_baseMaterial;
				}
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
				UnityEngine.Object.DestroyImmediate(m_fontMaterial);
			}
			m_isMaskingEnabled = false;
		}

		private void UpdateMask()
		{
			if (m_rectTransform != null)
			{
				if (!ShaderUtilities.isInitialized)
				{
					ShaderUtilities.GetShaderPropertyIDs();
				}
				m_isScrollRegionSet = true;
				float num = Mathf.Min(Mathf.Min(m_margin.x, m_margin.z), m_sharedMaterial.GetFloat(ShaderUtilities.ID_MaskSoftnessX));
				float num2 = Mathf.Min(Mathf.Min(m_margin.y, m_margin.w), m_sharedMaterial.GetFloat(ShaderUtilities.ID_MaskSoftnessY));
				num = ((!(num > 0f)) ? 0f : num);
				num2 = ((!(num2 > 0f)) ? 0f : num2);
				float z = (m_rectTransform.rect.width - Mathf.Max(m_margin.x, 0f) - Mathf.Max(m_margin.z, 0f)) / 2f + num;
				float w = (m_rectTransform.rect.height - Mathf.Max(m_margin.y, 0f) - Mathf.Max(m_margin.w, 0f)) / 2f + num2;
				Vector3 localPosition = m_rectTransform.localPosition;
				Vector2 pivot = m_rectTransform.pivot;
				float x = (0.5f - pivot.x) * m_rectTransform.rect.width + (Mathf.Max(m_margin.x, 0f) - Mathf.Max(m_margin.z, 0f)) / 2f;
				Vector2 pivot2 = m_rectTransform.pivot;
				Vector2 vector = localPosition + new Vector3(x, (0.5f - pivot2.y) * m_rectTransform.rect.height + (0f - Mathf.Max(m_margin.y, 0f) + Mathf.Max(m_margin.w, 0f)) / 2f);
				Vector4 value = new Vector4(vector.x, vector.y, z, w);
				m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, value);
			}
		}

		protected override void SetFontMaterial(Material mat)
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (m_uiRenderer == null)
			{
				m_uiRenderer = GetComponent<CanvasRenderer>();
			}
			if (m_maskingMaterial != null)
			{
				MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
				m_maskingMaterial = null;
			}
			m_stencilID = MaterialManager.GetStencilID(base.gameObject);
			if (m_fontMaterial == null || m_fontMaterial.GetInstanceID() != mat.GetInstanceID())
			{
				m_fontMaterial = CreateMaterialInstance(mat);
			}
			if (m_stencilID > 0)
			{
				m_fontMaterial = MaterialManager.SetStencil(m_fontMaterial, m_stencilID);
			}
			m_sharedMaterial = m_fontMaterial;
			SetShaderDepth();
			m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetFontSharedMaterial(Material mat)
		{
			ShaderUtilities.GetShaderPropertyIDs();
			if (m_uiRenderer == null)
			{
				m_uiRenderer = GetComponent<CanvasRenderer>();
			}
			if (mat == null)
			{
				mat = m_baseMaterial;
				m_isNewBaseMaterial = true;
			}
			m_stencilID = MaterialManager.GetStencilID(base.gameObject);
			if (m_stencilID == 0)
			{
				if (m_maskingMaterial != null)
				{
					MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
					m_maskingMaterial = null;
				}
				m_baseMaterial = mat;
			}
			else
			{
				if (m_maskingMaterial == null)
				{
					m_maskingMaterial = MaterialManager.GetStencilMaterial(mat, m_stencilID);
				}
				else if (m_maskingMaterial.GetInt(ShaderUtilities.ID_StencilID) != m_stencilID || m_isNewBaseMaterial)
				{
					MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
					m_maskingMaterial = MaterialManager.GetStencilMaterial(mat, m_stencilID);
				}
				mat = m_maskingMaterial;
			}
			m_isNewBaseMaterial = false;
			m_sharedMaterial = mat;
			SetShaderDepth();
			m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetFontBaseMaterial(Material mat)
		{
			UnityEngine.Debug.Log("Changing Base Material from [" + ((!(m_lastBaseMaterial == null)) ? m_lastBaseMaterial.name : "Null") + "] to [" + mat.name + "].");
			m_baseMaterial = mat;
			m_lastBaseMaterial = mat;
		}

		protected override void SetOutlineThickness(float thickness)
		{
			if (m_fontMaterial != null && m_sharedMaterial.GetInstanceID() != m_fontMaterial.GetInstanceID())
			{
				m_sharedMaterial = m_fontMaterial;
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			}
			else if (m_fontMaterial == null)
			{
				m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
				m_sharedMaterial = m_fontMaterial;
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			}
			thickness = Mathf.Clamp01(thickness);
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetFaceColor(Color32 color)
		{
			if (m_fontMaterial != null && m_sharedMaterial.GetInstanceID() != m_fontMaterial.GetInstanceID())
			{
				m_sharedMaterial = m_fontMaterial;
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			}
			else if (m_fontMaterial == null)
			{
				m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
				m_sharedMaterial = m_fontMaterial;
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			}
			m_sharedMaterial.SetColor(ShaderUtilities.ID_FaceColor, color);
		}

		protected override void SetOutlineColor(Color32 color)
		{
			if (m_fontMaterial != null && m_sharedMaterial.GetInstanceID() != m_fontMaterial.GetInstanceID())
			{
				m_sharedMaterial = m_fontMaterial;
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			}
			else if (m_fontMaterial == null)
			{
				m_fontMaterial = CreateMaterialInstance(m_sharedMaterial);
				m_sharedMaterial = m_fontMaterial;
				m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			}
			m_sharedMaterial.SetColor(ShaderUtilities.ID_OutlineColor, color);
		}

		private Material CreateMaterialInstance(Material source)
		{
			Material material = new Material(source);
			material.shaderKeywords = source.shaderKeywords;
			material.hideFlags = HideFlags.DontSave;
			material.name += " (Instance)";
			return material;
		}

		protected override void SetShaderDepth()
		{
			if (!(m_canvas == null) && !(m_sharedMaterial == null))
			{
				if (m_canvas.renderMode == RenderMode.ScreenSpaceOverlay || m_isOverlay)
				{
					m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 0f);
				}
				else
				{
					m_sharedMaterial.SetFloat(ShaderUtilities.ShaderTag_ZTestMode, 4f);
				}
			}
		}

		protected override void SetCulling()
		{
			if (m_isCullingEnabled)
			{
				m_uiRenderer.GetMaterial().SetFloat("_CullMode", 2f);
			}
			else
			{
				m_uiRenderer.GetMaterial().SetFloat("_CullMode", 0f);
			}
		}

		private void SetPerspectiveCorrection()
		{
			if (m_isOrthographic)
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0f);
			}
			else
			{
				m_sharedMaterial.SetFloat(ShaderUtilities.ID_PerspectiveFilter, 0.875f);
			}
		}

		protected override float GetPaddingForMaterial(Material mat)
		{
			m_padding = ShaderUtilities.GetPadding(mat, m_enableExtraPadding, m_isUsingBold);
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			m_isSDFShader = mat.HasProperty(ShaderUtilities.ID_WeightNormal);
			return m_padding;
		}

		protected override float GetPaddingForMaterial()
		{
			m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			m_isSDFShader = m_sharedMaterial.HasProperty(ShaderUtilities.ID_WeightNormal);
			return m_padding;
		}

		private void SetMeshArrays(int size)
		{
			m_textInfo.meshInfo[0].ResizeMeshInfo(size);
			m_uiRenderer.SetMesh(m_textInfo.meshInfo[0].mesh);
		}

		protected override int SetArraySizes(int[] chars)
		{
			int num = 0;
			int num2 = 0;
			int endIndex = 0;
			int num3 = 0;
			m_isUsingBold = false;
			m_isParsingText = false;
			m_textElementType = TMP_TextElementType.Character;
			m_VisibleCharacters.Clear();
			m_currentFontAsset = m_fontAsset;
			for (int i = 0; chars[i] != 0; i++)
			{
				int num4 = chars[i];
				if (m_isRichText && num4 == 60 && ValidateHtmlTag(chars, i + 1, out endIndex))
				{
					i = endIndex;
					if ((m_style & FontStyles.Underline) == FontStyles.Underline)
					{
						num += 3;
					}
					if ((m_style & FontStyles.Bold) == FontStyles.Bold)
					{
						m_isUsingBold = true;
					}
					if (m_textElementType == TMP_TextElementType.Sprite)
					{
						num3++;
						num2++;
						m_VisibleCharacters.Add((char)(57344 + m_spriteIndex));
						m_textElementType = TMP_TextElementType.Character;
					}
				}
				else
				{
					if (!char.IsWhiteSpace((char)num4))
					{
						num++;
					}
					m_VisibleCharacters.Add((char)num4);
					num2++;
				}
			}
			if (num3 > 0)
			{
				if (m_inlineGraphics == null)
				{
					m_inlineGraphics = (GetComponent<InlineGraphicManager>() ?? base.gameObject.AddComponent<InlineGraphicManager>());
				}
				m_inlineGraphics.AllocatedVertexBuffers(num3);
			}
			else if (m_inlineGraphics != null)
			{
				m_inlineGraphics.ClearUIVertex();
			}
			m_spriteCount = num3;
			if (m_textInfo == null || m_textInfo.characterInfo == null || num2 > m_textInfo.characterInfo.Length)
			{
				if (m_textInfo == null)
				{
					m_textInfo = new TMP_TextInfo();
				}
				m_textInfo.characterInfo = new TMP_CharacterInfo[(num2 <= 1024) ? Mathf.NextPowerOfTwo(num2) : (num2 + 256)];
			}
			if (m_textInfo.meshInfo[0].vertices == null)
			{
				m_textInfo.meshInfo[0] = new TMP_MeshInfo(m_mesh, num);
			}
			if (num * 4 > m_textInfo.meshInfo[0].vertices.Length)
			{
				if (m_isFirstAllocation)
				{
					SetMeshArrays(num);
					m_isFirstAllocation = false;
				}
				else
				{
					SetMeshArrays((num <= 1024) ? Mathf.NextPowerOfTwo(num) : (num + 256));
				}
			}
			return num2;
		}

		protected override void ComputeMarginSize()
		{
			if (base.rectTransform != null)
			{
				m_marginWidth = m_rectTransform.rect.width - m_margin.x - m_margin.z;
				m_marginHeight = m_rectTransform.rect.height - m_margin.y - m_margin.w;
				m_RectTransformCorners = GetTextContainerLocalCorners();
			}
		}

		protected override void OnDidApplyAnimationProperties()
		{
			m_havePropertiesChanged = true;
			SetVerticesDirty();
			SetLayoutDirty();
		}

		protected override void OnTransformParentChanged()
		{
			if (IsActive())
			{
				m_canvas = base.canvas;
				int stencilID = m_stencilID;
				m_stencilID = MaterialManager.GetStencilID(base.gameObject);
				if (stencilID != m_stencilID)
				{
					RecalculateMasking();
				}
				GraphicRegistry.RegisterGraphicForCanvas(base.canvas, this);
				ComputeMarginSize();
				RecalculateClipping();
				SetVerticesDirty();
				SetLayoutDirty();
				m_havePropertiesChanged = true;
			}
		}

		protected override void OnRectTransformDimensionsChange()
		{
			if (base.gameObject.activeInHierarchy)
			{
				ComputeMarginSize();
				SetVerticesDirty();
				SetLayoutDirty();
			}
		}

		private void LateUpdate()
		{
			if (!m_rectTransform.hasChanged)
			{
				return;
			}
			Vector3 lossyScale = m_rectTransform.lossyScale;
			if (lossyScale != m_previousLossyScale)
			{
				if (!m_havePropertiesChanged && m_previousLossyScale.z != 0f && m_text != string.Empty)
				{
					UpdateSDFScale(m_previousLossyScale.z, lossyScale.z);
				}
				m_previousLossyScale = lossyScale;
			}
			m_rectTransform.hasChanged = false;
		}

		private void OnPreRenderCanvas()
		{
			if (!IsActive())
			{
				return;
			}
			if (m_canvas == null)
			{
				m_canvas = base.canvas;
				if (m_canvas == null)
				{
					return;
				}
			}
			loopCountA = 0;
			if (m_havePropertiesChanged || m_isLayoutDirty)
			{
				if (m_isInputParsingRequired || m_isTextTruncated)
				{
					ParseInputText();
				}
				if (m_enableAutoSizing)
				{
					m_fontSize = Mathf.Clamp(m_fontSize, m_fontSizeMin, m_fontSizeMax);
				}
				m_maxFontSize = m_fontSizeMax;
				m_minFontSize = m_fontSizeMin;
				m_lineSpacingDelta = 0f;
				m_charWidthAdjDelta = 0f;
				m_recursiveCount = 0;
				m_isCharacterWrappingEnabled = false;
				m_isTextTruncated = false;
				m_isLayoutDirty = false;
				GenerateTextMesh();
				m_havePropertiesChanged = false;
			}
		}

		protected override void GenerateTextMesh()
		{
			if (m_fontAsset == null || m_fontAsset.characterDictionary == null)
			{
				UnityEngine.Debug.LogWarning("Can't Generate Mesh! No Font Asset has been assigned to Object ID: " + GetInstanceID());
				return;
			}
			if (m_textInfo != null)
			{
				m_textInfo.Clear();
			}
			if (m_char_buffer == null || m_char_buffer.Length == 0 || m_char_buffer[0] == 0)
			{
				ClearMesh();
				if (m_inlineGraphics != null)
				{
					m_inlineGraphics.ClearUIVertex();
				}
				m_preferredWidth = 0f;
				m_preferredHeight = 0f;
				return;
			}
			m_currentFontAsset = m_fontAsset;
			m_currentMaterial = m_sharedMaterial;
			int count = m_VisibleCharacters.Count;
			m_fontScale = m_fontSize / m_currentFontAsset.fontInfo.PointSize;
			float num = m_fontSize / m_fontAsset.fontInfo.PointSize * m_fontAsset.fontInfo.Scale;
			float num2 = m_fontScale;
			m_currentFontSize = m_fontSize;
			m_sizeStack.SetDefault(m_currentFontSize);
			float num3 = 0f;
			int num4 = 0;
			m_style = m_fontStyle;
			m_lineJustification = m_textAlignment;
			if (checkPaddingRequired)
			{
				m_padding = GetPaddingForMaterial();
				checkPaddingRequired = false;
				m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			}
			float num5 = 0f;
			float num6 = 0f;
			float num7 = 1f;
			m_baselineOffset = 0f;
			bool flag = false;
			Vector3 start = Vector3.zero;
			Vector3 zero = Vector3.zero;
			bool flag2 = false;
			Vector3 start2 = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			m_fontColor32 = m_fontColor;
			m_htmlColor = m_fontColor32;
			m_colorStack.SetDefault(m_htmlColor);
			m_styleStack.Clear();
			m_lineOffset = 0f;
			m_lineHeight = 0f;
			m_cSpacing = 0f;
			m_monoSpacing = 0f;
			float num8 = 0f;
			m_xAdvance = 0f;
			tag_LineIndent = 0f;
			tag_Indent = 0f;
			m_indentStack.SetDefault(0f);
			tag_NoParsing = false;
			m_characterCount = 0;
			m_visibleCharacterCount = 0;
			m_visibleSpriteCount = 0;
			m_firstCharacterOfLine = 0;
			m_lastCharacterOfLine = 0;
			m_firstVisibleCharacterOfLine = 0;
			m_lastVisibleCharacterOfLine = 0;
			m_maxLineAscender = float.NegativeInfinity;
			m_maxLineDescender = float.PositiveInfinity;
			m_lineNumber = 0;
			bool flag3 = true;
			m_pageNumber = 0;
			int num9 = Mathf.Clamp(m_pageToDisplay - 1, 0, m_textInfo.pageInfo.Length - 1);
			int num10 = 0;
			Vector4 margin = m_margin;
			float marginWidth = m_marginWidth;
			float marginHeight = m_marginHeight;
			m_marginLeft = 0f;
			m_marginRight = 0f;
			m_width = -1f;
			m_meshExtents = new Extents(k_InfinityVector, -k_InfinityVector);
			m_textInfo.ClearLineInfo();
			m_maxAscender = 0f;
			m_maxDescender = 0f;
			float num11 = 0f;
			float num12 = 0f;
			bool flag4 = false;
			m_isNewPage = false;
			bool flag5 = true;
			bool flag6 = false;
			m_SavedLineState = default(WordWrapState);
			SaveWordWrappingState(ref m_SavedLineState, 0, 0);
			m_SavedWordWrapState = default(WordWrapState);
			int num13 = 0;
			loopCountA++;
			int endIndex = 0;
			for (int i = 0; m_char_buffer[i] != 0; i++)
			{
				num4 = m_char_buffer[i];
				m_textElementType = TMP_TextElementType.Character;
				if (m_isRichText && num4 == 60)
				{
					m_isParsingText = true;
					if (ValidateHtmlTag(m_char_buffer, i + 1, out endIndex))
					{
						i = endIndex;
						if (m_textElementType == TMP_TextElementType.Character)
						{
							continue;
						}
					}
				}
				m_isParsingText = false;
				bool flag7 = false;
				if (m_textElementType == TMP_TextElementType.Character)
				{
					if ((m_style & FontStyles.UpperCase) == FontStyles.UpperCase)
					{
						if (char.IsLower((char)num4))
						{
							num4 = char.ToUpper((char)num4);
						}
					}
					else if ((m_style & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num4))
						{
							num4 = char.ToLower((char)num4);
						}
					}
					else if ((m_fontStyle & FontStyles.SmallCaps) == FontStyles.SmallCaps || (m_style & FontStyles.SmallCaps) == FontStyles.SmallCaps)
					{
						float num14 = m_currentFontSize;
						if ((m_style & FontStyles.Subscript) == FontStyles.Subscript || (m_style & FontStyles.Superscript) == FontStyles.Superscript)
						{
							num14 *= ((!(m_currentFontAsset.fontInfo.SubSize > 0f)) ? 1f : m_currentFontAsset.fontInfo.SubSize);
						}
						if (char.IsLower((char)num4))
						{
							m_fontScale = num14 * 0.8f / m_currentFontAsset.fontInfo.PointSize * m_currentFontAsset.fontInfo.Scale * ((!m_isOrthographic) ? 0.1f : 1f);
							num4 = char.ToUpper((char)num4);
						}
						else
						{
							m_fontScale = num14 / m_currentFontAsset.fontInfo.PointSize * m_currentFontAsset.fontInfo.Scale * ((!m_isOrthographic) ? 0.1f : 1f);
						}
					}
				}
				if (m_textElementType == TMP_TextElementType.Sprite)
				{
					TMP_Sprite sprite = m_inlineGraphics.GetSprite(m_spriteIndex);
					if (sprite == null)
					{
						continue;
					}
					num4 = 57344 + m_spriteIndex;
					m_cached_TextElement = sprite;
					float num15 = m_currentFontSize / m_fontAsset.fontInfo.PointSize * m_fontAsset.fontInfo.Scale * ((!m_isOrthographic) ? 0.1f : 1f);
					num2 = m_fontAsset.fontInfo.Ascender / sprite.height * sprite.scale * num15;
					m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Sprite;
					num5 = 0f;
				}
				else if (m_textElementType == TMP_TextElementType.Character)
				{
					m_currentFontAsset.characterDictionary.TryGetValue(num4, out TMP_Glyph value);
					if (value == null)
					{
						if (char.IsLower((char)num4))
						{
							if (m_currentFontAsset.characterDictionary.TryGetValue(char.ToUpper((char)num4), out value))
							{
								num4 = char.ToUpper((char)num4);
							}
						}
						else if (char.IsUpper((char)num4) && m_currentFontAsset.characterDictionary.TryGetValue(char.ToLower((char)num4), out value))
						{
							num4 = char.ToLower((char)num4);
						}
						if (value == null)
						{
							m_currentFontAsset.characterDictionary.TryGetValue(88, out value);
							if (value == null)
							{
								if (!m_warningsDisabled)
								{
									UnityEngine.Debug.LogWarning("Character with ASCII value of " + num4 + " was not found in the Font Asset Glyph Table.", this);
								}
								continue;
							}
							if (!m_warningsDisabled)
							{
								UnityEngine.Debug.LogWarning("Character with ASCII value of " + num4 + " was not found in the Font Asset Glyph Table.", this);
							}
							num4 = 88;
							flag7 = true;
						}
					}
					m_cached_TextElement = value;
					num2 = m_fontScale;
					m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Character;
					num5 = m_padding;
				}
				m_textInfo.characterInfo[m_characterCount].character = (char)num4;
				m_textInfo.characterInfo[m_characterCount].pointSize = m_currentFontSize;
				m_textInfo.characterInfo[m_characterCount].color = m_htmlColor;
				m_textInfo.characterInfo[m_characterCount].style = m_style;
				m_textInfo.characterInfo[m_characterCount].index = (short)i;
				if (m_enableKerning && m_characterCount >= 1)
				{
					int character = m_textInfo.characterInfo[m_characterCount - 1].character;
					KerningPairKey kerningPairKey = new KerningPairKey(character, num4);
					m_currentFontAsset.kerningDictionary.TryGetValue(kerningPairKey.key, out KerningPair value2);
					if (value2 != null)
					{
						m_xAdvance += value2.XadvanceOffset * num2;
					}
				}
				float num16 = 0f;
				if (m_monoSpacing != 0f)
				{
					num16 = (m_monoSpacing / 2f - (m_cached_TextElement.width / 2f + m_cached_TextElement.xOffset) * num2) * (1f - m_charWidthAdjDelta);
					m_xAdvance += num16;
				}
				if ((m_style & FontStyles.Bold) == FontStyles.Bold || (m_fontStyle & FontStyles.Bold) == FontStyles.Bold)
				{
					num6 = m_currentFontAsset.boldStyle * 2f;
					num7 = 1f + m_currentFontAsset.boldSpacing * 0.01f;
				}
				else
				{
					num6 = m_currentFontAsset.normalStyle * 2f;
					num7 = 1f;
				}
				float baseline = m_currentFontAsset.fontInfo.Baseline;
				Vector3 vector = new Vector3(m_xAdvance + (m_cached_TextElement.xOffset - num5 - num6) * num2 * (1f - m_charWidthAdjDelta), (baseline + m_cached_TextElement.yOffset + num5) * num2 - m_lineOffset + m_baselineOffset, 0f);
				Vector3 vector2 = new Vector3(vector.x, vector.y - (m_cached_TextElement.height + num5 * 2f) * num2, 0f);
				Vector3 vector3 = new Vector3(vector2.x + (m_cached_TextElement.width + num5 * 2f + num6 * 2f) * num2 * (1f - m_charWidthAdjDelta), vector.y, 0f);
				Vector3 vector4 = new Vector3(vector3.x, vector2.y, 0f);
				if ((m_style & FontStyles.Italic) == FontStyles.Italic || (m_fontStyle & FontStyles.Italic) == FontStyles.Italic)
				{
					float num17 = (float)(int)m_currentFontAsset.italicStyle * 0.01f;
					Vector3 b = new Vector3(num17 * ((m_cached_TextElement.yOffset + num5 + num6) * num2), 0f, 0f);
					Vector3 b2 = new Vector3(num17 * ((m_cached_TextElement.yOffset - m_cached_TextElement.height - num5 - num6) * num2), 0f, 0f);
					vector += b;
					vector2 += b2;
					vector3 += b;
					vector4 += b2;
				}
				m_textInfo.characterInfo[m_characterCount].bottomLeft = vector2;
				m_textInfo.characterInfo[m_characterCount].topLeft = vector;
				m_textInfo.characterInfo[m_characterCount].topRight = vector3;
				m_textInfo.characterInfo[m_characterCount].bottomRight = vector4;
				m_textInfo.characterInfo[m_characterCount].scale = num2;
				m_textInfo.characterInfo[m_characterCount].origin = m_xAdvance;
				m_textInfo.characterInfo[m_characterCount].baseLine = 0f - m_lineOffset + m_baselineOffset;
				m_textInfo.characterInfo[m_characterCount].aspectRatio = m_cached_TextElement.width / m_cached_TextElement.height;
				float num18 = m_currentFontAsset.fontInfo.Ascender * ((m_textElementType != 0) ? num : num2) + m_baselineOffset;
				m_textInfo.characterInfo[m_characterCount].ascender = num18 - m_lineOffset;
				m_maxLineAscender = ((!(num18 > m_maxLineAscender)) ? m_maxLineAscender : num18);
				float num19 = m_currentFontAsset.fontInfo.Descender * ((m_textElementType != 0) ? num : num2) + m_baselineOffset;
				float num20 = m_textInfo.characterInfo[m_characterCount].descender = num19 - m_lineOffset;
				m_maxLineDescender = ((!(num19 < m_maxLineDescender)) ? m_maxLineDescender : num19);
				if ((m_style & FontStyles.Subscript) == FontStyles.Subscript || (m_style & FontStyles.Superscript) == FontStyles.Superscript)
				{
					float num21 = (num18 - m_baselineOffset) / m_currentFontAsset.fontInfo.SubSize;
					num18 = m_maxLineAscender;
					m_maxLineAscender = ((!(num21 > m_maxLineAscender)) ? m_maxLineAscender : num21);
					float num22 = (num19 - m_baselineOffset) / m_currentFontAsset.fontInfo.SubSize;
					num19 = m_maxLineDescender;
					m_maxLineDescender = ((!(num22 < m_maxLineDescender)) ? m_maxLineDescender : num22);
				}
				if (m_lineNumber == 0)
				{
					m_maxAscender = ((!(m_maxAscender > num18)) ? num18 : m_maxAscender);
				}
				if (m_lineOffset == 0f)
				{
					num11 = ((!(num11 > num18)) ? num18 : num11);
				}
				m_textInfo.characterInfo[m_characterCount].isVisible = false;
				if (num4 == 9 || !char.IsWhiteSpace((char)num4) || m_textElementType == TMP_TextElementType.Sprite)
				{
					m_textInfo.characterInfo[m_characterCount].isVisible = true;
					float num23 = (m_width == -1f) ? (marginWidth + 0.0001f - m_marginLeft - m_marginRight) : Mathf.Min(marginWidth + 0.0001f - m_marginLeft - m_marginRight, m_width);
					m_textInfo.lineInfo[m_lineNumber].width = num23;
					m_textInfo.lineInfo[m_lineNumber].marginLeft = m_marginLeft;
					if (m_xAdvance + m_cached_TextElement.xAdvance * (1f - m_charWidthAdjDelta) * num2 > num23)
					{
						num10 = m_characterCount - 1;
						if (base.enableWordWrapping && m_characterCount != m_firstCharacterOfLine)
						{
							if (num13 == m_SavedWordWrapState.previous_WordBreak || flag5)
							{
								if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
								{
									if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
									{
										loopCountA = 0;
										m_charWidthAdjDelta += 0.01f;
										GenerateTextMesh();
										return;
									}
									m_maxFontSize = m_fontSize;
									m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
									m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
									if (loopCountA <= 20)
									{
										GenerateTextMesh();
									}
									return;
								}
								if (!m_isCharacterWrappingEnabled)
								{
									m_isCharacterWrappingEnabled = true;
								}
								else
								{
									flag6 = true;
								}
								m_recursiveCount++;
								if (m_recursiveCount > 20)
								{
									continue;
								}
							}
							i = RestoreWordWrappingState(ref m_SavedWordWrapState);
							num13 = i;
							float num24 = 0f;
							if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == 0f && !m_isNewPage)
							{
								float num25 = m_maxLineAscender - m_startOfLineAscender;
								AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num25);
								m_lineOffset += num25;
								m_SavedWordWrapState.lineOffset = m_lineOffset;
								m_SavedWordWrapState.previousLineAscender = m_maxLineAscender;
							}
							m_isNewPage = false;
							float num26 = m_maxLineAscender - m_lineOffset;
							float num27 = m_maxLineDescender - m_lineOffset;
							m_maxDescender = ((!(m_maxDescender < num27)) ? num27 : m_maxDescender);
							if (!flag4)
							{
								num12 = m_maxDescender;
							}
							if (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines)
							{
								flag4 = true;
							}
							m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
							m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = m_firstVisibleCharacterOfLine;
							m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = ((m_characterCount - 1 > 0) ? (m_characterCount - 1) : 0);
							m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = m_lastVisibleCharacterOfLine;
							m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
							m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num27);
							m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num26);
							m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x;
							m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * num2;
							m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
							m_textInfo.lineInfo[m_lineNumber].ascender = num26;
							m_textInfo.lineInfo[m_lineNumber].descender = num27;
							m_firstCharacterOfLine = m_characterCount;
							SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount - 1);
							m_lineNumber++;
							flag3 = true;
							if (m_lineNumber >= m_textInfo.lineInfo.Length)
							{
								ResizeLineExtents(m_lineNumber);
							}
							if (m_lineHeight == 0f)
							{
								float num28 = m_textInfo.characterInfo[m_characterCount].ascender - m_textInfo.characterInfo[m_characterCount].baseLine;
								num8 = 0f - m_maxLineDescender + num28 + (num24 + m_lineSpacing + m_lineSpacingDelta) * num2;
								m_lineOffset += num8;
								m_startOfLineAscender = num28;
							}
							else
							{
								m_lineOffset += m_lineHeight + m_lineSpacing * num;
							}
							m_maxLineAscender = float.NegativeInfinity;
							m_maxLineDescender = float.PositiveInfinity;
							m_xAdvance = tag_Indent;
							continue;
						}
						if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
						{
							if (m_charWidthAdjDelta < m_charWidthMaxAdj / 100f)
							{
								loopCountA = 0;
								m_charWidthAdjDelta += 0.01f;
								GenerateTextMesh();
								return;
							}
							m_maxFontSize = m_fontSize;
							m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
							m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
							m_recursiveCount = 0;
							if (loopCountA <= 20)
							{
								GenerateTextMesh();
							}
							return;
						}
						switch (m_overflowMode)
						{
						case TextOverflowModes.Overflow:
							if (m_isMaskingEnabled)
							{
								DisableMasking();
							}
							break;
						case TextOverflowModes.Ellipsis:
							if (m_isMaskingEnabled)
							{
								DisableMasking();
							}
							m_isTextTruncated = true;
							if (m_characterCount < 1)
							{
								m_textInfo.characterInfo[m_characterCount].isVisible = false;
								m_visibleCharacterCount = 0;
								break;
							}
							m_char_buffer[i - 1] = 8230;
							m_char_buffer[i] = 0;
							GenerateTextMesh();
							return;
						case TextOverflowModes.Masking:
							if (!m_isMaskingEnabled)
							{
								EnableMasking();
							}
							break;
						case TextOverflowModes.ScrollRect:
							if (!m_isMaskingEnabled)
							{
								EnableMasking();
							}
							break;
						case TextOverflowModes.Truncate:
							if (m_isMaskingEnabled)
							{
								DisableMasking();
							}
							m_textInfo.characterInfo[m_characterCount].isVisible = false;
							break;
						}
					}
					if (num4 != 9)
					{
						Color32 vertexColor = flag7 ? ((Color32)Color.red) : ((!m_overrideHtmlColors) ? m_htmlColor : m_fontColor32);
						if (m_textElementType == TMP_TextElementType.Character)
						{
							SaveGlyphVertexInfo(num5, num6, vertexColor);
						}
						else if (m_textElementType == TMP_TextElementType.Sprite)
						{
							SaveSpriteVertexInfo(vertexColor);
						}
					}
					else
					{
						m_textInfo.characterInfo[m_characterCount].isVisible = false;
						m_lastVisibleCharacterOfLine = m_characterCount;
						m_textInfo.lineInfo[m_lineNumber].spaceCount++;
						m_textInfo.spaceCount++;
					}
					if (m_textInfo.characterInfo[m_characterCount].isVisible)
					{
						if (m_textElementType == TMP_TextElementType.Sprite)
						{
							m_visibleSpriteCount++;
						}
						else
						{
							m_visibleCharacterCount++;
						}
						if (flag3)
						{
							flag3 = false;
							m_firstVisibleCharacterOfLine = m_characterCount;
						}
						m_lastVisibleCharacterOfLine = m_characterCount;
					}
				}
				else if (char.IsSeparator((char)num4))
				{
					m_textInfo.lineInfo[m_lineNumber].spaceCount++;
					m_textInfo.spaceCount++;
				}
				if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == 0f && !m_isNewPage)
				{
					float num29 = m_maxLineAscender - m_startOfLineAscender;
					AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num29);
					num20 -= num29;
					m_lineOffset += num29;
					m_startOfLineAscender += num29;
					m_SavedWordWrapState.lineOffset = m_lineOffset;
					m_SavedWordWrapState.previousLineAscender = m_startOfLineAscender;
				}
				m_textInfo.characterInfo[m_characterCount].lineNumber = (short)m_lineNumber;
				m_textInfo.characterInfo[m_characterCount].pageNumber = (short)m_pageNumber;
				if ((num4 != 10 && num4 != 13 && num4 != 8230) || m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
				{
					m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
				}
				if (m_maxAscender - num20 > marginHeight + 0.0001f)
				{
					if (m_enableAutoSizing && m_lineSpacingDelta > m_lineSpacingMax && m_lineNumber > 0)
					{
						m_lineSpacingDelta -= 1f;
						GenerateTextMesh();
						return;
					}
					if (m_enableAutoSizing && m_fontSize > m_fontSizeMin)
					{
						m_maxFontSize = m_fontSize;
						m_fontSize -= Mathf.Max((m_fontSize - m_minFontSize) / 2f, 0.05f);
						m_fontSize = (float)(int)(Mathf.Max(m_fontSize, m_fontSizeMin) * 20f + 0.5f) / 20f;
						m_recursiveCount = 0;
						if (loopCountA <= 20)
						{
							GenerateTextMesh();
						}
						return;
					}
					switch (m_overflowMode)
					{
					case TextOverflowModes.Overflow:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						break;
					case TextOverflowModes.Ellipsis:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						if (m_lineNumber > 0)
						{
							m_char_buffer[m_textInfo.characterInfo[num10].index] = 8230;
							m_char_buffer[m_textInfo.characterInfo[num10].index + 1] = 0;
							GenerateTextMesh();
							m_isTextTruncated = true;
						}
						else
						{
							ClearMesh();
						}
						return;
					case TextOverflowModes.Masking:
						if (!m_isMaskingEnabled)
						{
							EnableMasking();
						}
						break;
					case TextOverflowModes.ScrollRect:
						if (!m_isMaskingEnabled)
						{
							EnableMasking();
						}
						break;
					case TextOverflowModes.Truncate:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						if (m_lineNumber > 0)
						{
							m_char_buffer[m_textInfo.characterInfo[num10].index + 1] = 0;
							m_VisibleCharacters.RemoveRange(num10 + 1, count - (num10 + 1));
							GenerateTextMesh();
							m_isTextTruncated = true;
						}
						else
						{
							ClearMesh();
						}
						return;
					case TextOverflowModes.Page:
						if (m_isMaskingEnabled)
						{
							DisableMasking();
						}
						if (num4 == 13 || num4 == 10)
						{
							break;
						}
						i = RestoreWordWrappingState(ref m_SavedLineState);
						if (i == 0)
						{
							ClearMesh();
							return;
						}
						m_isNewPage = true;
						m_xAdvance = tag_Indent;
						m_lineOffset = 0f;
						m_lineNumber++;
						m_pageNumber++;
						continue;
					}
				}
				if (num4 == 9)
				{
					m_xAdvance += m_currentFontAsset.fontInfo.TabWidth * num2;
				}
				else if (m_monoSpacing != 0f)
				{
					m_xAdvance += (m_monoSpacing - num16 + (m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				}
				else
				{
					m_xAdvance += ((m_cached_TextElement.xAdvance * num7 + m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * num2 + m_cSpacing) * (1f - m_charWidthAdjDelta);
				}
				m_textInfo.characterInfo[m_characterCount].xAdvance = m_xAdvance;
				if (num4 == 13)
				{
					m_xAdvance = tag_Indent;
				}
				if (num4 == 10 || m_characterCount == count - 1)
				{
					float num30 = 0f;
					if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == 0f && !m_isNewPage)
					{
						float num31 = m_maxLineAscender - m_startOfLineAscender;
						AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num31);
						num20 -= num31;
						m_lineOffset += num31;
					}
					m_isNewPage = false;
					float num32 = m_maxLineAscender - m_lineOffset;
					float num33 = m_maxLineDescender - m_lineOffset;
					m_maxDescender = ((!(m_maxDescender < num33)) ? num33 : m_maxDescender);
					if (!flag4)
					{
						num12 = m_maxDescender;
					}
					if (m_characterCount >= m_maxVisibleCharacters || m_lineNumber >= m_maxVisibleLines)
					{
						flag4 = true;
					}
					m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex = m_firstCharacterOfLine;
					m_textInfo.lineInfo[m_lineNumber].firstVisibleCharacterIndex = m_firstVisibleCharacterOfLine;
					m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex = m_characterCount;
					m_textInfo.lineInfo[m_lineNumber].lastVisibleCharacterIndex = ((m_lastVisibleCharacterOfLine < m_firstVisibleCharacterOfLine) ? m_firstVisibleCharacterOfLine : m_lastVisibleCharacterOfLine);
					m_textInfo.lineInfo[m_lineNumber].characterCount = m_textInfo.lineInfo[m_lineNumber].lastCharacterIndex - m_textInfo.lineInfo[m_lineNumber].firstCharacterIndex + 1;
					m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num33);
					m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num32);
					m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x - num5 * num2;
					m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * num2;
					m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
					m_textInfo.lineInfo[m_lineNumber].ascender = num32;
					m_textInfo.lineInfo[m_lineNumber].descender = num33;
					m_firstCharacterOfLine = m_characterCount + 1;
					if (num4 == 10)
					{
						SaveWordWrappingState(ref m_SavedLineState, i, m_characterCount);
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_lineNumber++;
						flag3 = true;
						if (m_lineNumber >= m_textInfo.lineInfo.Length)
						{
							ResizeLineExtents(m_lineNumber);
						}
						if (m_lineHeight == 0f)
						{
							num8 = 0f - m_maxLineDescender + num18 + (num30 + m_lineSpacing + m_paragraphSpacing + m_lineSpacingDelta) * num2;
							m_lineOffset += num8;
						}
						else
						{
							m_lineOffset += m_lineHeight + (m_lineSpacing + m_paragraphSpacing) * num;
						}
						m_maxLineAscender = float.NegativeInfinity;
						m_maxLineDescender = float.PositiveInfinity;
						m_startOfLineAscender = num18;
						m_xAdvance = tag_LineIndent + tag_Indent;
						num10 = m_characterCount - 1;
					}
				}
				if (m_textInfo.characterInfo[m_characterCount].isVisible)
				{
					m_meshExtents.min = new Vector2(Mathf.Min(m_meshExtents.min.x, m_textInfo.characterInfo[m_characterCount].bottomLeft.x), Mathf.Min(m_meshExtents.min.y, m_textInfo.characterInfo[m_characterCount].bottomLeft.y));
					m_meshExtents.max = new Vector2(Mathf.Max(m_meshExtents.max.x, m_textInfo.characterInfo[m_characterCount].topRight.x), Mathf.Max(m_meshExtents.max.y, m_textInfo.characterInfo[m_characterCount].topRight.y));
				}
				if (num4 != 13 && num4 != 10 && m_pageNumber < 16)
				{
					m_textInfo.pageInfo[m_pageNumber].ascender = num11;
					m_textInfo.pageInfo[m_pageNumber].descender = ((!(num19 < m_textInfo.pageInfo[m_pageNumber].descender)) ? m_textInfo.pageInfo[m_pageNumber].descender : num19);
					if (m_pageNumber == 0 && m_characterCount == 0)
					{
						m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
					}
					else if (m_characterCount > 0 && m_pageNumber != m_textInfo.characterInfo[m_characterCount - 1].pageNumber)
					{
						m_textInfo.pageInfo[m_pageNumber - 1].lastCharacterIndex = m_characterCount - 1;
						m_textInfo.pageInfo[m_pageNumber].firstCharacterIndex = m_characterCount;
					}
					else if (m_characterCount == count - 1)
					{
						m_textInfo.pageInfo[m_pageNumber].lastCharacterIndex = m_characterCount;
					}
				}
				if (m_enableWordWrapping || m_overflowMode == TextOverflowModes.Truncate || m_overflowMode == TextOverflowModes.Ellipsis)
				{
					if ((num4 == 9 || num4 == 32) && !m_isNonBreakingSpace)
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag5 = false;
					}
					else if (!m_currentFontAsset.lineBreakingInfo.leadingCharacters.ContainsKey(num4) && m_characterCount < count - 1 && !m_currentFontAsset.lineBreakingInfo.followingCharacters.ContainsKey(m_VisibleCharacters[m_characterCount + 1]) && num4 > 11904 && num4 < 40959)
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag5 = false;
					}
					else if (flag5 || m_isCharacterWrappingEnabled || flag6)
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
					}
				}
				m_characterCount++;
			}
			num3 = m_maxFontSize - m_minFontSize;
			if (!m_isCharacterWrappingEnabled && m_enableAutoSizing && num3 > 0.051f && m_fontSize < m_fontSizeMax)
			{
				m_minFontSize = m_fontSize;
				m_fontSize += Mathf.Max((m_maxFontSize - m_fontSize) / 2f, 0.05f);
				m_fontSize = (float)(int)(Mathf.Min(m_fontSize, m_fontSizeMax) * 20f + 0.5f) / 20f;
				if (loopCountA <= 20)
				{
					GenerateTextMesh();
				}
				return;
			}
			m_isCharacterWrappingEnabled = false;
			if (m_canvas == null || !base.gameObject.activeInHierarchy)
			{
				return;
			}
			if (m_visibleCharacterCount == 0 && m_visibleSpriteCount == 0)
			{
				ClearMesh();
				return;
			}
			int index = m_visibleCharacterCount * 4;
			Array.Clear(m_textInfo.meshInfo[0].vertices, index, m_textInfo.meshInfo[0].vertices.Length - index);
			Vector3 a = Vector3.zero;
			Vector3[] rectTransformCorners = m_RectTransformCorners;
			switch (m_textAlignment)
			{
			case TextAlignmentOptions.TopLeft:
			case TextAlignmentOptions.Top:
			case TextAlignmentOptions.TopRight:
			case TextAlignmentOptions.TopJustified:
				a = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[1] + new Vector3(margin.x, 0f - m_textInfo.pageInfo[num9].ascender - margin.y, 0f)) : (rectTransformCorners[1] + new Vector3(margin.x, 0f - m_maxAscender - margin.y, 0f)));
				break;
			case TextAlignmentOptions.Left:
			case TextAlignmentOptions.Center:
			case TextAlignmentOptions.Right:
			case TextAlignmentOptions.Justified:
				a = ((m_overflowMode == TextOverflowModes.Page) ? ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(margin.x, 0f - (m_textInfo.pageInfo[num9].ascender + margin.y + m_textInfo.pageInfo[num9].descender - margin.w) / 2f, 0f)) : ((rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(margin.x, 0f - (m_maxAscender + margin.y + num12 - margin.w) / 2f, 0f)));
				break;
			case TextAlignmentOptions.BottomLeft:
			case TextAlignmentOptions.Bottom:
			case TextAlignmentOptions.BottomRight:
			case TextAlignmentOptions.BottomJustified:
				a = ((m_overflowMode == TextOverflowModes.Page) ? (rectTransformCorners[0] + new Vector3(margin.x, 0f - m_textInfo.pageInfo[num9].descender + margin.w, 0f)) : (rectTransformCorners[0] + new Vector3(margin.x, 0f - num12 + margin.w, 0f)));
				break;
			case TextAlignmentOptions.BaselineLeft:
			case TextAlignmentOptions.Baseline:
			case TextAlignmentOptions.BaselineRight:
			case TextAlignmentOptions.BaselineJustified:
				a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(margin.x, 0f, 0f);
				break;
			case TextAlignmentOptions.MidlineLeft:
			case TextAlignmentOptions.Midline:
			case TextAlignmentOptions.MidlineRight:
			case TextAlignmentOptions.MidlineJustified:
				a = (rectTransformCorners[0] + rectTransformCorners[1]) / 2f + new Vector3(margin.x, 0f - (m_meshExtents.max.y + margin.y + m_meshExtents.min.y - margin.w) / 2f, 0f);
				break;
			}
			Vector3 vector5 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			int num34 = 0;
			int num35 = 0;
			int num36 = 0;
			int num37 = 0;
			int num38 = 0;
			bool flag8 = false;
			int num39 = 0;
			int num40 = 0;
			if (m_canvas == null)
			{
				m_canvas = base.canvas;
			}
			bool flag9 = (!(m_canvas.worldCamera == null)) ? true : false;
			Vector3 lossyScale = m_rectTransform.lossyScale;
			float num41;
			if (lossyScale.z != 0f)
			{
				Vector3 lossyScale2 = m_rectTransform.lossyScale;
				num41 = lossyScale2.z;
			}
			else
			{
				num41 = 1f;
			}
			float num42 = num41;
			RenderMode renderMode = m_canvas.renderMode;
			float scaleFactor = m_canvas.scaleFactor;
			Color32 underlineColor = Color.white;
			Color32 underlineColor2 = Color.white;
			float num43 = 0f;
			float num44 = 0f;
			float num45 = 0f;
			float num46 = float.PositiveInfinity;
			int num47 = 0;
			float num48 = 0f;
			float num49 = 0f;
			float b3 = 0f;
			TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
			for (int j = 0; j < m_characterCount; j++)
			{
				int lineNumber = characterInfo[j].lineNumber;
				char character2 = characterInfo[j].character;
				TMP_LineInfo tMP_LineInfo = m_textInfo.lineInfo[lineNumber];
				TextAlignmentOptions alignment = tMP_LineInfo.alignment;
				num37 = lineNumber + 1;
				switch (alignment)
				{
				case TextAlignmentOptions.TopLeft:
				case TextAlignmentOptions.Left:
				case TextAlignmentOptions.BottomLeft:
				case TextAlignmentOptions.BaselineLeft:
				case TextAlignmentOptions.MidlineLeft:
					vector5 = new Vector3(tMP_LineInfo.marginLeft, 0f, 0f);
					break;
				case TextAlignmentOptions.Top:
				case TextAlignmentOptions.Center:
				case TextAlignmentOptions.Bottom:
				case TextAlignmentOptions.Baseline:
				case TextAlignmentOptions.Midline:
					vector5 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width / 2f - tMP_LineInfo.maxAdvance / 2f, 0f, 0f);
					break;
				case TextAlignmentOptions.TopRight:
				case TextAlignmentOptions.Right:
				case TextAlignmentOptions.BottomRight:
				case TextAlignmentOptions.BaselineRight:
				case TextAlignmentOptions.MidlineRight:
					vector5 = new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width - tMP_LineInfo.maxAdvance, 0f, 0f);
					break;
				case TextAlignmentOptions.TopJustified:
				case TextAlignmentOptions.Justified:
				case TextAlignmentOptions.BottomJustified:
				case TextAlignmentOptions.BaselineJustified:
				case TextAlignmentOptions.MidlineJustified:
				{
					num4 = m_textInfo.characterInfo[j].character;
					char character3 = m_textInfo.characterInfo[tMP_LineInfo.lastCharacterIndex].character;
					if (char.IsControl(character3) || lineNumber >= m_lineNumber)
					{
						vector5 = new Vector3(tMP_LineInfo.marginLeft, 0f, 0f);
						break;
					}
					float num50 = tMP_LineInfo.width - tMP_LineInfo.maxAdvance;
					float num51 = (tMP_LineInfo.spaceCount <= 2) ? 1f : m_wordWrappingRatios;
					if (lineNumber != num38 || j == 0)
					{
						vector5 = new Vector3(tMP_LineInfo.marginLeft, 0f, 0f);
					}
					else if (num4 == 9 || char.IsSeparator((char)num4))
					{
						int num52 = (tMP_LineInfo.spaceCount - 1 <= 0) ? 1 : (tMP_LineInfo.spaceCount - 1);
						vector5 += new Vector3(num50 * (1f - num51) / (float)num52, 0f, 0f);
					}
					else
					{
						vector5 += new Vector3(num50 * num51 / (float)(tMP_LineInfo.characterCount - tMP_LineInfo.spaceCount - 1), 0f, 0f);
					}
					break;
				}
				}
				zero3 = a + vector5;
				bool isVisible = characterInfo[j].isVisible;
				if (isVisible)
				{
					TMP_TextElementType elementType = characterInfo[j].elementType;
					switch (elementType)
					{
					case TMP_TextElementType.Character:
					{
						Extents lineExtents = tMP_LineInfo.lineExtents;
						float num53 = m_uvLineOffset * (float)lineNumber % 1f + m_uvOffset.x;
						switch (m_horizontalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[j].vertex_BL.uv2.x = m_uvOffset.x;
							characterInfo[j].vertex_TL.uv2.x = m_uvOffset.x;
							characterInfo[j].vertex_TR.uv2.x = 1f + m_uvOffset.x;
							characterInfo[j].vertex_BR.uv2.x = 1f + m_uvOffset.x;
							break;
						case TextureMappingOptions.Line:
							if (m_textAlignment != TextAlignmentOptions.Justified)
							{
								characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
								characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
								characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
								characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num53;
							}
							else
							{
								characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
								characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
								characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
								characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num53;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							switch (m_verticalMapping)
							{
							case TextureMappingOptions.Character:
								characterInfo[j].vertex_BL.uv2.y = m_uvOffset.y;
								characterInfo[j].vertex_TL.uv2.y = 1f + m_uvOffset.y;
								characterInfo[j].vertex_TR.uv2.y = m_uvOffset.y;
								characterInfo[j].vertex_BR.uv2.y = 1f + m_uvOffset.y;
								break;
							case TextureMappingOptions.Line:
								characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num53;
								characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num53;
								characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
								characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num53;
								characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num53;
								characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
								characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								UnityEngine.Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float num54 = (1f - (characterInfo[j].vertex_BL.uv2.y + characterInfo[j].vertex_TL.uv2.y) * characterInfo[j].aspectRatio) / 2f;
							characterInfo[j].vertex_BL.uv2.x = characterInfo[j].vertex_BL.uv2.y * characterInfo[j].aspectRatio + num54 + num53;
							characterInfo[j].vertex_TL.uv2.x = characterInfo[j].vertex_BL.uv2.x;
							characterInfo[j].vertex_TR.uv2.x = characterInfo[j].vertex_TL.uv2.y * characterInfo[j].aspectRatio + num54 + num53;
							characterInfo[j].vertex_BR.uv2.x = characterInfo[j].vertex_TR.uv2.x;
							break;
						}
						}
						switch (m_verticalMapping)
						{
						case TextureMappingOptions.Character:
							characterInfo[j].vertex_BL.uv2.y = m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = 1f + m_uvOffset.y;
							characterInfo[j].vertex_TR.uv2.y = 1f + m_uvOffset.y;
							characterInfo[j].vertex_BR.uv2.y = m_uvOffset.y;
							break;
						case TextureMappingOptions.Line:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender) + m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - tMP_LineInfo.descender) / (tMP_LineInfo.ascender - tMP_LineInfo.descender) + m_uvOffset.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + m_uvOffset.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float num55 = (1f - (characterInfo[j].vertex_BL.uv2.x + characterInfo[j].vertex_TR.uv2.x) / characterInfo[j].aspectRatio) / 2f;
							characterInfo[j].vertex_BL.uv2.y = num55 + characterInfo[j].vertex_BL.uv2.x / characterInfo[j].aspectRatio + m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = num55 + characterInfo[j].vertex_TR.uv2.x / characterInfo[j].aspectRatio + m_uvOffset.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						}
						}
						float num56 = characterInfo[j].scale * (1f - m_charWidthAdjDelta);
						if ((characterInfo[j].style & FontStyles.Bold) == FontStyles.Bold)
						{
							num56 *= -1f;
						}
						switch (renderMode)
						{
						case RenderMode.ScreenSpaceOverlay:
							num56 *= num42 / scaleFactor;
							break;
						case RenderMode.ScreenSpaceCamera:
							num56 *= ((!flag9) ? 1f : num42);
							break;
						case RenderMode.WorldSpace:
							num56 *= num42;
							break;
						}
						float x = characterInfo[j].vertex_BL.uv2.x;
						float y = characterInfo[j].vertex_BL.uv2.y;
						float x2 = characterInfo[j].vertex_TR.uv2.x;
						float y2 = characterInfo[j].vertex_TR.uv2.y;
						float num57 = Mathf.Floor(x);
						float num58 = Mathf.Floor(y);
						x -= num57;
						x2 -= num57;
						y -= num58;
						y2 -= num58;
						characterInfo[j].vertex_BL.uv2 = PackUV(x, y, num56);
						characterInfo[j].vertex_TL.uv2 = PackUV(x, y2, num56);
						characterInfo[j].vertex_TR.uv2 = PackUV(x2, y2, num56);
						characterInfo[j].vertex_BR.uv2 = PackUV(x2, y, num56);
						break;
					}
					}
					if (j < m_maxVisibleCharacters && lineNumber < m_maxVisibleLines && m_overflowMode != TextOverflowModes.Page)
					{
						characterInfo[j].vertex_BL.position += zero3;
						characterInfo[j].vertex_TL.position += zero3;
						characterInfo[j].vertex_TR.position += zero3;
						characterInfo[j].vertex_BR.position += zero3;
					}
					else if (j < m_maxVisibleCharacters && lineNumber < m_maxVisibleLines && m_overflowMode == TextOverflowModes.Page && characterInfo[j].pageNumber == num9)
					{
						characterInfo[j].vertex_BL.position += zero3;
						characterInfo[j].vertex_TL.position += zero3;
						characterInfo[j].vertex_TR.position += zero3;
						characterInfo[j].vertex_BR.position += zero3;
					}
					else
					{
						characterInfo[j].vertex_BL.position *= 0f;
						characterInfo[j].vertex_TL.position *= 0f;
						characterInfo[j].vertex_TR.position *= 0f;
						characterInfo[j].vertex_BR.position *= 0f;
					}
					switch (elementType)
					{
					case TMP_TextElementType.Character:
						FillCharacterVertexBuffers(j, num34);
						num34 += 4;
						break;
					case TMP_TextElementType.Sprite:
						FillSpriteVertexBuffers(j, num35);
						num35 += 4;
						break;
					}
				}
				m_textInfo.characterInfo[j].bottomLeft += zero3;
				m_textInfo.characterInfo[j].topLeft += zero3;
				m_textInfo.characterInfo[j].topRight += zero3;
				m_textInfo.characterInfo[j].bottomRight += zero3;
				m_textInfo.characterInfo[j].origin += zero3.x;
				m_textInfo.characterInfo[j].ascender += zero3.y;
				m_textInfo.characterInfo[j].descender += zero3.y;
				m_textInfo.characterInfo[j].baseLine += zero3.y;
				if (isVisible)
				{
				}
				if (lineNumber != num38 || j == m_characterCount - 1)
				{
					if (lineNumber != num38)
					{
						m_textInfo.lineInfo[num38].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num38].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[num38].descender);
						m_textInfo.lineInfo[num38].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num38].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[num38].ascender);
						m_textInfo.lineInfo[num38].baseline += zero3.y;
						m_textInfo.lineInfo[num38].ascender += zero3.y;
						m_textInfo.lineInfo[num38].descender += zero3.y;
					}
					if (j == m_characterCount - 1)
					{
						m_textInfo.lineInfo[lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[lineNumber].descender);
						m_textInfo.lineInfo[lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[lineNumber].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[lineNumber].ascender);
						m_textInfo.lineInfo[lineNumber].baseline += zero3.y;
						m_textInfo.lineInfo[lineNumber].ascender += zero3.y;
						m_textInfo.lineInfo[lineNumber].descender += zero3.y;
					}
				}
				if (char.IsLetterOrDigit(character2) || character2 == '\'' || character2 == '’')
				{
					if (!flag8)
					{
						flag8 = true;
						num39 = j;
					}
					if (flag8 && j == m_characterCount - 1)
					{
						int num59 = m_textInfo.wordInfo.Length;
						int wordCount = m_textInfo.wordCount;
						if (m_textInfo.wordCount + 1 > num59)
						{
							TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num59 + 1);
						}
						num40 = j;
						m_textInfo.wordInfo[wordCount].firstCharacterIndex = num39;
						m_textInfo.wordInfo[wordCount].lastCharacterIndex = num40;
						m_textInfo.wordInfo[wordCount].characterCount = num40 - num39 + 1;
						m_textInfo.wordInfo[wordCount].textComponent = this;
						num36++;
						m_textInfo.wordCount++;
						m_textInfo.lineInfo[lineNumber].wordCount++;
					}
				}
				else if (flag8 || (j == 0 && (char.IsPunctuation(character2) || char.IsWhiteSpace(character2) || j == m_characterCount - 1)))
				{
					num40 = ((j != m_characterCount - 1 || !char.IsLetterOrDigit(character2)) ? (j - 1) : j);
					flag8 = false;
					int num60 = m_textInfo.wordInfo.Length;
					int wordCount2 = m_textInfo.wordCount;
					if (m_textInfo.wordCount + 1 > num60)
					{
						TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num60 + 1);
					}
					m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num39;
					m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num40;
					m_textInfo.wordInfo[wordCount2].characterCount = num40 - num39 + 1;
					m_textInfo.wordInfo[wordCount2].textComponent = this;
					num36++;
					m_textInfo.wordCount++;
					m_textInfo.lineInfo[lineNumber].wordCount++;
				}
				if ((m_textInfo.characterInfo[j].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool flag10 = true;
					int pageNumber = m_textInfo.characterInfo[j].pageNumber;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != m_pageToDisplay))
					{
						flag10 = false;
					}
					if (!char.IsWhiteSpace(character2))
					{
						num45 = Mathf.Max(num45, m_textInfo.characterInfo[j].scale);
						num46 = Mathf.Min((pageNumber != num47) ? float.PositiveInfinity : num46, m_textInfo.characterInfo[j].baseLine + base.font.fontInfo.Underline * num45);
						num47 = pageNumber;
					}
					if (!flag && flag10 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character2 != '\n' && character2 != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character2)))
					{
						flag = true;
						num43 = m_textInfo.characterInfo[j].scale;
						if (num45 == 0f)
						{
							num45 = num43;
						}
						start = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, num46, 0f);
						underlineColor = m_textInfo.characterInfo[j].color;
					}
					if (flag && m_characterCount == 1)
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num46, 0f);
						num44 = m_textInfo.characterInfo[j].scale;
						DrawUnderlineMesh(start, zero, ref index, num43, num44, num45, underlineColor);
						num45 = 0f;
						num46 = float.PositiveInfinity;
					}
					else if (flag && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
					{
						if (char.IsWhiteSpace(character2))
						{
							int lastVisibleCharacterIndex = tMP_LineInfo.lastVisibleCharacterIndex;
							zero = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num46, 0f);
							num44 = m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num46, 0f);
							num44 = m_textInfo.characterInfo[j].scale;
						}
						flag = false;
						DrawUnderlineMesh(start, zero, ref index, num43, num44, num45, underlineColor);
						num45 = 0f;
						num46 = float.PositiveInfinity;
					}
					else if (flag && !flag10)
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num46, 0f);
						num44 = m_textInfo.characterInfo[j - 1].scale;
						DrawUnderlineMesh(start, zero, ref index, num43, num44, num45, underlineColor);
						num45 = 0f;
						num46 = float.PositiveInfinity;
					}
				}
				else if (flag)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num46, 0f);
					num44 = m_textInfo.characterInfo[j - 1].scale;
					DrawUnderlineMesh(start, zero, ref index, num43, num44, num45, underlineColor);
					num45 = 0f;
					num46 = float.PositiveInfinity;
				}
				if ((m_textInfo.characterInfo[j].style & FontStyles.Strikethrough) == FontStyles.Strikethrough)
				{
					bool flag11 = true;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && m_textInfo.characterInfo[j].pageNumber + 1 != m_pageToDisplay))
					{
						flag11 = false;
					}
					if (!flag2 && flag11 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character2 != '\n' && character2 != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character2)))
					{
						flag2 = true;
						num48 = m_textInfo.characterInfo[j].pointSize;
						num49 = m_textInfo.characterInfo[j].scale;
						start2 = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2.75f * num49, 0f);
						underlineColor2 = m_textInfo.characterInfo[j].color;
						b3 = m_textInfo.characterInfo[j].baseLine;
					}
					if (flag2 && m_characterCount == 1)
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num49, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num49, num49, num49, underlineColor2);
					}
					else if (flag2 && j == tMP_LineInfo.lastCharacterIndex)
					{
						if (!char.IsWhiteSpace(character2))
						{
							zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num49, 0f);
						}
						else
						{
							int lastVisibleCharacterIndex2 = tMP_LineInfo.lastVisibleCharacterIndex;
							zero2 = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num49, 0f);
						}
						flag2 = false;
						DrawUnderlineMesh(start2, zero2, ref index, num49, num49, num49, underlineColor2);
					}
					else if (flag2 && j < m_characterCount && (m_textInfo.characterInfo[j + 1].pointSize != num48 || !TMP_Math.Approximately(m_textInfo.characterInfo[j + 1].baseLine + zero3.y, b3)))
					{
						flag2 = false;
						int lastVisibleCharacterIndex3 = tMP_LineInfo.lastVisibleCharacterIndex;
						zero2 = ((j > lastVisibleCharacterIndex3) ? new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num49, 0f) : new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num49, 0f));
						DrawUnderlineMesh(start2, zero2, ref index, num49, num49, num49, underlineColor2);
					}
					else if (flag2 && !flag11)
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num49, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num49, num49, num49, underlineColor2);
					}
				}
				else if (flag2)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * m_fontScale, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num49, num49, num49, underlineColor2);
				}
				num38 = lineNumber;
			}
			m_textInfo.characterCount = (short)m_characterCount;
			m_textInfo.spriteCount = m_spriteCount;
			m_textInfo.lineCount = (short)num37;
			m_textInfo.wordCount = ((num36 == 0 || m_characterCount <= 0) ? 1 : ((short)num36));
			m_textInfo.pageCount = m_pageNumber + 1;
			if (m_renderMode == TextRenderFlags.Render)
			{
				m_mesh.MarkDynamic();
				m_mesh.vertices = m_textInfo.meshInfo[0].vertices;
				m_mesh.uv = m_textInfo.meshInfo[0].uvs0;
				m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
				m_mesh.colors32 = m_textInfo.meshInfo[0].colors32;
				m_mesh.RecalculateBounds();
				m_uiRenderer.SetMesh(m_mesh);
				if (m_inlineGraphics != null)
				{
					m_inlineGraphics.DrawSprite(m_inlineGraphics.uiVertex, m_visibleSpriteCount);
				}
			}
		}

		protected override void SaveSpriteVertexInfo(Color32 vertexColor)
		{
			Vector2 uv = new Vector2(m_cached_TextElement.x / (float)m_inlineGraphics.spriteAsset.spriteSheet.width, m_cached_TextElement.y / (float)m_inlineGraphics.spriteAsset.spriteSheet.height);
			Vector2 uv2 = new Vector2(uv.x, (m_cached_TextElement.y + m_cached_TextElement.height) / (float)m_inlineGraphics.spriteAsset.spriteSheet.height);
			Vector2 uv3 = new Vector2((m_cached_TextElement.x + m_cached_TextElement.width) / (float)m_inlineGraphics.spriteAsset.spriteSheet.width, uv.y);
			Vector2 uv4 = new Vector2(uv3.x, uv2.y);
			if (m_tintAllSprites)
			{
				m_tintSprite = true;
			}
			//Color32 color = (!m_tintSprite) ? m_spriteColor : m_spriteColor.Multiply(vertexColor);
			//color.a = ((color.a >= m_fontColor32.a) ? m_fontColor32.a : (color.a = ((color.a >= vertexColor.a) ? vertexColor.a : color.a)));
			TMP_Vertex tMP_Vertex = default(TMP_Vertex);
			tMP_Vertex.position = m_textInfo.characterInfo[m_characterCount].bottomLeft;
			tMP_Vertex.uv = uv;
			//tMP_Vertex.color = color;
			m_textInfo.characterInfo[m_characterCount].vertex_BL = tMP_Vertex;
			tMP_Vertex.position = m_textInfo.characterInfo[m_characterCount].topLeft;
			tMP_Vertex.uv = uv2;
			//tMP_Vertex.color = color;
			m_textInfo.characterInfo[m_characterCount].vertex_TL = tMP_Vertex;
			tMP_Vertex.position = m_textInfo.characterInfo[m_characterCount].topRight;
			tMP_Vertex.uv = uv4;
			//tMP_Vertex.color = color;
			m_textInfo.characterInfo[m_characterCount].vertex_TR = tMP_Vertex;
			tMP_Vertex.position = m_textInfo.characterInfo[m_characterCount].bottomRight;
			tMP_Vertex.uv = uv3;
			//tMP_Vertex.color = color;
			m_textInfo.characterInfo[m_characterCount].vertex_BR = tMP_Vertex;
		}

		protected override void FillSpriteVertexBuffers(int i, int spriteIndex_X4)
		{
			m_textInfo.characterInfo[i].vertexIndex = (short)spriteIndex_X4;
			TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
			UIVertex[] uiVertex = m_inlineGraphics.uiVertex;
			UIVertex uIVertex = default(UIVertex);
			uIVertex.position = characterInfo[i].vertex_BL.position;
			uIVertex.uv0 = characterInfo[i].vertex_BL.uv;
			uIVertex.color = characterInfo[i].vertex_BL.color;
			uiVertex[spriteIndex_X4] = uIVertex;
			uIVertex.position = characterInfo[i].vertex_TL.position;
			uIVertex.uv0 = characterInfo[i].vertex_TL.uv;
			uIVertex.color = characterInfo[i].vertex_TL.color;
			uiVertex[spriteIndex_X4 + 1] = uIVertex;
			uIVertex.position = characterInfo[i].vertex_TR.position;
			uIVertex.uv0 = characterInfo[i].vertex_TR.uv;
			uIVertex.color = characterInfo[i].vertex_TR.color;
			uiVertex[spriteIndex_X4 + 2] = uIVertex;
			uIVertex.position = characterInfo[i].vertex_BR.position;
			uIVertex.uv0 = characterInfo[i].vertex_BR.uv;
			uIVertex.color = characterInfo[i].vertex_BR.color;
			uiVertex[spriteIndex_X4 + 3] = uIVertex;
			m_inlineGraphics.SetUIVertex(uiVertex);
		}

		protected override Vector3[] GetTextContainerLocalCorners()
		{
			if (m_rectTransform == null)
			{
				m_rectTransform = base.rectTransform;
			}
			m_rectTransform.GetLocalCorners(m_RectTransformCorners);
			return m_RectTransformCorners;
		}

		private void DrawUnderlineMesh(Vector3 start, Vector3 end, ref int index, float startScale, float endScale, float maxScale, Color32 underlineColor)
		{
			if (m_cached_Underline_GlyphInfo == null)
			{
				if (!m_warningsDisabled)
				{
					UnityEngine.Debug.LogWarning("Unable to add underline since the Font Asset doesn't contain the underline character.", this);
				}
				return;
			}
			int num = index + 12;
			if (num > m_textInfo.meshInfo[0].vertices.Length)
			{
				m_textInfo.meshInfo[0].ResizeMeshInfo(num / 4);
			}
			start.y = Mathf.Min(start.y, end.y);
			end.y = Mathf.Min(start.y, end.y);
			float num2 = m_cached_Underline_GlyphInfo.width / 2f * maxScale;
			if (end.x - start.x < m_cached_Underline_GlyphInfo.width * maxScale)
			{
				num2 = (end.x - start.x) / 2f;
			}
			float num3 = m_padding * startScale / maxScale;
			float num4 = m_padding * endScale / maxScale;
			float height = m_cached_Underline_GlyphInfo.height;
			Vector3[] vertices = m_textInfo.meshInfo[0].vertices;
			vertices[index] = start + new Vector3(0f, 0f - (height + m_padding) * maxScale, 0f);
			vertices[index + 1] = start + new Vector3(0f, m_padding * maxScale, 0f);
			vertices[index + 2] = vertices[index + 1] + new Vector3(num2, 0f, 0f);
			vertices[index + 3] = vertices[index] + new Vector3(num2, 0f, 0f);
			vertices[index + 4] = vertices[index + 3];
			vertices[index + 5] = vertices[index + 2];
			vertices[index + 6] = end + new Vector3(0f - num2, m_padding * maxScale, 0f);
			vertices[index + 7] = end + new Vector3(0f - num2, (0f - (height + m_padding)) * maxScale, 0f);
			vertices[index + 8] = vertices[index + 7];
			vertices[index + 9] = vertices[index + 6];
			vertices[index + 10] = end + new Vector3(0f, m_padding * maxScale, 0f);
			vertices[index + 11] = end + new Vector3(0f, (0f - (height + m_padding)) * maxScale, 0f);
			Vector2[] uvs = m_textInfo.meshInfo[0].uvs0;
			Vector2 vector = new Vector2((m_cached_Underline_GlyphInfo.x - num3) / m_fontAsset.fontInfo.AtlasWidth, 1f - (m_cached_Underline_GlyphInfo.y + m_padding + m_cached_Underline_GlyphInfo.height) / m_fontAsset.fontInfo.AtlasHeight);
			Vector2 vector2 = new Vector2(vector.x, 1f - (m_cached_Underline_GlyphInfo.y - m_padding) / m_fontAsset.fontInfo.AtlasHeight);
			Vector2 vector3 = new Vector2((m_cached_Underline_GlyphInfo.x - num3 + m_cached_Underline_GlyphInfo.width / 2f) / m_fontAsset.fontInfo.AtlasWidth, vector2.y);
			Vector2 vector4 = new Vector2(vector3.x, vector.y);
			Vector2 vector5 = new Vector2((m_cached_Underline_GlyphInfo.x + num4 + m_cached_Underline_GlyphInfo.width / 2f) / m_fontAsset.fontInfo.AtlasWidth, vector2.y);
			Vector2 vector6 = new Vector2(vector5.x, vector.y);
			Vector2 vector7 = new Vector2((m_cached_Underline_GlyphInfo.x + num4 + m_cached_Underline_GlyphInfo.width) / m_fontAsset.fontInfo.AtlasWidth, vector2.y);
			Vector2 vector8 = new Vector2(vector7.x, vector.y);
			uvs[index] = vector;
			uvs[1 + index] = vector2;
			uvs[2 + index] = vector3;
			uvs[3 + index] = vector4;
			uvs[4 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector.y);
			uvs[5 + index] = new Vector2(vector3.x - vector3.x * 0.001f, vector2.y);
			uvs[6 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector2.y);
			uvs[7 + index] = new Vector2(vector3.x + vector3.x * 0.001f, vector.y);
			uvs[8 + index] = vector6;
			uvs[9 + index] = vector5;
			uvs[10 + index] = vector7;
			uvs[11 + index] = vector8;
			float num5 = 0f;
			float x = (vertices[index + 2].x - start.x) / (end.x - start.x);
			Vector3 lossyScale = m_rectTransform.lossyScale;
			float num6 = maxScale * lossyScale.z;
			float scale = num6;
			Vector2[] uvs2 = m_textInfo.meshInfo[0].uvs2;
			uvs2[index] = PackUV(0f, 0f, num6);
			uvs2[1 + index] = PackUV(0f, 1f, num6);
			uvs2[2 + index] = PackUV(x, 1f, num6);
			uvs2[3 + index] = PackUV(x, 0f, num6);
			num5 = (vertices[index + 4].x - start.x) / (end.x - start.x);
			x = (vertices[index + 6].x - start.x) / (end.x - start.x);
			uvs2[4 + index] = PackUV(num5, 0f, scale);
			uvs2[5 + index] = PackUV(num5, 1f, scale);
			uvs2[6 + index] = PackUV(x, 1f, scale);
			uvs2[7 + index] = PackUV(x, 0f, scale);
			num5 = (vertices[index + 8].x - start.x) / (end.x - start.x);
			x = (vertices[index + 6].x - start.x) / (end.x - start.x);
			uvs2[8 + index] = PackUV(num5, 0f, num6);
			uvs2[9 + index] = PackUV(num5, 1f, num6);
			uvs2[10 + index] = PackUV(1f, 1f, num6);
			uvs2[11 + index] = PackUV(1f, 0f, num6);
			Color32[] colors = m_textInfo.meshInfo[0].colors32;
			colors[index] = underlineColor;
			colors[1 + index] = underlineColor;
			colors[2 + index] = underlineColor;
			colors[3 + index] = underlineColor;
			colors[4 + index] = underlineColor;
			colors[5 + index] = underlineColor;
			colors[6 + index] = underlineColor;
			colors[7 + index] = underlineColor;
			colors[8 + index] = underlineColor;
			colors[9 + index] = underlineColor;
			colors[10 + index] = underlineColor;
			colors[11 + index] = underlineColor;
			index += 12;
		}

		private void ClearMesh()
		{
			m_uiRenderer.SetMesh(null);
		}

		private void UpdateSDFScale(float prevScale, float newScale)
		{
			Vector2[] uvs = m_textInfo.meshInfo[0].uvs2;
			for (int i = 0; i < uvs.Length; i++)
			{
				uvs[i].y = uvs[i].y / prevScale * newScale;
			}
			m_mesh.uv2 = uvs;
			m_uiRenderer.SetMesh(m_mesh);
		}

		protected override void AdjustLineOffset(int startIndex, int endIndex, float offset)
		{
			Vector3 vector = new Vector3(0f, offset, 0f);
			for (int i = startIndex; i <= endIndex; i++)
			{
				m_textInfo.characterInfo[i].bottomLeft -= vector;
				m_textInfo.characterInfo[i].topLeft -= vector;
				m_textInfo.characterInfo[i].topRight -= vector;
				m_textInfo.characterInfo[i].bottomRight -= vector;
				m_textInfo.characterInfo[i].ascender -= vector.y;
				m_textInfo.characterInfo[i].baseLine -= vector.y;
				m_textInfo.characterInfo[i].descender -= vector.y;
				if (m_textInfo.characterInfo[i].isVisible)
				{
					m_textInfo.characterInfo[i].vertex_BL.position -= vector;
					m_textInfo.characterInfo[i].vertex_TL.position -= vector;
					m_textInfo.characterInfo[i].vertex_TR.position -= vector;
					m_textInfo.characterInfo[i].vertex_BR.position -= vector;
				}
			}
		}

		public void CalculateLayoutInputHorizontal()
		{
			if (base.gameObject.activeInHierarchy && (m_isCalculateSizeRequired || m_rectTransform.hasChanged))
			{
				m_preferredWidth = GetPreferredWidth();
				ComputeMarginSize();
				m_isLayoutDirty = true;
			}
		}

		public void CalculateLayoutInputVertical()
		{
			if (base.gameObject.activeInHierarchy)
			{
				if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
				{
					m_preferredHeight = GetPreferredHeight();
					ComputeMarginSize();
					m_isLayoutDirty = true;
				}
				m_isCalculateSizeRequired = false;
			}
		}

		public override void SetVerticesDirty()
		{
			if (!m_verticesAlreadyDirty && IsActive())
			{
				m_verticesAlreadyDirty = true;
				CanvasUpdateRegistry.RegisterCanvasElementForGraphicRebuild(this);
			}
		}

		public override void SetLayoutDirty()
		{
			if (!m_layoutAlreadyDirty && IsActive())
			{
				m_layoutAlreadyDirty = true;
				LayoutRebuilder.MarkLayoutForRebuild(base.rectTransform);
				m_isLayoutDirty = true;
			}
		}

		public override void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.PreRender)
			{
				OnPreRenderCanvas();
				m_verticesAlreadyDirty = false;
				m_layoutAlreadyDirty = false;
			}
		}

		public override void RecalculateClipping()
		{
			base.RecalculateClipping();
		}

		public override void RecalculateMasking()
		{
			if (m_fontAsset == null || !m_isAwake)
			{
				return;
			}
			m_stencilID = MaterialManager.GetStencilID(base.gameObject);
			if (m_stencilID == 0)
			{
				if (m_maskingMaterial != null)
				{
					MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
					m_maskingMaterial = null;
					m_sharedMaterial = m_baseMaterial;
				}
				else if (m_fontMaterial != null)
				{
					m_sharedMaterial = MaterialManager.SetStencil(m_fontMaterial, 0);
				}
				else
				{
					m_sharedMaterial = m_baseMaterial;
				}
			}
			else
			{
				ShaderUtilities.GetShaderPropertyIDs();
				if (m_fontMaterial != null)
				{
					m_sharedMaterial = MaterialManager.SetStencil(m_fontMaterial, m_stencilID);
				}
				else if (m_maskingMaterial == null)
				{
					m_maskingMaterial = MaterialManager.GetStencilMaterial(m_baseMaterial, m_stencilID);
					m_sharedMaterial = m_maskingMaterial;
				}
				else if (m_maskingMaterial.GetInt(ShaderUtilities.ID_StencilID) != m_stencilID || m_isNewBaseMaterial)
				{
					MaterialManager.ReleaseStencilMaterial(m_maskingMaterial);
					m_maskingMaterial = MaterialManager.GetStencilMaterial(m_baseMaterial, m_stencilID);
					m_sharedMaterial = m_maskingMaterial;
				}
				if (m_isMaskingEnabled)
				{
					EnableMasking();
				}
			}
			m_uiRenderer.SetMaterial(m_sharedMaterial, m_sharedMaterial.mainTexture);
			m_padding = ShaderUtilities.GetPadding(m_sharedMaterial, m_enableExtraPadding, m_isUsingBold);
		}

		public override void UpdateMeshPadding()
		{
			m_padding = ShaderUtilities.GetPadding(new Material[1]
			{
				m_uiRenderer.GetMaterial()
			}, m_enableExtraPadding, m_isUsingBold);
			m_havePropertiesChanged = true;
		}

		public override void ForceMeshUpdate()
		{
			OnPreRenderCanvas();
		}

		public void UpdateFontAsset()
		{
			LoadFontAsset();
		}
	}
}
