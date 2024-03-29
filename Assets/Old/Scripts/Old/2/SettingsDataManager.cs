using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
    public class SettingsDataManager : DataManagerBase<SettingsDataManager> {
        protected override string FileName => "SettingsData";

        [HideInInspector] public SettingsData data;

        private void Awake()
        {
            data = DataSetup<SettingsData>(data);
        }

        // ゲーム終了時に保存
        private void OnDestroy()
        {
            Save(data);
        }
    }
}