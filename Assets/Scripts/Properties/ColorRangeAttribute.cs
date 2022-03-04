using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using Object = UnityEngine.Object;

public class ColorRangeAttribute : MultiPropertyAttribute
{
    public float r1, g1, b1;
    public float r2, g2, b2;

    public ColorRangeAttribute(float r1, float g1, float b1, float r2, float g2, float b2)
    {
         this.r1 = r1;
         this.g1 = g1;
         this.b1 = b1;
         this.r2 = r2;
         this.g2 = g2;
         this.b2 = b2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {

        Object target = property.serializedObject.targetObject;

        Type t = target.GetType();
        FieldInfo info = t.GetField(property.name);

        if(info.GetCustomAttributes(typeof(ClampedVector3Attribute), false).Length > 0)
        {
            ClampedVector3Attribute cva = (ClampedVector3Attribute)info.GetCustomAttributes(typeof(ClampedVector3Attribute), false)[0];

            GUI.color = Color.Lerp(new Color(r1, g1, b1), new Color(r2, g2, b2), Mathf.InverseLerp(cva.min, cva.max, property.vector3Value.x));
        }
        else if (info.GetCustomAttributes(typeof(ClampedAttribute), false).Length > 0 && property.type == "float")
        {
            ClampedAttribute ra = (ClampedAttribute)info.GetCustomAttributes(typeof(ClampedAttribute), false)[0];

            GUI.color = Color.Lerp(new Color(r1, g1, b1), new Color(r2, g2, b2), Mathf.InverseLerp(ra.min, ra.max, property.floatValue));
        }
        else if (info.GetCustomAttributes(typeof(ClampedAttribute), false).Length > 0 && property.type == "int")
        {
            ClampedAttribute ra = (ClampedAttribute)info.GetCustomAttributes(typeof(ClampedAttribute), false)[0];

            GUI.color = Color.Lerp(new Color(r1, g1, b1), new Color(r2, g2, b2), Mathf.InverseLerp(ra.min, ra.max, property.intValue));
        }
    }
}
