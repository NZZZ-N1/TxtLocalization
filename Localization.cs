using System;
using System.Collections.Generic;
using System.Linq;
using EasySaving;

namespace TxtLocalization
{
	public static class Localization
	{
		private static LocalizationInfo info => LocalizationInfo.Instance;
		public static void CheckVaildCall()
		{
			TxtReader.CheckVaildCall();
			if (info == null)
				throw new System.NullReferenceException("You didn't instantiate a LocalizationInfo");
		}
		
		#region LanguageData
		public static string Language { get; private set; } = null;
		private const string FILENAME = "Localization";
		private const string FILEKEY = "Language";
		
		public static void SetLanguage(string v)
		{
			Language = v;
			SavingPack p = new SavingPack();
			p.Add(FILEKEY, v);
			DataSaving.Save(p, FILENAME);
		}
		#endregion
		
		private static Dictionary<string, string> Dic = new Dictionary<string, string>();
		public static string GetContent(string id, bool logWhenMissingContent = true)
		{
			CheckVaildCall();
			if (Dic.TryGetValue(id, out var v))
				return v;
			if (logWhenMissingContent)
				Console.WriteLine("Missing localization:" + id);
			return id;
		}
		
		public static void ReloadLocalization(string language)
		{
			CheckVaildCall();
			if (language == null)
				throw new System.NullReferenceException("Language is null");
			if (!TxtReader.GetAvailableLanguageFileNames().Contains(language))
				throw new System.Exception("Language file not found: " + language);
			Dic.Clear();
			foreach (var i in TxtReader.GetPairs(language))
				Dic.Add(i.ID, i.Content);
		}
		public static void ReloadLocalization() => ReloadLocalization(Language);
		
		public static void InitializeLocalization()
		{
			CheckVaildCall();
			string str = DataSaving.Load(FILENAME).TryGetValue<string>(FILEKEY, null);
			string[] all = TxtReader.GetAvailableLanguageFileNames();
			if (!all.Contains(str) || str == null)
			{
				str = null;
				foreach (var i in info.DefaultSelections)
					if (all.Contains(i))
					{
						str = i;
						break;
					}
			}
			if (str == null)
				throw new System.MissingMemberException("There is not any language can be choose");
			SetLanguage(str);
			ReloadLocalization(str);
		}
	}
}