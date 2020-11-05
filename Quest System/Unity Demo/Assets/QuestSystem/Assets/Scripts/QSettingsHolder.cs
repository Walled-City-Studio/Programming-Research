using System.Collections.Generic;
using UnityEngine;


public class QSettingsHolder : ScriptableObject
{
    public List<Quest> prefabQuests = new List<Quest>();

    public List<Criteria> prefabCriteria = new List<Criteria>();

    public List<Reward> prefabReward = new List<Reward>();

    public List<QuestNode> questNodes = new List<QuestNode>();

    public bool showQuestName;

    public bool showDescription;

    public bool showCriterias;

    public bool showRewards;

    public GameObject handInObjectPrefab;

    public GameObject criteriaSpawnPrefab;

    public GameObject questGiverPrefab;

}
