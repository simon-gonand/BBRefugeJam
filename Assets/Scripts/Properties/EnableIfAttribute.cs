using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnableIfAttribute : MultiPropertyAttribute
{
    public string booleanName;

    
    bool result = true;

    public EnableIfAttribute(string booleanName)
    {
        this.booleanName = booleanName;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Object target = property.serializedObject.targetObject;

        for (int i = 0; i < target.GetType().GetFields().Length; i++)
        {
            if (target.GetType().GetFields()[i].Name == booleanName)
            {
                result = (bool)target.GetType().GetFields()[i].GetValue(target);
            }
        }

        GUI.enabled = result;
    }
}
