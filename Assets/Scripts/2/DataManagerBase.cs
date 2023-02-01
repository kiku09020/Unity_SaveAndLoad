using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace scene_2 {
    public abstract class DataManagerBase<T> : SimpleSingleton<T> where T : DataManagerBase<T> {
        protected abstract string FileName { get; }     // ファイル名
        string FilePath { get; set; }                   // ファイルパス

        const string DATA_FOLDER_NAME = "Data";         // フォルダ名

        void GetFilePath()
        {
            var directoryName = "";

#if UNITY_EDITOR
            directoryName = $"{Application.dataPath}/{DATA_FOLDER_NAME}";               // Editor上のパス
#else
        directoryName = $"{ Application.persistentDataPath}/{ DATA_FOLDER_NAME}";   // ビルド時のパス
#endif

            var fileNameWithExt = $"{FileName}.json";                                   // 拡張子付きのファイル名
            FilePath = $"{directoryName}/{fileNameWithExt}";                            // ファイルパス
        }

        /// <summary>
        /// データのセットアップ
        /// </summary>
        protected Data DataSetup<Data>(IData data) where Data : IData
        {
            // ファイルパス取得
            GetFilePath();

            // ファイルがなければ、新規作成
            if (!File.Exists(FilePath)) {
                Save(data);
            }

            // ファイルロード
            return Load<Data>();
        }

        /// <summary>
        /// jsonファイルとしてデータ保存
        /// </summary>
        protected void Save(IData data)
        {
            var json = JsonUtility.ToJson(data);
            var wr = new StreamWriter(FilePath, false);

            wr.WriteLine(json);
            wr.Close();
        }

        /// <summary>
        /// jsonファイル読み込み
        /// </summary>
        protected Data Load<Data>() where Data : IData
        {
            var rd = new StreamReader(FilePath);
            var json = rd.ReadToEnd();
            rd.Close();

            return JsonUtility.FromJson<Data>(json);
        }
    }
}