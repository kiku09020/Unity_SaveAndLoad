using UnityEngine;

namespace scene_2 {
    public interface IData { }

    [System.Serializable]
    public class SaveData : IData {
        public Vector2 playerPos;       // プレイヤーの初期位置
    }

    [System.Serializable]
    public class SettingsData : IData {
        public bool activate;       // setActive
        public bool trailEnabled;   // Trailが有効
    }
}