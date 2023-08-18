using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Extentions.DataManagement
{
	public static class FileDataHandler
	{
		// 暗号鍵 ※暗号化する場合は変更してください
		const string KEYWORD = "hello";		

		//-------------------------------------------------------------------
		/* Constractor */

		/// <summary> フルパスの取得 </summary>
		/// <param name="fileDirPath">ディレクトリパス</param>
		/// <param name="fileName">ファイル名</param>
		public static string GetFullPath(string fileDirPath, string fileName)
		{
			try {
				return Path.Combine(fileDirPath, fileName);
			}

			catch (System.Exception e) {
				throw new System.Exception(e.Message);
			}
		}

		/// <summary> フォルダ名を指定したフルパスの取得 </summary>
		/// <param name="fileDirPath">ディレクトリパス</param>
		/// <param name="fileName">ファイル名</param>
		/// <param name="mainFolderName">メインフォルダ名</param>
		/// <param name="subFolderName">サブフォルダ名</param>
		public static string GetFullPath(string fileDirPath, string fileName,
											string mainFolderName, string subFolderName = null)

		{
			// サブフォルダ名が指定されていない場合
			if (string.IsNullOrEmpty(subFolderName)) {
				try {
					return Path.Combine(fileDirPath, mainFolderName, fileName);
				}

				catch (System.Exception e) {
					throw new System.Exception(e.Message);
				}
			}

			try {
				return Path.Combine(fileDirPath, mainFolderName, subFolderName, fileName);
			}

			catch (System.Exception e) {
				throw new System.Exception(e.Message);
			}

		}

		/// <summary> 2つ以上のフォルダ名を指定したフルパスの取得 </summary>
		/// <param name="paths">パス、フォルダ名など</param>
		public static string GetFullPath(params string[] paths)
		{
			try {
				return Path.Combine(paths);
			}

			catch (System.Exception e) {
				throw new System.Exception(e.Message);
			}
		}

		//-------------------------------------------------------------------
		/* Public Methods */
		/// <summary> データをファイルに書き込む </summary>
		public static void WriteDataToFile<T>(T data, string path, bool isEncrypted)
			where T : GameDataBase
		{
			try {
				// ファイルが存在しなければ作成する
				Directory.CreateDirectory(Path.GetDirectoryName(path));

				// データをJsonに変換(シリアライズ)
				string jsonData = JsonUtility.ToJson(data, true);

				// 暗号化
				if (isEncrypted) {
					jsonData = EncAndDec(jsonData);
				}

				// ファイルに書き込む
				using (var stream = new FileStream(path, FileMode.Create)) {
					using (var writer = new StreamWriter(stream)) {
						writer.Write(jsonData);
					}
				}
			}

			catch (System.Exception e) {
				Debug.LogError(e.Message);
			}
		}

		/// <summary> ファイルからデータを読み込む </summary>
		public static T ReadDataFromFile<T>(string path, bool isEncrypted)
			where T : GameDataBase
		{
			// ファイルが存在しなければnullを返す
			if (!File.Exists(path)) {
				return null;
			}

			T data = null;

			try {
				string loadedData = "";

				// ファイルから読み込む
				using (var stream = new FileStream(path, FileMode.Open)) {
					using (var reader = new StreamReader(stream)) {
						loadedData = reader.ReadToEnd();
					}
				}

				// 複合化
				if (isEncrypted) {
					loadedData = EncAndDec(loadedData);
				}

				// Jsonからデータに変換(デシリアライズ)
				data = JsonUtility.FromJson<T>(loadedData);
			}

			catch (System.Exception e) {
				Debug.LogError(e.Message);
			}

			return data;
		}

		//-------------------------------------------------------------------

		// 暗号化、復号化
		static string EncAndDec(string data)
		{
			string modifiedData = "";

			for (int i = 0; i < data.Length; i++) {
				modifiedData += (char)(data[i] ^ KEYWORD[i % KEYWORD.Length]);
			}

			return modifiedData;
		}
	}
}