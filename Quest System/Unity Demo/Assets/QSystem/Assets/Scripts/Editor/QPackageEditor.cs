using UnityEditor;

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
            Type = serializedObject.FindProperty("type");
            Weight = serializedObject.FindProperty("weight");
            Description = serializedObject.FindProperty("description");
            PickUpLocation = serializedObject.FindProperty("pickUpLocation");
            DeliveryLocation = serializedObject.FindProperty("deliveryLocation");
            LegalStatus = serializedObject.FindProperty("legalStatus");
            PickUpPrefab = serializedObject.FindProperty("pickUpPrefab");
            DeliveryPrefab = serializedObject.FindProperty("deliveryPrefab");
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
