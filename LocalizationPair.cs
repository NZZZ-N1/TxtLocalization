namespace TxtLocalization
{
	/// <summary>
	/// Record info temporarily
	/// You don't need use it
	/// </summary>
	public struct LocalizationPair
	{
		public readonly string ID;
		public readonly string Content;

		public LocalizationPair(string id, string content)
		{
			ID = id;
			Content = content;
		}
	}
}