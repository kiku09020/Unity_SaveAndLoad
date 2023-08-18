using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extentions.DataManagement
{
	/// <summary> データを利用するクラスに実装するインターフェース </summary>
	public interface IDataUserBase<T> where T : GameDataBase
	{
		/// <summary> データをセットする(保存する) </summary>
		void SetData(ref T data);

		/// <summary> データを取得する(読み込む) </summary>
		void GetData(T data);
	}
}