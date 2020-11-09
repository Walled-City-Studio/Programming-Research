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
        private Quest CurrentQuest;
        private QManager QManager;
        private DialogueManager DialogueManager;

        private void Start()
        {
            QManager = FindObjectOfType<QManager>();
            DialogueManager = FindObjectOfType<DialogueManager>();
            CurrentQuest = Quests.First();
            AddSphereCollider();
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
            Quests.Remove(CurrentQuest);
            CurrentQuest = Quests.First();
        }

        public void RemoveQuest(Quest AcceptedQuest)
        {
            Quests.Remove(AcceptedQuest);
        }

        void OnTriggerEnter(Collider other)
        {
<<<<<<< HEAD
            if(other.gameObject.CompareTag("Player") && NextQuest != null)
            {
                QManager.Instance.AddQuestDiscovered(NextQuest);
                QManager.Instance.SetCurrentDiaglogueQuest(NextQuest, this);
                DialogueManager.Instance.SetAgreeButton(true);
                DialogueManager.Instance.StartDialogue(NextQuest.Dialogue);
            }
=======
            FindObjectOfType<DialogueManager>().StartDialogue(CurrentQuest.Dialogue, CurrentQuest);
>>>>>>> parent of 9c55c73... Latest
        }

        void OnTriggerExit(Collider other)
        {
<<<<<<< HEAD
            QManager.Instance.SetCurrentDiaglogueQuest();
            DialogueManager.Instance.SetAgreeButton(false);
            DialogueManager.Instance.EndDialogue();
=======
            DialogueManager.EndDialogue();
>>>>>>> parent of 9c55c73... Latest
        }
    }
}