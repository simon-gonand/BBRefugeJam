using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using Object = UnityEngine.Object;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class ButtonAttribute : MultiPropertyAttribute
{

#if UNITY_EDITOR
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        Object target = property.serializedObject.targetObject;




        for (int i = 0; i < target.GetType().GetFields().Length; i++)
        {
            /*Debug.Log(property.type);
            Debug.Log(property.serializedObject.targetObject.GetType());*/

        }
    }
#endif
}