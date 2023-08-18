using UnityEngine;

namespace scene_2 {
    public interface IData { }

    [System.Serializable]
    public class SaveData : IData {
        public Vector2 playerPos;       // �v���C���[�̏����ʒu
    }

    [System.Serializable]
    public class SettingsData : IData {
        public bool activate;       // setActive
        public bool trailEnabled;   // Trail���L��
    }
}