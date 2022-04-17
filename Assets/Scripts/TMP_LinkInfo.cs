namespace TMPro
{
	public struct TMP_LinkInfo
	{
		public TMP_Text textComponent;

		public int hashCode;

		public int linkIdFirstCharacterIndex;

		public int linkIdLength;

		public int linkTextfirstCharacterIndex;

		public int linkTextLength;

		public string GetLinkText()
		{
			string text = string.Empty;
			TMP_TextInfo textInfo = textComponent.textInfo;
			for (int i = linkTextfirstCharacterIndex; i < linkTextfirstCharacterIndex + linkTextLength; i++)
			{
				text += textInfo.characterInfo[i].character;
			}
			return text;
		}

		public string GetLinkID()
		{
			if (textComponent == null)
			{
				return string.Empty;
			}
			return textComponent.text.Substring(linkIdFirstCharacterIndex, linkIdLength);
		}
	}
}
