namespace TMPro
{
	public static class TMP_Math
	{
		public static bool Approximately(float a, float b)
		{
			return b - 1E-05f < a && a < b + 1E-05f;
		}
	}
}
