using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UnitSelector : MonoBehaviour
{
    private List<Unit> _unitList;

    private void Awake()
    {
        _unitList = new List<Unit>();
    }


}
