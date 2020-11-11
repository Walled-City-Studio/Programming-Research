using UnityEditor;

namespace QSystem
{
    [CustomEditor(typeof(QReward))]
    public class QRewardEditor : Editor
    {
        SerializedProperty RewardType;
        SerializedProperty ResourceType;
        SerializedProperty Item;
        SerializedProperty Amount;

        private void OnEnable()
        {
            RewardType = serializedObject.FindProperty("rewardType");
            ResourceType = serializedObject.FindProperty("resourceType");
            Item = serializedObject.FindProperty("item");
            Amount = serializedObject.FindProperty("amount");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(RewardType);

            if (RewardType.enumValueIndex == (int)REWARD_TYPE.RESOURCE)
            {
                EditorGUILayout.PropertyField(ResourceType);
            }

            else if (RewardType.enumValueIndex == (int)REWARD_TYPE.ITEM)
            {
                EditorGUILayout.PropertyField(Item);
            }

            EditorGUILayout.PropertyField(Amount);

            serializedObject.ApplyModifiedProperties();
        }
    }
}