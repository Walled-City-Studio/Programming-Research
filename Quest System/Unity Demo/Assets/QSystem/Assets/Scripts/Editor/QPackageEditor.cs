﻿using UnityEditor;

namespace QSystem
{
    [CustomEditor(typeof(QPackage))]
    public class QPackageEditor : Editor
    {
        SerializedProperty Type;
        SerializedProperty Weight;
        SerializedProperty Description;
        SerializedProperty PickUpLocation;
        SerializedProperty DeliveryLocation;
        SerializedProperty LegalStatus;
        SerializedProperty PickUpPrefab;
        SerializedProperty DeliveryPrefab;

        private void OnEnable()
        {
            Type = serializedObject.FindProperty("Type");
            Weight = serializedObject.FindProperty("Weight");
            Description = serializedObject.FindProperty("Description");
            PickUpLocation = serializedObject.FindProperty("PickUpLocation");
            DeliveryLocation = serializedObject.FindProperty("DeliveryLocation");
            LegalStatus = serializedObject.FindProperty("LegalStatus");
            PickUpPrefab = serializedObject.FindProperty("PickUpPrefab");
            DeliveryPrefab = serializedObject.FindProperty("DeliveryPrefab");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(Type);
            EditorGUILayout.PropertyField(Weight);
            EditorGUILayout.PropertyField(Description);
            EditorGUILayout.PropertyField(PickUpLocation);
            EditorGUILayout.PropertyField(DeliveryLocation);
            EditorGUILayout.PropertyField(LegalStatus);
            EditorGUILayout.PropertyField(PickUpPrefab);
            EditorGUILayout.PropertyField(DeliveryPrefab);
           
            serializedObject.ApplyModifiedProperties();
        }
    }
}