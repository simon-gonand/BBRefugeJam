using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System;
using Object = UnityEngine.Object;

public class ButtonAttribute : MultiPropertyAttribute
{

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Object target = property.serializedObject.targetObject;




        for (int i = 0; i < target.GetType().GetFields().Length; i++)
        {
            /*Debug.Log(property.type);
            Debug.Log(property.serializedObject.targetObject.GetType());*/

        }
    }
}