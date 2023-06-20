using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using EnesShahn.PField;

using UnityEngine;

[CreateAssetMenu(fileName = "DataContainer", menuName = "Game/Data Container")]
public class DataContainer : ScriptableObject
{
    public PList<BaseAttribute> _attributes;

    private Dictionary<Type, BaseAttribute> _attributeMap = new Dictionary<Type, BaseAttribute>();

    public T GetAttribute<T>() where T : BaseAttribute
    {
        Type queryType = typeof(T);
        if (_attributeMap.ContainsKey(queryType))
        {
            return (T)_attributeMap[queryType];
        }
        foreach (var attribute in _attributes)
        {
            Type attributeType = attribute.GetType();
            if (attributeType == queryType)
            {
                _attributeMap.Add(attributeType, attribute);
                return (T)attribute;
            }
        }
        return null;
    }
}