using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Extentions.DataManagement
{
	/// <summary> �f�[�^�𗘗p����N���X�Ɏ�������C���^�[�t�F�[�X </summary>
	public interface IDataUserBase<T> where T : GameDataBase
	{
		/// <summary> �f�[�^���Z�b�g����(�ۑ�����) </summary>
		void SetData(ref T data);

		/// <summary> �f�[�^���擾����(�ǂݍ���) </summary>
		void GetData(T data);
	}
}