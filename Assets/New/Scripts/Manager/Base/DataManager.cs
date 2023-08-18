using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extentions.DataManagement
{
	/// <summary> �Q�[���̃f�[�^���Ǘ�����N���X </summary>
	public class DataManager<Data> : MonoBehaviour
			 where Data : GameDataBase, new()
	{
		[Header("PathSettings")]
		[SerializeField, Tooltip("�쐬����f�[�^�t�@�C����")]
		string dataFileName;
		[SerializeField, Tooltip("�g���q")]
		string extension = ".dat";
		[SerializeField, Tooltip("�T�u�t�H���_��(�ȗ���)")]
		string folderName;

		[Header("Encryption")]
		[SerializeField, Tooltip("�Í������邩")]
		bool useEncryption = true;

		[Header("Debug")]
		[SerializeField, Tooltip("���O�\�����邩")]
		bool isDebug = true;

		const string mainFolderName = "Savedata";       // ���C���t�H���_��
		string fullPath;

		Data data;

		//-------------------------------------------------------------------
		/* Events */
		public delegate void SaveDelegate(ref Data data);

		/// <summary> �f�[�^��ۑ�����Ƃ��ɌĂ΂��C�x���g </summary>
		public event SaveDelegate OnDataSaved;

		/// <summary> �f�[�^��ǂݍ��ނƂ��ɌĂ΂��C�x���g </summary>
		public event Action<Data> OnDataLoaded;

		//-------------------------------------------------------------------
		/// <summary> �f�[�^�̕ۑ� </summary>
		public void SaveData()
		{
			// �N���C�A���g�̃f�[�^���擾
			OnDataSaved?.Invoke(ref data);

			// �t�@�C���ɏ�������
			FileDataHandler.WriteDataToFile(data, fullPath, useEncryption);

			// ���O�\��
			if (isDebug && Debug.isDebugBuild) {
				print($"Saved {dataFileName}.");
			}
		}

		/// <summary> �f�[�^�̓ǂݍ��� </summary>
		public void LoadData()
		{
			SetFullPath();		// �t���p�X�ݒ�

			// �t�@�C������ǂݍ���
			data = FileDataHandler.ReadDataFromFile<Data>(fullPath, useEncryption);

			// �f�[�^���Ȃ���΍쐬����
			if (data == null) {
				CreateData();
			}

			// ���O�\��
			else if (isDebug && Debug.isDebugBuild) {
				print($"Loaded {dataFileName}.");
			}

			// �N���C�A���g�Ƀf�[�^��n��
			OnDataLoaded?.Invoke(data);
		}

		//-------------------------------------------------------------------
		// �f�[�^�̐V�K�쐬
		void CreateData()
		{
			data = new Data();

			// ���O�\��
			if(isDebug && Debug.isDebugBuild) {
				print($"Created {dataFileName}.");
			}
		}

		// �t���p�X�̐ݒ�
		void SetFullPath()
		{
			string dirPath = "";

			// �G�f�B�^�Ƃ���ȊO�Ńf�[�^�̕ۑ����ς���
#if UNITY_EDITOR
			dirPath = Application.dataPath;
#else
		dirPath = Application.persistentDataPath;
#endif

			fullPath = FileDataHandler.GetFullPath(dirPath, dataFileName + extension, mainFolderName, folderName);
		}
	}
}