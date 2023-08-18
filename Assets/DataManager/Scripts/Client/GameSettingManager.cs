using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Extentions.DataManagement;

public class GameSettingManager : MonoBehaviour, IDataUserBase<SettingData>
{
	[Header("Components")]
	[SerializeField] SettingDataManager dataManager;

	[Header("UI")]
	[SerializeField] Slider bgmSlider;
	[SerializeField] Slider seSlider;

	//-------------------------------------------------------------------
	void Awake()
	{
		// �C�x���g�̓o�^
		dataManager.OnDataSaved += SetData;
		dataManager.OnDataLoaded += GetData;

		// �ǂݍ���
		dataManager.LoadData();
	}

	void OnDestroy()
	{
		// �ۑ�
		dataManager.SaveData();
	}

	//-------------------------------------------------------------------
	public void GetData(SettingData data)
	{
		bgmSlider.value = data.bgmVolume;
		seSlider.value = data.seVolume;
	}

	public void SetData(ref SettingData data)
	{
		data.bgmVolume = bgmSlider.value;
		data.seVolume = seSlider.value;
	}
}
