using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Object = UnityEngine.Object;

public class ClampedAttribute : MultiPropertyAttribute
{
    public float min;
    public float max;

    public ClampedAttribute(float _min, float _max)
    {
        this.min = _min;
        this.max = _max;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = EditorGUI.IntField(position, label, (int)Mathf.Clamp(property.intValue, min, max));
                break;
            case SerializedPropertyType.Float:
                property.floatValue = EditorGUI.FloatField(position, label, Mathf.Clamp(property.floatValue, min, max));
                break;
            case SerializedPropertyType.Vector2:
                property.vector2Value = EditorGUI.Vector2Field(position, label, new Vector3(Mathf.Clamp(property.vector2Value.x, min, max), Mathf.Clamp(property.vector2Value.y, min, max)));
                break;
            case SerializedPropertyType.Vector3:
                property.vector3Value = EditorGUI.Vector3Field(position, label, new Vector3(Mathf.Clamp(property.vector3Value.x, min, max), Mathf.Clamp(property.vector3Value.y, min, max), Mathf.Clamp(property.vector3Value.z, min, max)));
                break;
            case SerializedPropertyType.Vector4:
                break;
            case SerializedPropertyType.Bounds:
                break;
            case SerializedPropertyType.Quaternion:
                break;
            case SerializedPropertyType.Vector2Int:
                break;
            case SerializedPropertyType.Vector3Int:
                break;
            case SerializedPropertyType.BoundsInt:
                break;
        }

    }
}
