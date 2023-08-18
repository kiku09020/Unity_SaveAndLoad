using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSingleton<T> : MonoBehaviour where T : Component
{
    static T instance;

    public static T Instance {
		get{
			if (!instance) {
                instance = (T)FindObjectOfType(typeof(T));      // ���ɍ쐬���ꂽ�C���X�^���X��T��

                if (!instance) {
                    SetupInstance();        // �V�K�쐬
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
