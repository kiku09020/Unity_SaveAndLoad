using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour 
{
    public Rank_JsonData data = new Rank_JsonData();       // json�ϊ�����f�[�^�̃N���X
    string path;                                    // �p�X
    string fileName = "/Data.json";                 // json�t�@�C����

    void Awake()
    {
        path = Application.dataPath + fileName;     // �p�X�����߂�

        data = Load(path);
    }

    // json�Ƃ��ăf�[�^��ۑ�
    public void Save(Rank_JsonData data)
    {
        string str = JsonUtility.ToJson(data);                  // json�Ƃ��ĕϊ�
        StreamWriter wr = new StreamWriter(path, false);        // �t�@�C���J��
        wr.WriteLine(str);                                      // json��������
        wr.Flush();                                             // �o�b�t�@�폜
        wr.Close();                                             // �t�@�C������
    }

    // json�t�@�C���ǂݍ���
    public Rank_JsonData Load(string path)
    {
        StreamReader rd = new StreamReader(path);               // �ǂݍ���
        string str = rd.ReadToEnd();                            // �t�@�C�����e�S�ēǂݍ���
        rd.Close();                                             // �t�@�C������
                                                                
        return JsonUtility.FromJson<Rank_JsonData>(str);        // json�t�@�C�����^�ɖ߂��ĕԂ�
    }

    void OnDestroy()
    {
        Save(data);
    }
}