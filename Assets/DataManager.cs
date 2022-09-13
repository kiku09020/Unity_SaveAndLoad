using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour 
{
    public Rank_JsonData data = new Rank_JsonData();       // json変換するデータのクラス
    string path;                                    // パス
    string fileName = "/Data.json";                 // jsonファイル名

    void Awake()
    {
        path = Application.dataPath + fileName;     // パス名決める

        data = Load(path);
    }

    // jsonとしてデータを保存
    public void Save(Rank_JsonData data)
    {
        string str = JsonUtility.ToJson(data);                  // jsonとして変換
        StreamWriter wr = new StreamWriter(path, false);        // ファイル開く
        wr.WriteLine(str);                                      // json書き込み
        wr.Flush();                                             // バッファ削除
        wr.Close();                                             // ファイル閉じる
    }

    // jsonファイル読み込み
    public Rank_JsonData Load(string path)
    {
        StreamReader rd = new StreamReader(path);               // 読み込み
        string str = rd.ReadToEnd();                            // ファイル内容全て読み込む
        rd.Close();                                             // ファイル閉じる
                                                                
        return JsonUtility.FromJson<Rank_JsonData>(str);        // jsonファイルを型に戻して返す
    }

    void OnDestroy()
    {
        Save(data);
    }
}