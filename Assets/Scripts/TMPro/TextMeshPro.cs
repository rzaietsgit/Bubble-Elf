using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace TMPro
{
	[ExecuteInEditMode]
	[RequireComponent(typeof(TextContainer))]
	[RequireComponent(typeof(MeshRenderer))]
	[RequireComponent(typeof(MeshFilter))]
	[AddComponentMenu("Mesh/TextMesh Pro")]
	public class TextMeshPro : TMP_Text, ILayoutElement
	{
		[SerializeField]
		private Vector2 m_uvOffset = Vector2.zero;

		[SerializeField]
		private float m_uvLineOffset;

		[SerializeField]
		private bool m_hasFontAssetChanged;

		[SerializeField]
		private bool m_isRightToLeft;

		private Vector3 m_previousLossyScale;

		[SerializeField]
		private Renderer m_renderer;

		private MeshFilter m_meshFilter;

		private bool m_isFirstAllocation;

		private int m_max_characters = 8;

		private int m_max_numberOfLines = 4;

		private WordWrapState m_SavedWordWrapState = default(WordWrapState);

		private WordWrapState m_SavedLineState = default(WordWrapState);

		private Bounds m_default_bounds = new Bounds(Vector3.zero, new Vector3(1000f, 1000f, 0f));

		private Dictionary<int, Material> m_referencedMaterials = new Dictionary<int, Material>();

		private List<Material> m_sharedMaterials = new List<Material>(16);

		private bool m_isMaskingEnabled;

		private bool isMaskUpdateRequired;

		[SerializeField]
		private MaskingTypes m_maskType;

		private Matrix4x4 m_EnvMapMatrix = default(Matrix4x4);

		private TextContainer m_textContainer;

		[NonSerialized]
		private bool m_isRegisteredForEvents;

		private int m_recursiveCount;

		private int loopCountA;

		private float m_lineLength;

		private TMP_Compatibility.AnchorPositions m_anchor;

		private bool m_autoSizeTextContainer;

		private bool m_currentAutoSizeMode;

		public override Material fontSharedMaterial
		{
			get
			{
				return m_renderer.sharedMaterial;
			}
			set
			{
				if (m_sharedMaterial != value)
				{
					SetFontSharedMaterial(value);
					m_havePropertiesChanged = true;
					SetVerticesDirty();
				}
			}
		}

		[Obsolete("The length of the line is now controlled by the size of the text container and margins.")]
		public float lineLength
		{
			get
			{
				return m_lineLength;
			}
			set
			{
				UnityEngine.Debug.Log("lineLength set called.");
			}
		}

		[Obsolete("The length of the line is now controlled by the size of the text container and margins.")]
		public TMP_Compatibility.AnchorPositions anchor => m_anchor;

		public override Vector4 margin
		{
			get
			{
				return m_margin;
			}
			set
			{
				m_margin = value;
				textContainer.margins = m_margin;
				ComputeMarginSize();
				m_havePropertiesChanged = true;
				SetVerticesDirty();
			}
		}

		public int sortingLayerID
		{
			get
			{
				return m_renderer.sortingLayerID;
			}
			set
			{
				m_renderer.sortingLayerID = value;
			}
		}

		public int sortingOrder
		{
			get
			{
				return m_renderer.sortingOrder;
			}
			set
			{
				m_renderer.sortingOrder = value;
			}
		}

		public override bool autoSizeTextContainer
		{
			get
			{
				return m_autoSizeTextContainer;
			}
			set
			{
				m_autoSizeTextContainer = value;
				if (m_autoSizeTextContainer)
				{
					TMP_UpdateManager.RegisterTextElementForLayoutRebuild(this);
					SetLayoutDirty();
				}
			}
		}

		public TextContainer textContainer
		{
			get
			{
				if (m_textContainer == null)
				{
					m_textContainer = GetComponent<TextContainer>();
				}
				return m_textContainer;
			}
		}

		public new Transform transform
		{
			get
			{
				if (m_transform == null)
				{
					m_transform = GetComponent<Transform>();
				}
				return m_transform;
			}
		}

		public Renderer renderer
		{
			get
			{
				if (m_renderer == null)
				{
					m_renderer = GetComponent<Renderer>();
				}
				return m_renderer;
			}
		}

		public override Mesh mesh => m_mesh;

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

		public MaskingTypes maskType
		{
			get
			{
				return m_maskType;
			}
			set
			{
				m_maskType = value;
				SetMask(m_maskType);
			}
		}

		protected override void Awake()
		{
			if (m_fontColor == Color.white && m_fontColor32 != Color.white)
			{
				UnityEngine.Debug.LogWarning("Converting Vertex Colors from Color32 to Color.", this);
				m_fontColor = m_fontColor32;
			}
			m_textContainer = GetComponent<TextContainer>();
			m_renderer = GetComponent<Renderer>();
			if (m_renderer == null)
			{
				m_renderer = base.gameObject.AddComponent<Renderer>();
			}
			if (base.canvasRenderer != null)
			{
				base.canvasRenderer.hideFlags = HideFlags.HideInInspector;
			}
			m_rectTransform = base.rectTransform;
			m_transform = transform;
			m_meshFilter = GetComponent<MeshFilter>();
			if (m_meshFilter == null)
			{
				m_meshFilter = base.gameObject.AddComponent<MeshFilter>();
			}
			if (m_mesh == null)
			{
				m_mesh = new Mesh();
				m_mesh.hideFlags = HideFlags.HideAndDontSave;
				m_meshFilter.mesh = m_mesh;
			}
			m_meshFilter.hideFlags = HideFlags.HideInInspector;
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
			m_char_buffer = new int[m_max_characters];
			m_cached_TextElement = new TMP_Glyph();
			m_isFirstAllocation = true;
			m_textInfo = new TMP_TextInfo(this);
			if (m_fontAsset == null)
			{
				UnityEngine.Debug.LogWarning("Please assign a Font Asset to this " + transform.name + " gameobject.", this);
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
			if (m_meshFilter.sharedMesh == null)
			{
				m_meshFilter.mesh = m_mesh;
			}
			if (!m_isRegisteredForEvents)
			{
				m_isRegisteredForEvents = true;
			}
			ComputeMarginSize();
			m_verticesAlreadyDirty = false;
			SetVerticesDirty();
		}

		protected override void OnDisable()
		{
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
		}

		protected override void OnDestroy()
		{
			if (m_mesh != null)
			{
				UnityEngine.Object.DestroyImmediate(m_mesh);
			}
			m_isRegisteredForEvents = false;
			TMP_UpdateManager.UnRegisterTextElementForRebuild(this);
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
				m_renderer.sharedMaterial = m_fontAsset.material;
				m_sharedMaterial = m_fontAsset.material;
				m_sharedMaterial.SetFloat("_CullMode", 0f);
				m_sharedMaterial.SetFloat("_ZTestMode", 4f);
				m_renderer.receiveShadows = false;
				m_renderer.shadowCastingMode = ShadowCastingMode.Off;
			}
			else
			{
				if (m_fontAsset.characterDictionary == null)
				{
					m_fontAsset.ReadFontDefinition();
				}
				if (m_renderer.sharedMaterial == null || m_renderer.sharedMaterial.mainTexture == null || m_fontAsset.atlas.GetInstanceID() != m_renderer.sharedMaterial.GetTexture(ShaderUtilities.ID_MainTex).GetInstanceID())
				{
					m_renderer.sharedMaterial = m_fontAsset.material;
					m_sharedMaterial = m_fontAsset.material;
				}
				else
				{
					m_sharedMaterial = m_renderer.sharedMaterial;
				}
				m_sharedMaterial.SetFloat("_ZTestMode", 4f);
				if (m_sharedMaterial.passCount > 1)
				{
					m_renderer.receiveShadows = true;
					m_renderer.shadowCastingMode = ShadowCastingMode.On;
				}
				else
				{
					m_renderer.receiveShadows = false;
					m_renderer.shadowCastingMode = ShadowCastingMode.Off;
				}
			}
			m_padding = GetPaddingForMaterial();
			m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			if (!m_fontAsset.characterDictionary.TryGetValue(95, out m_cached_Underline_GlyphInfo) && m_settings == null && !m_settings.warningsDisabled)
			{
				UnityEngine.Debug.LogWarning("Underscore character wasn't found in the current Font Asset. No characters assigned for Underline.", this);
			}
			if (!m_fontAsset_Dict.ContainsKey(m_fontAsset.fontHashCode))
			{
				m_fontAsset_Dict.Add(m_fontAsset.fontHashCode, m_fontAsset);
			}
			int simpleHashCode = TMP_TextUtilities.GetSimpleHashCode(m_sharedMaterial.name);
			if (!m_fontMaterial_Dict.ContainsKey(simpleHashCode))
			{
				m_fontMaterial_Dict.Add(simpleHashCode, m_sharedMaterial);
			}
			m_sharedMaterials.Add(m_sharedMaterial);
		}

		private void ScheduleUpdate()
		{
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

		private void SetMask(MaskingTypes maskType)
		{
			switch (maskType)
			{
			case MaskingTypes.MaskOff:
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				break;
			case MaskingTypes.MaskSoft:
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				break;
			case MaskingTypes.MaskHard:
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				break;
			}
		}

		private void SetMaskCoordinates(Vector4 coords)
		{
			m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
		}

		private void SetMaskCoordinates(Vector4 coords, float softX, float softY)
		{
			m_sharedMaterial.SetVector(ShaderUtilities.ID_ClipRect, coords);
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, softX);
			m_sharedMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, softY);
		}

		private void EnableMasking()
		{
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				m_sharedMaterial.EnableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				m_isMaskingEnabled = true;
				UpdateMask();
			}
		}

		private void DisableMasking()
		{
			if (m_sharedMaterial.HasProperty(ShaderUtilities.ID_ClipRect))
			{
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_SOFT);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_HARD);
				m_sharedMaterial.DisableKeyword(ShaderUtilities.Keyword_MASK_TEX);
				m_isMaskingEnabled = false;
				UpdateMask();
			}
		}

		private void UpdateMask()
		{
			if (m_isMaskingEnabled)
			{
				if (m_isMaskingEnabled && m_fontMaterial == null)
				{
					CreateMaterialInstance();
				}
				Vector4 margins = m_textContainer.margins;
				float x = margins.x;
				Vector4 margins2 = m_textContainer.margins;
				float num = Mathf.Min(Mathf.Min(x, margins2.z), m_sharedMaterial.GetFloat(ShaderUtilities.ID_MaskSoftnessX));
				Vector4 margins3 = m_textContainer.margins;
				float y = margins3.y;
				Vector4 margins4 = m_textContainer.margins;
				float num2 = Mathf.Min(Mathf.Min(y, margins4.w), m_sharedMaterial.GetFloat(ShaderUtilities.ID_MaskSoftnessY));
				num = ((!(num > 0f)) ? 0f : num);
				num2 = ((!(num2 > 0f)) ? 0f : num2);
				float width = m_textContainer.width;
				Vector4 margins5 = m_textContainer.margins;
				float num3 = width - Mathf.Max(margins5.x, 0f);
				Vector4 margins6 = m_textContainer.margins;
				float z = (num3 - Mathf.Max(margins6.z, 0f)) / 2f + num;
				float height = m_textContainer.height;
				Vector4 margins7 = m_textContainer.margins;
				float num4 = height - Mathf.Max(margins7.y, 0f);
				Vector4 margins8 = m_textContainer.margins;
				float w = (num4 - Mathf.Max(margins8.w, 0f)) / 2f + num2;
				Vector2 pivot = m_textContainer.pivot;
				float num5 = (0.5f - pivot.x) * m_textContainer.width;
				Vector4 margins9 = m_textContainer.margins;
				float num6 = Mathf.Max(margins9.x, 0f);
				Vector4 margins10 = m_textContainer.margins;
				float x2 = num5 + (num6 - Mathf.Max(margins10.z, 0f)) / 2f;
				Vector2 pivot2 = m_textContainer.pivot;
				float num7 = (0.5f - pivot2.y) * m_textContainer.height;
				Vector4 margins11 = m_textContainer.margins;
				float num8 = 0f - Mathf.Max(margins11.y, 0f);
				Vector4 margins12 = m_textContainer.margins;
				Vector2 vector = new Vector2(x2, num7 + (num8 + Mathf.Max(margins12.w, 0f)) / 2f);
				Vector4 value = new Vector4(vector.x, vector.y, z, w);
				m_fontMaterial.SetVector(ShaderUtilities.ID_ClipRect, value);
				m_fontMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessX, num);
				m_fontMaterial.SetFloat(ShaderUtilities.ID_MaskSoftnessY, num2);
			}
		}

		protected override void SetFontMaterial(Material mat)
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<Renderer>();
			}
			m_renderer.material = mat;
			m_fontMaterial = m_renderer.material;
			m_sharedMaterial = m_fontMaterial;
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetFontSharedMaterial(Material mat)
		{
			if (m_renderer == null)
			{
				m_renderer = GetComponent<Renderer>();
			}
			m_renderer.sharedMaterial = mat;
			m_sharedMaterial = m_renderer.sharedMaterial;
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetOutlineThickness(float thickness)
		{
			thickness = Mathf.Clamp01(thickness);
			m_renderer.material.SetFloat(ShaderUtilities.ID_OutlineWidth, thickness);
			if (m_fontMaterial == null)
			{
				m_fontMaterial = m_renderer.material;
			}
			m_fontMaterial = m_renderer.material;
			m_sharedMaterial = m_fontMaterial;
			m_padding = GetPaddingForMaterial();
		}

		protected override void SetFaceColor(Color32 color)
		{
			m_renderer.material.SetColor(ShaderUtilities.ID_FaceColor, color);
			if (m_fontMaterial == null)
			{
				m_fontMaterial = m_renderer.material;
			}
			m_sharedMaterial = m_fontMaterial;
		}

		protected override void SetOutlineColor(Color32 color)
		{
			m_renderer.material.SetColor(ShaderUtilities.ID_OutlineColor, color);
			if (m_fontMaterial == null)
			{
				m_fontMaterial = m_renderer.material;
			}
			m_sharedMaterial = m_fontMaterial;
		}

		private void CreateMaterialInstance()
		{
			Material material = new Material(m_sharedMaterial);
			material.shaderKeywords = m_sharedMaterial.shaderKeywords;
			material.name += " Instance";
			m_fontMaterial = material;
		}

		private void SetShaderType()
		{
			if (m_isOverlay)
			{
				m_renderer.material.SetFloat("_ZTestMode", 8f);
				m_renderer.material.renderQueue = 4000;
				m_sharedMaterial = m_renderer.material;
			}
			else
			{
				m_renderer.material.SetFloat("_ZTestMode", 4f);
				m_renderer.material.renderQueue = -1;
				m_sharedMaterial = m_renderer.material;
			}
		}

		protected override void SetCulling()
		{
			if (m_isCullingEnabled)
			{
				m_renderer.material.SetFloat("_CullMode", 2f);
			}
			else
			{
				m_renderer.material.SetFloat("_CullMode", 0f);
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
			m_mesh.bounds = m_default_bounds;
		}

		protected override int SetArraySizes(int[] chars)
		{
			int num = 0;
			int num2 = 0;
			int endIndex = 0;
			m_isUsingBold = false;
			m_isParsingText = false;
			m_VisibleCharacters.Clear();
			m_currentFontAsset = m_fontAsset;
			for (int i = 0; chars[i] != 0; i++)
			{
				int num3 = chars[i];
				if (m_isRichText && num3 == 60 && ValidateHtmlTag(chars, i + 1, out endIndex))
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
				}
				else
				{
					if (!char.IsWhiteSpace((char)num3))
					{
						num++;
					}
					m_VisibleCharacters.Add((char)num3);
					num2++;
				}
			}
			if (m_textInfo.characterInfo == null || num2 > m_textInfo.characterInfo.Length)
			{
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
			if (m_textContainer != null)
			{
				Vector4 margins = m_textContainer.margins;
				m_marginWidth = m_textContainer.rect.width - margins.z - margins.x;
				m_marginHeight = m_textContainer.rect.height - margins.y - margins.w;
			}
		}

		protected override void OnDidApplyAnimationProperties()
		{
			m_havePropertiesChanged = true;
			isMaskUpdateRequired = true;
			SetVerticesDirty();
		}

		protected override void OnTransformParentChanged()
		{
			SetVerticesDirty();
			SetLayoutDirty();
		}

		protected override void OnRectTransformDimensionsChange()
		{
			ComputeMarginSize();
			SetVerticesDirty();
			SetLayoutDirty();
		}

		private void LateUpdate()
		{
			if (!m_transform.hasChanged)
			{
				return;
			}
			Vector3 lossyScale = m_transform.lossyScale;
			if (lossyScale != m_previousLossyScale)
			{
				if (!m_havePropertiesChanged && m_previousLossyScale.z != 0f && m_text != string.Empty)
				{
					UpdateSDFScale(m_previousLossyScale.z, lossyScale.z);
				}
				m_previousLossyScale = lossyScale;
			}
		}

		private void OnPreRenderObject()
		{
			if (!IsActive())
			{
				return;
			}
			loopCountA = 0;
			if (m_transform.hasChanged)
			{
				m_transform.hasChanged = false;
				if (m_textContainer != null && m_textContainer.hasChanged)
				{
					ComputeMarginSize();
					isMaskUpdateRequired = true;
					m_textContainer.hasChanged = false;
					m_havePropertiesChanged = true;
				}
			}
			if (m_havePropertiesChanged || m_isLayoutDirty)
			{
				if (isMaskUpdateRequired)
				{
					UpdateMask();
					isMaskUpdateRequired = false;
				}
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
				m_havePropertiesChanged = false;
				m_isLayoutDirty = false;
				GenerateTextMesh();
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
				m_preferredWidth = 0f;
				m_preferredHeight = 0f;
				return;
			}
			m_currentFontAsset = m_fontAsset;
			m_currentMaterial = m_sharedMaterial;
			int count = m_VisibleCharacters.Count;
			m_fontScale = m_fontSize / m_currentFontAsset.fontInfo.PointSize * ((!m_isOrthographic) ? 0.1f : 1f);
			float num = m_fontSize / m_fontAsset.fontInfo.PointSize * m_fontAsset.fontInfo.Scale * ((!m_isOrthographic) ? 0.1f : 1f);
			float fontScale = m_fontScale;
			m_currentFontSize = m_fontSize;
			m_sizeStack.SetDefault(m_currentFontSize);
			float num2 = 0f;
			int num3 = 0;
			m_style = m_fontStyle;
			m_lineJustification = m_textAlignment;
			if (checkPaddingRequired)
			{
				m_padding = GetPaddingForMaterial();
				checkPaddingRequired = false;
				m_isMaskingEnabled = ShaderUtilities.IsMaskingEnabled(m_sharedMaterial);
			}
			float num4 = 0f;
			float num5 = 0f;
			float num6 = 1f;
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
			float num7 = 0f;
			m_xAdvance = 0f;
			tag_LineIndent = 0f;
			tag_Indent = 0f;
			m_indentStack.SetDefault(0f);
			tag_NoParsing = false;
			m_characterCount = 0;
			m_visibleCharacterCount = 0;
			m_firstCharacterOfLine = 0;
			m_lastCharacterOfLine = 0;
			m_firstVisibleCharacterOfLine = 0;
			m_lastVisibleCharacterOfLine = 0;
			m_maxLineAscender = float.NegativeInfinity;
			m_maxLineDescender = float.PositiveInfinity;
			m_lineNumber = 0;
			bool flag3 = true;
			m_pageNumber = 0;
			int num8 = Mathf.Clamp(m_pageToDisplay - 1, 0, m_textInfo.pageInfo.Length - 1);
			int num9 = 0;
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
			float num10 = 0f;
			float num11 = 0f;
			bool flag4 = false;
			m_isNewPage = false;
			bool flag5 = true;
			bool flag6 = false;
			m_SavedLineState = default(WordWrapState);
			SaveWordWrappingState(ref m_SavedLineState, 0, 0);
			m_SavedWordWrapState = default(WordWrapState);
			int num12 = 0;
			loopCountA++;
			int endIndex = 0;
			for (int i = 0; m_char_buffer[i] != 0; i++)
			{
				num3 = m_char_buffer[i];
				m_textElementType = TMP_TextElementType.Character;
				if (m_isRichText && num3 == 60)
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
						if (char.IsLower((char)num3))
						{
							num3 = char.ToUpper((char)num3);
						}
					}
					else if ((m_style & FontStyles.LowerCase) == FontStyles.LowerCase)
					{
						if (char.IsUpper((char)num3))
						{
							num3 = char.ToLower((char)num3);
						}
					}
					else if ((m_fontStyle & FontStyles.SmallCaps) == FontStyles.SmallCaps || (m_style & FontStyles.SmallCaps) == FontStyles.SmallCaps)
					{
						float num13 = m_currentFontSize;
						if ((m_style & FontStyles.Subscript) == FontStyles.Subscript || (m_style & FontStyles.Superscript) == FontStyles.Superscript)
						{
							num13 *= ((!(m_currentFontAsset.fontInfo.SubSize > 0f)) ? 1f : m_currentFontAsset.fontInfo.SubSize);
						}
						if (char.IsLower((char)num3))
						{
							m_fontScale = num13 * 0.8f / m_currentFontAsset.fontInfo.PointSize * m_currentFontAsset.fontInfo.Scale * ((!m_isOrthographic) ? 0.1f : 1f);
							num3 = char.ToUpper((char)num3);
						}
						else
						{
							m_fontScale = num13 / m_currentFontAsset.fontInfo.PointSize * m_currentFontAsset.fontInfo.Scale * ((!m_isOrthographic) ? 0.1f : 1f);
						}
					}
				}
				if (m_textElementType != TMP_TextElementType.Sprite && m_textElementType == TMP_TextElementType.Character)
				{
					m_currentFontAsset.characterDictionary.TryGetValue(num3, out TMP_Glyph value);
					if (value == null)
					{
						if (char.IsLower((char)num3))
						{
							if (m_currentFontAsset.characterDictionary.TryGetValue(char.ToUpper((char)num3), out value))
							{
								num3 = char.ToUpper((char)num3);
							}
						}
						else if (char.IsUpper((char)num3) && m_currentFontAsset.characterDictionary.TryGetValue(char.ToLower((char)num3), out value))
						{
							num3 = char.ToLower((char)num3);
						}
						if (value == null)
						{
							m_currentFontAsset.characterDictionary.TryGetValue(88, out value);
							if (value == null)
							{
								if (!m_warningsDisabled)
								{
									UnityEngine.Debug.LogWarning("Character with ASCII value of " + num3 + " was not found in the Font Asset Glyph Table.", this);
								}
								continue;
							}
							if (!m_warningsDisabled)
							{
								UnityEngine.Debug.LogWarning("Character with ASCII value of " + num3 + " was not found in the Font Asset Glyph Table.", this);
							}
							num3 = 88;
							flag7 = true;
						}
					}
					m_cached_TextElement = value;
					fontScale = m_fontScale;
					m_textInfo.characterInfo[m_characterCount].elementType = TMP_TextElementType.Character;
					num4 = m_padding;
				}
				if (m_isRightToLeft)
				{
					m_xAdvance -= ((m_cached_TextElement.xAdvance * num6 + m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * fontScale + m_cSpacing) * (1f - m_charWidthAdjDelta);
				}
				m_textInfo.characterInfo[m_characterCount].character = (char)num3;
				m_textInfo.characterInfo[m_characterCount].pointSize = m_currentFontSize;
				m_textInfo.characterInfo[m_characterCount].color = m_htmlColor;
				m_textInfo.characterInfo[m_characterCount].style = m_style;
				m_textInfo.characterInfo[m_characterCount].index = (short)i;
				if (m_enableKerning && m_characterCount >= 1)
				{
					int character = m_textInfo.characterInfo[m_characterCount - 1].character;
					KerningPairKey kerningPairKey = new KerningPairKey(character, num3);
					m_currentFontAsset.kerningDictionary.TryGetValue(kerningPairKey.key, out KerningPair value2);
					if (value2 != null)
					{
						m_xAdvance += value2.XadvanceOffset * fontScale;
					}
				}
				float num14 = 0f;
				if (m_monoSpacing != 0f)
				{
					num14 = (m_monoSpacing / 2f - (m_cached_TextElement.width / 2f + m_cached_TextElement.xOffset) * fontScale) * (1f - m_charWidthAdjDelta);
					m_xAdvance += num14;
				}
				if ((m_style & FontStyles.Bold) == FontStyles.Bold || (m_fontStyle & FontStyles.Bold) == FontStyles.Bold)
				{
					num5 = m_currentFontAsset.boldStyle * 2f;
					num6 = 1f + m_currentFontAsset.boldSpacing * 0.01f;
				}
				else
				{
					num5 = m_currentFontAsset.normalStyle * 2f;
					num6 = 1f;
				}
				float baseline = m_currentFontAsset.fontInfo.Baseline;
				Vector3 vector = new Vector3(m_xAdvance + (m_cached_TextElement.xOffset - num4 - num5) * fontScale * (1f - m_charWidthAdjDelta), (baseline + m_cached_TextElement.yOffset + num4) * fontScale - m_lineOffset + m_baselineOffset, 0f);
				Vector3 vector2 = new Vector3(vector.x, vector.y - (m_cached_TextElement.height + num4 * 2f) * fontScale, 0f);
				Vector3 vector3 = new Vector3(vector2.x + (m_cached_TextElement.width + num4 * 2f + num5 * 2f) * fontScale * (1f - m_charWidthAdjDelta), vector.y, 0f);
				Vector3 vector4 = new Vector3(vector3.x, vector2.y, 0f);
				if ((m_style & FontStyles.Italic) == FontStyles.Italic || (m_fontStyle & FontStyles.Italic) == FontStyles.Italic)
				{
					float num15 = (float)(int)m_currentFontAsset.italicStyle * 0.01f;
					Vector3 b = new Vector3(num15 * ((m_cached_TextElement.yOffset + num4 + num5) * fontScale), 0f, 0f);
					Vector3 b2 = new Vector3(num15 * ((m_cached_TextElement.yOffset - m_cached_TextElement.height - num4 - num5) * fontScale), 0f, 0f);
					vector += b;
					vector2 += b2;
					vector3 += b;
					vector4 += b2;
				}
				m_textInfo.characterInfo[m_characterCount].bottomLeft = vector2;
				m_textInfo.characterInfo[m_characterCount].topLeft = vector;
				m_textInfo.characterInfo[m_characterCount].topRight = vector3;
				m_textInfo.characterInfo[m_characterCount].bottomRight = vector4;
				m_textInfo.characterInfo[m_characterCount].scale = fontScale;
				m_textInfo.characterInfo[m_characterCount].origin = m_xAdvance;
				m_textInfo.characterInfo[m_characterCount].baseLine = 0f - m_lineOffset + m_baselineOffset;
				m_textInfo.characterInfo[m_characterCount].aspectRatio = m_cached_TextElement.width / m_cached_TextElement.height;
				float num16 = m_currentFontAsset.fontInfo.Ascender * ((m_textElementType != 0) ? num : fontScale) + m_baselineOffset;
				m_textInfo.characterInfo[m_characterCount].ascender = num16 - m_lineOffset;
				m_maxLineAscender = ((!(num16 > m_maxLineAscender)) ? m_maxLineAscender : num16);
				float num17 = m_currentFontAsset.fontInfo.Descender * ((m_textElementType != 0) ? num : fontScale) + m_baselineOffset;
				float num18 = m_textInfo.characterInfo[m_characterCount].descender = num17 - m_lineOffset;
				m_maxLineDescender = ((!(num17 < m_maxLineDescender)) ? m_maxLineDescender : num17);
				if ((m_style & FontStyles.Subscript) == FontStyles.Subscript || (m_style & FontStyles.Superscript) == FontStyles.Superscript)
				{
					float num19 = (num16 - m_baselineOffset) / m_currentFontAsset.fontInfo.SubSize;
					num16 = m_maxLineAscender;
					m_maxLineAscender = ((!(num19 > m_maxLineAscender)) ? m_maxLineAscender : num19);
					float num20 = (num17 - m_baselineOffset) / m_currentFontAsset.fontInfo.SubSize;
					num17 = m_maxLineDescender;
					m_maxLineDescender = ((!(num20 < m_maxLineDescender)) ? m_maxLineDescender : num20);
				}
				if (m_lineNumber == 0)
				{
					m_maxAscender = ((!(m_maxAscender > num16)) ? num16 : m_maxAscender);
				}
				if (m_lineOffset == 0f)
				{
					num10 = ((!(num10 > num16)) ? num16 : num10);
				}
				m_textInfo.characterInfo[m_characterCount].isVisible = false;
				if (num3 == 9 || !char.IsWhiteSpace((char)num3) || m_textElementType == TMP_TextElementType.Sprite)
				{
					m_textInfo.characterInfo[m_characterCount].isVisible = true;
					float num21 = (m_width == -1f) ? (marginWidth + 0.0001f - m_marginLeft - m_marginRight) : Mathf.Min(marginWidth + 0.0001f - m_marginLeft - m_marginRight, m_width);
					m_textInfo.lineInfo[m_lineNumber].width = num21;
					m_textInfo.lineInfo[m_lineNumber].marginLeft = m_marginLeft;
					if (Mathf.Abs(m_xAdvance) + (m_isRightToLeft ? 0f : m_cached_TextElement.xAdvance) * (1f - m_charWidthAdjDelta) * fontScale > num21)
					{
						num9 = m_characterCount - 1;
						if (base.enableWordWrapping && m_characterCount != m_firstCharacterOfLine)
						{
							if (num12 == m_SavedWordWrapState.previous_WordBreak || flag5)
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
							num12 = i;
							float num22 = 0f;
							if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == 0f && !m_isNewPage)
							{
								float num23 = m_maxLineAscender - m_startOfLineAscender;
								AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num23);
								m_lineOffset += num23;
								m_SavedWordWrapState.lineOffset = m_lineOffset;
								m_SavedWordWrapState.previousLineAscender = m_maxLineAscender;
							}
							m_isNewPage = false;
							float num24 = m_maxLineAscender - m_lineOffset;
							float num25 = m_maxLineDescender - m_lineOffset;
							m_maxDescender = ((!(m_maxDescender < num25)) ? num25 : m_maxDescender);
							if (!flag4)
							{
								num11 = m_maxDescender;
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
							m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num25);
							m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num24);
							m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * fontScale;
							m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
							m_textInfo.lineInfo[m_lineNumber].ascender = num24;
							m_textInfo.lineInfo[m_lineNumber].descender = num25;
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
								float num26 = m_textInfo.characterInfo[m_characterCount].ascender - m_textInfo.characterInfo[m_characterCount].baseLine;
								num7 = 0f - m_maxLineDescender + num26 + (num22 + m_lineSpacing + m_lineSpacingDelta) * fontScale;
								m_lineOffset += num7;
								m_startOfLineAscender = num26;
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
					if (num3 != 9)
					{
						Color32 vertexColor = flag7 ? ((Color32)Color.red) : ((!m_overrideHtmlColors) ? m_htmlColor : m_fontColor32);
						if (m_textElementType == TMP_TextElementType.Character)
						{
							SaveGlyphVertexInfo(num4, num5, vertexColor);
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
						if (flag3)
						{
							flag3 = false;
							m_firstVisibleCharacterOfLine = m_characterCount;
						}
						m_visibleCharacterCount++;
						m_lastVisibleCharacterOfLine = m_characterCount;
					}
				}
				else if (char.IsSeparator((char)num3))
				{
					m_textInfo.lineInfo[m_lineNumber].spaceCount++;
					m_textInfo.spaceCount++;
				}
				if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == 0f && !m_isNewPage)
				{
					float num27 = m_maxLineAscender - m_startOfLineAscender;
					AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num27);
					num18 -= num27;
					m_lineOffset += num27;
					m_startOfLineAscender += num27;
					m_SavedWordWrapState.lineOffset = m_lineOffset;
					m_SavedWordWrapState.previousLineAscender = m_startOfLineAscender;
				}
				m_textInfo.characterInfo[m_characterCount].lineNumber = (short)m_lineNumber;
				m_textInfo.characterInfo[m_characterCount].pageNumber = (short)m_pageNumber;
				if ((num3 != 10 && num3 != 13 && num3 != 8230) || m_textInfo.lineInfo[m_lineNumber].characterCount == 1)
				{
					m_textInfo.lineInfo[m_lineNumber].alignment = m_lineJustification;
				}
				if (m_maxAscender - num18 > marginHeight + 0.0001f)
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
							m_char_buffer[m_textInfo.characterInfo[num9].index] = 8230;
							m_char_buffer[m_textInfo.characterInfo[num9].index + 1] = 0;
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
							m_char_buffer[m_textInfo.characterInfo[num9].index + 1] = 0;
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
						if (num3 == 13 || num3 == 10)
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
				if (num3 == 9)
				{
					m_xAdvance += m_currentFontAsset.fontInfo.TabWidth * fontScale;
				}
				else if (m_monoSpacing != 0f)
				{
					m_xAdvance += (m_monoSpacing - num14 + (m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * fontScale + m_cSpacing) * (1f - m_charWidthAdjDelta);
				}
				else if (!m_isRightToLeft)
				{
					m_xAdvance += ((m_cached_TextElement.xAdvance * num6 + m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * fontScale + m_cSpacing) * (1f - m_charWidthAdjDelta);
				}
				m_textInfo.characterInfo[m_characterCount].xAdvance = m_xAdvance;
				if (num3 == 13)
				{
					m_xAdvance = tag_Indent;
				}
				if (num3 == 10 || m_characterCount == count - 1)
				{
					float num28 = 0f;
					if (m_lineNumber > 0 && !TMP_Math.Approximately(m_maxLineAscender, m_startOfLineAscender) && m_lineHeight == 0f && !m_isNewPage)
					{
						float num29 = m_maxLineAscender - m_startOfLineAscender;
						AdjustLineOffset(m_firstCharacterOfLine, m_characterCount, num29);
						num18 -= num29;
						m_lineOffset += num29;
					}
					m_isNewPage = false;
					float num30 = m_maxLineAscender - m_lineOffset;
					float num31 = m_maxLineDescender - m_lineOffset;
					m_maxDescender = ((!(m_maxDescender < num31)) ? num31 : m_maxDescender);
					if (!flag4)
					{
						num11 = m_maxDescender;
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
					m_textInfo.lineInfo[m_lineNumber].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_firstVisibleCharacterOfLine].bottomLeft.x, num31);
					m_textInfo.lineInfo[m_lineNumber].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].topRight.x, num30);
					m_textInfo.lineInfo[m_lineNumber].length = m_textInfo.lineInfo[m_lineNumber].lineExtents.max.x - num4 * fontScale;
					m_textInfo.lineInfo[m_lineNumber].maxAdvance = m_textInfo.characterInfo[m_lastVisibleCharacterOfLine].xAdvance - (m_characterSpacing + m_currentFontAsset.normalSpacingOffset) * fontScale;
					m_textInfo.lineInfo[m_lineNumber].baseline = 0f - m_lineOffset;
					m_textInfo.lineInfo[m_lineNumber].ascender = num30;
					m_textInfo.lineInfo[m_lineNumber].descender = num31;
					m_firstCharacterOfLine = m_characterCount + 1;
					m_preferredHeight = m_maxAscender - m_maxDescender;
					if (num3 == 10)
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
							num7 = 0f - m_maxLineDescender + num16 + (num28 + m_lineSpacing + m_paragraphSpacing + m_lineSpacingDelta) * fontScale;
							m_lineOffset += num7;
						}
						else
						{
							m_lineOffset += m_lineHeight + (m_lineSpacing + m_paragraphSpacing) * num;
						}
						m_maxLineAscender = float.NegativeInfinity;
						m_maxLineDescender = float.PositiveInfinity;
						m_startOfLineAscender = num16;
						m_xAdvance = tag_LineIndent + tag_Indent;
						num9 = m_characterCount - 1;
					}
				}
				if (m_textInfo.characterInfo[m_characterCount].isVisible)
				{
					m_meshExtents.min = new Vector2(Mathf.Min(m_meshExtents.min.x, m_textInfo.characterInfo[m_characterCount].bottomLeft.x), Mathf.Min(m_meshExtents.min.y, m_textInfo.characterInfo[m_characterCount].bottomLeft.y));
					m_meshExtents.max = new Vector2(Mathf.Max(m_meshExtents.max.x, m_textInfo.characterInfo[m_characterCount].topRight.x), Mathf.Max(m_meshExtents.max.y, m_textInfo.characterInfo[m_characterCount].topRight.y));
				}
				if (num3 != 13 && num3 != 10 && m_pageNumber < 16)
				{
					m_textInfo.pageInfo[m_pageNumber].ascender = num10;
					m_textInfo.pageInfo[m_pageNumber].descender = ((!(num17 < m_textInfo.pageInfo[m_pageNumber].descender)) ? m_textInfo.pageInfo[m_pageNumber].descender : num17);
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
					if ((num3 == 9 || num3 == 32) && !m_isNonBreakingSpace)
					{
						SaveWordWrappingState(ref m_SavedWordWrapState, i, m_characterCount);
						m_isCharacterWrappingEnabled = false;
						flag5 = false;
					}
					else if (!m_currentFontAsset.lineBreakingInfo.leadingCharacters.ContainsKey(num3) && m_characterCount < count - 1 && !m_currentFontAsset.lineBreakingInfo.followingCharacters.ContainsKey(m_VisibleCharacters[m_characterCount + 1]) && num3 > 11904 && num3 < 40959)
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
			num2 = m_maxFontSize - m_minFontSize;
			if ((!m_textContainer.isDefaultWidth || !m_textContainer.isDefaultHeight) && !m_isCharacterWrappingEnabled && m_enableAutoSizing && num2 > 0.051f && m_fontSize < m_fontSizeMax)
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
			if (m_visibleCharacterCount == 0 && m_visibleSpriteCount == 0)
			{
				ClearMesh();
				return;
			}
			int index = m_visibleCharacterCount * 4;
			Array.Clear(m_textInfo.meshInfo[0].vertices, index, m_textInfo.meshInfo[0].vertices.Length - index);
			Vector3 a = Vector3.zero;
			Vector3[] textContainerLocalCorners = GetTextContainerLocalCorners();
			switch (m_textAlignment)
			{
			case TextAlignmentOptions.TopLeft:
			case TextAlignmentOptions.Top:
			case TextAlignmentOptions.TopRight:
			case TextAlignmentOptions.TopJustified:
				a = ((m_overflowMode == TextOverflowModes.Page) ? (textContainerLocalCorners[1] + new Vector3(margin.x, 0f - m_textInfo.pageInfo[num8].ascender - margin.y, 0f)) : (textContainerLocalCorners[1] + new Vector3(margin.x, 0f - m_maxAscender - margin.y, 0f)));
				break;
			case TextAlignmentOptions.Left:
			case TextAlignmentOptions.Center:
			case TextAlignmentOptions.Right:
			case TextAlignmentOptions.Justified:
				a = ((m_overflowMode == TextOverflowModes.Page) ? ((textContainerLocalCorners[0] + textContainerLocalCorners[1]) / 2f + new Vector3(margin.x, 0f - (m_textInfo.pageInfo[num8].ascender + margin.y + m_textInfo.pageInfo[num8].descender - margin.w) / 2f, 0f)) : ((textContainerLocalCorners[0] + textContainerLocalCorners[1]) / 2f + new Vector3(margin.x, 0f - (m_maxAscender + margin.y + num11 - margin.w) / 2f, 0f)));
				break;
			case TextAlignmentOptions.BottomLeft:
			case TextAlignmentOptions.Bottom:
			case TextAlignmentOptions.BottomRight:
			case TextAlignmentOptions.BottomJustified:
				a = ((m_overflowMode == TextOverflowModes.Page) ? (textContainerLocalCorners[0] + new Vector3(margin.x, 0f - m_textInfo.pageInfo[num8].descender + margin.w, 0f)) : (textContainerLocalCorners[0] + new Vector3(margin.x, 0f - num11 + margin.w, 0f)));
				break;
			case TextAlignmentOptions.BaselineLeft:
			case TextAlignmentOptions.Baseline:
			case TextAlignmentOptions.BaselineRight:
			case TextAlignmentOptions.BaselineJustified:
				a = (textContainerLocalCorners[0] + textContainerLocalCorners[1]) / 2f + new Vector3(margin.x, 0f, 0f);
				break;
			case TextAlignmentOptions.MidlineLeft:
			case TextAlignmentOptions.Midline:
			case TextAlignmentOptions.MidlineRight:
			case TextAlignmentOptions.MidlineJustified:
				a = (textContainerLocalCorners[0] + textContainerLocalCorners[1]) / 2f + new Vector3(margin.x, 0f - (m_meshExtents.max.y + margin.y + m_meshExtents.min.y - margin.w) / 2f, 0f);
				break;
			}
			Vector3 vector5 = Vector3.zero;
			Vector3 zero3 = Vector3.zero;
			int num32 = 0;
			int num33 = 0;
			int num34 = 0;
			int num35 = 0;
			int num36 = 0;
			bool flag8 = false;
			int num37 = 0;
			int num38 = 0;
			Color32 underlineColor = Color.white;
			Color32 underlineColor2 = Color.white;
			float num39 = 0f;
			float num40 = 0f;
			float num41 = 0f;
			float num42 = float.PositiveInfinity;
			int num43 = 0;
			float num44 = 0f;
			float num45 = 0f;
			float b3 = 0f;
			Vector3 lossyScale = m_transform.lossyScale;
			float z = lossyScale.z;
			TMP_CharacterInfo[] characterInfo = m_textInfo.characterInfo;
			for (int j = 0; j < m_characterCount; j++)
			{
				char character2 = characterInfo[j].character;
				int lineNumber = characterInfo[j].lineNumber;
				TMP_LineInfo tMP_LineInfo = m_textInfo.lineInfo[lineNumber];
				num35 = lineNumber + 1;
				switch (tMP_LineInfo.alignment)
				{
				case TextAlignmentOptions.TopLeft:
				case TextAlignmentOptions.Left:
				case TextAlignmentOptions.BottomLeft:
				case TextAlignmentOptions.BaselineLeft:
				case TextAlignmentOptions.MidlineLeft:
					vector5 = (m_isRightToLeft ? new Vector3(0f - tMP_LineInfo.maxAdvance, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
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
					vector5 = (m_isRightToLeft ? new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width, 0f, 0f) : new Vector3(tMP_LineInfo.marginLeft + tMP_LineInfo.width - tMP_LineInfo.maxAdvance, 0f, 0f));
					break;
				case TextAlignmentOptions.TopJustified:
				case TextAlignmentOptions.Justified:
				case TextAlignmentOptions.BottomJustified:
				case TextAlignmentOptions.MidlineJustified:
				{
					char character3 = characterInfo[tMP_LineInfo.lastCharacterIndex].character;
					if (char.IsControl(character3) || lineNumber >= m_lineNumber)
					{
						vector5 = new Vector3(tMP_LineInfo.marginLeft, 0f, 0f);
						break;
					}
					float num46 = tMP_LineInfo.width - tMP_LineInfo.maxAdvance;
					float num47 = (tMP_LineInfo.spaceCount <= 2) ? 1f : m_wordWrappingRatios;
					vector5 = ((lineNumber == num36 && j != 0) ? ((character2 != '\t' && !char.IsSeparator(character2)) ? (vector5 + new Vector3(num46 * num47 / (float)(tMP_LineInfo.characterCount - tMP_LineInfo.spaceCount - 1), 0f, 0f)) : (vector5 + new Vector3(num46 * (1f - num47) / (float)(tMP_LineInfo.spaceCount - 1), 0f, 0f))) : new Vector3(tMP_LineInfo.marginLeft, 0f, 0f));
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
						float num48 = m_uvLineOffset * (float)lineNumber % 1f + m_uvOffset.x;
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
								characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num48;
								characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num48;
								characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num48;
								characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x - lineExtents.min.x) / (lineExtents.max.x - lineExtents.min.x) + num48;
							}
							else
							{
								characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
								characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
								characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
								characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
							}
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.x = (characterInfo[j].vertex_BL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
							characterInfo[j].vertex_TL.uv2.x = (characterInfo[j].vertex_TL.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
							characterInfo[j].vertex_TR.uv2.x = (characterInfo[j].vertex_TR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
							characterInfo[j].vertex_BR.uv2.x = (characterInfo[j].vertex_BR.position.x + vector5.x - m_meshExtents.min.x) / (m_meshExtents.max.x - m_meshExtents.min.x) + num48;
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
								characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num48;
								characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + num48;
								characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
								characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.Paragraph:
								characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num48;
								characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + num48;
								characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
								characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
								break;
							case TextureMappingOptions.MatchAspect:
								UnityEngine.Debug.Log("ERROR: Cannot Match both Vertical & Horizontal.");
								break;
							}
							float num49 = (1f - (characterInfo[j].vertex_BL.uv2.y + characterInfo[j].vertex_TL.uv2.y) * characterInfo[j].aspectRatio) / 2f;
							characterInfo[j].vertex_BL.uv2.x = characterInfo[j].vertex_BL.uv2.y * characterInfo[j].aspectRatio + num49 + num48;
							characterInfo[j].vertex_TL.uv2.x = characterInfo[j].vertex_BL.uv2.x;
							characterInfo[j].vertex_TR.uv2.x = characterInfo[j].vertex_TL.uv2.y * characterInfo[j].aspectRatio + num49 + num48;
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
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - lineExtents.min.y) / (lineExtents.max.y - lineExtents.min.y) + m_uvOffset.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.Paragraph:
							characterInfo[j].vertex_BL.uv2.y = (characterInfo[j].vertex_BL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = (characterInfo[j].vertex_TL.position.y - m_meshExtents.min.y) / (m_meshExtents.max.y - m_meshExtents.min.y) + m_uvOffset.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							break;
						case TextureMappingOptions.MatchAspect:
						{
							float num50 = (1f - (characterInfo[j].vertex_BL.uv2.x + characterInfo[j].vertex_TR.uv2.x) / characterInfo[j].aspectRatio) / 2f;
							characterInfo[j].vertex_BL.uv2.y = num50 + characterInfo[j].vertex_BL.uv2.x / characterInfo[j].aspectRatio + m_uvOffset.y;
							characterInfo[j].vertex_TL.uv2.y = num50 + characterInfo[j].vertex_TR.uv2.x / characterInfo[j].aspectRatio + m_uvOffset.y;
							characterInfo[j].vertex_BR.uv2.y = characterInfo[j].vertex_BL.uv2.y;
							characterInfo[j].vertex_TR.uv2.y = characterInfo[j].vertex_TL.uv2.y;
							break;
						}
						}
						float num51 = m_textInfo.characterInfo[j].scale * z * (1f - m_charWidthAdjDelta);
						if ((m_textInfo.characterInfo[j].style & FontStyles.Bold) == FontStyles.Bold)
						{
							num51 *= -1f;
						}
						float x = characterInfo[j].vertex_BL.uv2.x;
						float y = characterInfo[j].vertex_BL.uv2.y;
						float x2 = characterInfo[j].vertex_TR.uv2.x;
						float y2 = characterInfo[j].vertex_TR.uv2.y;
						float num52 = Mathf.Floor(x);
						float num53 = Mathf.Floor(y);
						x -= num52;
						x2 -= num52;
						y -= num53;
						y2 -= num53;
						characterInfo[j].vertex_BL.uv2 = PackUV(x, y, num51);
						characterInfo[j].vertex_TL.uv2 = PackUV(x, y2, num51);
						characterInfo[j].vertex_TR.uv2 = PackUV(x2, y2, num51);
						characterInfo[j].vertex_BR.uv2 = PackUV(x2, y, num51);
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
					else if (j < m_maxVisibleCharacters && lineNumber < m_maxVisibleLines && m_overflowMode == TextOverflowModes.Page && characterInfo[j].pageNumber == num8)
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
						FillCharacterVertexBuffers(j, num32);
						num32 += 4;
						break;
					case TMP_TextElementType.Sprite:
						num33 += 4;
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
				if (lineNumber != num36 || j == m_characterCount - 1)
				{
					if (lineNumber != num36)
					{
						m_textInfo.lineInfo[num36].lineExtents.min = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num36].firstCharacterIndex].bottomLeft.x, m_textInfo.lineInfo[num36].descender);
						m_textInfo.lineInfo[num36].lineExtents.max = new Vector2(m_textInfo.characterInfo[m_textInfo.lineInfo[num36].lastVisibleCharacterIndex].topRight.x, m_textInfo.lineInfo[num36].ascender);
						m_textInfo.lineInfo[num36].baseline += zero3.y;
						m_textInfo.lineInfo[num36].ascender += zero3.y;
						m_textInfo.lineInfo[num36].descender += zero3.y;
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
						num37 = j;
					}
					if (flag8 && j == m_characterCount - 1)
					{
						int num54 = m_textInfo.wordInfo.Length;
						int wordCount = m_textInfo.wordCount;
						if (m_textInfo.wordCount + 1 > num54)
						{
							TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num54 + 1);
						}
						num38 = j;
						m_textInfo.wordInfo[wordCount].firstCharacterIndex = num37;
						m_textInfo.wordInfo[wordCount].lastCharacterIndex = num38;
						m_textInfo.wordInfo[wordCount].characterCount = num38 - num37 + 1;
						m_textInfo.wordInfo[wordCount].textComponent = this;
						num34++;
						m_textInfo.wordCount++;
						m_textInfo.lineInfo[lineNumber].wordCount++;
					}
				}
				else if (flag8 || (j == 0 && (char.IsPunctuation(character2) || char.IsWhiteSpace(character2) || j == m_characterCount - 1)))
				{
					num38 = ((j != m_characterCount - 1 || !char.IsLetterOrDigit(character2)) ? (j - 1) : j);
					flag8 = false;
					int num55 = m_textInfo.wordInfo.Length;
					int wordCount2 = m_textInfo.wordCount;
					if (m_textInfo.wordCount + 1 > num55)
					{
						TMP_TextInfo.Resize(ref m_textInfo.wordInfo, num55 + 1);
					}
					m_textInfo.wordInfo[wordCount2].firstCharacterIndex = num37;
					m_textInfo.wordInfo[wordCount2].lastCharacterIndex = num38;
					m_textInfo.wordInfo[wordCount2].characterCount = num38 - num37 + 1;
					m_textInfo.wordInfo[wordCount2].textComponent = this;
					num34++;
					m_textInfo.wordCount++;
					m_textInfo.lineInfo[lineNumber].wordCount++;
				}
				if ((m_textInfo.characterInfo[j].style & FontStyles.Underline) == FontStyles.Underline)
				{
					bool flag9 = true;
					int pageNumber = m_textInfo.characterInfo[j].pageNumber;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && pageNumber + 1 != m_pageToDisplay))
					{
						flag9 = false;
					}
					if (!char.IsWhiteSpace(character2))
					{
						num41 = Mathf.Max(num41, m_textInfo.characterInfo[j].scale);
						num42 = Mathf.Min((pageNumber != num43) ? float.PositiveInfinity : num42, m_textInfo.characterInfo[j].baseLine + base.font.fontInfo.Underline * num41);
						num43 = pageNumber;
					}
					if (!flag && flag9 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character2 != '\n' && character2 != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character2)))
					{
						flag = true;
						num39 = m_textInfo.characterInfo[j].scale;
						if (num41 == 0f)
						{
							num41 = num39;
						}
						start = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, num42, 0f);
						underlineColor = m_textInfo.characterInfo[j].color;
					}
					if (flag && m_characterCount == 1)
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num42, 0f);
						num40 = m_textInfo.characterInfo[j].scale;
						DrawUnderlineMesh(start, zero, ref index, num39, num40, num41, underlineColor);
						num41 = 0f;
						num42 = float.PositiveInfinity;
					}
					else if (flag && (j == tMP_LineInfo.lastCharacterIndex || j >= tMP_LineInfo.lastVisibleCharacterIndex))
					{
						if (char.IsWhiteSpace(character2))
						{
							int lastVisibleCharacterIndex = tMP_LineInfo.lastVisibleCharacterIndex;
							zero = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex].topRight.x, num42, 0f);
							num40 = m_textInfo.characterInfo[lastVisibleCharacterIndex].scale;
						}
						else
						{
							zero = new Vector3(m_textInfo.characterInfo[j].topRight.x, num42, 0f);
							num40 = m_textInfo.characterInfo[j].scale;
						}
						flag = false;
						DrawUnderlineMesh(start, zero, ref index, num39, num40, num41, underlineColor);
						num41 = 0f;
						num42 = float.PositiveInfinity;
					}
					else if (flag && !flag9)
					{
						flag = false;
						zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num42, 0f);
						num40 = m_textInfo.characterInfo[j - 1].scale;
						DrawUnderlineMesh(start, zero, ref index, num39, num40, num41, underlineColor);
						num41 = 0f;
						num42 = float.PositiveInfinity;
					}
				}
				else if (flag)
				{
					flag = false;
					zero = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, num42, 0f);
					num40 = m_textInfo.characterInfo[j - 1].scale;
					DrawUnderlineMesh(start, zero, ref index, num39, num40, num41, underlineColor);
					num41 = 0f;
					num42 = float.PositiveInfinity;
				}
				if ((m_textInfo.characterInfo[j].style & FontStyles.Strikethrough) == FontStyles.Strikethrough)
				{
					bool flag10 = true;
					if (j > m_maxVisibleCharacters || lineNumber > m_maxVisibleLines || (m_overflowMode == TextOverflowModes.Page && m_textInfo.characterInfo[j].pageNumber + 1 != m_pageToDisplay))
					{
						flag10 = false;
					}
					if (!flag2 && flag10 && j <= tMP_LineInfo.lastVisibleCharacterIndex && character2 != '\n' && character2 != '\r' && (j != tMP_LineInfo.lastVisibleCharacterIndex || !char.IsSeparator(character2)))
					{
						flag2 = true;
						num44 = m_textInfo.characterInfo[j].pointSize;
						num45 = m_textInfo.characterInfo[j].scale;
						start2 = new Vector3(m_textInfo.characterInfo[j].bottomLeft.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2.75f * num45, 0f);
						underlineColor2 = m_textInfo.characterInfo[j].color;
						b3 = m_textInfo.characterInfo[j].baseLine;
					}
					if (flag2 && m_characterCount == 1)
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num45, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num45, num45, num45, underlineColor2);
					}
					else if (flag2 && j == tMP_LineInfo.lastCharacterIndex)
					{
						if (!char.IsWhiteSpace(character2))
						{
							zero2 = new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num45, 0f);
						}
						else
						{
							int lastVisibleCharacterIndex2 = tMP_LineInfo.lastVisibleCharacterIndex;
							zero2 = new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex2].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex2].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num45, 0f);
						}
						flag2 = false;
						DrawUnderlineMesh(start2, zero2, ref index, num45, num45, num45, underlineColor2);
					}
					else if (flag2 && j < m_characterCount && (m_textInfo.characterInfo[j + 1].pointSize != num44 || !TMP_Math.Approximately(m_textInfo.characterInfo[j + 1].baseLine + zero3.y, b3)))
					{
						flag2 = false;
						int lastVisibleCharacterIndex3 = tMP_LineInfo.lastVisibleCharacterIndex;
						zero2 = ((j > lastVisibleCharacterIndex3) ? new Vector3(m_textInfo.characterInfo[lastVisibleCharacterIndex3].topRight.x, m_textInfo.characterInfo[lastVisibleCharacterIndex3].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num45, 0f) : new Vector3(m_textInfo.characterInfo[j].topRight.x, m_textInfo.characterInfo[j].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num45, 0f));
						DrawUnderlineMesh(start2, zero2, ref index, num45, num45, num45, underlineColor2);
					}
					else if (flag2 && !flag10)
					{
						flag2 = false;
						zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * num45, 0f);
						DrawUnderlineMesh(start2, zero2, ref index, num45, num45, num45, underlineColor2);
					}
				}
				else if (flag2)
				{
					flag2 = false;
					zero2 = new Vector3(m_textInfo.characterInfo[j - 1].topRight.x, m_textInfo.characterInfo[j - 1].baseLine + (base.font.fontInfo.Ascender + base.font.fontInfo.Descender) / 2f * m_fontScale, 0f);
					DrawUnderlineMesh(start2, zero2, ref index, num45, num45, num45, underlineColor2);
				}
				num36 = lineNumber;
			}
			m_textInfo.characterCount = (short)m_characterCount;
			m_textInfo.lineCount = (short)num35;
			m_textInfo.wordCount = ((num34 == 0 || m_characterCount <= 0) ? 1 : ((short)num34));
			m_textInfo.pageCount = m_pageNumber + 1;
			if (m_renderMode == TextRenderFlags.Render)
			{
				m_mesh.MarkDynamic();
				m_mesh.vertices = m_textInfo.meshInfo[0].vertices;
				m_mesh.uv = m_textInfo.meshInfo[0].uvs0;
				m_mesh.uv2 = m_textInfo.meshInfo[0].uvs2;
				m_mesh.colors32 = m_textInfo.meshInfo[0].colors32;
			}
			m_mesh.RecalculateBounds();
			TMPro_EventManager.ON_TEXT_CHANGED(this);
		}

		protected override Vector3[] GetTextContainerLocalCorners()
		{
			return textContainer.corners;
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
			if (m_textInfo.meshInfo[0].mesh == null)
			{
				m_textInfo.meshInfo[0].mesh = m_mesh;
			}
			m_textInfo.ClearMeshInfo();
		}

		private void UpdateSDFScale(float prevScale, float newScale)
		{
			Vector2[] uvs = m_textInfo.meshInfo[0].uvs2;
			for (int i = 0; i < uvs.Length; i++)
			{
				uvs[i].y = uvs[i].y / prevScale * newScale;
			}
			m_mesh.uv2 = uvs;
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
				m_textInfo.characterInfo[i].descender -= vector.y;
				m_textInfo.characterInfo[i].baseLine -= vector.y;
				m_textInfo.characterInfo[i].ascender -= vector.y;
				if (m_textInfo.characterInfo[i].isVisible)
				{
					m_textInfo.characterInfo[i].vertex_BL.position -= vector;
					m_textInfo.characterInfo[i].vertex_TL.position -= vector;
					m_textInfo.characterInfo[i].vertex_TR.position -= vector;
					m_textInfo.characterInfo[i].vertex_BR.position -= vector;
				}
			}
		}

		public void SetMask(MaskingTypes type, Vector4 maskCoords)
		{
			SetMask(type);
			SetMaskCoordinates(maskCoords);
		}

		public void SetMask(MaskingTypes type, Vector4 maskCoords, float softnessX, float softnessY)
		{
			SetMask(type);
			SetMaskCoordinates(maskCoords, softnessX, softnessY);
		}

		public override void SetVerticesDirty()
		{
			if (!m_verticesAlreadyDirty && IsActive())
			{
				TMP_UpdateManager.RegisterTextElementForGraphicRebuild(this);
				m_verticesAlreadyDirty = true;
			}
		}

		public override void SetLayoutDirty()
		{
			if (!m_layoutAlreadyDirty && IsActive())
			{
				m_layoutAlreadyDirty = true;
			}
		}

		public override void Rebuild(CanvasUpdate update)
		{
			if (update == CanvasUpdate.Prelayout && m_autoSizeTextContainer)
			{
				CalculateLayoutInputHorizontal();
				if (m_textContainer.isDefaultWidth)
				{
					m_textContainer.width = m_preferredWidth;
				}
				CalculateLayoutInputVertical();
				if (m_textContainer.isDefaultHeight)
				{
					m_textContainer.height = m_preferredHeight;
				}
			}
			if (update == CanvasUpdate.PreRender)
			{
				OnPreRenderObject();
				m_verticesAlreadyDirty = false;
				m_layoutAlreadyDirty = false;
			}
		}

		public override void UpdateMeshPadding()
		{
			m_padding = ShaderUtilities.GetPadding(m_renderer.sharedMaterials, m_enableExtraPadding, m_isUsingBold);
			m_havePropertiesChanged = true;
		}

		public override void ForceMeshUpdate()
		{
			OnPreRenderObject();
		}

		public void UpdateFontAsset()
		{
			LoadFontAsset();
		}

		public void CalculateLayoutInputHorizontal()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			IsRectTransformDriven = true;
			m_currentAutoSizeMode = m_enableAutoSizing;
			if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
			{
				m_minWidth = 0f;
				m_flexibleWidth = 0f;
				m_renderMode = TextRenderFlags.GetPreferredSizes;
				if (m_enableAutoSizing)
				{
					m_fontSize = m_fontSizeMax;
				}
				m_marginWidth = float.PositiveInfinity;
				m_marginHeight = float.PositiveInfinity;
				if (m_isInputParsingRequired || m_isTextTruncated)
				{
					ParseInputText();
				}
				GenerateTextMesh();
				m_renderMode = TextRenderFlags.Render;
				ComputeMarginSize();
				m_isLayoutDirty = true;
			}
		}

		public void CalculateLayoutInputVertical()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				return;
			}
			IsRectTransformDriven = true;
			if (m_isCalculateSizeRequired || m_rectTransform.hasChanged)
			{
				m_minHeight = 0f;
				m_flexibleHeight = 0f;
				m_renderMode = TextRenderFlags.GetPreferredSizes;
				if (m_enableAutoSizing)
				{
					m_currentAutoSizeMode = true;
					m_enableAutoSizing = false;
				}
				m_marginHeight = float.PositiveInfinity;
				GenerateTextMesh();
				m_enableAutoSizing = m_currentAutoSizeMode;
				m_renderMode = TextRenderFlags.Render;
				ComputeMarginSize();
				m_isLayoutDirty = true;
			}
			m_isCalculateSizeRequired = false;
		}
	}
}
