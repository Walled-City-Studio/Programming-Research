using UnityEditor;

namespace QSystem
{
    [CustomEditor(typeof(QPackage))]
    public class QPackageEditor : Editor
    {
        SerializedProperty PickUpLocation;
        SerializedProperty DeliveryLocation;
        SerializedProperty LegalStatus;
        SerializedProperty Size;
        SerializedProperty PickUpPrefab;
        SerializedProperty DeliveryType;
        SerializedProperty DeliveryPrefab;
        SerializedProperty DeliveryTransform;

        private void OnEnable()
        {
            PickUpLocation = serializedObject.FindProperty("PickUpLocation");
            DeliveryLocation = serializedObject.FindProperty("DeliveryLocation");
            LegalStatus = serializedObject.FindProperty("LegalStatus");
            Size = serializedObject.FindProperty("Size");
            DeliveryType = serializedObject.FindProperty("DeliveryType");
            PickUpPrefab = serializedObject.FindProperty("PickUpPrefab");
            DeliveryPrefab = serializedObject.FindProperty("DeliveryPrefab");
            DeliveryTransform = serializedObject.FindProperty("DeliveryTransform");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(PickUpLocation);
            EditorGUILayout.PropertyField(DeliveryLocation);
            EditorGUILayout.PropertyField(LegalStatus);
            EditorGUILayout.PropertyField(Size);
            EditorGUILayout.PropertyField(PickUpPrefab);
            EditorGUILayout.PropertyField(DeliveryType);

            if (DeliveryType.enumValueIndex == (int)DELIVERY_TYPE.Prefab)
            {
                EditorGUILayout.PropertyField(DeliveryPrefab);
            }

            else if (DeliveryType.enumValueIndex == (int)DELIVERY_TYPE.Transform)
            {
                EditorGUILayout.PropertyField(DeliveryTransform);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
