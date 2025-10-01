using System;
using EasySaving;

namespace TxtLocalization
{
	/// <summary>
	/// This is used to record the information
	/// You must instantiate one when the application start running so you can use the function of TxtLocalization
	/// </summary>
	public sealed class LocalizationInfo
	{
		public static LocalizationInfo Instance = null;

		public LocalizationInfo(string folder, string[] defaultSelections)
		{
			if (SavingInfo.Instance == null)
				throw new InfoInstanceMissingException();
			if (folder == null || defaultSelections == null || defaultSelections.Length == 0)
				throw new ArgumentException();
			Folder = folder;
			DefaultSelections = defaultSelections;
			Instance = this;
		}

		public readonly string Folder;
		public readonly string[] DefaultSelections;
	}
}