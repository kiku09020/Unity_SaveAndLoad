using UnityEngine;

public class SimpleSingleton<T> : MonoBehaviour where T : Component
{
	static T instance;

	public static T Instance {
		get {
			if (!instance) {
				instance = (T)FindObjectOfType(typeof(T));      // 既に作成されたインスタンスを探す

				if (!instance) {
					SetupInstance();        // 新規作成
				}
			}

			return instance;
		}
	}

	//-------------------------------------------------------------------
	static void SetupInstance()
	{
		var obj = new GameObject();
		obj.name = typeof(T).Name;

		instance = obj.AddComponent<T>();
	}
}