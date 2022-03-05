using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Reflection;
using Object = UnityEngine.Object;
using UnityEngine;

public class ButtonHideRangeAttribute : MultiPropertyAttribute
{
    public string from;
    public string to;

    public ButtonHideRangeAttribute(string from, string to)
    {
        this.from = from;
        this.to = to;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Object target = property.serializedObject.targetObject;


        FieldInfo fromInfo;
        FieldInfo toInfo;

        for (int i = 0; i < target.GetType().GetFields().Length; i++)
        {
            if (target.GetType().GetFields()[i].Name == from)
            {
                fromInfo = target.GetType().GetFields()[i];
            }

            if (target.GetType().GetFields()[i].Name == to)
            {
                toInfo = target.GetType().GetFields()[i];
            }
        }

    }
}
