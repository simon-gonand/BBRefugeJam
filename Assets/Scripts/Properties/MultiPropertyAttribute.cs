using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[AttributeUsage(AttributeTargets.Field)]
public abstract class MultiPropertyAttribute : PropertyAttribute
{
    public List<object> stored = new List<object>();
    public virtual GUIContent BuildLabel(GUIContent label)
    {
        return label;
    }
#if UNITY_EDITOR

    public abstract void OnGUI(Rect position, SerializedProperty property, GUIContent label);

    public virtual float? GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return null;
    }
#endif
}

