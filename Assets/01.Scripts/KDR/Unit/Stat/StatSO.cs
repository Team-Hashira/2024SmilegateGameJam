using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EStatType
{
    MaxHealth,
    Damage,
    Speed,
    DetectRadius,
    AttackRadius,
}

[System.Serializable]
public struct StatInfo
{
    public EStatType statType;
    public float defaultValue;
}

[CreateAssetMenu(menuName = "SO/Stat")]
public class StatSO : ScriptableObject
{
    private Dictionary<EStatType, StatElement> statDictionary;
    [SerializeField] private List<StatInfo> statInfos;

    private void OnEnable()
    {
        SetupDictionary();
    }

    private void SetupDictionary()
    {
        statDictionary = new Dictionary<EStatType, StatElement>();
        foreach (StatInfo statInfo in statInfos)
        {
            StatElement stat = new StatElement(statInfo.defaultValue);
            statDictionary.Add(statInfo.statType, stat);
        }
    }

    public StatElement GetStatElement(EStatType statType)
    {
        if (statDictionary.ContainsKey(statType))
            return statDictionary[statType];
        else
            return null;
    }
    public float GetStatValue(EStatType statType)
    {
        if (statDictionary.ContainsKey(statType))
            return statDictionary[statType].GetValue();
        else
            return 0;
    }

    [ContextMenu("CreateAllStat")]
    public void CreateAllStat()
    {
        foreach (EStatType statType in Enum.GetValues(typeof(EStatType)))
        {
            bool flag = false;
            foreach (var item in statInfos)
            {
                if (item.statType == statType)
                {
                    flag = true;
                    break;
                }
            }
            if (flag) continue;

            StatInfo statInfo = new StatInfo();
            statInfo.statType = statType;
            statInfos.Add(statInfo);
        }
        SetupDictionary();
    }
}
