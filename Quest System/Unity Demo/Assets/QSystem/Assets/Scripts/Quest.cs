using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QSystem
{
    public class Quest : MonoBehaviour
    {
        [SerializeField] private string Title;
        [SerializeField] private string Description;

        [SerializeField] private QReward QReward;

        [SerializeField] private QPackage QPackage;

    }
}
