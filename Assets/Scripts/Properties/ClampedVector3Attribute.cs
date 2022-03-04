using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

public class ClampedVector3Attribute : MultiPropertyAttribute
{
    public float min;
    public float max;

    public ClampedVector3Attribute(float _min, float _max)
    {
        this.min = _min;
        this.max = _max;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        Vector3 result = new Vector3(Mathf.Clamp(property.vector3Value.x, min, max), Mathf.Clamp(property.vector3Value.y, min, max), Mathf.Clamp(property.vector3Value.z, min, max));
        property.vector3Value = EditorGUI.Vector3Field(position, label, result);
    }
}