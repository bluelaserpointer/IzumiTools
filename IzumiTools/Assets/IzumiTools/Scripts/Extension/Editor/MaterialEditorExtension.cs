using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class MaterialEditorExtension
{
    enum BoolEnum { True, False }
    /// <summary>
    /// Add a material option to a material keyword
    /// </summary>
    /// <param name="materialKeyword"></param>
    /// <param name="keyword"></param>
    /// <param name="optionDisplayName"></param>
    /// <returns></returns>
    public static bool MakeMaterialKeywordOption(this Material material, string optionDisplayName, string materialKeyword)
    {
        return material.MakeMaterialKeywordOption(optionDisplayName, materialKeyword, BoolEnum.True, BoolEnum.False);
    }

    /// <summary>
    /// Add a material option to a material keyword
    /// </summary>
    /// <param name="material"></param>
    /// <param name="optionDisplayName"></param>
    /// <param name="materialKeyword"></param>
    /// <param name="trueValue"></param>
    /// <param name="falseValue"></param>
    /// <returns></returns>
    public static bool MakeMaterialKeywordOption(this Material material, string optionDisplayName, string materialKeyword, Enum trueValue, Enum falseValue)
    {
        Enum selection = material.IsKeywordEnabled(materialKeyword) ? trueValue : falseValue;
        EditorGUI.BeginChangeCheck();
        selection = EditorGUILayout.EnumPopup(new GUIContent(optionDisplayName), selection);
        if (EditorGUI.EndChangeCheck())
        {
            if (selection.Equals(trueValue))
                material.EnableKeyword(materialKeyword);
            else
                material.DisableKeyword(materialKeyword);
        }
        return selection.Equals(trueValue);
    }

    /// <summary>
    /// Add multiple material options to each material keywords
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="material"></param>
    /// <param name="optionDisplayName"></param>
    /// <param name="materialKeywordAndEnum"></param>
    /// <param name="defaultSelection"></param>
    /// <returns></returns>
    public static T MakeMaterialKeywordOption<T>(this Material material, string optionDisplayName, Dictionary<T, string> materialKeywordAndEnum, T defaultSelection = default(T)) where T : Enum
    {
        T selection = defaultSelection;
        foreach(KeyValuePair<T, string> pair in materialKeywordAndEnum)
        {
            if(material.IsKeywordEnabled(pair.Value))
            {
                selection = pair.Key;
                break;
            }
        }
        EditorGUI.BeginChangeCheck();
        T newSelection = (T)EditorGUILayout.EnumPopup(new GUIContent(optionDisplayName), selection);
        if (EditorGUI.EndChangeCheck())
        {
            string removingKeyword;
            if (materialKeywordAndEnum.TryGetValue(selection, out removingKeyword))
                material.DisableKeyword(removingKeyword);
            material.EnableKeyword(materialKeywordAndEnum[newSelection]);
        }
        return newSelection;
    }
}
