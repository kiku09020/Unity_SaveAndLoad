using Extentions.DataManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour, IDataUserBase<HighScoreData>
{
	[Header("Components")]
	[SerializeField] GameDataManager dataManager;

	[Header("UI")]
	[SerializeField] Text text;

	public int Count { get; private set; }

	//-------------------------------------------------------------------
	void Awake()
	{
		dataManager.OnDataSaved += SetData;
		dataManager.OnDataLoaded += GetData;

		dataManager.LoadData();
		text.text = Count.ToString();
	}

	public void SetData(ref HighScoreData data)
	{
		data.highScore = Count;
	}

	public void GetData(HighScoreData data)
	{
		Count = data.highScore;
	}

	//-------------------------------------------------------------------
	// �J�E���g�ǉ��{�^���p
	public void AddCount()
	{
		Count++;
		text.text= Count.ToString();
	}

	// �ۑ��{�^���p
	public void Save()
	{
		dataManager.SaveData();
	}
}
