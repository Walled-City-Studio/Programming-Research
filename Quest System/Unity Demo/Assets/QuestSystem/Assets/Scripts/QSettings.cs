using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestSystem
{
    public static class QSettings
    {
        private static List<Quest> editorQuests;
        private static List<Criteria> editorCriterias;
        private static List<Reward> editorRewards;
        private static List<QuestNode> questNodes;
        private static bool showQuestName;
        private static bool showDescription;
        private static bool showCriterias;
        private static bool showRewards;


        /// <summary>
        /// A bool to either show or hide the rewards of the quest in the questUI
        /// </summary>
        public static bool ShowRewards
        {
            get { return showRewards; }
            set
            {
                showRewards = value;
                QSettingsHolder.showRewards = showRewards;
            }
        }

        public static void Start()
        {
            editorQuests = qSettingsHolder.prefabQuests;
            editorCriterias = qSettingsHolder.prefabCriteria;
            editorRewards = qSettingsHolder.prefabReward;
            questNodes = qSettingsHolder.questNodes;
            showQuestName = qSettingsHolder.showQuestName;
            showDescription = qSettingsHolder.showDescription;
            showCriterias = qSettingsHolder.showCriterias;
            showRewards = qSettingsHolder.showRewards;

            criteriaSpecificRewards = SettingsHolder.criteriaSpecificRewards;

            optional = SettingsHolder.optional;
        }

        public static QSettingsHolder qSettingsHolder
        {
            get
            {
                if (qSettingsHolder == null)
                {
#if UNITY_EDITOR
                    qSettingsHolder = (QSettingsHolder)AssetDatabase.LoadAssetAtPath("Assets/QuestSystem/Assets/Prefabs/QSettings.asset",
                            typeof(QSettingsHolder));
#endif

                    if (qSettingsHolder == null)
                    {
                        CreateHolder();
                    }
                }
                return qSettingsHolder;
            }
            set
            {
                if (qSettingsHolder == null)
                {
#if UNITY_EDITOR
                    qSettingsHolder = (QSettingsHolder)AssetDatabase.LoadAssetAtPath(
                        "Assets/QuestSystem/Assets/Prefabs/QSettings.asset", typeof(QSettingsHolder));
#endif
                    // settingsHolder = SettingsHolder.Instance;
                    if (qSettingsHolder == null)
                    {
                        CreateHolder();
                    }
                }
                qSettingsHolder = value;
            }
        }

        private static void CreateHolder()
        {
            qSettingsHolder = ScriptableObject.CreateInstance<QSettingsHolder>();

#if UNITY_EDITOR
            AssetDatabase.CreateAsset(qSettingsHolder, "Assets/QuestSystem/Assets/Prefabs/QSettings.asset");
            AssetDatabase.SaveAssets();
#endif
        }

    }
}
