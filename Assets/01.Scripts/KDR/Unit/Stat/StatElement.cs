using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StatElement
{
    [SerializeField] private float _baseValue;
    [SerializeField] private List<float> _addModifies = new List<float>();
    [SerializeField] private List<float> _percentModifies = new List<float>();

    public StatElement(float baseValue) 
    {
        _baseValue = baseValue;
    }

    public float GetValue()
    {
        //���� ������� ����
        float numValue = _baseValue;
        for (int i = 0; i < _addModifies.Count; i++)
        {
            numValue += _addModifies[i];
        }

        //�ۼ�Ʈ ������� ����
        float percentModify = 0;
        for (int i = 0; i < _percentModifies.Count; i++)
        {
            percentModify += _percentModifies[i];
        }

        //���� ���� ���� �� �ۼ�Ʈ ���� ����
        float value = numValue * (1 + (float)percentModify / 100);

        return value;
    }

    public void AddModify(float modify, bool _isPercentModify)
    {
        if (_isPercentModify)
            _percentModifies.Add(modify);
        else
            _addModifies.Add(modify);
    }
    public void RemoveModify(float modify, bool _isPercentModify)
    {
        if (_isPercentModify && _percentModifies.Contains(modify))
                _percentModifies.Add(modify);
        else if (_addModifies.Contains(modify))
                _addModifies.Add(modify);
    }
}