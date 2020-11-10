using UnityEditor;

namespace QSystem
{
    [CustomEditor(typeof(Quest))]
    public class QuestEditor : Editor
    {
        SerializedProperty Dialogue;
        SerializedProperty QPackage;
        SerializedProperty Title;
        SerializedProperty Description;
        SerializedProperty QReward;
        SerializedProperty MaxDeliverTime;
        SerializedProperty ChallengeType;
        SerializedProperty ScaleReward;

        private void OnEnable()
        {
            Dialogue = serializedObject.FindProperty("Dialogue");
            QPackage = serializedObject.FindProperty("QPackage");
            Title = serializedObject.FindProperty("Title");
            Description = serializedObject.FindProperty("Description");
            QReward = serializedObject.FindProperty("QReward");
            MaxDeliverTime = serializedObject.FindProperty("MaxDeliverTime");
            ChallengeType = serializedObject.FindProperty("ChallengeType");
            ScaleReward = serializedObject.FindProperty("ScaleReward");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(Dialogue);
            EditorGUILayout.PropertyField(QPackage);
            EditorGUILayout.PropertyField(Title);
            EditorGUILayout.PropertyField(Description);
            EditorGUILayout.PropertyField(QReward);
            EditorGUILayout.PropertyField(MaxDeliverTime);
            EditorGUILayout.PropertyField(ChallengeType);
            EditorGUILayout.PropertyField(ScaleReward);

            serializedObject.ApplyModifiedProperties();
        }
    }
}