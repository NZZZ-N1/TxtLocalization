namespace TxtLocalization
{
	public static class LocalizationEx
	{
		public static string Localized(this string str) => Localization.GetContent(str);
	}
}