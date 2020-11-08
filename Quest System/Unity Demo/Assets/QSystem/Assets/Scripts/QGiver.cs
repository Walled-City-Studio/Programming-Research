using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace QSystem
{
    public class QGiver : MonoBehaviour
    {
        [SerializeField] private string Name = "NPC";
        [SerializeField] private float InteractRadius = 3f;
        [SerializeField] private List<Quest> Quests;

        private Quest NextQuest;
        private SphereCollider SphereCollider;

        private void Start()
        {
            NextQuest = Quests.First();
            AddSphereCollider();
        }

        void AddSphereCollider()
        {
            SphereCollider = gameObject.AddComponent<SphereCollider>();
            SphereCollider.isTrigger = true;
            SphereCollider.center = Vector3.zero;
            SphereCollider.radius = InteractRadius;
        }

        public void RemoveQuest(Quest quest)
        {
            Quests.Remove(quest);
            NextQuest = Quests.Count > 0 ? Quests.First() : null;
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Player") && NextQuest != null)
            {
                QHandler.Instance.AddQuestDiscovered(NextQuest);
                QHandler.Instance.SetCurrentDiaglogueQuest(NextQuest, this);
                DialogueManager.Instance.SetAgreeButton(true);
                DialogueManager.Instance.StartDialogue(NextQuest.Dialogue);
            }
        }

        void OnTriggerExit(Collider other)
        {
            QHandler.Instance.SetCurrentDiaglogueQuest();
            DialogueManager.Instance.SetAgreeButton(false);
            DialogueManager.Instance.EndDialogue();
        }
 
    }
}