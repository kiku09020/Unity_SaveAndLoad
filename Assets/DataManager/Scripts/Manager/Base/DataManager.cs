using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extentions.DataManagement
{
	/// <summary> ゲームのデータを管理するクラス </summary>
	public class DataManager<Data> : MonoBehaviour
			 where Data : GameDataBase, new()
	{
		[Header("PathSettings")]
		[SerializeField, Tooltip("作成するデータファイル名")]
		string dataFileName;
		[SerializeField, Tooltip("拡張子")]
		string extension = ".dat";
		[SerializeField, Tooltip("サブフォルダ名(省略可)")]
		string folderName;

		[Header("Encryption")]
		[SerializeField, Tooltip("暗号化するか")]
		bool useEncryption = true;

		[Header("Debug")]
		[SerializeField, Tooltip("ログ表示するか")]
		bool isDebug = true;

		const string mainFolderName = "Savedata";       // メインフォルダ名
		string fullPath;

		Data data;

		//-------------------------------------------------------------------
		/* Events */
		public delegate void SaveDelegate(ref Data data);

		/// <summary> データを保存するときに呼ばれるイベント </summary>
		public event SaveDelegate OnDataSaved;

		/// <summary> データを読み込むときに呼ばれるイベント </summary>
		public event Action<Data> OnDataLoaded;

		//-------------------------------------------------------------------
		/// <summary> データの保存 </summary>
		public void SaveData()
		{
			// クライアントのデータを取得
			OnDataSaved?.Invoke(ref data);

			// ファイルに書き込み
			FileDataHandler.WriteDataToFile(data, fullPath, useEncryption);

			// ログ表示
			if (isDebug && Debug.isDebugBuild) {
				print($"Saved {dataFileName}.");
			}
		}

		/// <summary> データの読み込み </summary>
		public void LoadData()
		{
			SetFullPath();		// フルパス設定

			// ファイルから読み込み
			data = FileDataHandler.ReadDataFromFile<Data>(fullPath, useEncryption);

			// データがなければ作成する
			if (data == null) {
				CreateData();
			}

			// ログ表示
			else if (isDebug && Debug.isDebugBuild) {
				print($"Loaded {dataFileName}.");
			}

			// クライアントにデータを渡す
			OnDataLoaded?.Invoke(data);
		}

		//-------------------------------------------------------------------
		// データの新規作成
		void CreateData()
		{
			data = new Data();

			// ログ表示
			if(isDebug && Debug.isDebugBuild) {
				print($"Created {dataFileName}.");
			}
		}

		// フルパスの設定
		void SetFullPath()
		{
			string dirPath = "";

			// エディタとそれ以外でデータの保存先を変える
#if UNITY_EDITOR
			dirPath = Application.dataPath;
#else
		dirPath = Application.persistentDataPath;
#endif

			fullPath = FileDataHandler.GetFullPath(dirPath, dataFileName + extension, mainFolderName, folderName);
		}
	}
}