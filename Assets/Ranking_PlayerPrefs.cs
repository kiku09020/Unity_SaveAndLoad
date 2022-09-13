using UnityEngine;
using UnityEngine.UI;

/* PlayerPrefsでのランキング作成
 * ・1つのスクリプトで済む
 * ・1つの値しか保存できない
 * ・
 */
public class Ranking_PlayerPrefs : MonoBehaviour
{
    /* 値 */
    string[] rankKeyNames = { "1st", "2nd", "3rd" };    // ランキングのキー名
    const int rankCnt = 3;                              // ランキング数
    int[] rankVal = new int[rankCnt];                   // ランキングのスコア

    /* コンポーネント取得用 */
    Text[] rankText = new Text[rankCnt];


//-------------------------------------------------------------------
    void Start()
    {
        for(int i = 0; i < rankCnt; i++) {
            rankText[i] = GameObject.Find("RankTexts").transform.GetChild(i).GetComponent<Text>();
        }

        GetRank();


    }

//-------------------------------------------------------------------
    void FixedUpdate()
    {
        DispRank();
    }

//-------------------------------------------------------------------

    // ランキング取得
    void GetRank()
    {
        for(int i = 0; i < rankCnt; i++) {
            rankVal[i] = PlayerPrefs.GetInt(rankKeyNames[i]);
        }
    }

    void DispRank()
    {
        for (int i = 0; i < rankCnt; i++) {
            rankText[i].text = (rankKeyNames[i] + " : " + rankVal[i]);
        }
    }

    // ランキング保存
    public void SetRank()
    {
        InputField inpFld = GameObject.Find("InputField").GetComponent<InputField>();
        int score = int.Parse(inpFld.text);     // string -> int

        for (int i = 0; i < rankCnt; i++) {

            // スコアがランキング内の値よりも大きいときは入れ替え
            if (score > rankVal[i]) {
                var rep = rankVal[i];
                rankVal[i] = score;
                score = rep;
            }
        }

        // 保存
        for(int i = 0; i < rankCnt; i++) {
            PlayerPrefs.SetInt(rankKeyNames[i], rankVal[i]);
        }
    }

    public void DelRank()
    {
        PlayerPrefs.DeleteAll();

        for(int i = 0; i < rankCnt; i++) {
            rankVal[i] = 0;
        }
    }
}
