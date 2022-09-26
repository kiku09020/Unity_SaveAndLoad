using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    /* 値 */
    string[]    rankNames = { "1st", "2nd", "3rd" };        // ランキング名
    const int   rankCnt = SaveData.rankCnt;                 // ランキング数

    /* コンポーネント取得用 */
    Text[]      rankTexts = new Text[rankCnt];              // ランキングのテキスト
    SaveData    data;                                       // 参照するセーブデータ

    //-------------------------------------------------------------------
    void Start()
    {
        data = GetComponent<DataManager>().data;            // セーブデータをDataManagerから参照

        for (int i = 0; i < rankCnt; i++) {
            Transform rankChilds = GameObject.Find("RankTexts").transform.GetChild(i);      // 子オブジェクト取得
            rankTexts[i] = rankChilds.GetComponent<Text>();                                 // 子オブジェクトのコンポーネント取得
        }
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        DispRank();
    }

    // ランキング表示
    void DispRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            rankTexts[i].text = (rankNames[i] + " : " + data.rank[i]);
        }
    }

    // ランキング保存
    public void SetRank()
    {
        InputField inpFld = GameObject.Find("InputField").GetComponent<InputField>();
        int score = int.Parse(inpFld.text);     // string -> int

        // スコアがランキング内の値よりも大きいときは入れ替え
        for (int i = 0; i < rankCnt; i++) {
            if (score > data.rank[i]) {
                var rep = data.rank[i];
                data.rank[i] = score;
                score = rep;
            }
        }
    }

    // ランクデータの削除
    public void DelRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            data.rank[i] = 0;
        }
    }
}
