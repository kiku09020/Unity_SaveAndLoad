using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseData 
{
    /// <summary>
    /// Data -> Parameters
    /// </summary>
    void SetParameters();

    /// <summary>
    /// Parameters -> Data
    /// </summary>
    void SaveParameterData();
}
