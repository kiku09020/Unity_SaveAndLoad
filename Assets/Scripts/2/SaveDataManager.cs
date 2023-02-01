using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
    public class SaveDataManager : DataManagerBase<SaveDataManager> {
        protected override string FileName => "SaveData";

        [HideInInspector] public SaveData data;

        private void Awake()
        {
            data = DataSetup<SaveData>(data);
        }

        // �Q�[���I�����ɕۑ�
        private void OnDestroy()
        {
            Save(data);
        }
    }
}