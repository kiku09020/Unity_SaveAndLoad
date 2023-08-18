// データ用クラス
namespace Extentions.DataManagement
{
	/// <summary> ハイスコアのデータ </summary>
	[System.Serializable]
	public class HighScoreData : GameDataBase
	{
		public int highScore;       // ハイスコア
	}

	/// <summary> ゲームの設定データ </summary>
	[System.Serializable]
	public class SettingData : GameDataBase
	{
		// 音量ボリューム
		public float bgmVolume;
		public float seVolume;

		// コンストラクタ
		public SettingData()
		{
			bgmVolume = 1.0f;
			seVolume = 1.0f;
		}
	}
}