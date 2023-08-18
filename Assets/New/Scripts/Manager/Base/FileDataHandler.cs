using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Extentions.DataManagement
{
	public static class FileDataHandler
	{
		// �Í��� ���Í�������ꍇ�͕ύX���Ă�������
		const string KEYWORD = "hello";		

		//-------------------------------------------------------------------
		/* Constractor */

		/// <summary> �t���p�X�̎擾 </summary>
		/// <param name="fileDirPath">�f�B���N�g���p�X</param>
		/// <param name="fileName">�t�@�C����</param>
		public static string GetFullPath(string fileDirPath, string fileName)
		{
			try {
				return Path.Combine(fileDirPath, fileName);
			}

			catch (System.Exception e) {
				throw new System.Exception(e.Message);
			}
		}

		/// <summary> �t�H���_�����w�肵���t���p�X�̎擾 </summary>
		/// <param name="fileDirPath">�f�B���N�g���p�X</param>
		/// <param name="fileName">�t�@�C����</param>
		/// <param name="mainFolderName">���C���t�H���_��</param>
		/// <param name="subFolderName">�T�u�t�H���_��</param>
		public static string GetFullPath(string fileDirPath, string fileName,
											string mainFolderName, string subFolderName = null)

		{
			// �T�u�t�H���_�����w�肳��Ă��Ȃ��ꍇ
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

		/// <summary> 2�ȏ�̃t�H���_�����w�肵���t���p�X�̎擾 </summary>
		/// <param name="paths">�p�X�A�t�H���_���Ȃ�</param>
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
		/// <summary> �f�[�^���t�@�C���ɏ������� </summary>
		public static void WriteDataToFile<T>(T data, string path, bool isEncrypted)
			where T : GameDataBase
		{
			try {
				// �t�@�C�������݂��Ȃ���΍쐬����
				Directory.CreateDirectory(Path.GetDirectoryName(path));

				// �f�[�^��Json�ɕϊ�(�V���A���C�Y)
				string jsonData = JsonUtility.ToJson(data, true);

				// �Í���
				if (isEncrypted) {
					jsonData = EncAndDec(jsonData);
				}

				// �t�@�C���ɏ�������
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

		/// <summary> �t�@�C������f�[�^��ǂݍ��� </summary>
		public static T ReadDataFromFile<T>(string path, bool isEncrypted)
			where T : GameDataBase
		{
			// �t�@�C�������݂��Ȃ����null��Ԃ�
			if (!File.Exists(path)) {
				return null;
			}

			T data = null;

			try {
				string loadedData = "";

				// �t�@�C������ǂݍ���
				using (var stream = new FileStream(path, FileMode.Open)) {
					using (var reader = new StreamReader(stream)) {
						loadedData = reader.ReadToEnd();
					}
				}

				// ������
				if (isEncrypted) {
					loadedData = EncAndDec(loadedData);
				}

				// Json����f�[�^�ɕϊ�(�f�V���A���C�Y)
				data = JsonUtility.FromJson<T>(loadedData);
			}

			catch (System.Exception e) {
				Debug.LogError(e.Message);
			}

			return data;
		}

		//-------------------------------------------------------------------

		// �Í����A������
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