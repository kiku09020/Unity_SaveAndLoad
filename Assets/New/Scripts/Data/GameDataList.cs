// �f�[�^�p�N���X
namespace Extentions.DataManagement
{
	/// <summary> �n�C�X�R�A�̃f�[�^ </summary>
	[System.Serializable]
	public class HighScoreData : GameDataBase
	{
		public int highScore;       // �n�C�X�R�A
	}

	/// <summary> �Q�[���̐ݒ�f�[�^ </summary>
	[System.Serializable]
	public class SettingData : GameDataBase
	{
		// ���ʃ{�����[��
		public float bgmVolume;
		public float seVolume;

		// �R���X�g���N�^
		public SettingData()
		{
			bgmVolume = 1.0f;
			seVolume = 1.0f;
		}
	}
}