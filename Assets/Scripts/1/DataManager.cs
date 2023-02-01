using System.IO;
using UnityEngine;

namespace scene_1 {
    public class DataManager : MonoBehaviour {
        [HideInInspector] public SaveData data;     // json�ϊ�����f�[�^�̃N���X
        string filepath;                            // json�t�@�C���̃p�X
        string fileName = "Data.json";              // json�t�@�C����

        //-------------------------------------------------------------------
        // �J�n���Ƀt�@�C���`�F�b�N�A�ǂݍ���
        void Awake()
        {
            // �p�X���擾
            filepath = Application.dataPath + "/" + fileName;

            // �t�@�C�����Ȃ��Ƃ��A�t�@�C���쐬
            if (!File.Exists(filepath)) {
                Save(data);
            }

            // �t�@�C����ǂݍ����data�Ɋi�[
            data = Load(filepath);
        }

        //-------------------------------------------------------------------
        // json�Ƃ��ăf�[�^��ۑ�
        void Save(SaveData data)
        {
            string json = JsonUtility.ToJson(data);                 // json�Ƃ��ĕϊ�
            StreamWriter wr = new StreamWriter(filepath, false);    // �t�@�C���������ݎw��

            wr.WriteLine(json);                                     // json�ϊ�����������������
            wr.Close();                                             // �t�@�C������
        }

        // json�t�@�C���ǂݍ���
        SaveData Load(string path)
        {
            StreamReader rd = new StreamReader(path);               // �t�@�C���ǂݍ��ݎw��
            string json = rd.ReadToEnd();                           // �t�@�C�����e�S�ēǂݍ���
            rd.Close();                                             // �t�@�C������

            return JsonUtility.FromJson<SaveData>(json);            // json�t�@�C�����^�ɖ߂��ĕԂ�
        }

        //-------------------------------------------------------------------
        // �Q�[���I�����ɕۑ�
        void OnDestroy()
        {
            Save(data);
        }
    }
}