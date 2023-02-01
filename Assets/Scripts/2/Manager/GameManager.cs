using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace scene_2 {
    public class GameManager : MonoBehaviour,IUseData {
        [SerializeField] PlayerController player;

        public PlayerController playerInstance { get; private set; }
        public TrailRenderer trail { get; private set; }

		//-------------------------------------------------------------------
		private void Start()
        {
            SetParameters();
            trail = playerInstance.GetComponent<TrailRenderer>();
        }

		private void OnApplicationQuit()
        {
            SaveParameterData();
        }

        //-------------------------------------------------------------------
        public void SetParameters()
        {
            var data = SaveDataManager.Instance.data;

            playerInstance = Instantiate(player, data.playerPos, Quaternion.identity);        // �O��̈ʒu�ɐ���
        }

        public void SaveParameterData()
        {
            SaveDataManager.Instance.data.playerPos = playerInstance.transform.position;      // ���������v���C���[�̈ʒu��ۑ�
        }
    }
}