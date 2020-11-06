using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QSystem
{
    public class QGiver : MonoBehaviour
    {
        [SerializeField] private float Radius = 3;
        [SerializeField] private List<Quest> Quests;

        private SphereCollider SphereCollider;
        private Quest NextQuest;

        private void Start()
        {
            AddSphereCollider();
            NextQuest = Quests.First();
        }

        void AddSphereCollider()
        {
            SphereCollider = gameObject.AddComponent<SphereCollider>();
            SphereCollider.isTrigger = true;
            SphereCollider.center = Vector3.zero;
            SphereCollider.radius = Radius;
        }

        public void AddQuest()
        {
            Quests.Remove(NextQuest);
            NextQuest = Quests.First();
        }

        void OnTriggerEnter(Collider other)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(NextQuest.Dialogue, true);
        }

        void OnTriggerExit(Collider other)
        {
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }
}