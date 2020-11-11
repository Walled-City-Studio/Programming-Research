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
            Dialogue = serializedObject.FindProperty("dialogue");
            QPackage = serializedObject.FindProperty("qPackage");
            Title = serializedObject.FindProperty("title");
            Description = serializedObject.FindProperty("description");
            QReward = serializedObject.FindProperty("qReward");
            MaxDeliverTime = serializedObject.FindProperty("maxDeliverTime");
            ChallengeType = serializedObject.FindProperty("challengeType");
            ScaleReward = serializedObject.FindProperty("scaleReward");
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