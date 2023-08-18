using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace scene_2 {
    public abstract class DataManagerBase<T> : SimpleSingleton<T> where T : DataManagerBase<T> {
        protected abstract string FileName { get; }     // �t�@�C����
        string FilePath { get; set; }                   // �t�@�C���p�X

        const string DATA_FOLDER_NAME = "Data";         // �t�H���_��

        void GetFilePath()
        {
            var directoryName = "";

#if UNITY_EDITOR
            directoryName = $"{Application.dataPath}/{DATA_FOLDER_NAME}";               // Editor��̃p�X
#else
        directoryName = $"{ Application.persistentDataPath}/{ DATA_FOLDER_NAME}";   // �r���h���̃p�X
#endif

            var fileNameWithExt = $"{FileName}.json";                                   // �g���q�t���̃t�@�C����
            FilePath = $"{directoryName}/{fileNameWithExt}";                            // �t�@�C���p�X
        }

        /// <summary>
        /// �f�[�^�̃Z�b�g�A�b�v
        /// </summary>
        protected Data DataSetup<Data>(IData data) where Data : IData
        {
            // �t�@�C���p�X�擾
            GetFilePath();

            // �t�@�C�����Ȃ���΁A�V�K�쐬
            if (!File.Exists(FilePath)) {
                Save(data);
            }

            // �t�@�C�����[�h
            return Load<Data>();
        }

        /// <summary>
        /// json�t�@�C���Ƃ��ăf�[�^�ۑ�
        /// </summary>
        protected void Save(IData data)
        {
            var json = JsonUtility.ToJson(data);
            var wr = new StreamWriter(FilePath, false);

            wr.WriteLine(json);
            wr.Close();
        }

        /// <summary>
        /// json�t�@�C���ǂݍ���
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