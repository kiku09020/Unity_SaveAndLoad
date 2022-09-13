using UnityEngine;
using UnityEngine.UI;

/* ★〇〇に関するスクリプトです */
public class Ranking_Json : MonoBehaviour
{
    /* 値 */
    string[] rankKeyNames = { "1st", "2nd", "3rd" };    // ランキングのキー名
    const int rankCnt = 3;                              // ランキング数

    /* コンポーネント取得用 */
    Text[] rankText = new Text[rankCnt];
    Rank_JsonData rankData;

    //-------------------------------------------------------------------
    void Start()
    {
        rankData = GetComponent<DataManager>().data;

        for (int i = 0; i < rankCnt; i++) {
            rankText[i] = GameObject.Find("RankTexts").transform.GetChild(i).GetComponent<Text>();
        }
    }

    //-------------------------------------------------------------------
    void FixedUpdate()
    {
        DispRank();
    }

    void DispRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            rankText[i].text = (rankKeyNames[i] + " : " + rankData.rank[i]);
        }
    }

    // ランキング保存
    public void SetRank()
    {
        InputField inpFld = GameObject.Find("InputField").GetComponent<InputField>();
        int score = int.Parse(inpFld.text);     // string -> int

        for (int i = 0; i < rankCnt; i++) {

            // スコアがランキング内の値よりも大きいときは入れ替え
            if (score > rankData.rank[i]) {
                var rep = rankData.rank[i];
                rankData.rank[i] = score;
                score = rep;
            }
        }

        // 保存
        for (int i = 0; i < rankCnt; i++) {
            rankData.rank[i]=rankData.rank[i];
        }
    }

    public void DelRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            rankData.rank[i] = 0;
        }
    }
}
