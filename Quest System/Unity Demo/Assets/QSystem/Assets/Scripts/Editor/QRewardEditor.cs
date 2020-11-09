using QSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QReward))]
public class QRewardEditor : Editor
{
    SerializedProperty RewardType;
    SerializedProperty ResourceType;
    SerializedProperty Item;
    SerializedProperty Ammount;

    private void OnEnable()
    {
        RewardType = serializedObject.FindProperty("RewardType");
        ResourceType = serializedObject.FindProperty("ResourceType");
        Item = serializedObject.FindProperty("Item");
        Ammount = serializedObject.FindProperty("Ammount");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(RewardType);

        if (RewardType.enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField(ResourceType);
        }

        else if (RewardType.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(Item);
        }

        EditorGUILayout.PropertyField(Ammount);

        serializedObject.ApplyModifiedProperties();
    }
}
