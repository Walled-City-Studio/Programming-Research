using UnityEditor;

namespace QSystem
{
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

            if (RewardType.enumValueIndex == (int)REWARD_TYPE.RESOURCE)
            {
                EditorGUILayout.PropertyField(ResourceType);
            }

            else if (RewardType.enumValueIndex == (int)REWARD_TYPE.ITEM)
            {
                EditorGUILayout.PropertyField(Item);
            }

            EditorGUILayout.PropertyField(Ammount);

            serializedObject.ApplyModifiedProperties();
        }
    }
}