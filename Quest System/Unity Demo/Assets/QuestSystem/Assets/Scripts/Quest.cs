using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace QuestSystem.Quest
{
    public class Quest : MonoBehaviour
    {
        public string title;
        public string description;


       
   

        public List<QGiver> qGivers = new List<QGiver>();

        

    }
}