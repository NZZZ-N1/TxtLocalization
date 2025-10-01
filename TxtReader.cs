using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TxtLocalization
{
	/// <summary>
	/// You can use it to read the content without being produced
	/// </summary>
	public static class TxtReader
	{
		private static LocalizationInfo info => LocalizationInfo.Instance;
		public static void CheckVaildCall()
		{
			if (info == null)
				throw new System.NullReferenceException("info is null");
		}

		/// <summary>
		/// Read the txts which are not divided into the right format
		/// </summary>
		private static string[] GetOriginalTxts(string fileName)
		{
			CheckVaildCall();
			string path = Path.Combine(info.Folder, fileName);
			path = Path.ChangeExtension(path, "txt");
			string[] strs;
			
			try
			{
				strs = File.ReadLines(path).ToArray();
			}
			catch (System.Exception ex)
			{
				Console.WriteLine($"读取文件时出错: {ex.Message}");
				return new string[0];
			}

			LinkedList<string> list = new LinkedList<string>(strs);
			foreach (var i in strs)
				if (i == null || i.Trim() == "" || i.StartsWith("//") || i.StartsWith("#"))
					list.Remove(i);
			return list.ToArray();
		}
		/// <summary>
		/// Get all files' names that can be read
		/// </summary>
		public static string[] GetAvailableLanguageFileNames()
		{
			CheckVaildCall();
			
			if (!Directory.Exists(info.Folder))
				throw new DirectoryNotFoundException($"文件夹不存在: {info.Folder}");
			string[] files = Directory.GetFiles(info.Folder, "*.txt");
			string[] fileNames = files.Select(file => 
				Path.GetFileNameWithoutExtension(file)).ToArray();
			return fileNames;
		}

		/// <summary>
		/// Get txts have been divided into the right format
		/// </summary>
		public static LocalizationPair[] GetPairs(string fileName)
		{
			CheckVaildCall();
			LinkedList<LocalizationPair> list = new LinkedList<LocalizationPair>();
			string[] strs = GetOriginalTxts(fileName);

			foreach (var str in strs)
			{
				int startIndex = 0;
				LinkedList<char> l = new LinkedList<char>();
				string id;

				for (int i = 0; i < str.Length; i++)
					if (str[i] == '[')
					{
						startIndex = i + 1;
						goto L1;
					}
				throw new System.ArgumentException("The txt is not legal");
				L1:

				for (int i = startIndex; i < str.Length; i++)
				{
					char c = str[i];
					if (c == ']')
					{
						id = new string(l.ToArray());
						startIndex = i + 1;
						goto L2;
					}
					l.AddLast(c);
				}
				throw new System.ArgumentException("The txt is not legal");
				L2:
				l.Clear();

				for (int i = startIndex; i < str.Length; i++)
				{
					char c = str[i];
					l.AddLast(c);
				}
				var content = new string(l.ToArray());
				list.AddLast(new LocalizationPair(id, content));
			}

			return list.ToArray();
		}
	}
}