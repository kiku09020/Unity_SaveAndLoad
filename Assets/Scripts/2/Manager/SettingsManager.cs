using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace scene_2 {
    public class SettingsManager : MonoBehaviour,IUseData {
        [SerializeField] Toggle activeToggle;
        [SerializeField] Toggle trailToggle;

        //-------------------------------------------------------------------
        void Start()
        {
            SetParameters();
        }

		private void OnApplicationQuit()
        {
            SaveParameterData();
        }

        //-------------------------------------------------------------------
        // Data -> Parameters
        public void SetParameters()
        {
            var data = SettingsDataManager.Instance.data;

            activeToggle.isOn = data.activate;
            trailToggle.isOn = data.trailEnabled;
        }

        // Parameters -> Data
        public void SaveParameterData()
        {
            var data = SettingsDataManager.Instance.data;

            data.activate = activeToggle.isOn;
            data.trailEnabled = trailToggle.isOn;

            SettingsDataManager.Instance.data = data;
        }
    }
}